using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.ResourceService.Dto
{
    public class EditProjectUserDto
    {
        public long ProjectId { get; set; }
        public DateTime StartTime { get; set; }
        public byte AllocatePercentage { get; set; }
        public string Note { get; set; }
    }
}
