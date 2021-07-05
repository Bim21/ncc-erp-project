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
                        join prpi in WorkScope.GetAll<PMReportProjectIssue>() on p.Id equals prpi.PMReportProjectId
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
    }
}
