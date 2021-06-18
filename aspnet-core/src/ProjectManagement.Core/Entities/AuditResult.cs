using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class AuditResult : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(AuditSessionId))]
        public AuditSession AuditSession { get; set; }
        public long AuditSessionId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public long ProjectId { get; set; }
        [MaxLength(10000)]
        public string Note { get; set; }
    }
}
