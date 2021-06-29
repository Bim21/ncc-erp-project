using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Constants.Enum
{
    public class ProjectEnum
    {
        public enum CriticalEnum
        {
            Low = 0,
            Medium = 1,
            High = 2
        }
        public enum CurrencyEnum
        {
            VND = 0,
            USD = 1,
            EUR = 2,
        }
        public enum FlagEnum
        {
            Red = 0,
            Green = 1
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
        public enum ProjectHealthEnum
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
            Maintain = 2,
            Closed = 3
        }
        public enum ProjectType
        {
            ODC = 0,
            TandM = 1,
            FIXPRICE = 2,
            PRODUCT = 3
        }
        public enum ProjectUserStatus
        {
            Present = 0,
            Past = 1,
            Future = 2
        }
        public enum ResourceRequestStatus
        {
            DONE = 0,
            PENDING = 1,
            CANCELLED = 2
        }
        public enum SourceEmum
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
        public enum UserRole
        {
            PM = 0,
            DEV = 1,
            TESTER = 2,
            DM = 3
        }
    }
}
