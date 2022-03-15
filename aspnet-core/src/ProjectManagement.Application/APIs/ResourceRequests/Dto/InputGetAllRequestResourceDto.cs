using NccCore.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class InputGetAllRequestResourceDto : GridParam
    {
        public List<long> SkillIds { get; set; }
    }
}
