using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class ResourceRequestPlanDto
    {
        public long? Id { get; set; }

        public long? UserId { get; set; }

        public DateTime? JoinDate { get; set; }

        public long ResourceRequestId { get; set; }

        public bool IsFutureActive { get; set; }
    }
}
