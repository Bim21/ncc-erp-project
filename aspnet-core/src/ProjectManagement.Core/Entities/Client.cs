using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Entities
{
    public  class Client : FullAuditedEntity<long>
    {
        public string Name { set; get; }
        public string Address { set; get; }
        public string Country { set; get; }
    }
}
