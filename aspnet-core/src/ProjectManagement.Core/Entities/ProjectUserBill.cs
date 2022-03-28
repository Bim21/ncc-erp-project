﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Entities
{
    public class ProjectUserBill : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public long UserId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Projectuser Project { get; set; }
        public long ProjectId { get; set; }
        public string BillRole { get; set; }
        public float BillRate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public CurrencyCode Currency { get; set; }
        public string Note { get; set; }
        public string shadowNote { get; set; }
        public bool isActive { get; set; }
    }
}
