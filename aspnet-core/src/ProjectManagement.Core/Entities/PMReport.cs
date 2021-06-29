﻿using Abp.Domain.Entities.Auditing;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Entities
{
    public class PMReport : FullAuditedEntity<long>
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public PMReportStatus Status { get; set; }
        public PMReportType Type { get; set; }
    }
}
