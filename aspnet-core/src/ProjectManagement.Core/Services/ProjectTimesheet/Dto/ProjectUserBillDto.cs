using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.ProjectTimesheet.Dto
{
    public class ProjectUserBillDto
    {
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public float BillRate { get; set; }
        public string BillRole { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}
