using Abp.Application.Services.Dto;
using NccCore.Anotations;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class GetResourceRequestDto : EntityDto<long>
    {
        [ApplySearchAttribute]
        public string Name { get; set; }
        public long ProjectId { get; set; }
        [ApplySearchAttribute]
        public string ProjectName { get; set; }
        public DateTime TimeNeed { get; set; }
        public ResourceRequestStatus Status { get; set; }
        public DateTime? TimeDone { get; set; }
        [ApplySearchAttribute]
        public string PMNote { get; set; }
        [ApplySearchAttribute]
        public string DMNote { get; set; }
        public List<long> SkillIds { get; set; }
        public List<GetResourceRequestDto_SkillInfo> Skills { get; set; }
        public bool IsRecruitmentSend { get; set; }
        public string RecruitmentUrl { get; set; }
        public UserLevel Level { get; set; }
        public Priority Priority { get; set; }
        public DateTime RequestStartTime { get; set; }
        public string StatusName
        {
            get
            {
                return Enum.GetName(typeof(ResourceRequestStatus), Status);
            }
        }

        public string PriorityName
        {
            get
            {
                return Enum.GetName(typeof(Priority), Priority);
            }
        }

        public string LevelName
        {
            get
            {
                return Enum.GetName(typeof(UserLevel), Level);
            }
        }
        public PlanUserInfoDto PlanUserInfo { get; set; }
        public DateTime CreationTime { get; set; }
    }

    public class GetResourceRequestDto_SkillInfo
    {
        public long SkillId { get; set; }

        public string SkillName { get; set; }
    }

    public class PlanUserInfoDto
    {
        public long? ProjectUserId { get; set; }
        public string PlannedEmployee { get; set; }
        public DateTime? PlannedDate { get; set; }
        public Branch? Branch { get; set; }
        public UserType? UserType { get; set; }
        public UserLevel? UserLevel { get; set; }
        public string LevelName
        {
            get
            {
                if(UserLevel != null)
                    return Enum.GetName(typeof(UserLevel), UserLevel);
                return null;
            }
        }
    }
}
