using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class AvailableResourceFutureDto : EntityDto<long>
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long Projectid { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public byte Use { get; set; }
    }
}
