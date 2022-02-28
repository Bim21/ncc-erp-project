using NccCore.Anotations;
using ProjectManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class GetAllPoolResourceDto
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
        public List<ProjectUserPlan> ProjectUserPlans { get; set; }
        public List<UserSkillDto> UserSkills { get; set; }
        public int? StarRate { get; set; }
        public int TotalFreeDay { get; set; }

        public string PoolNote { get; set; }

        public DateTime DateStartPool { get; set; }
        public List<ProjectHistoryDto> ProjectHistorys { get; set; }
    }
}
