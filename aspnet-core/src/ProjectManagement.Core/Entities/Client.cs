using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectManagement.Entities
{
    public class Client : FullAuditedEntity<long>
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
    }
}
