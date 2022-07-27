using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.TimeSheetProjectBills.Dto
{
    public class TimesheetProjectBillInfoDto
    {
        public string UserFullName { get; set; }
        public string AccountName { get; set; }
        public string BillRole { get; set; }
        public float BillRate { get; set; }
        public float WorkingTime { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public ChargeType? ChargeType { get; set; }
        public string FullName => string.IsNullOrEmpty(AccountName) ? UserFullName : AccountName;
    }
}
