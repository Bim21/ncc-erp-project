using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectManagement.Entities
{
    public class CheckListCategory : FullAuditedEntity<long>
    {
        [MaxLength(1000)]
        public string Name { get; set; }
    }
}
