﻿using Abp.Authorization.Users;
using Abp.Domain.Entities.Auditing;
using ProjectManagement.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class AuditResultPeople : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(AuditResultId))]
        public AuditResult AuditResult { get; set; }
        public long AuditResultId { get; set; }

        [ForeignKey(nameof(CheckListItemId))]
        public CheckListItem CheckListItem { get; set; }
        public long CheckListItemId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public long UserId { get; set; }
        [MaxLength(10000)]
        public string Note { get; set; }

        public byte Quantity { get; set; }
        public UserRole Role { get; set; }

        [ForeignKey(nameof(PMId))]
        public User PM { get; set; }
        public long PMId { get; set; }
    }
}