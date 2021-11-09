using Abp.Authorization;
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

            var timesheetProjectBills = await WorkScope.GetAll<TimesheetProjectBill>().Include(x => x.User).Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId).ToListAsync();
            var billInfomation = new StringBuilder();

            foreach (var item in timesheetProjectBills)
            {
                try
                {
                    var timesheetProjects = await WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId).FirstOrDefaultAsync();
                    if(timesheetProjects != null)
                    {
                        billInfomation.Append($"<b>{item.User.FullName}</b> - {item.BillRole} - {item.BillRate} - {item.Note} - {item.ShadowNote} <br>");
                        timesheetProjects.ProjectBillInfomation = $"{billInfomation}";
                        await WorkScope.UpdateAsync(timesheetProjects);
                    }    
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
