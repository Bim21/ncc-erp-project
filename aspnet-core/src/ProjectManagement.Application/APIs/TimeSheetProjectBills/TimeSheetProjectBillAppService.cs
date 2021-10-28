﻿using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using ProjectManagement.APIs.TimeSheetProjectBills.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.TimeSheetProjectBills
{
    public class TimeSheetProjectBillAppService : ProjectManagementAppServiceBase
    {
        [HttpGet]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_GetAll)]
        public async Task<List<GetTimeSheetProjectBillDto>> GetAll(long timesheetId, long projectId)
        {
            var tenantId = WorkScope.GetAll<TimesheetProjectBill>().Select(x => x.TenantId).FirstOrDefault();
            var ass = WorkScope.GetAll<TimesheetProjectBill>().ToList();
            var aass = ass.Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId);
            ; var query = WorkScope.GetAll<TimesheetProjectBill>().Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId).OrderByDescending(x => x.CreationTime)
                         .Select(x => new GetTimeSheetProjectBillDto
                         {
                             Id = x.Id,
                             UserId = x.UserId,
                             UserName = x.User.Name,
                             ProjectId = x.ProjectId,
                             ProjectName = x.Project.Name,
                             BillRole = x.BillRole,
                             BillRate = x.BillRate,
                             StartTime = x.StartTime.Date,
                             EndTime = x.EndTime.Value.Date,
                             Note = x.Note,
                             ShadowNote = x.ShadowNote,
                             IsActive = x.IsActive,
                             AvatarPath = "/avatars/" + x.User.AvatarPath,
                             FullName = x.User.FullName,
                             Branch = x.User.Branch,
                             EmailAddress = x.User.EmailAddress,
                             UserType = x.User.UserType,
                             WorkingTime = x.WorkingTime,
                             //ProjectBillInfomation = $"<b>{x.User.FullName}</b> - {x.BillRole} - {x.BillRate} - {x.Note} - {x.ShadowNote} <br>"
                         });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Update)]
        public async Task<TimeSheetProjectBillDto> Create(TimeSheetProjectBillDto input)
        {
            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<TimesheetProjectBill>(input));
            await UpdateProjectBillInformation(input.ProjectId, input.TimeSheetId.Value);
            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Update)]
        public async Task<TimeSheetProjectBillDto> Update(TimeSheetProjectBillDto input)
        {
            var timesheetProjectBill = await WorkScope.GetAsync<TimesheetProjectBill>(input.Id);

            if (input.EndTime.HasValue && input.StartTime.Date > input.EndTime.Value.Date)
                throw new UserFriendlyException($"Start date cannot be greater than end date !");

            await WorkScope.UpdateAsync(ObjectMapper.Map<TimeSheetProjectBillDto, TimesheetProjectBill>(input, timesheetProjectBill));
            await UpdateProjectBillInformation(input.ProjectId, input.TimeSheetId.Value);

            return input;
        }

        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_UpdateFromProjectUserBill)]
        public async Task<object> UpdateFromProjectUserBill(long projectId, long timesheetId)
        {
            var timesheet = await WorkScope.GetAsync<Timesheet>(timesheetId);
            var projectUserBills = WorkScope.GetAll<ProjectUserBill>()
                .Include(x => x.User)
                .Where(x => x.ProjectId == projectId && (!x.EndTime.HasValue || x.EndTime > timesheet.CreationTime || (x.EndTime.Value.Month == timesheet.Month)));
            var updateUserIds = projectUserBills.Select(x => x.UserId).ToList();

            var currentTimesheetProject = await WorkScope.GetAll<TimesheetProject>()
                .Where(x => x.ProjectId == projectId)
                .OrderByDescending(x => x.CreationTime)
                .FirstOrDefaultAsync();

            var timesheetProjectBills = WorkScope.GetAll<TimesheetProjectBill>().Where(x => x.ProjectId == projectId && x.TimesheetId == timesheetId);
            var currentUserIds = timesheetProjectBills.Select(x => x.UserId).ToList();

            var insertUserIds = updateUserIds.Except(currentUserIds).ToList();
            var insertUsers = projectUserBills.Include(x => x.User).Where(x => insertUserIds.Contains(x.UserId)).ToList();
            var successList = new List<string>();
            var failList = new List<string>();

            foreach (var item in insertUsers)
            {
                try
                {
                    var timesheetProjectBill = new TimeSheetProjectBillDto
                    {
                        UserId = item.UserId,
                        ProjectId = item.ProjectId,
                        TimeSheetId = timesheetId,
                        BillRole = item.BillRole,
                        BillRate = item.BillRate,
                        StartTime = item.StartTime.Date,
                        EndTime = item.EndTime.GetValueOrDefault().Date,
                        Note = item.Note,
                        ShadowNote = item.shadowNote,
                        IsActive = item.isActive,
                        Currency = CurrencyCode.VND
                    };
                    timesheetProjectBill.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<TimesheetProjectBill>(timesheetProjectBill));
                    successList.Add(timesheetProjectBill.UserId.ToString());
                }
                catch (Exception ex)
                {
                    failList.Add($"error: {item.UserId} " + ex.Message);
                }

            }
            await UpdateProjectBillInformation(projectId, timesheetId);
            return new
            {
                successList,
                failList
            };
        }

        private async Task<List<string>> UpdateProjectBillInformation(long projectId, long timesheetId)
        {
            var failList = new List<string>();

            var timesheetProjectBills = await WorkScope.GetAll<TimesheetProjectBill>().Include(x => x.User).Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId).ToListAsync();
            var billInfomation = new StringBuilder();

            foreach (var item in timesheetProjectBills)
            {
                try
                {
                    var timesheetProjects = await WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId).FirstOrDefaultAsync();
                    billInfomation.Append($"<b>{item.User.FullName}</b> - {item.BillRole} - {item.BillRate} - {item.Note} - {item.ShadowNote} <br>");
                    timesheetProjects.ProjectBillInfomation = $"{billInfomation}";
                    await WorkScope.UpdateAsync(timesheetProjects);
                }
                catch (Exception ex)
                {
                    failList.Add($"error UserId = {item.UserId} : " + ex.Message);
                }
            }
            return failList;
        }
       

    }
}
