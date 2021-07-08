using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.PMReportProjectIssues.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReportProjectIssues
{
    public class PMReportProjectIssueAppService : ProjectManagementAppServiceBase
    {
        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek)]
        public async Task<List<GetPMReportProjectIssueDto>> ProblemsOfTheWeek(long ProjectId)
        {
            var query = from pr in WorkScope.GetAll<PMReport>().Where(x => x.IsActive)
                        join prp in WorkScope.GetAll<PMReportProject>().Where(x => x.ProjectId == ProjectId)
                        on pr.Id equals prp.PMReportId into lstPrp
                        from p in lstPrp.DefaultIfEmpty()
                        join prpi in WorkScope.GetAll<PMReportProjectIssue>().OrderByDescending(x => x.CreationTime)
                        on p.Id equals prpi.PMReportProjectId
                        select new GetPMReportProjectIssueDto
                        {
                            PMReportProjectId = prpi.PMReportProjectId,
                            Description = prpi.Description,
                            Impact = prpi.Impact,
                            Critical = prpi.Critical.ToString(),
                            Source = prpi.Source.ToString(),
                            Solution = prpi.Solution,
                            MeetingSolution = prpi.MeetingSolution,
                            Status = prpi.Status.ToString()
                        };
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProjectIssue_Create)]
        public async Task<PMReportProjectIssueDto> Create(PMReportProjectIssueDto input)
        {
            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<PMReportProjectIssue>(input));
            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProjectIssue_Update)]
        public async Task<PMReportProjectIssueDto> Update(PMReportProjectIssueDto input)
        {
            var pmReportProjectIssue = await WorkScope.GetAsync<PMReportProjectIssue>(input.Id);

            await WorkScope.UpdateAsync(ObjectMapper.Map<PMReportProjectIssueDto, PMReportProjectIssue>(input, pmReportProjectIssue));
            return input;
        } 

        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProjectIssue_Delete)]
        public async Task Delete(long pmReportProjectIssueId)
        {
            await WorkScope.DeleteAsync<PMReportProjectIssue>(pmReportProjectIssueId);
        }
    }
}
