using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Users.Dto
{
    public class UpdateUserSkillDto
    {
        public long UserId { get; set; }
        public List<long> UserSkills { get; set; }

    }
}
