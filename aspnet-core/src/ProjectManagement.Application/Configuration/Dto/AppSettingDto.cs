using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Configuration.Dto
{
    public class AppSettingDto
    {
        public string ClientAppId { get; set; }
        public string SecretCode { get; set; }
        public string FinanceUri { get; set; }
        public string FinanceSecretKey { get; set; }
        public string TimesheetUri { get; set; }
        public string TimesheetSecretKey { get; set; }
        public string CanSendDay { get; set; }
        public string CanSendHour { get; set; }
        public string ExpiredDay { get; set; }
        public string ExpiredHour { get; set; }
        public string KomuUrl { get; set; }
        public string KomuUserNames { get; set; }
        public string UserBot { get; set; }
        public string PasswordBot { get; set; }
        public string ProjectUri { get; set; }
        public string HRMUri { get; set; }
        public string KomuRoom { get; set; }
    }
}
