using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Constants.Enum
{
    public class ProjectEnum
    {
        public enum IssueCritical
        {
            Low = 0,
            Medium = 1,
            High = 2
        }
        public enum Currency
        {
            VND = 0,
            USD = 1,
            EUR = 2,
        }
        public enum MilestoneFlag
        {
            Green = 0,
            Red = 1
            
        }
        public enum PMReportProjectIssueStatus
        {
            InProgress = 0,
            Done = 1
        }
        public enum PMReportProjectStatus
        {
            Draft = 0,
            Sent = 1
        }
        public enum PMReportStatus
        {
            Active = 0,
            Done = 1
        }
        public enum PMReportType
        {
            Weekly = 0,
            Monthly = 1
        }
        public enum ProjectHealth
        {
            Green = 0,
            Yellow = 1,
            Red = 2
        }
        public enum ProjectMilestoneStatus
        {
            Paid = 0,
            UAT = 1
        }
        public enum ProjectStatus
        {
            Potential = 0,
            InProgress = 1,
            Closed = 2
        }
        public enum ProjectType
        {
            ODC = 0,
            TimeAndMaterials = 1,
            FIXPRICE = 2,
            PRODUCT = 3,
            NoBill = 4
        }
        public enum ProjectUserStatus
        {
            Present = 0,
            Past = 1,
            Future = 2
        }
        public enum ResourceRequestStatus
        {
            PENDING = 0,
            DONE = 1,            
            CANCELLED = 2
        }
        public enum ProjectIssueSource
        {
            Internal = 0,
            External = 1,
            Others = 2
        }
        public enum TimesheetStatus
        {
            Active = 0,
            Done = 1
        }
        public enum ProjectUserRole
        {
            PM = 0,
            DEV = 1,
            TESTER = 2,
            BA = 3,
            Artist = 4,
        }
    }
}
