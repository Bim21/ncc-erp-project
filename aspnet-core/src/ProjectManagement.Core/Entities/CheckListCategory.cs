using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Entities
{
    public class CheckListCategory : FullAuditedEntity<long>
    {
        public string Name { get; set; }
    }
}
