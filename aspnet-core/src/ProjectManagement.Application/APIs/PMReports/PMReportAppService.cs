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
            var query = from pr in WorkScope.GetAll<PMReport>()
                        join prp in WorkScope.GetAll<PMReportProject>() on pr.Id equals prp.PMReportId
                        group prp by new { pr.Id, pr.Name, pr.Year, pr.Type, pr.IsActive } into pp
                        select new GetPMReportDto
                        {
                            Id = pp.Key.Id,
                            Name = pp.Key.Name,
                            Year = pp.Key.Year,
                            IsActive = pp.Key.IsActive,
                            Type = pp.Key.Type,
                            NumberOfProject = pp.Count()
                        };
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

            var activeReport = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).ToListAsync();
            foreach(var item in activeReport)
            {
                item.IsActive = false;
                await WorkScope.UpdateAsync(item);
            }

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<PMReport>(input));

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
                await WorkScope.InsertAndGetIdAsync(pmReportProject);
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

            pmReport.IsActive = false;
            await WorkScope.UpdateAsync(pmReport);

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
