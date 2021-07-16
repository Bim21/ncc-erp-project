using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Users.Dto
{
    public class UserSkillDto
    {
        public long UserId { get; set; }
        public long SkillId { get; set; }
    }
}
