﻿using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using ProjectManagement.APIs.TimeSheetProjectBills.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
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
        private readonly UserManager _userManager;
        private readonly IAbpSession _abpSession;
        public TimeSheetProjectBillAppService( UserManager userManager, IAbpSession abpSession)
        {
            _userManager = userManager;
            _abpSession = abpSession;
        }
        [HttpGet]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_GetAll)]
        public async Task<List<GetTimeSheetProjectBillDto>> GetAll(long timesheetId, long projectId)
        {
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var roles = await _userManager.GetRolesAsync(currentUser);
            var tenantId = WorkScope.GetAll<TimesheetProjectBill>().Select(x => x.TenantId).FirstOrDefault();
            var ass = WorkScope.GetAll<TimesheetProjectBill>().ToList();
            var aass = ass.Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId);
            var query = WorkScope.GetAll<TimesheetProjectBill>().Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId).OrderByDescending(x => x.CreationTime)
                         .Select(x => new GetTimeSheetProjectBillDto
                         {
                             Id = x.Id,
                             UserId = x.UserId,
                             UserName = x.User.Name,
                             ProjectId = x.ProjectId,
                             ProjectName = x.Project.Name,
                             BillRole = x.BillRole,
                             BillRate = roles.Contains(StaticRoleNames.Tenants.Admin) ? x.BillRate : -1,
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
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Update, PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_ChangeUser)]
        public async Task<List<TimeSheetProjectBillDto>> Update(List<TimeSheetProjectBillDto> input)
        {
            var timesheetProjectBillIds = input.Select(x => x.Id).ToList();
            var timesheetProjectBills = await WorkScope.GetAll<TimesheetProjectBill>().Where(x => timesheetProjectBillIds.Contains(x.Id)).ToListAsync();
            foreach (var bill in input)
            {
                var timesheetProjectBill = timesheetProjectBills.Where(x => x.Id == bill.Id).FirstOrDefault();
                await WorkScope.UpdateAsync(ObjectMapper.Map<TimeSheetProjectBillDto, TimesheetProjectBill>(bill, timesheetProjectBill));
                CurrentUnitOfWork.SaveChanges();

                await UpdateProjectBillInformation(bill.ProjectId, bill.TimeSheetId.Value);
            }
            //await WorkScope.UpdateRangeAsync(ObjectMapper.Map<List<TimeSheetProjectBillDto>, List<TimesheetProjectBill>>(input, timesheetProjectBills));
            //CurrentUnitOfWork.SaveChanges();

            //input.ForEach(async x => await UpdateProjectBillInformation(x.ProjectId, x.TimeSheetId.Value));
            return input;
        }

        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_UpdateFromProjectUserBill)]
        public async Task<object> UpdateFromProjectUserBill(long projectId, long timesheetId)
        {
            var timesheet = await WorkScope.GetAsync<Timesheet>(timesheetId);
            var projectUserBills = WorkScope.GetAll<ProjectUserBill>()
                .Include(x => x.User)
                .Where(x => x.ProjectId == projectId && (!x.EndTime.HasValue || x.EndTime.Value.Date > timesheet.CreationTime.Date || (x.EndTime.Value.Month >= timesheet.Month && x.EndTime.Value.Year >= timesheet.Year)));
            ;

            var timesheetProjectBills = await WorkScope.GetAll<TimesheetProjectBill>()
                .Where(x => x.ProjectId == projectId && x.TimesheetId == timesheetId)
                .Select(x => x.UserId).ToListAsync();
            var sucessList = new List<string>();
            var failList = new List<string>();
            foreach (var pUserBill in projectUserBills)
            {
                if (timesheetProjectBills.Contains(pUserBill.UserId))
                {
                    try
                    {
                        var timesheetProjectBill = await WorkScope.GetAll<TimesheetProjectBill>()
                            .Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId && x.UserId == pUserBill.UserId).FirstOrDefaultAsync();
                        var timesheetProjectBillInput = new TimeSheetProjectBillDto
                        {
                            Id = timesheetProjectBill.Id,
                            UserId = pUserBill.UserId,
                            ProjectId = pUserBill.ProjectId,
                            TimeSheetId = timesheetId,
                            BillRole = pUserBill.BillRole,
                            BillRate = pUserBill.BillRate,
                            StartTime = pUserBill.StartTime.Date,
                            EndTime = pUserBill.EndTime.GetValueOrDefault().Date,
                            Note = pUserBill.Note,
                            ShadowNote = pUserBill.shadowNote,
                            IsActive = pUserBill.isActive,
                            Currency = CurrencyCode.VND
                        };
                        await WorkScope.UpdateAsync(ObjectMapper.Map<TimeSheetProjectBillDto, TimesheetProjectBill>(timesheetProjectBillInput, timesheetProjectBill));
                        sucessList.Add($"{pUserBill.UserId}");
                        CurrentUnitOfWork.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        failList.Add($"error: {pUserBill.UserId}" + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        var isExist = await WorkScope.GetAll<TimesheetProjectBill>()
                            .Where(x => x.ProjectId == projectId && x.TimesheetId == timesheetId)
                            .AnyAsync(x => x.UserId == pUserBill.UserId);
                        if (!isExist)
                        {
                            var timesheetProjectBillInput = new TimeSheetProjectBillDto
                            {
                                UserId = pUserBill.UserId,
                                ProjectId = pUserBill.ProjectId,
                                TimeSheetId = timesheetId,
                                BillRole = pUserBill.BillRole,
                                BillRate = pUserBill.BillRate,
                                StartTime = pUserBill.StartTime.Date,
                                EndTime = pUserBill.EndTime.GetValueOrDefault().Date,
                                Note = pUserBill.Note,
                                ShadowNote = pUserBill.shadowNote,
                                IsActive = pUserBill.isActive,
                                Currency = CurrencyCode.VND
                            };
                            await WorkScope.InsertAsync(ObjectMapper.Map<TimesheetProjectBill>(timesheetProjectBillInput));
                            sucessList.Add($"{pUserBill.UserId}");
                            CurrentUnitOfWork.SaveChanges();
                            var timesheetProjectBillssss = await WorkScope.GetAll<TimesheetProjectBill>()
                            .Where(x => x.ProjectId == projectId && x.TimesheetId == timesheetId).Select(x => x.UserId)
                            .ToListAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        failList.Add($"error: {pUserBill.UserId}" + ex.Message);
                    }
                }
            }
            await UpdateProjectBillInformation(projectId, timesheetId);
            return new { sucessList, failList };
        }

        private async Task<List<string>> UpdateProjectBillInformation(long projectId, long timesheetId)
        {
            var failList = new List<string>();

            var timesheetProjectBills = await (from tspb in WorkScope.GetAll<TimesheetProjectBill>().Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId)
                                               join u in WorkScope.GetAll<User>() on tspb.UserId equals u.Id
                                               select new
                                               {
                                                   FullName = u.FullName,
                                                   BillRole = tspb.BillRole,
                                                   BillRate = tspb.BillRate,
                                                   Note = tspb.Note,
                                                   ShadowNote = tspb.ShadowNote
                                               }).ToListAsync();

            var billInfomation = new StringBuilder();

            foreach (var item in timesheetProjectBills)
            {
                try
                {
                    var timesheetProjects = await WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId).FirstOrDefaultAsync();
                    if (timesheetProjects != null)
                    {
                        billInfomation.Append($"<b>{item.FullName}</b> - {item.BillRole} - {item.BillRate} - {item.Note} - {item.ShadowNote} <br>");
                        timesheetProjects.ProjectBillInfomation = $"{billInfomation}";
                        await WorkScope.UpdateAsync(timesheetProjects);
                    }
                }
                catch (Exception ex)
                {
                    failList.Add($"error UserId = {item.FullName} : " + ex.Message);
                }
            }
            return failList;
        }


    }
}
