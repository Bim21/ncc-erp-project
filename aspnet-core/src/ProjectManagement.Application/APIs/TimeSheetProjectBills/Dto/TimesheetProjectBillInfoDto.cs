using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.TimeSheetProjectBills.Dto
{
    public class TimesheetProjectBillInfoDto
    {
        public string FullName { get; set; }
        public string BillRole { get; set; }
        public float BillRate { get; set; }
        public float WorkingTime { get; set; }
        public string Description { get; set; }
    }
}
