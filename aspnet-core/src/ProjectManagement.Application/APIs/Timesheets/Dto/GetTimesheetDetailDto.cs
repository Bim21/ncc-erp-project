using Abp.Application.Services.Dto;
using NccCore.Anotations;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using ProjectManagement.APIs.TimeSheetProjectBills.Dto;
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
        [ApplySearchAttribute]
        public string ProjectName { get; set; }
        public long PmId { get; set; }
        [ApplySearchAttribute]
        public string PmEmailAddress { get; set; }
        public string PmUserName { get; set; }
        [ApplySearchAttribute]
        public string PmFullName { get; set; }
        public string PmAvatarPath { get; set; }
        public UserType PmUserType { get; set; }
        public Branch PmBranch { get; set; }
        public long? ClientId { get; set; }
        [ApplySearchAttribute]
        public string ClientName { get; set; }
        public string File { get; set; }
        public string HistoryFile { get; set; }
        public List<TimesheetProjectBillInfoDto> ProjectBillInfomation { get; set; }
        public string Note { get; set; }
        public PMReportProjectStatus IsSendReport { get; set; }
        public bool HasFile { get; set; }
        public bool? IsComplete { get; set; }
        public bool RequireTimesheetFile { get; set; }
        public string Currency { get; set; }
        public ChargeType? ChargeType { get; set; }
    }

}
