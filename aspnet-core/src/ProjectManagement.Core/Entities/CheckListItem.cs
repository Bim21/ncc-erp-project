using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class CheckListItem : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public CheckListCategory CheckListCategory { get; set; }
        public long CategoryId { get; set; }

        public string Description { get; set; }
        public string AuditTarget { get; set; }
        public string PersonInCharge { get; set; }
        public string Note { get; set; }

    }
}
