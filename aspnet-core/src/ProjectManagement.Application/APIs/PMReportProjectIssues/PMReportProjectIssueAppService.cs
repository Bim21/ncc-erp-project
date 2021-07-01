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
        [HttpPost]
        public async Task<GridResult<GetPMReportProjectIssueDto>> ProblemsOfTheWeek(GridParam input, long ProjectId)
        {
            var query = from pr in WorkScope.GetAll<PMReport>().Where(x => x.IsActive == true)
                        join prp in WorkScope.GetAll<PMReportProject>() on pr.Id equals prp.PMReportId into lstPrp
                        from p in lstPrp.DefaultIfEmpty()
                        join prpi in WorkScope.GetAll<PMReportProjectIssue>() on p.Id equals prpi.PMReportProjectId into lstIssue
                        from rs in lstIssue.DefaultIfEmpty()
                        select new GetPMReportProjectIssueDto
                        {
                            PMReportProjectId = rs.PMReportProjectId,
                            Description = rs.Description,
                            Impact = rs.Impact,
                            Critical = rs.Critical.ToString(),
                            Source = rs.Source.ToString(),
                            Solution = rs.Solution,
                            MeetingSolution = rs.MeetingSolution,
                            Status = rs.Status.ToString()
                        };
            return await query.GetGridResult(query, input);
        }
    }
}
