using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReportProjects.Dto
{
    [AutoMapTo(typeof(PMReportProject))]
    public class PMReportProjectDto : EntityDto<long>
    {
        public long PMReportId { get; set; }
        public long ProjectId { get; set; }
        public PMReportProjectStatus Status { get; set; }
        public ProjectHealth ProjectHealth { get; set; }
        public long PMId { get; set; }
        public string Note { get; set; }
        public bool Seen { get; set; }
    }
}
