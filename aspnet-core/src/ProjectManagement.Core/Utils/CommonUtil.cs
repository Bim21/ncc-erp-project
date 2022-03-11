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
    }
}
