using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class TimesheetProject : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public long ProjectId { get; set; }

        public string TimesheetFile { get; set; }

        [ForeignKey(nameof(TimesheetId))]
        public Timesheet Timesheet { get; set; }
        public long TimesheetId { get; set; }

        public string Note { get; set; }
    }
}
