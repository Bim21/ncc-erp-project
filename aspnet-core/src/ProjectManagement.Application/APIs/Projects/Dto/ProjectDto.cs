using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NccCore.Anotations;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.StatusEnum;

namespace ProjectManagement.APIs.Projects.Dto
{
    [AutoMapTo(typeof(Project))]
    public class ProjectDto : EntityDto<long>
    {
        [ApplySearchAttribute]
        public string name { get; set; }
        public ProjectType projectType { get; set; }
        public ProjectStatus projectStatus { get; set; }
        public string clientName { get; set; }
        public Boolean stillCharge { set; get; }
        public DateTime startTime { set; get; }
        public DateTime? endTime { set; get; }
    }
}
