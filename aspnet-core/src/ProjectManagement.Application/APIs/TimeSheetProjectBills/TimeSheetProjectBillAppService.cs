using Abp.Authorization;
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
        [HttpGet]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_GetAll)]
        public async Task<List<GetTimeSheetProjectBillDto>> GetAll(long timesheetId, long projectId)
        {
            var permissionViewProjectBillInfo = PermissionChecker.IsGranted(PermissionNames.Timesheet_TimesheetDetail_ViewTimesheetAndBillInfoOfAllProject);
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
                             BillRate = permissionViewProjectBillInfo ? x.BillRate : -1,
                             StartTime = x.StartTime.Date,
                             EndTime = x.EndTime.Value.Date,
                             Note = x.Note,
                             ShadowNote = x.ShadowNote,
                             IsActive = x.IsActive,
                             AvatarPath = x.User.AvatarPath,
                             FullName = x.User.FullName,
                             Branch = x.User.Branch,
                             EmailAddress = x.User.EmailAddress,
                             UserType = x.User.UserType,
                             WorkingTime = x.WorkingTime,
                             UserLevel = x.User.UserLevel,
                             Currency = x.Project.Currency.Name,
                             ChargeType = x.Project.ChargeType,
                             //ProjectBillInfomation = $"<b>{x.User.FullName}</b> - {x.BillRole} - {x.BillRate} - {x.Note} - {x.ShadowNote} <br>"
                         });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Update)]
        public async Task<TimeSheetProjectBillDto> Create(TimeSheetProjectBillDto input)
        {
            var user = await WorkScope.GetAsync<User>(input.UserId);
            var isExist = await WorkScope.GetAll<TimesheetProjectBill>()
                .Where(x => x.TimesheetId == input.TimeSheetId && x.ProjectId == input.ProjectId)
                .AnyAsync(x => x.UserId == input.UserId);
            if (isExist)
            {
                throw new UserFriendlyException($"User has name: {user.FullName} is already existed");
            }
            if (string.IsNullOrWhiteSpace(input.BillRole) || string.IsNullOrWhiteSpace(input.BillRate.ToString()))
            {
                throw new UserFriendlyException("You must complete all required fields");
            }
            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<TimesheetProjectBill>(input));
            await UpdateProjectBillInformation(input.ProjectId, input.TimeSheetId.Value);
            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Update, PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_ChangeUser)]
        public async Task<List<TimeSheetProjectBillDto>> Update(List<TimeSheetProjectBillDto> input)
        {
            //đếm số lượng user trùng
            if(input.Count() > 1)
            {
                var existCount = (from i in input
                                  group i by i.UserId into g
                                  select new
                                  {
                                      UserId = g.Key,
                                      Count = g.Count()
                                  }).ToList();
                foreach (var item in existCount)
                {
                    if (item.Count >= 2)
                    {
                        var user = await WorkScope.GetAsync<User>(item.UserId);
                        throw new UserFriendlyException($"User has name: {user.FullName} is already existed");
                    }
                };
            }    
           
            foreach (var bill in input)
            {
                if(input.Count() <= 1)
                {
                    var currentBill = await WorkScope.GetAsync<TimesheetProjectBill>(bill.Id);
                    var user = await WorkScope.GetAsync<User>(bill.UserId);
                    var isExist = await WorkScope.GetAll<TimesheetProjectBill>()
                        .Where(x => x.TimesheetId == bill.TimeSheetId && x.ProjectId == bill.ProjectId && x.UserId != currentBill.UserId)
                        .AnyAsync(x => x.UserId == bill.UserId);
                    if(isExist)
                    {
                        throw new UserFriendlyException($"User has name: {user.FullName} is already existed");
                    }
                }

                if (string.IsNullOrWhiteSpace(bill.BillRole) || string.IsNullOrWhiteSpace(bill.BillRate.ToString()))
                {
                    throw new UserFriendlyException("You must complete all required fields");
                }
            }
            var timesheetProjectBillIds = input.Select(x => x.Id).ToList();
            var timesheetProjectBills = await WorkScope.GetAll<TimesheetProjectBill>().Where(x => timesheetProjectBillIds.Contains(x.Id)).ToListAsync();
            foreach (var bill in input)
            {
                var timesheetProjectBill = timesheetProjectBills.Where(x => x.Id == bill.Id).FirstOrDefault();
                if (bill.BillRate == -1)
                    bill.BillRate = timesheetProjectBill.BillRate;
                await WorkScope.UpdateAsync(ObjectMapper.Map<TimeSheetProjectBillDto, TimesheetProjectBill>(bill, timesheetProjectBill));
                CurrentUnitOfWork.SaveChanges();

                await UpdateProjectBillInformation(bill.ProjectId, bill.TimeSheetId.Value);
            }
            //await WorkScope.UpdateRangeAsync(ObjectMapper.Map<List<TimeSheetProjectBillDto>, List<TimesheetProjectBill>>(input, timesheetProjectBills));
            //CurrentUnitOfWork.SaveChanges();

            //input.ForEach(async x => await UpdateProjectBillInformation(x.ProjectId, x.TimeSheetId.Value));
            return input;
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_UpdateOnlyMyProjectPM, PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_ChangeUser)]
        public async Task<IActionResult> UpdateTSOfPM(List<UpdateTimesheetPMDto> input)
        {
            var timesheetProjectBillIds = input.Select(x => x.Id).ToList();
            var timesheetProjectBills = await WorkScope.GetAll<TimesheetProjectBill>()
                .Where(x => timesheetProjectBillIds.Contains(x.Id))
                .ToDictionaryAsync( x => x.Id,
                                    x => x);
            foreach (var bill in input)
            {
                var timesheetProjectBill = timesheetProjectBills[bill.Id];
                if (timesheetProjectBill != null)
                {
                    timesheetProjectBill.WorkingTime = bill.WorkingTime;
                    timesheetProjectBill.IsActive = bill.IsActive;
                    timesheetProjectBill.Note = bill.Note;
                }
            }
            CurrentUnitOfWork.SaveChanges();
            return new OkObjectResult("Save Success!");
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
                            Currency = CurrencyCode.VND,
                            WorkingTime = timesheetProjectBill.WorkingTime
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
                                               where tspb.IsActive == true
                                               select new
                                               {
                                                   FullName = u.FullName,
                                                   BillRole = tspb.BillRole,
                                                   BillRate = tspb.BillRate,
                                                   Note = tspb.Note,
                                                   IsCharge = tspb.IsActive
                                               }).ToListAsync();

            var billInfomation = new StringBuilder();
            var timesheetProjects = await WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId && x.ProjectId == projectId).FirstOrDefaultAsync();
            if(timesheetProjects != null)
            {
                foreach (var item in timesheetProjectBills)
                {
                    try
                    {
                        billInfomation.Append($"<b>{item.FullName}</b> - {item.BillRole} - {item.BillRate} - {item.Note} <br>");
                        timesheetProjects.ProjectBillInfomation = $"{billInfomation}";
                        await WorkScope.UpdateAsync(timesheetProjects);
                    }
                    catch (Exception ex)
                    {
                        failList.Add($"error UserId = {item.FullName} : " + ex.Message);
                    }
                }
            }
            return failList;
        }

        public async Task<List<GetUserForTimesheetProjectBillDto>> GetUserForTimesheetProjectBill(long timesheetId, long projectId, bool isEdited)
        {
            var currentUserIds = await WorkScope.GetAll<TimesheetProjectBill>()
                .Where(x => x.TimesheetId == timesheetId && x.ProjectId ==  projectId)
                .Select(x => x.UserId).ToListAsync();
        
            var users = WorkScope.GetAll<User>()
                                .Where(x => !isEdited ? !currentUserIds.Contains(x.Id) : true)
                                .Select(x => new GetUserForTimesheetProjectBillDto
                                {
                                    UserId = x.Id,
                                    FullName = x.FullName,
                                    Email = x.FullName
                                }).ToList();
            return users;
        }
    }
}
