using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.Timesheet.Dto
{
    public class TimesheetDto
    {
        public string EmailAddress { get; set; }
        public string TaskName { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public int WorkingMinute { get; set; }
        public string Note { get; set; }
        public DateTime DateAt { get; set; }
    }

    public class TimesheetTaxDto
    {
        public List<DateTime> ListWorkingDay { get; set; }
        public List<TimesheetDto> ListTimesheet { get; set; }
    }

    public class ProjectInvoice
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public List<MemberOfProject> ListMemberOfProject { get; set; }
    }
    public class MemberOfProject
    {
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public float WorkingTimeDay { get; set; }
        public double BillRate { get; set; }
        public double LineTotal { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<TimesheetDto> ListTimesheetOfUserInProject { get; set; }
    }

    public class ResultProjectInvoice
    {
        public List<string> ListProjectCode { get; set; }
        public List<ProjectInvoice> ListProjectInvoice { get; set; }
    }
    public class TimesheetDetailOfUser
    {
        public string FullName { get; set; }
        public string ProjectName { get; set; }
        public float WorkingTimeDay { get; set; }
        public double BillRate { get; set; }
        public double LineTotal { get; set; }
        public List<TimesheetDetail> ListTimesheetDetail { get; set; }
    }

    public class TimesheetDetail
    {
        public string DateAt { get; set; }
        public float WorkingTime { get; set; }
        public string TaskName { get; set; }
        public string Note { get; set; }
    }
}
