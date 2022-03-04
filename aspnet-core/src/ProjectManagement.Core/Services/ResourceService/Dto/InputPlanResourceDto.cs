using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.ResourceService.Dto
{
    public class InputPlanResourceDto
    {
        public long UserId { get; set; }
        public long ProjectId { get; set; }
        public DateTime StartTime { get; set; }
        public string Note { get; set; }
    }
}
