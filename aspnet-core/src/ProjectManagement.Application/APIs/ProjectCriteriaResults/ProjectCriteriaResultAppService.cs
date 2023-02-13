﻿using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.APIs.ProjectCriteriaResults.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using ProjectManagement.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ProjectCriteriaResults
{
    public class ProjectCriteriaResultAppService : ProjectManagementAppServiceBase
    {
        [AbpAuthorize(
            PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria,
            PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria_Edit,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria_Edit,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria_Edit,
            PermissionNames.WeeklyReport_ReportDetail_ProjectHealthCriteria_Edit,
            PermissionNames.WeeklyReport_ReportDetail_ProjectHealthCriteria_ChangeStatus
            )]
        [HttpPost]
        public async Task<CreateProjectCriteriaResultDto> Create(CreateProjectCriteriaResultDto input)
        {
            var projectWeeklyReportStatus = WorkScope.GetAll<PMReportProject>()
                .Where(x => x.ProjectId == input.ProjectId && x.PMReportId == input.PMReportId).Select(x => x.Status).First();
            input.Id = await WorkScope.InsertAndGetIdAsync<ProjectCriteriaResult>(new ProjectCriteriaResult
            {
                Note = input.Note,
                PMReportId = input.PMReportId,
                Status = input.Status,
                ProjectCriteriaId = input.ProjectCriteriaId,
                ProjectId = input.ProjectId,
            });
            var listStatus = WorkScope.GetAll<ProjectCriteriaResult>()
                .Where(x => x.PMReportId == input.PMReportId && x.ProjectId == input.ProjectId && x.Id != input.Id)
                .Select(x => x.Status)
                .OrderByDescending(x => x)
                .ToList();

            if (projectWeeklyReportStatus == PMReportProjectStatus.Sent)
            {
                await SetProjectHealth(input, listStatus);
            }
            return input;
        }

        [AbpAuthorize(
            PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria,
            PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria_Edit,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria_Edit,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria_Edit,
            PermissionNames.WeeklyReport_ReportDetail_ProjectHealthCriteria_Edit,
            PermissionNames.WeeklyReport_ReportDetail_ProjectHealthCriteria_ChangeStatus
            )]
        [HttpDelete]
        public async Task Delete(long projectCriteriaResultId)
        {
            var isExist = await WorkScope.GetAll<ProjectCriteriaResult>().AnyAsync(x => x.Id == projectCriteriaResultId);
            await WorkScope.DeleteAsync<ProjectCriteria>(projectCriteriaResultId);
        }

        [HttpGet]
        public async Task<GetProjectCriteriaResultDto> Get(long id)
        {
            var res = await WorkScope.GetAsync<ProjectCriteriaResult>(id);
            return new GetProjectCriteriaResultDto
            {
                CriteriaName = res.ProjectCriteria.Name,
                Guideline = res.ProjectCriteria.Guideline,
                Id = res.Id,
                Note = res.Note,
                PMReportId = res.PMReportId,
                ProjectId = res.ProjectId,
                Status = res.Status,
                ProjectCriteriaId = res.ProjectCriteriaId
            };
        }

        [HttpGet]
        public async Task<List<GetProjectCriteriaResultDto>> GetAll(long pmReportId, long projectId)
        {
            return await WorkScope.GetAll<ProjectCriteriaResult>().Where(x => x.ProjectId == projectId && x.PMReportId == pmReportId)
                .Select(x => new GetProjectCriteriaResultDto
                {
                    CriteriaName = x.ProjectCriteria.Name,
                    Guideline = x.ProjectCriteria.Guideline,
                    Id = x.Id,
                    Note = x.Note,
                    PMReportId = x.PMReportId,
                    ProjectId = x.ProjectId,
                    Status = x.Status,
                    ProjectCriteriaId = x.ProjectCriteriaId
                }).ToListAsync();
        }

        [AbpAuthorize(
            PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria,
            PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria_Edit,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria_Edit,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectHealthCriteria_Edit,
            PermissionNames.WeeklyReport_ReportDetail_ProjectHealthCriteria_Edit,
            PermissionNames.WeeklyReport_ReportDetail_ProjectHealthCriteria_ChangeStatus
            )]
        [HttpPut]
        public async Task<CreateProjectCriteriaResultDto> Update(CreateProjectCriteriaResultDto input)
        {
            var prjCriteria = await WorkScope.GetAsync<ProjectCriteriaResult>(input.Id);
            var projectWeeklyReportStatus = WorkScope.GetAll<PMReportProject>()
                .Where(x => x.ProjectId == input.ProjectId && x.PMReportId == input.PMReportId).Select(x => x.Status).First();
            await WorkScope.UpdateAsync(ObjectMapper.Map<CreateProjectCriteriaResultDto, ProjectCriteriaResult>(input, prjCriteria));
            var listStatus = WorkScope.GetAll<ProjectCriteriaResult>()
                .Where(x => x.PMReportId == input.PMReportId && x.ProjectId == input.ProjectId && x.Id != input.Id)
                .Select(x => x.Status)
                .OrderByDescending(x => x)
                .ToList();

            if (projectWeeklyReportStatus == PMReportProjectStatus.Sent)
            {
                await SetProjectHealth(input, listStatus);
            }
            return input;
        }

        private async Task UpdateHealth(long pmReport, long projectId, ProjectHealth health)
        {
            var item = WorkScope.GetAll<PMReportProject>()
                .Where(x => x.PMReportId == pmReport && x.ProjectId == projectId).First();
            item.ProjectHealth = health;
            await WorkScope.UpdateAsync(item);
        }

        private async Task SetProjectHealth(CreateProjectCriteriaResultDto input, List<ProjectCriteriaResultStatus> listStatus)
        {
            var max = input.Status >= listStatus.First() ? input.Status : listStatus.First();
            await UpdateHealth(input.PMReportId, input.ProjectId, CommonUtil.GetProjectHealthByString(max.ToString()));
        }
    }
}