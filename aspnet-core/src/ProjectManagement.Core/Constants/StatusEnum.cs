using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Constants
{
   public class StatusEnum
    {
        public enum ProjectStatus
        {
            
            Potential=1,
            InProgress=2,
            Maintain=3,
            Closed=4,
        }
        public enum ProjectType
        {
            TimeAndMaterials = 0,
            FixedFee = 1,
            NoneBillable = 2,
            ODC = 3
        }

        public enum StillCharge:byte
        {
            True = 1,
            False = 0
        }
        public enum JoinType
        {
            PlanJoin=1,
            RealJoin=2
        }
        public enum TypeUserProject
        {
                Shadow=1,
                Bill=2,
                All=3
        }
    }
}
