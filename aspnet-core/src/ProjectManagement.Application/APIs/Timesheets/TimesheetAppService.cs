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
        [AbpAuthorize(PermissionNames.PmManager_Timesheet_ViewAll)]
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
                    Status = x.Status,
                    TotalProject = timesheetProject.Where(y => y.TimesheetId == x.Id).Select(x => x.ProjectId).Distinct().Count(),
                    TotalTimesheet = timesheetProject.Where(y => y.TimesheetId == x.Id && y.TimesheetFile != null).Select(x => x.TimesheetId).Distinct().Count()
                });

            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_Timesheet_Get)]
        public async Task<TimesheetDto> Get(long timesheetId)
        {
            var query = WorkScope.GetAll<Timesheet>().Where(x => x.Id == timesheetId)
                                .Select(x => new TimesheetDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Month = x.Month,
                                    Year = x.Year,
                                    Status = x.Status
                                });
            return await query.FirstOrDefaultAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_Timesheet_GetTimesheetDetail)]
        public async Task<List<GetTimesheetDetailDto>> GetTimesheetDetail(long timesheetId)
        {
            var rolePM = _roleManager.GetRoleByName(RoleConstants.ROLE_PM);
            var userRolePMs = (await _userManager.GetUsersInRoleAsync(rolePM.NormalizedName)).ToList();

            var roleKetoan = _roleManager.GetRoleByName(RoleConstants.ROLE_KETOAN);
            var userRoleKetoan = (await _userManager.GetUsersInRoleAsync(rolePM.NormalizedName)).ToList();

            var projectUserBill = from pu in WorkScope.GetAll<ProjectUser>()
                                  join pub in WorkScope.GetAll<ProjectUserBill>() on pu.UserId equals pub.UserId into pp
                                  from p in pp.DefaultIfEmpty()
                                  select new
                                  {
                                      ProjectId = pu.ProjectId,
                                      userId = pu.UserId,
                                      UserName = pu.User.Name,
                                      Role = pu.ProjectRole,
                                      BillRate = p.BillRate
                                  };

            var query = WorkScope.GetAll<TimesheetProject>()
                                .Where(x => x.TimesheetId == timesheetId)
                                .Where(x => userRolePMs.Select(y => y.Id).Contains(AbpSession.UserId.Value) ? x.Project.PmId == AbpSession.UserId.Value : true)
                                .Where(x => userRoleKetoan.Select(y => y.Id).Contains(AbpSession.UserId.Value) ? x.Project.Status != ProjectStatus.Closed : true)
                                .Select(x => new GetTimesheetDetailDto
                                {
                                    Id = x.Id,
                                    ProjectId = x.ProjectId,
                                    TimesheetId = x.TimesheetId,
                                    ProjectName = x.Project.Name,
                                    PmId = x.Project.PmId,
                                    PmName = x.Project.PM.Name,
                                    ClientId = x.Project.ClientId,
                                    ClientName = x.Project.Clients.Name,
                                    File = "/timesheets/" + x.TimesheetFile,
                                    ProjectUserBill = projectUserBill.Where(y => y.ProjectId == x.ProjectId).Select(y => new GetProjectUserBillDto
                                    {
                                        UserId = y.userId,
                                        UserName = y.UserName,
                                        BillRole = y.Role,
                                        BillRate = y.BillRate
                                    }).ToList(),
                                    Note = x.Note
                                });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Timesheet_Create)]
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

            var project = await WorkScope.GetAll<Project>().Where(x => x.Status != ProjectStatus.Potential && x.Status != ProjectStatus.Closed && x.IsCharge == true).ToListAsync();
            foreach (var item in project)
            {
                var timesheetProject = new TimesheetProject
                {
                    ProjectId = item.Id,
                    TimesheetId = input.Id,
                };
                await WorkScope.InsertAndGetIdAsync(timesheetProject);
            }

            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_Timesheet_Update)]
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
        [AbpAuthorize(PermissionNames.PmManager_Timesheet_Delete)]
        public async Task Delete(long timesheetId)
        {
            var timesheet = await WorkScope.GetAsync<Timesheet>(timesheetId);

            var hasTimesheetproject = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.TimesheetId == timesheetId);
            if (hasTimesheetproject)
                throw new UserFriendlyException("Timesheet has Timesheet Project");

            await WorkScope.DeleteAsync(timesheet);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Timesheet_DoneTimesheetById)]
        public async Task DoneTimesheetById(long timesheetId)
        {
            var timeSheet = await WorkScope.GetAsync<Timesheet>(timesheetId);

            timeSheet.Status = TimesheetStatus.Done;
            await WorkScope.UpdateAsync(timeSheet);
        }
    }
}
