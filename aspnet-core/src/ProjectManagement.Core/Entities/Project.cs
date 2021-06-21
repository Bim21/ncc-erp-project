using Abp.Domain.Entities.Auditing;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class Project : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        public ProjectType ProjectType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ProjectStatus Status { get; set; }
        public long ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client Clients { get; set; }
        public bool IsCharge { get; set; }
        public long PmId { get; set; }
        [ForeignKey(nameof(PmId))]
        public User PM { get; set; }
       
    }
}
