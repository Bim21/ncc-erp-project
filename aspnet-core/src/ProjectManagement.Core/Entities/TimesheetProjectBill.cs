using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ProjectManagement.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Entities
{
    public class TimesheetProjectBill : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public long UserId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey(nameof(TimesheetId))]
        public Timesheet TimeSheet { get; set; }
        public long? TimesheetId { get; set; }
        public string BillRole { get; set; }
        public float BillRate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public CurrencyCode Currency { get; set; }
        public string Note { get; set; }
        public string ShadowNote { get; set; }
        public bool IsActive { get; set; }
        public float WorkingTime { get; set; }

    }
}
