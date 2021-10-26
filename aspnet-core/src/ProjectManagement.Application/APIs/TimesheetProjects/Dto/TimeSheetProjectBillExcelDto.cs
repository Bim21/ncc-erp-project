using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.TimesheetProjects.Dto
{
    public class TimeSheetProjectBillExcelDto
    {
        public string FullName { get; set; }
        public float WorkingDay { get; set; }
        public double BillRate { get; set; }
    }
}
