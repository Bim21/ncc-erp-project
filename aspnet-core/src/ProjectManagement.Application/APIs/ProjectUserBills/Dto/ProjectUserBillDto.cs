using Abp.Application.Services.Dto;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ProjectUserBills.Dto
{
    public class ProjectUserBillDto : EntityDto<long>
    {
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public ProjectUserRole BillRole { get; set; }
        public float BillRate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Currency Currency { get; set; }
    }
}
