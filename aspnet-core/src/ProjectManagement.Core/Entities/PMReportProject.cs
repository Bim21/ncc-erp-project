using Abp.Domain.Entities.Auditing;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class PMReportProject : FullAuditedEntity<long>
    {
        public long PMReportId { get; set; }
        [ForeignKey(nameof(PMReportId))]
        public PMReport PMReport { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public PMReportProjectStatus Status { get; set; }
        public ProjectHealthEnum ProjectHealth { get; set; }
        public long PMId { get; set; }
        [ForeignKey(nameof(PMId))]
        public User PM { get; set; }
        public string Note { get; set; }
    }
}
