using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.PMReports.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReports
{
    public class PMReportAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_ViewAll)]
        public async Task<GridResult<GetPMReportDto>> GetAllPaging(GridParam input)
        {
            var pmRepoertProject = WorkScope.GetAll<PMReportProject>();
            var query = WorkScope.GetAll<PMReport>().Select(x => new GetPMReportDto
            {
                Id = x.Id,
                Name = x.Name,
                Year = x.Year,
                IsActive = x.IsActive,
                Type = x.Type,
                NumberOfProject = pmRepoertProject.Where(y => y.PMReportId == x.Id).Count()
            });
            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        public async Task<List<PMReportDto>> GetAll()
        {
            return await WorkScope.GetAll<PMReport>().Select(x => new PMReportDto
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                Year = x.Year,
                Type = x.Type
            }).ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_Create)]
        public async Task<PMReportDto> Create(PMReportDto input)
        {
            var isExist = await WorkScope.GetAll<PMReport>().AnyAsync(x => x.Name == input.Name && x.Type == input.Type && x.Year == input.Year);
            if (isExist)
                throw new UserFriendlyException("PM Report already exist !");

            var activeReport = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            activeReport.IsActive = false;
            await WorkScope.UpdateAsync(activeReport);

            input.Year = DateTime.Now.Year;
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<PMReport>(input));

            var pmReportProjectInProcess = from prp in WorkScope.GetAll<PMReportProject>().Where(x => x.PMReportId == activeReport.Id)
                                           join prpi in WorkScope.GetAll<PMReportProjectIssue>().Where(x => x.Status == PMReportProjectIssueStatus.InProgress)
                                           on prp.Id equals prpi.PMReportProjectId
                                           select new
                                           {
                                               ProjectId = prp.ProjectId,
                                               Issue = prpi
                                           };

            var projectActive = await WorkScope.GetAll<Project>().Where(x => x.Status != ProjectStatus.Potential && x.Status != ProjectStatus.Closed).ToListAsync();
            foreach (var item in projectActive)
            {
                
                var pmReportProject = new PMReportProject
                {
                    PMReportId = input.Id,
                    ProjectId = item.Id,
                    Status = PMReportProjectStatus.Draft,
                    ProjectHealth = ProjectHealth.Green,
                    PMId = item.PMId,
                    Note = null
                };
                pmReportProject.Id = await WorkScope.InsertAndGetIdAsync(pmReportProject);

                var listInProcess = pmReportProjectInProcess.Where(x => x.ProjectId == item.Id).Select(x => x.Issue);
                foreach(var issue in listInProcess)
                {
                    var pmReportProjectIssue = new PMReportProjectIssue
                    {
                       PMReportProjectId = pmReportProject.Id,
                       CreationTime = issue.CreationTime,
                       Description = issue.Description,
                       Impact = issue.Impact,
                       Critical = issue.Critical,
                       Source = issue.Source,
                       Solution = issue.Solution,
                       MeetingSolution = issue.MeetingSolution,
                       Status = issue.Status
                    };
                    await WorkScope.InsertAsync(pmReportProjectIssue);
                }
            }

            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_Update)]
        public async Task<PMReportDto> Update(PMReportDto input)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(input.Id);

            var isExist = await WorkScope.GetAll<PMReport>().AnyAsync(x => x.Id != input.Id && x.Name == input.Name && x.Type == input.Type && x.Year == input.Year);
            if (isExist)
                throw new UserFriendlyException("PM Report already exist !");

            if(input.IsActive != pmReport.IsActive)
            {
                throw new UserFriendlyException("Report status cannot be edited !");
            }

            await WorkScope.UpdateAsync(ObjectMapper.Map<PMReportDto, PMReport>(input, pmReport));
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_Delete)]
        public async Task Delete(long pmReportId)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(pmReportId);

            var hasPmReportProject = await WorkScope.GetAll<PMReportProject>().AnyAsync(x => x.PMReportId == pmReportId);
            if (hasPmReportProject)
                throw new UserFriendlyException("PM Report has PmReportProject !");

            await WorkScope.DeleteAsync(pmReport);
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_CloseReport)]
        public async Task<string> CloseReport(long pmReportId)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(pmReportId);

            var newPmReport = new PMReportDto
            {
                Name = pmReport.Name + " (1)",
                Year = DateTime.Now.Year,
                IsActive = true,
                Type = PMReportType.Weekly
            };
            await Create(newPmReport);

            return $"{pmReport.Name} locked, new PmReport with name {pmReport.Name} (1) created";
        }
    }
}
