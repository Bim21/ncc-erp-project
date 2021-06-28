using Abp.Application.Services.Dto;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ProjectUserBills.Dto
{
    public class GetProjectUserBillDto  : EntityDto<long>
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public UserRole BillRole { get; set; }
        public float BillRate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public CurrencyEnum Currency { get; set; }
    }
}
