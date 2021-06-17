using Abp.Domain.Entities.Auditing;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class ProjectMilestone : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public long ProjectId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public FlagEnum Flag { get; set; }
        public ProjectMilestoneStatus Status { get; set; }
        public DateTime UATTimeStart { get; set; }
        public DateTime UATTimeEnd { get; set; }
        public string Note { get; set; }
    }
}
