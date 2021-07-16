using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.Skills.Dto
{
    public class SkillDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
