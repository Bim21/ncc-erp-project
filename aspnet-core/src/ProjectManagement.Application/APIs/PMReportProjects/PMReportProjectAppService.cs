using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.PMReportProjects.Dto;
using ProjectManagement.APIs.ProjectUsers;
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
        public async Task<List<GetPMReportProjectDto>> GetAllByPmReport(long pmReportId)
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
            return await query.ToListAsync();
        }

        [HttpGet]
        public async Task<List<GetPMReportProjectDto>> GetAllPmReportProjectForDropDown()
        {
            var query = WorkScope.GetAll<PMReportProject>().Include(x => x.PM)
                              .Where(x => x.PMReport.IsActive)
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
                                  Note = x.Note
                              });

            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek)]
        public async Task<List<GetProjectUserDto>> ResourceChangesDuringTheWeek(long projectId)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.ProjectId == projectId && x.PMReport.IsActive)
                            .Where(x => x.Status == ProjectUserStatus.Present).OrderByDescending(x => x.CreationTime)
                            .Select(x => new GetProjectUserDto
                            {
                                Id = x.Id,
                                UserId = x.UserId,
                                FullName = x.User.FullName,
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
                            });

            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesInTheFuture)]
        public async Task<List<GetProjectUserDto>> ResourceChangesInTheFuture(long projectId)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.ProjectId == projectId && x.PMReport.IsActive)
                            .Where(x => x.Status == ProjectUserStatus.Future).OrderByDescending(x => x.CreationTime)
                            .Select(x => new GetProjectUserDto
                            {
                                Id = x.Id,
                                UserId = x.UserId,
                                FullName = x.User.FullName,
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
                            });

            return await query.ToListAsync();
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
