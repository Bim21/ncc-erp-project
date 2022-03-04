using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.ResourceManager.Dto
{    
    public class UserOfProjectDto : EntityDto<long>
    {
        public long UserId { get; set; }
        public long ProjectId { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string AvatarPath { get; set; }
        public UserType UserType { get; set; }
        public UserLevel UserLevel { get; set; }
        public Branch Branch { get; set; }
        public bool IsAvtive { get; set; }
        public byte AllocatePercentage { get; set; }
        public DateTime StartTime { get; set; }
        public ProjectUserStatus Status { get; set; }
        public ProjectUserRole ProjectRole { get; set; }

        public bool IsPool { get; set; }

        public string WorkType
        {
            get
            {
                return CommonUtil.ProjectUserWorkType(this.IsPool);
            }
        }

        public string ProjectRoleName
        {
            get
            {
                return CommonUtil.ToString(this.ProjectRole);
            }
        }

    }
}
