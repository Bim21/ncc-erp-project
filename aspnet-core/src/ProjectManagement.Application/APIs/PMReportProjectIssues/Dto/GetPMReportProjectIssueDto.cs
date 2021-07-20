using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReportProjectIssues.Dto
{
    public class GetResultpmReportProjectIssue
    {
        public List<GetPMReportProjectIssueDto> ListGreen { get; set; }
        public List<GetPMReportProjectIssueDto> ListYellow { get; set; }
        public List<GetPMReportProjectIssueDto> ListRed { get; set; }
    }

    public class GetPMReportProjectIssueDto : EntityDto<long>
    {
        public long PMReportProjectId { get; set; }
        public string Description { get; set; }
        public string Impact { get; set; }
        public string Critical { get; set; }
        public string Source { get; set; }
        public string Solution { get; set; }
        public string MeetingSolution { get; set; }
        public ProjectHealth ProjectHealth { get; set; }
        public string Status { get; set; }
    }
}
