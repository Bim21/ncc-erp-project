using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class TimesheetProject : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public long ProjectId { get; set; }
        [MaxLength(1000)]
        public string FilePath { get; set; }
        [ForeignKey(nameof(TimesheetId))]
        public Timesheet Timesheet { get; set; }
        public long TimesheetId { get; set; }
        [MaxLength(10000)]
        public string Note { get; set; }
    }
}
