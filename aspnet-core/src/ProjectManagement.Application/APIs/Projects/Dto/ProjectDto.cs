using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.Projects.Dto
{
    [AutoMapTo(typeof(Project))]
    public class ProjectDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long ClientId { get; set; }
        public bool IsCharge { get; set; }
        public long PmId { get; set; }
    }
}
