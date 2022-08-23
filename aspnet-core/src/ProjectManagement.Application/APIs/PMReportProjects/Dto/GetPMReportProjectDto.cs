﻿using Abp.Application.Services.Dto;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReportProjects.Dto
{
    public class GetPMReportProjectDto : EntityDto<long>
    {
        public long PMReportId { get; set; }
        public string PMReportName { get; set; }
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public string Status { get; set; }
        public ProjectHealth ProjectHealthEnum { get; set; }
        public string ProjectHealth => ProjectHealthEnum.ToString();
        public long PMId { get; set; }
        public string PmName { get; set; }
        public string Note { get; set; }
        public string AutomationNote { get; set; }
        public string PmEmailAddress { get; set; }
        public string PmUserName { get; set; }
        public string PmFullName { get; set; }
        public string PmAvatarPath { get; set; }
        public string PmAvatarFullPath => FileUtils.FullFilePath(PmAvatarPath);
        public UserType PmUserType { get; set; }
        public Branch PmBranch { get; set; }
        public bool Seen { get; set; }
        public int TotalNormalWorkingTime { get; set; }
        public int TotalOverTime { get; set; }
    }
}
