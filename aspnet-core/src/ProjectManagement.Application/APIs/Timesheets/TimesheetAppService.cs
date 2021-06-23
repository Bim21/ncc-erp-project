using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Timesheets.Dto;
using ProjectManagement.Authorization;
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
        public async Task<GridResult<TimesheetDto>> GetAllPaging(GridParam input)
        {
            var query = WorkScope.GetAll<Timesheet>().Select(x => new TimesheetDto
            {
                Id = x.Id,
                Name = x.Name,
                Month = x.Month,
                Year = x.Year,
                Status = x.Status
            });
            return await query.GetGridResult(query, input);
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

            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<Timesheet>(input));
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

            await WorkScope.UpdateAsync<Timesheet>(timesheet);
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
