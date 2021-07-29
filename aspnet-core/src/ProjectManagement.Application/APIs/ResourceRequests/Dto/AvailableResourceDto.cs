using NccCore.Anotations;
using System;
using System.Collections.Generic;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class AvailableResourceDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        [ApplySearchAttribute]
        public string FullName { get; set; }
        public string AvatarPath { get; set; }
        public UserType UserType { get; set; }
        public Branch Branch { get; set; }
        public List<string> Projects { get; set; }
        public int Used { get; set; }
        public List<ProjectUserPlan> ProjectUserPlans { get; set; }
    }

    public class ProjectUserPlan
    {
        public string ProjectName { get; set; }
        public DateTime StartTime { get; set; }
        public int AllocatePercentage { get; set; }
    }
}
