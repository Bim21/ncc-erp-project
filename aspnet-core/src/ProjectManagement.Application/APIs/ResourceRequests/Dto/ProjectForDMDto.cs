using ProjectManagement.APIs.PMReportProjectIssues.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class ProjectForDMDto
    {
        public string ProjectName { get; set; }
        public string PMName { get; set; }
        public List<UserBaseDto> ListUsers { get; set; }
        public List<GetPMReportProjectIssueDto> ProblemsOfTheWeek { get; set; }
    }
    public class UserBaseDto
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string AvatarPath { get; set; }
        public UserType UserType { get; set; }
        public Branch Branch { get; set; }
    }

}
