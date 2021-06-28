using Abp.Application.Services.Dto;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.Projects.Dto
{
    public class GetProjectDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ProjectType ProjectType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ProjectStatus Status { get; set; }
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public bool IsCharge { get; set; }
        public long PmId { get; set; }
        public string PmName { get; set; }
    }
}
