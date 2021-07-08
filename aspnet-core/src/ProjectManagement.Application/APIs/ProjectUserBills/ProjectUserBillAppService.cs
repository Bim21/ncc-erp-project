using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.ProjectUserBills
{
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
                            isActive = x.isActive
                        });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_Create)]
        public async Task<ProjectUserBillDto> Create(ProjectUserBillDto input)
        {
            if (input.StartTime.Date > input.EndTime.Value.Date)
                throw new UserFriendlyException($"Start date cannot be greater than end date !");

            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectUserBill>(input));

            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_Update)]
        public async Task<ProjectUserBillDto> Update(ProjectUserBillDto input)
        {
            var projectUserBill = await WorkScope.GetAsync<ProjectUserBill>(input.Id);

            if (input.StartTime.Date > input.EndTime.Value.Date)
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
