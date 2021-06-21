using Abp.Domain.Entities.Auditing;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectManagement.Entities
{
    public class Timesheet : FullAuditedEntity<long>
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
