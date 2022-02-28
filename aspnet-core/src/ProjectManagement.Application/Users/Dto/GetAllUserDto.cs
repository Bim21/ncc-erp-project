using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using NccCore.Anotations;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Entities;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Users.Dto
{
    
    public class GetAllUserDto : EntityDto<long>
    {      
     
        [ApplySearchAttribute]
        public string EmailAddress { get; set; }
        public string AvatarPath { get; set; }
        public UserType UserType { get; set; }
        public UserLevel UserLevel { get; set; }
        public Branch Branch { get; set; }
        public List<UserSkillDto> UserSkills { get; set; }
        public bool IsActive { get; set; }

        [ApplySearchAttribute]
        public string FullName { get; set; }
        
        public DateTime CreationTime { get; set; }

        public string[] RoleNames { get; set; }

        public List<WorkingProjectDto> WorkingProjects { get; set; }
        
    }
}
