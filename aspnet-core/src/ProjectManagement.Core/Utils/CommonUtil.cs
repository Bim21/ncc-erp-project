using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;
using static ProjectManagement.Constants.Enum.ClientEnum;

namespace ProjectManagement.Utils
{
    public class CommonUtil
    {
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

        public static string PaymentDueByToString(byte paymentDueBy)
        {
            switch (paymentDueBy)
            {
                case 1:
                    return "1st next month";
                case 2:
                    return "2nd next month";
                case 3:
                    return "3rd next month";
                case 4:
                    return "4th next month";
                case 5:
                    return "5th next month";
                case 6:
                    return "6th next month";
                case 7:
                    return "7th next month";
                case 8:
                    return "8th next month";
                case 9:
                    return "9th next month";
                case 10:
                    return "10th next month";
                case 11:
                    return "11th next month";
                case 12:
                    return "12th next month";
                case 13:
                    return "13th next month";
                case 14:
                    return "14th next month";
                case 15:
                    return "15th next month";
                case 16:
                    return "16th next month";
                case 17:
                    return "17th next month";
                case 18:
                    return "18th next month";
                case 19:
                    return "19th next month";
                case 20:
                    return "20th next month";
                case 21:
                    return "21st next month";
                case 22:
                    return "22nd next month";
                case 23:
                    return "23rd next month";
                case 24:
                    return "24th next month";
                case 25:
                    return "25th next month";
                case 26:
                    return "26th next month";
                case 27:
                    return "27th next month";
                case 28:
                    return "28th next month";
                case 29:
                    return "29th next month";
                case 30:
                    return "30th next month";

                default: return "Last date next month";
            }
        }
        public static string InvoiceDateSettingToString(InvoiceDateSetting invoiceDateSetting)
        {
            if (invoiceDateSetting == InvoiceDateSetting.LastDateThisMonth)
            {
                return "Last date this month";
            }
            return "First date next month";
        }

    }
}
