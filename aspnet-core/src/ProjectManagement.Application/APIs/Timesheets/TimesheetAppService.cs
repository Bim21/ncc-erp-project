using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.APIs.Timesheets.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.TimeSheets
{
    public class TimeSheetAppService : ProjectManagementAppServiceBase
    {
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
                    TotalTimesheet = timesheetProject.Where(y => y.TimesheetId == x.Id && y.TimesheetFile != null).Select(x => x.TimesheetId).Distinct().Count(),
                    HasInvoice = timesheetProject.Select(y => y.Id).Contains(x.Id)
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
            var query = WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId)
                                .Select(x => new GetTimesheetDetailDto
                                {
                                    Id = x.Id,
                                    ProjectId = x.ProjectId,
                                    ProjectName = x.Project.Name,
                                    PmId = x.Project.PmId,
                                    PmName = x.Project.PM.Name,
                                    ClientId = x.Project.ClientId,
                                    ClientName = x.Project.Clients.Name,
                                    File = x.TimesheetFile,
                                    Note = x.Note
                                });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Timesheet_Create)]
        public async Task<TimesheetDto> Create(TimesheetDto input)
        {
            var nameExist = await WorkScope.GetAll<Timesheet>().AnyAsync(x => x.Name == input.Name);
            if(nameExist)
            {
                throw new UserFriendlyException("Name is already exist !");
            }

            var alreadyCreated = await WorkScope.GetAll<Timesheet>().AnyAsync(x => x.Year == input.Year && x.Month == input.Month);
            if(alreadyCreated)
            {
                throw new UserFriendlyException($"Timesheet {input.Month}/{input.Year} already exist !");
            }

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<Timesheet>(input));

            var project = await WorkScope.GetAll<Project>().Where(x => x.Status != ProjectStatus.Closed && x.IsCharge == true).ToListAsync();
            foreach(var item in project)
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

            await WorkScope.UpdateAsync<Timesheet>(ObjectMapper.Map<Timesheet>(input));
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
    }
}
