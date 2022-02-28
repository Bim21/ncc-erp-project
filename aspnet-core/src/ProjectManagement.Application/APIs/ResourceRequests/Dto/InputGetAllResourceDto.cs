using NccCore.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class InputGetAllResourceDto : GridParam
    {
        public List<long> SkillIds { get; set; }
        public List<string> ProjectName { get; set; }
        public List<string> PlanProjectName { get; set; }

    }
}
