using NccCore.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.ResourceService.Dto
{
    public class InputGetResourceDto : GridParam
    {
        public List<long> SkillIds { get; set; }
        public bool IsAndCondition { get; set; }
    }
}
