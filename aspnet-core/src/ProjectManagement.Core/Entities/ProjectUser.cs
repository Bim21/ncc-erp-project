using Abp.Domain.Entities.Auditing;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class ProjectUser : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public long ProjectId { get; set; }

        public UserRole ProjectRole { get; set; }
        public float AllocatePercentage { get; set; }
        public DateTime StartTime { get; set; }
        public ProjectUserStatus Status { get; set; }
        public bool ExpenseCount { get; set; }

        [ForeignKey(nameof(ResourceRequestId))]
        public ResourceRequest ResourceRequest { get; set; }
        public long ResourceRequestId { get; set; }

        [ForeignKey(nameof(PMReportId))]
        public PMReport PMReport { get; set; }
        public long PMReportId { get; set; } 
    }
}
