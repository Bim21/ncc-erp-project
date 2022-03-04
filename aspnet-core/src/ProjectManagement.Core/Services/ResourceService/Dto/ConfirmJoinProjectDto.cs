using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.ResourceService.Dto
{
    public class ConfirmJoinProjectDto
    {
        public long projectUserId { get; set; }
        public DateTime startTime { get; set; }
        public ProjectUserRole ProjectRole { get; set; }
        public long UserId { get; set; }
    }
}
