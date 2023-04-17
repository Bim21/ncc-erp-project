using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Configuration.Dto
{
    public class AppSettingDto
    {
        public string ClientAppId { get; set; }
        public string ProjectUri { get; set; }
        public string SecurityCode { get; set; }
        public string FinanceUri { get; set; }
        public string FinanceSecretCode { get; set; }
        public string TimesheetUri { get; set; }
        public string TimesheetSecretCode { get; set; }
        public string AutoUpdateProjectInfoToTimesheetTool { get; set; }
        public string HRMUri { get; set; }
        public string HRMSecretCode { get; set; }
        public string CanSendDay { get; set; }
        public string CanSendHour { get; set; }
        public string ExpiredDay { get; set; }
        public string ExpiredHour { get; set; }
        public string KomuUrl { get; set; }
        public string NoticeToKomu { get; set; }
        public string KomuSecretCode { get; set; }
        public string KomuUserNames { get; set; }
        public string UserBot { get; set; }
        public string PasswordBot { get; set; }
        public string KomuRoom { get; set; }
        public string DefaultWorkingHours { get; set; }
        public string TrainingRequestChannel { get; set; }
        public string TalentUriBA { get; set; }
        public string TalentUriFE { get; set; }
        public string TalentSecurityCode { get; set; }
        public string MaxCountHistory { get; set; }
    }

    public class ProjectSetting
    {
        public string SecurityCode { get; set; }
    }
    public class WeeklyReportSettingDto
    {
        public int TimeCountDown { get; set; } //s
    }
    public class AuditScoreDto
    {
        public int FINAL_SCORE { get; set; }
        public int PROJECT_SCORE_WHEN_STATUS_GREEN { get; set; }
        public int PROJECT_SCORE_WHEN_STATUS_AMBER { get; set; }
        public int PROJECT_SCORE_WHEN_STATUS_RED { get; set; }
        public int PROJECT_PROCESS_CRITERIA_RESULT_STATUS_NC { get; set; }
        public int PROJECT_PROCESS_CRITERIA_RESULT_STATUS_OBSERVATION { get; set; }
        public int PROJECT_PROCESS_CRITERIA_RESULT_STATUS_RE { get; set; }
        public int PROJECT_PROCESS_CRITERIA_RESULT_STATUS_EX { get; set; }
    }
}
