using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NccCore.Anotations;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.Projects.Dto
{
    [AutoMapTo(typeof(Project))]
    public class ProjectDto : /*EntityDto<long>,*/  PagedResultRequestDto
    {
        public long Id { get; set; }
        [ApplySearchAttribute]
        public string Name { get; set; }
        public string Code { get; set; }
        public int? Number { get; set; }
        public bool IsActive { get; set; }
    }
}
