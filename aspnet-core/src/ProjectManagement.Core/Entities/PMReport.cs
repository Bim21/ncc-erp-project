using Abp.Domain.Entities.Auditing;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Entities
{
    public class PMReport : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public PMReportStatus Status { get; set; }
        public PMReportType Type { get; set; }
    }
}
