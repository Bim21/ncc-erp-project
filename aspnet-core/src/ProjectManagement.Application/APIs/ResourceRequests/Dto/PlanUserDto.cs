﻿using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class PlanUserDto
    {
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public byte PercentUsage { get; set; }
        public ProjectUserRole ProjectRole { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsExpense { get; set; }
    }
}