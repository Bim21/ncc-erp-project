﻿using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using ProjectManagement.APIs.TimeSheetProjectBills.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.ProjectUserBills
{
    [AbpAuthorize]
    public class ProjectUserBillAppService : ProjectManagementAppServiceBase
    {
        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_GetAllPaging)]
        public async Task<GridResult<GetProjectUserBillDto>> GetAllPaging(GridParam input)
        {
            var query = WorkScope.GetAll<ProjectUserBill>()
                        .Select(x => new GetProjectUserBillDto
                        {
                           Id = x.Id,
                           UserId = x.UserId,
                           UserName = x.User.FullName,
                           ProjectId = x.ProjectId,
                           ProjectName = x.Project.Name,
                           BillRole = x.BillRole,
                           BillRate = x.BillRate,
                           StartTime = x.StartTime.Date,
                           EndTime = x.EndTime.Value.Date,
                           Currency = x.Currency,
                           Note= x.Note,
                           shadowNote = x.shadowNote,
                           isActive = x.isActive
                        });
            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_GetAllByproject)]
        public async Task<List<GetProjectUserBillDto>> GetAllByProject(long projectId)
        {
            var query = WorkScope.GetAll<ProjectUserBill>().Where(x => x.ProjectId == projectId).OrderByDescending(x => x.CreationTime)
                        .Select(x => new GetProjectUserBillDto
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
                            Currency = x.Currency,
                            Note = x.Note,
                            shadowNote = x.shadowNote,
                            isActive = x.isActive,
                            AvatarPath = "/avatars/" + x.User.AvatarPath,
                            FullName = x.User.FullName,
                            Branch = x.User.Branch,
                            EmailAddress = x.User.EmailAddress,
                            UserType = x.User.UserType
                        });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_Create)]
        public async Task<ProjectUserBillDto> Create(ProjectUserBillDto input)
        {
            if (input.EndTime.HasValue && input.StartTime.Date > input.EndTime.Value.Date)
                throw new UserFriendlyException($"Start date cannot be greater than end date !");

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectUserBill>(input));

            var currentTimesheetProject = await WorkScope.GetAll<TimesheetProject>()
                .Include(x => x.Timesheet)
                .Where(x => x.ProjectId == input.ProjectId)
                .OrderByDescending(x => x.CreationTime)
                .FirstOrDefaultAsync();
            if(currentTimesheetProject != default && (!input.EndTime.HasValue || input.EndTime > currentTimesheetProject.CreationTime || input.EndTime.Value.Month == currentTimesheetProject.Timesheet.Month))
            {
                var user = await WorkScope.GetAsync<User>(input.UserId);
                currentTimesheetProject.ProjectBillInfomation += $"<b>{user.FullName}</b> - {input.BillRole} - {input.BillRate} <br>";
                await WorkScope.UpdateAsync(currentTimesheetProject);
                var timesheetProjectBill = new TimeSheetProjectBillDto
                {
                    ProjectId = input.ProjectId,
                    TimeSheetId = input.Id,
                    UserId = input.UserId,
                    BillRole = input.BillRole,
                    BillRate = input.BillRate,
                    StartTime = input.StartTime,
                    EndTime = input.EndTime,
                    Note = input.Note,
                    ShadowNote = input.shadowNote,
                    IsActive = input.isActive
                };
                await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<TimesheetProjectBill>(timesheetProjectBill));
            }

            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_Update)]
        public async Task<ProjectUserBillDto> Update(ProjectUserBillDto input)
        {
            var projectUserBill = await WorkScope.GetAsync<ProjectUserBill>(input.Id);

            if (input.EndTime.HasValue && input.StartTime.Date > input.EndTime.Value.Date)
                throw new UserFriendlyException($"Start date cannot be greater than end date !");

            await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectUserBillDto, ProjectUserBill>(input, projectUserBill));

            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_Delete)]
        public async Task Delete(long projectUserBillId)
        {
            var projectUserBill = await WorkScope.GetAsync<ProjectUserBill>(projectUserBillId);

            await WorkScope.DeleteAsync(projectUserBill);
        }
    }
}
