using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

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
                case UserLevel.Intern_600K:
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
    }
}
