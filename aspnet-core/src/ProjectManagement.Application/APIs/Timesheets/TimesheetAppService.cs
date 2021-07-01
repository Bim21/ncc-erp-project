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
    public class TimeSheetAppService : ProjectManagementAppServiceBase
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        public TimeSheetAppService(
                   UserManager userManager,
                   RoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_ViewAll)]
        public async Task<GridResult<GetTimesheetDto>> GetAllPaging(GridParam input)
        {
            var timesheetProject = WorkScope.GetAll<TimesheetProject>();
            var query = WorkScope.GetAll<Timesheet>()
                .Select(x => new GetTimesheetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Month = x.Month,
                    Year = x.Year,
                    IsActive = x.IsActive,
                    TotalProject = timesheetProject.Where(y => y.TimesheetId == x.Id).Select(x => x.ProjectId).Count(),
                    TotalTimesheet = timesheetProject.Where(y => y.TimesheetId == x.Id && y.FilePath != null).Select(x => x.TimesheetId).Count()
                });
            //var query1 = from ts in WorkScope.GetAll<Timesheet>()
            //             join tsp in WorkScope.GetAll<TimesheetProject>() on ts.Id equals tsp.TimesheetId
            //             /*group tsp by new { ts.Id, ts.Name, ts.Month, ts.Year, ts.IsActive }*/ into pp
            //             from p in pp.DefaultIfEmpty()
            //             select new GetTimesheetDto
            //             {
            //                 Id = ts.Id,
            //                 Name = ts.Name,
            //                 Month = ts.Month,
            //                 Year = ts.Year,
            //                 IsActive = ts.IsActive,
            //                 TotalProject = pp.Count(),
            //                 TotalTimesheet = pp.Where(x => x.FilePath != null).Count()
            //             };

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
                                });
            return await query.FirstOrDefaultAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_Create)]
        public async Task<TimesheetDto> Create(TimesheetDto input)
        {
            var nameExist = await WorkScope.GetAll<Timesheet>().AnyAsync(x => x.Name == input.Name);
            if (nameExist)
            {
                throw new UserFriendlyException("Name is already exist !");
            }

            var alreadyCreated = await WorkScope.GetAll<Timesheet>().AnyAsync(x => x.Year == input.Year && x.Month == input.Month);
            if (alreadyCreated)
            {
                throw new UserFriendlyException($"Timesheet {input.Month}/{input.Year} already exist !");
            }

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<Timesheet>(input));

            

            var project = await WorkScope.GetAll<Project>().Where(x => x.Status != ProjectStatus.Potential && x.Status != ProjectStatus.Closed && x.IsCharge).ToListAsync();
            foreach (var item in project)
            {
                var billInfomation = new StringBuilder();
                var projectUserBill = from pu in WorkScope.GetAll<ProjectUser>().Where(x => x.ProjectId == item.Id)
                                      join pub in WorkScope.GetAll<ProjectUserBill>().Where(x => x.isActive) on pu.Id equals pub.ProjectId into pp
                                      from p in pp.DefaultIfEmpty()
                                      select new
                                      {
                                          UserName = p.User.FullName,
                                          BillRole = p.BillRole,
                                          BillRate = p.BillRate
                                      };

                foreach (var b in projectUserBill)
                {
                    billInfomation.Append($"{b.UserName} - {b.BillRole} - {b.BillRate}<br>");
                }

                var timesheetProject = new TimesheetProject
                {
                    ProjectId = item.Id,
                    TimesheetId = input.Id,
                    ProjectBillInfomation = $"{billInfomation}"
                };
                await WorkScope.InsertAndGetIdAsync(timesheetProject);
            }

            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_Update)]
        public async Task<TimesheetDto> Update(TimesheetDto input)
        {
            var timesheet = await WorkScope.GetAsync<Timesheet>(input.Id);

            var nameExist = await WorkScope.GetAll<Timesheet>().AnyAsync(x => x.Id != input.Id && x.Name == input.Name);
            if (nameExist)
            {
                throw new UserFriendlyException("Name is already exist !");
            }

            await WorkScope.UpdateAsync(ObjectMapper.Map<TimesheetDto, Timesheet>(input, timesheet));
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.Timesheet_Timesheet_Delete)]
        public async Task Delete(long timesheetId)
        {
            var timesheet = await WorkScope.GetAsync<Timesheet>(timesheetId);

            var hasTimesheetproject = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.TimesheetId == timesheetId);
            if (hasTimesheetproject)
                throw new UserFriendlyException("Timesheet has Timesheet Project");

            await WorkScope.DeleteAsync(timesheet);
        }

        //[HttpPost]
        //[AbpAuthorize(PermissionNames.Timesheet_Timesheet_DoneTimesheetById)]
        //public async Task DoneTimesheetById(long timesheetId)
        //{
        //    var timeSheet = await WorkScope.GetAsync<Timesheet>(timesheetId);

        //    timeSheet.IsActive = TimesheetStatus.Done;
        //    await WorkScope.UpdateAsync(timeSheet);
        //}
    }
}
