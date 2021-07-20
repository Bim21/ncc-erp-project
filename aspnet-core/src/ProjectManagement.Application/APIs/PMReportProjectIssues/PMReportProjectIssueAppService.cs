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
        public async Task<List<GetPMReportProjectIssueDto>> ProblemsOfTheWeek(long ProjectId, long pmReportId)
        {
            var query = from prp in WorkScope.GetAll<PMReportProject>().Where(x => x.ProjectId == ProjectId && x.PMReportId == pmReportId)
                        join prpi in WorkScope.GetAll<PMReportProjectIssue>().OrderByDescending(x => x.CreationTime)
                        on prp.Id equals prpi.PMReportProjectId into lst
                        from p in lst.DefaultIfEmpty()
                        select new GetPMReportProjectIssueDto
                        {
                            Id = p.Id,
                            PMReportProjectId = p.PMReportProjectId,
                            Description = p.Description,
                            Impact = p.Impact,
                            Critical = p.Critical.ToString(),
                            Source = p.Source.ToString(),
                            Solution = p.Solution,
                            MeetingSolution = p.MeetingSolution,
                            Flag = p.Flag.ToString(),
                            Status = p.Status.ToString()
                        };
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProjectIssue_Create)]
        public async Task<PMReportProjectIssueDto> Create(PMReportProjectIssueDto input, long projectId)
        {
            var pmReportProjectActive = await WorkScope.GetAll<PMReportProject>().Where(x => x.PMReport.IsActive && x.ProjectId == projectId).FirstOrDefaultAsync();
            if (pmReportProjectActive == null)
                throw new UserFriendlyException("Can't find any PMReportproject !");

            input.PMReportProjectId = pmReportProjectActive.Id;
            input.Flag = PMReportProjectIssueFlag.Green;
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
