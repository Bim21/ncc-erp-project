using Abp.Application.Services.Dto;
using NccCore.Anotations;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class GetResourceRequestDto : EntityDto<long>
    {
        [ApplySearchAttribute]
        public string Name { get; set; }
        public long ProjectId { get; set; }
        [ApplySearchAttribute]
        public string ProjectName { get; set; }
        public DateTime TimeNeed { get; set; }
        public ResourceRequestStatus Status { get; set; }
        public string StatusName { get; set; }
        public DateTime? TimeDone { get; set; }
        public string Note { get; set; }
        public int PlannedNumberOfPersonnel { get; set; }
    }
}
