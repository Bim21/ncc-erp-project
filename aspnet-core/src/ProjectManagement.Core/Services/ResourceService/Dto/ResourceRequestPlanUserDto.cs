using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.ResourceService.Dto
{
    public class ResourceRequestPlanUserDto
    {
        public long UserId { get; set; }

        public string Name { get; set; }

        public string Fullname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
    }
}
