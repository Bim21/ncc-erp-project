using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.Timesheet.Dto
{
    public class TimesheetDetailDto
    {
        public DateTime DateAt { get; set; }
        public string DateAtView { get; set; }
        public float WorkingTime { get; set; }
        public string TaskName { get; set; }
        public string Note { get; set; }
        public string EmailAddress { get; set; }
        public string ProjectCode { get; set; }
    }

    public class TimesheetTaxDto
    {
        public List<DateTime> ListWorkingDay { get; set; }
        public List<TimesheetDetailDto> ListTimesheet { get; set; }
    }

    public class ProjectInvoice
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
    }
   
   
    public class ResultInvoice
    {
        public List<string> ListProjectCode { get; set; }
        public List<InvoiceDto> ListInvoice { get; set; }
    }
    public class InvoiceDto
    {
        public string ProjectCode { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string ProjectName { get; set; }
        public float WorkingTimeDay { get; set; }
        public float? TotalWorkingDay { get; set; }
        public int DefaultWorkingHours { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ChargeType ChargeType { get; set; }
        public float BillRate { get; set; }
        public double LineTotal
        {
            get
            {
                return WorkingTimeDay * BillRate;
            }
        }
        public List<TimesheetDetailDto> ListTimesheetDetail { get; set; }
    }
}
