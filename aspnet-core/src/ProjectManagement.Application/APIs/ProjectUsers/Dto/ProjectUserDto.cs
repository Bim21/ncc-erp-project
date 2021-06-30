using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ProjectUsers.Dto
{
    [AutoMapTo(typeof(ProjectUser))]
    public class ProjectUserDto : EntityDto<long>
    {
        public long UserId { get; set; }
        public long ProjectId { get; set; }
        public ProjectUserRole ProjectRole { get; set; }
        public float AllocatePercentage { get; set; }
        public DateTime StartTime { get; set; }
        public ProjectUserStatus Status { get; set; }
        public bool ExpenseCount { get; set; }
        public long ResourceRequestId { get; set; }
        public long PMReportId { get; set; }
        public bool IsActive { get; set; }
    }
}
