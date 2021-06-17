﻿using Abp.Domain.Entities.Auditing;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
    public class PMReportProjectIssue : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(PMReportProjectId))]
        public PMReportProject PMReportProject { get; set; }
        public long PMReportProjectId { get; set; }

        public string Description { get; set; }
        public string Impact { get; set; }
        public CriticalEnum Critical { get; set; }
        public SourceEmum Source { get; set; }
        public string Solution { get; set; }
        public string MeetingSolution { get; set; }// consider
        public PMReportProjectIssueStatus Status { get; set; }

    }
}
