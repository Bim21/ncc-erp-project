using ProjectManagement.APIs.PMReportProjectIssues.Dto;
using ProjectManagement.APIs.ProjectUsers.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.PMReports.Dto
{
    public class ReportStatisticsDto
    {
        public string Note { get; set; }
        public List<GetPMReportProjectIssueDto> Issues { get; set; }
        public List<ProjectUserStatistic> ResourceInTheWeek { get; set; }
        public List<ProjectUserStatistic> ResourceInTheFuture { get; set; }
    }

    public class ProjectUserStatistic
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string UserType { get; set; }
        public string Branch { get; set; }
        public string Email { get; set; }
        public int AllocatePercentage { get; set; }
    }
}
