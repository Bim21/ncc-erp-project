using ProjectManagement.APIs.PMReportProjectIssues.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class ProjectForDMDto
    {
        public string ProjectName { get; set; }
        public string PMName { get; set; }
        public List<string> ListUsers { get; set; }
        public List<GetPMReportProjectIssueDto> ProblemsOfTheWeek { get; set; }
    }
}
