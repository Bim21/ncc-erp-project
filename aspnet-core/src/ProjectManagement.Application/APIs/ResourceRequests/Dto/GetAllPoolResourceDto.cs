using NccCore.Anotations;
using NccCore.Uitls;
using ProjectManagement.Entities;
using ProjectManagement.Services.ResourceManager.Dto;
using ProjectManagement.Services.ResourceService.Dto;
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
        [ApplySearchAttribute]
        public string EmailAddress { get; set; }
        [ApplySearchAttribute]
        public string FullName { get; set; }        
        public string AvatarPath { get; set; }
        public UserType UserType { get; set; }
        public UserLevel UserLevel { get; set; }
        public Branch Branch { get; set; }
        public List<ProjectOfUserDto> PlannedProjects { get; set; }
        public List<ProjectOfUserDto> PoolProjects { get; set; }
        public List<UserSkillDto> UserSkills { get; set; }
        public int? StarRate { get; set; }
        
        public string PoolNote { get; set; }
        public bool IsOffical { get; set; }
        public DateTime PUPoolStartTime { get; set; }
        public DateTime UserCreationTime { get; set; }

        public int TotalFreeDay
        {
            get
            {
                if (this.PUPoolStartTime > this.UserCreationTime)
                {
                    return (DateTimeUtils.GetNow().Date - this.PUPoolStartTime.Date).Days;
                }
                return (DateTimeUtils.GetNow().Date - this.UserCreationTime.Date).Days;

            }
        }

        public DateTime DateStartPool
        {
            get
            {
                return this.PUPoolStartTime > this.UserCreationTime ? this.PUPoolStartTime : this.UserCreationTime;
            }
        }
    }
}
