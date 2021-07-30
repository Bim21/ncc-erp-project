using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class PlanUserDto : EntityDto<long>
    {
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public byte PercentUsage { get; set; }
        public ProjectUserRole ProjectRole { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsExpense { get; set; }
        public string Note { get; set; }
    }
}
