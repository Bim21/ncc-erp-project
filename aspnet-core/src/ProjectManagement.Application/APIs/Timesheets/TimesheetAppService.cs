using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
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
            var timesheetProject = WorkScope.GetAll<TimesheetProject>();
            var query = WorkScope.GetAll<Timesheet>().OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                .Select(x => new GetTimesheetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Month = x.Month,
                    Year = x.Year,
                    IsActive = x.IsActive,
                    CreatedInvoice = x.CreatedInvoice,
                    TotalProject = timesheetProject.Where(y => y.TimesheetId == x.Id).Count(),
                    TotalHasFile = timesheetProject.Where(y => y.TimesheetId == x.Id &&
                                                            y.FilePath != null && 
                                                            y.Project.RequireTimesheetFile)
                                                    .Count(),
                    TotalWorkingDay = x.TotalWorkingDay,
                    TotalIsRequiredFile = timesheetProject.Where(y => y.TimesheetId == x.Id &&
                                                            y.Project.RequireTimesheetFile)
                                                    .Count(),
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
            try
            {
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
                input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<Timesheet>(input));
                var timesheet = await WorkScope.GetAsync<Timesheet>(input.Id);
                var project = await WorkScope.GetAll<Project>().Where(x => x.IsCharge == true).ToListAsync();
                foreach (var item in project)
                {
                    var billInfomation = new StringBuilder();
                    var projectUserBills = WorkScope.GetAll<ProjectUserBill>().Include(x => x.User)
                        .Where(x => x.ProjectId == item.Id && (!x.EndTime.HasValue || x.EndTime.Value.Date > timesheet.CreationTime.Date || (x.EndTime.Value.Month >= timesheet.Month && x.EndTime.Value.Year >= timesheet.Year)));
                    ;
                    //.Select(x => new
                    //{
                    //    FullName = x.User.FullName,
                    //    BillRole = x.BillRole,
                    //    BillRate = x.BillRate,
                    //    Note = x.Note,
                    //    ShadowNote = x.shadowNote
                    //});

                    foreach (var b in projectUserBills)
                    {
                        try
                        {
                            billInfomation.Append($"<b>{b.User.FullName}</b> - {b.BillRole} - {b.BillRate} - {b.Note} - {b.shadowNote} <br>");
                            var timesheetProjectBill = new TimeSheetProjectBillDto
                            {
                                ProjectId = b.ProjectId,
                                TimeSheetId = input.Id,
                                UserId = b.UserId,
                                BillRole = b.BillRole,
                                BillRate = b.BillRate,
                                StartTime = b.StartTime,
                                EndTime = b.EndTime,
                                Note = b.Note,
                                ShadowNote = b.shadowNote,
                                IsActive = b.isActive
                            };
                            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<TimesheetProjectBill>(timesheetProjectBill));
                        }
                        catch (Exception e)
                        {
                            failList.Add($"error UserId = {b.UserId}" + e.Message);
                        }
                    }

                    var timesheetProject = new TimesheetProject
                    {
                        ProjectId = item.Id,
                        TimesheetId = input.Id,
                        ProjectBillInfomation = $"{billInfomation}"
                    };
                    await WorkScope.InsertAndGetIdAsync(timesheetProject);
                }
                return new { failList, input};
            }
            catch(Exception e)
            {
                throw new UserFriendlyException("error: " + e.Message);
            }
            
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
            var timesheet = await WorkScope.GetAsync<Timesheet>(timesheetId);

            if (!timesheet.IsActive)
            {
                throw new UserFriendlyException("Timesheet not active !");
            }

            var hasTimesheetproject = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.TimesheetId == timesheetId && x.FilePath != null);

            if (hasTimesheetproject)
                throw new UserFriendlyException("Timesheet has attached file !");

            var timesheetProject = await WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId).ToListAsync();
            foreach(var item in timesheetProject)
            {
                await WorkScope.DeleteAsync(item);
            }

            await WorkScope.DeleteAsync(timesheet);
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
