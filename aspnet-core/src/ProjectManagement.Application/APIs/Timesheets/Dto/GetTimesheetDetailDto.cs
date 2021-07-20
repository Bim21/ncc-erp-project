using Abp.Application.Services.Dto;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.Timesheets.Dto
{
    public class GetTimesheetDetailDto : EntityDto<long>
    {
        public long ProjectId { get; set; }
        public long TimesheetId { get; set; }
        public string ProjectName { get; set; }
        public long PmId { get; set; }
        public string PmName { get; set; }
        public string PmEmailAddress { get; set; }
        public string PmUserName { get; set; }
        public string PmFullName { get; set; }
        public string PmAvatarPath { get; set; }
        public UserType PmUserType { get; set; }
        public Branch PmBranch { get; set; }
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public string File { get; set; }
        public string ProjectBillInfomation { get; set; }
        public string Note { get; set; }
    }
}
