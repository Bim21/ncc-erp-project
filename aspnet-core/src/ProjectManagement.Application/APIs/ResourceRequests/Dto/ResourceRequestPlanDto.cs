using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class ResourceRequestPlanDto
    {
        public long ProjectUserId { get; set; }

        public long UserId { get; set; }

        public DateTime JoinDate { get; set; }

        public long ResourceRequestId { get; set; }
    }
}
