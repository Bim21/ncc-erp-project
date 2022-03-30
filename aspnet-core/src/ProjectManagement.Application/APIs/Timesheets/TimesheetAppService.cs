using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using NccCore.Uitls;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using ProjectManagement.APIs.TimeSheetProjectBills.Dto;
using ProjectManagement.APIs.Timesheets.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.TimeSheets
{
    [AbpAuthorize]
    public class TimeSheetAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_ViewAll)]
        public async Task<GridResult<GetTimesheetDto>> GetAllPaging(GridParam input)
        {
            var qtimesheetProject = WorkScope.GetAll<TimesheetProject>();
            var qtimesheetProjectBill = WorkScope.GetAll<TimesheetProjectBill>()
                .Where(s => s.IsActive);
            var query = WorkScope.GetAll<Timesheet>().OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                .Select(x => new GetTimesheetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Month = x.Month,
                    Year = x.Year,
                    IsActive = x.IsActive,
                    CreatedInvoice = x.CreatedInvoice,
                    TotalWorkingDay = x.TotalWorkingDay,

                    TotalProject = qtimesheetProject.Where(y => y.TimesheetId == x.Id).Count(),

                    TotalHasFile = qtimesheetProject.Where(y => y.TimesheetId == x.Id &&
                                                            y.FilePath != null &&
                                                            y.Project.RequireTimesheetFile).Count(),

                    WorkingDayOfUser = qtimesheetProjectBill.Where(s => s.TimesheetId == x.Id).Sum(s => s.WorkingTime),

                    TotalIsRequiredFile = qtimesheetProject.Where(y => y.TimesheetId == x.Id)
                    .Where(s => s.Project.RequireTimesheetFile).Count(),

                });

            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_Get)]
        public async Task<TimesheetDto> Get(long timesheetId)
        {
            var query = WorkScope.GetAll<Timesheet>().Where(x => x.Id == timesheetId)
                                .Select(x => new TimesheetDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Month = x.Month,
                                    Year = x.Year,
                                    IsActive = x.IsActive,
                                    CreatedInvoice = x.CreatedInvoice,
                                    TotalWorkingDay = x.TotalWorkingDay
                                });
            return await query.FirstOrDefaultAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_Create)]
        public async Task<object> Create(TimesheetDto input)
        {
            if (input.TotalWorkingDay == null || input.TotalWorkingDay <= 0)
            {
                throw new UserFriendlyException("Total Of Working Day field is required and greater than 0 !");
            }

            var failList = new List<string>();
            var nameExist = await WorkScope.GetAll<Timesheet>().AnyAsync(x => x.Name == input.Name);
            if (nameExist)
            {
                throw new UserFriendlyException("Name is already exist !");
            }

            var alreadyCreated = await WorkScope.GetAll<Timesheet>().AnyAsync(x => x.Year == input.Year && x.Month == input.Month);
            if (alreadyCreated)
            {
                throw new UserFriendlyException($"Timesheet {input.Month}-{input.Year} already exist !");
            }

            var timesheet = new Timesheet
            {
                Name = input.Name,
                IsActive = true,
                Year = input.Year,
                Month = input.Month,
                TotalWorkingDay = input.TotalWorkingDay
            };

            input.Id = await WorkScope.InsertAndGetIdAsync(timesheet);
            timesheet.Id = input.Id;

            var timesheetStartDate = new DateTime(timesheet.Year, timesheet.Month, 1).Date;
            var timesheetEndDate = timesheetStartDate.AddMonths(1).AddDays(-1);

            var mapProjectIdToBillInfo = await WorkScope.GetAll<ProjectUserBill>()
                .Where(s => s.Project.IsCharge == true)
                .Where(s => s.isActive)
                .Where(s => !s.EndTime.HasValue || s.EndTime.Value.Date <= timesheetEndDate)
                .GroupBy(s => s.ProjectId)
                .Select(s => new
                {
                    ProjectId = s.Key,
                    ListBillInfo = s.Select(x => new { x.UserId, x.BillRate, x.BillRole, x.StartTime, x.EndTime }).ToList()
                })
               .ToDictionaryAsync(s => s.ProjectId);

            foreach (var item in mapProjectIdToBillInfo)
            {
                var projectId = item.Key;
                var listBillInfo = item.Value.ListBillInfo;
                var timesheetProject = new TimesheetProject
                {
                    ProjectId = projectId,
                    TimesheetId = timesheet.Id,
                };

                foreach (var b in listBillInfo)
                {
                    var timesheetProjectBill = new TimesheetProjectBill
                    {
                        ProjectId = projectId,
                        TimesheetId = timesheet.Id,
                        UserId = b.UserId,
                        BillRole = b.BillRole,
                        BillRate = b.BillRate,
                        StartTime = b.StartTime,
                        EndTime = b.EndTime,
                        IsActive = true
                    };
                    await WorkScope.InsertAsync(timesheetProjectBill);
                }
            }

            return new { failList, input };

        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_Update)]
        public async Task<TimesheetDto> Update(TimesheetDto input)
        {
            if (input.TotalWorkingDay == null || input.TotalWorkingDay <= 0)
            {
                throw new UserFriendlyException("Total Of Working Day field is required and greater than 0 !");
            }
            var timesheet = await WorkScope.GetAsync<Timesheet>(input.Id);

            if (!timesheet.IsActive)
            {
                throw new UserFriendlyException("Timesheet not active !");
            }

            var nameExist = await WorkScope.GetAll<Timesheet>().AnyAsync(x => x.Id != input.Id && x.Name == input.Name);
            if (nameExist)
            {
                throw new UserFriendlyException("Name is already exist !");
            }

            var alreadyCreated = await WorkScope.GetAll<Timesheet>().AnyAsync(x => x.Id != input.Id && x.Year == input.Year && x.Month == input.Month);
            if (alreadyCreated)
            {
                throw new UserFriendlyException($"Timesheet {input.Month}-{input.Year} already exist !");
            }

            await WorkScope.UpdateAsync(ObjectMapper.Map<TimesheetDto, Timesheet>(input, timesheet));
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_Delete)]
        public async Task Delete(long timesheetId)
        {
            var ts = await WorkScope.GetAll<TimesheetProject>()
                .Where(s => s.TimesheetId == timesheetId)
                .Select(s => new
                {
                    s.Timesheet.IsActive,
                    s.FilePath
                }).FirstOrDefaultAsync();

            if (!ts.IsActive)
            {
                throw new UserFriendlyException("Timesheet not active !");
            }

            if (ts.FilePath != null)
                throw new UserFriendlyException("Timesheet has attached file !");

            await ForceDelete(timesheetId);
        }

       

        [HttpDelete]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_ForceDelete)]
        public async Task ForceDelete(long timesheetId)
        {
            var timesheet = await WorkScope.GetAsync<Timesheet>(timesheetId);

            var timesheetProject = await WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId).ToListAsync();
            foreach (var item in timesheetProject)
            {
                item.IsDeleted = true;
            }

            timesheet.IsDeleted = true;
            CurrentUnitOfWork.SaveChanges();
        }
        
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_ReverseActive)]
        public async Task ReverseActive(long id)
        {
            var timesheet = await WorkScope.GetAsync<Timesheet>(id);
            timesheet.IsActive = !timesheet.IsActive;
            await WorkScope.UpdateAsync(timesheet);
        }
    }
}
