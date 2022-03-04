using NccCore.Anotations;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.ResourceService.Dto
{
    public class GetAllResourceDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        [ApplySearchAttribute]
        public string EmailAddress { get; set; }
        [ApplySearchAttribute]
        public string FullName { get; set; }
        [ApplySearchAttribute]
        public string NormalFullName { get; set; }
        public string AvatarPath { get; set; }
        public UserType UserType { get; set; }
        public UserLevel UserLevel { get; set; }
        public Branch Branch { get; set; }
        public int Used { get; set; }
        public List<ProjectUserPlans> ProjectUserPlans { get; set; }
        public List<UserSkillDto> UserSkills { get; set; }
        public int? StarRate { get; set; }
        public List<WorkingProjectDto> WorkingProjects { get; set; }
    }
}
