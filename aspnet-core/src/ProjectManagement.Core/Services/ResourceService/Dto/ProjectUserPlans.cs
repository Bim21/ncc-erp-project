using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.ResourceService.Dto
{
    public class ProjectUserPlans
    {
        public long CreatorUserId { get; set; }
        public long ProjectUserId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartTime { get; set; }
        public int AllocatePercentage { get; set; }
    }
}
