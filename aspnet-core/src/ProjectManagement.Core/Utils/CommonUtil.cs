using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;
using static ProjectManagement.Constants.Enum.ClientEnum;
using Branch = ProjectManagement.Constants.Enum.ProjectEnum.Branch;
using Abp.Timing;
using Microsoft.Extensions.Configuration;

namespace ProjectManagement.Utils
{
    public class CommonUtil
    {

        private static readonly Dictionary<byte, string> PaymentDueByListReadOnly
        = new Dictionary<byte, string>
        {
            { 0, "Last date next month" },
            { 15, "15th next month" },
            { 1, "1st next month" },
            { 2, "2nd next month" },
            { 3, "3rd next month" },
            { 4, "4th next month" },
            { 5, "5th next month" },
            { 6, "6th next month" },
            { 7, "7th next month" },
            { 8, "8th next month" },
            { 9, "9th next month" },
            { 10, "10th next month" },
            { 11, "11th next month" },
            { 12, "12th next month" },
            { 13, "13th next month" },
            { 14, "14th next month" },
            { 16, "16th next month" },
            { 17, "17th next month" },
            { 18, "18th next month" },
            { 19, "19th next month" },
            { 20, "20th next month" },
            { 21, "21st next month" },
            { 22, "22nd next month" },
            { 23, "23rd next month" },
            { 24, "24th next month" },
            { 25, "25th next month" },
            { 26, "26th next month" },
            { 27, "27th next month" },
            { 28, "28th next month" },
            { 29, "29th next month" },
            { 30, "30th next month" },
        };

        public static Dictionary<byte, string> PaymentDueByList()
        {
            return PaymentDueByListReadOnly;
        }

        private static readonly Dictionary<InvoiceDateSetting, string> InvoiceDateListReadOnly
        = new Dictionary<InvoiceDateSetting, string>
        {
            { InvoiceDateSetting.LastDateThisMonth, "Last date this month" },
            { InvoiceDateSetting.FirstDateNextMonth, "First date next month" }
        };

        public static Dictionary<InvoiceDateSetting, string> InvoiceDateList()
        {
            return InvoiceDateListReadOnly;
        }

        public static string ToString(ProjectUserRole projectUserRole)
        {
            return Enum.GetName(typeof(ProjectUserRole), projectUserRole);
        }

        public static string ProjectUserWorkType(bool IsPool)
        {
            return IsPool ? "[Temp]" : "[Offical]";
        }

        public static string ProjectUserWorkTypeKomu(bool IsPool)
        {
            return IsPool ? "[Temp]" : "[**Offical**]";
        }

        public static string PUStatusToPlanConfirmKomu(ProjectUserStatus PUStatus)
        {
            if (PUStatus == ProjectUserStatus.Future)
            {
                return "**PLAN**";
            }
            if (PUStatus == ProjectUserStatus.Present)
            {
                return "**CONFIRM**";
            }
            return PUStatus.ToString();

        }

        public static string JoinOrOutProject(byte allocatePercentage)
        {
            return allocatePercentage > 0 ? "**JOIN**" : "**OUT**";
        }

        //public static string GetWorkingStatusMessage(ProjectUser pu)
        //{
        //    if (pu != null)
        //    {

        //    }
        //}

        public static string UserLevelName(UserLevel level)
        {
            switch (level)
            {
                case UserLevel.AnyLevel:
                    return "Any Level";
                case UserLevel.Fresher:
                    return "Fresher";
                case UserLevel.FresherMinus:
                    return "Fresher-";
                case UserLevel.FresherPlus:
                    return "Fresher+";
                case UserLevel.JuniorMinus:
                    return "Junior-";
                case UserLevel.JuniorPlus:
                    return "Junior+";
                case UserLevel.MiddleMinus:
                    return "Middle-";
                case UserLevel.MiddlePlus:
                    return "Middle+";
                case UserLevel.SeniorMinus:
                    return "Senior-";
                case UserLevel.Principal:
                    return "Principal";
                case UserLevel.Intern_0:
                    return "Intern_0";
                case UserLevel.Intern_1:
                    return "Intern_1";
                default:
                    return Enum.GetName(typeof(UserLevel), level);
            }

        }

        public static string BranchName(Branch? branch)
        {
            if (!branch.HasValue)
            {
                return "NoBranch";
            }
            switch (branch)
            {
                case Branch.DaNang:
                    return "ĐN";
                case Branch.HaNoi:
                    return "HN";
                case Branch.HCM:
                    return "HCM";
                case Branch.Vinh:
                    return "Vinh";
            }
            return Enum.GetName(typeof(Branch), branch);
        }

        public static string UserTypeName(UserType? type)
        {
            if (!type.HasValue)
            {
                return "NoType";
            }
            switch (type)
            {
                case UserType.Staff:
                    return "Staff";
                case UserType.Internship:
                    return "TTS";
                case UserType.Collaborators:
                    return "CTV";
                case UserType.ProbationaryStaff:
                    return "T.Việc";
            }
            return Enum.GetName(typeof(UserType), type);
        }

        public static string JobPositionName(Job? job)
        {
            if (!job.HasValue)
            {
                return "";
            }           
            return Enum.GetName(typeof(Job), job.Value);
        }


        public static string ProjectTypeName(ProjectType projectType)
        {
            switch (projectType)
            {
                case ProjectType.TimeAndMaterials:
                    return "T&M";
            }
            return Enum.GetName(typeof(ProjectType), projectType);

        }

        public static string ProjectStatusName(ProjectStatus status)
        {
            if (status == ProjectStatus.InProgress)
            {
                return "";
            }
            return Enum.GetName(typeof(ProjectStatus), status);

        }
        public static DateTime GetNow()
        {
            return Clock.Provider.Now;
        }

        public static long NowToMilliseconds()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public static string NowToYYYYMMddHHmmss()
        {
            return GetNow().ToString("yyyyMMddHHmmss");
        }
        
        public static string GetPathSendRecuitment(string path)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetValue<string>("TalentService:FEAddress");
            return config + path;
        }
    }
}
