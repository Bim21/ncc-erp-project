using Abp.Domain.Entities.Auditing;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class ResourceRequest : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public long ProjectId { get; set; }
        public DateTime TimeNeed { get; set; }
        public ResourceRequestStatus Status { get; set; }
        public DateTime TimeDone { get; set; }
        public string Note { get; set; }
    }
}
