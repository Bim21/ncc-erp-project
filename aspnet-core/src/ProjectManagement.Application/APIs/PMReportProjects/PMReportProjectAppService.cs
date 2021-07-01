using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.PMReportProjects.Dto;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReportProjects
{
    public class PMReportProjectAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_GetAllByPmReport)]
        public async Task<GridResult<GetPMReportProjectDto>> GetAllByPmReport(GridParam input, long pmReportId)
        {
            var query = WorkScope.GetAll<PMReportProject>().Where(x => x.PMReportId == pmReportId)
                .Select(x => new GetPMReportProjectDto
                {
                    Id = x.Id,
                    PMReportId = x.PMReportId,
                    PMReportName = x.PMReport.Name,
                    ProjectId = x.ProjectId,
                    ProjectName = x.Project.Name,
                    Status = x.Status.ToString(),
                    ProjectHealth = x.ProjectHealth.ToString(),
                    PMId = x.PMId,
                    PmName = x.PM.Name,
                    Note = x.Note
                });
            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        public async Task<GridResult<GetProjectUserDto>> ResourceChangesDuringTheWeek(GridParam input, long projectId)
        {
            var query = from p in WorkScope.GetAll<Project>().Where(x => x.Id == projectId)
                        join pu in WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Past) on p.Id equals pu.ProjectId into pp
                        from x in pp.DefaultIfEmpty()
                        select new GetProjectUserDto
                        {
                            Id = x.Id,
                            UserId = x.UserId,
                            UserName = x.User.FullName,
                            ProjectId = x.ProjectId,
                            ProjectName = x.Project.Name,
                            ProjectRole = x.ProjectRole.ToString(),
                            AllocatePercentage = x.AllocatePercentage,
                            StartTime = x.StartTime.Date,
                            Status = x.Status.ToString(),
                            IsExpense = x.IsExpense,
                            ResourceRequestId = x.ResourceRequestId,
                            ResourceRequestName = x.ResourceRequest.Name,
                            PMReportId = x.PMReportId,
                            PMReportName = x.PMReport.Name,
                            IsFutureActive = x.IsFutureActive
                        };

            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        public async Task<GridResult<GetProjectUserDto>> ResourceChangesInTheFuture(GridParam input, long projectId)
        {
            var query = from p in WorkScope.GetAll<Project>().Where(x => x.Id == projectId)
                                     join pu in WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Future) on p.Id equals pu.ProjectId into pp
                                     from x in pp.DefaultIfEmpty()
                                     select new GetProjectUserDto
                                     {
                                         Id = x.Id,
                                         UserId = x.UserId,
                                         UserName = x.User.FullName,
                                         ProjectId = x.ProjectId,
                                         ProjectName = x.Project.Name,
                                         ProjectRole = x.ProjectRole.ToString(),
                                         AllocatePercentage = x.AllocatePercentage,
                                         StartTime = x.StartTime.Date,
                                         Status = x.Status.ToString(),
                                         IsExpense = x.IsExpense,
                                         ResourceRequestId = x.ResourceRequestId,
                                         ResourceRequestName = x.ResourceRequest.Name,
                                         PMReportId = x.PMReportId,
                                         PMReportName = x.PMReport.Name,
                                         IsFutureActive = x.IsFutureActive
                                     };

            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_Create)]
        public async Task<PMReportProjectDto> Create(PMReportProjectDto input)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(input.PMReportId);
            if(!pmReport.IsActive)
            {
                throw new UserFriendlyException("PMReport is locked !");
            }

            var isExist = await WorkScope.GetAll<PMReportProject>().AnyAsync(x => x.PMReportId == input.PMReportId && x.ProjectId == input.ProjectId);
            if (isExist)
                throw new UserFriendlyException("PMReportProject already exist !");

            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<PMReportProject>(input));
            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_Update)]
        public async Task<PMReportProjectDto> Update(PMReportProjectDto input)
        {
            var pmReportProject = await WorkScope.GetAsync<PMReportProject>(input.Id);

            if (!pmReportProject.PMReport.IsActive)
            {
                throw new UserFriendlyException("PMReport is locked !");
            }

            var isExist = await WorkScope.GetAll<PMReportProject>().AnyAsync(x => x.Id != input.Id && x.PMReportId == input.PMReportId && x.ProjectId == input.ProjectId);
            if (isExist)
                throw new UserFriendlyException("PMReportProject already exist !");

            await WorkScope.UpdateAsync(ObjectMapper.Map<PMReportProjectDto, PMReportProject>(input, pmReportProject));
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_Delete)]
        public async Task Delete(long pmPeportProjectId)
        {
            var pmReportProject = await WorkScope.GetAsync<PMReportProject>(pmPeportProjectId);
            if (!pmReportProject.PMReport.IsActive)
            {
                throw new UserFriendlyException("PMReport is locked !");
            }

            await WorkScope.DeleteAsync(pmReportProject);
        }
    }
}
