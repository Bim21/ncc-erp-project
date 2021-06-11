using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static ProjectManagement.Constants.StatusEnum;

namespace ProjectManagement.Entities
{
    public class Project: FullAuditedEntity<long>,IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public DateTime? EndTime { set; get; }
        public DateTime StartTime { set; get; }
        public Boolean StillCharge { set; get; }
        public ProjectType Type { set; get; }
        public ProjectStatus Status { set; get; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
        public long? ClientId { get; set; }

    }
}
