
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
        public enum CurrencyCode
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
        public enum PMReportProjectStatus : byte
        {
            Draft = 0,
            Sent = 1
        }
        public enum PunishStatus : byte
        {
            None = 0,
            Low = 1,
            High = 2
        }
        public enum PMReportStatus : byte
        {
            New = 0,
            CanSend = 1,
            Expired = 2,
        }
        public enum PMReportType : byte
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
            Upcoming = 0,
            UAT = 1,
            Paid = 2,
            Fail = 3
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
            CANCELLED = 2,
            APPROVE = 3
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
        public enum AuditResultStatus
        {
            New = 0,
            InProcess = 1,
            Done = 2
        }
        public enum PhaseType
        {
            Main = 0,
            Sub = 1
        }
        public enum PhaseStatus
        {
            Active=0,
            DeActive=1,
            Done=2
        }
        public enum CheckPointUserType
        {
            PM = 0,
            Team = 1,
            Client = 2,
            Self = 3,
            Exam = 4
        }
        public enum CheckPointUserStatus
        {
            Draft = 0,
            Reviewed = 1
        }
        public enum CheckPointUserResultStatus
        {
            Draft = 0,
            UserDone = 1,
            PMDone = 2,
            FinalDone =3,
        }
        public enum UserLevel : byte
        {
            Intern_0 = 0,
            Intern_600K = 1,
            Intern_2M = 2,
            Intern_4M = 3,
            FresherMinus = 4,
            Fresher = 5,
            FresherPlus = 6,
            JuniorMinus = 7,
            Junior = 8,
            JuniorPlus = 9,
            MiddleMinus = 10,
            Middle = 11,
            MiddlePlus = 12,
            SeniorMinus = 13,
            Senior = 14,
            SeniorPlus = 15,
        }
        public enum UserType
        {
            Internship = 0,
            Collaborators = 1,
            Staff = 2,
            ProbationaryStaff = 3,
            FakeUser = 4
        }

        public enum Branch
        {
            HaNoi = 0,
            DaNang = 1,
            HCM = 2,
            Vinh = 3,
            Other = 4
        }
        public enum InvoiceStatus
        {
            New = 0,
            Sent = 1,
            PartialPayment = 2,
            Paid = 3,
            CantPay = 4
        }
    }
}
