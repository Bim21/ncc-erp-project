using Abp.Application.Services.Dto;
using NccCore.Anotations;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.Projects.Dto
{
    public class GetProjectDto : EntityDto<long>
    {
        [ApplySearchAttribute]
        public string Name { get; set; }
        [ApplySearchAttribute]
        public string Code { get; set; }
        public string ProjectType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ProjectStatus Status { get; set; }
        //public string Status { get; set; }
        public long ClientId { get; set; }
        [ApplySearchAttribute]
        public string ClientName { get; set; }
        public bool IsCharge { get; set; }
        public long PmId { get; set; }
        public string PmName { get; set; }
        public string PmFullName { get; set; }
        public string PmEmailAddress { get; set; }
        public string PmUserName { get; set; }
        public string PmAvatarPath { get; set; }
        public UserType PmUserType { get; set; }
        public Branch PmBranch { get; set; }
        public PMReportProjectStatus IsSent { get; set; }
        public DateTime? TimeSendReport { get; set; }
    }
}
