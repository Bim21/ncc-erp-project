using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class GetResourceRequestDto : EntityDto<long>
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public DateTime TimeNeed { get; set; }
        public string Status { get; set; }
        public DateTime TimeDone { get; set; }
        public string Note { get; set; }
    }
}
