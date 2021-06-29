using Abp.Application.Services.Dto;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReportProjects.Dto
{
    public class GetPMReportProjectDto : EntityDto<long>
    {
        public long PMReportId { get; set; }
        public string PMReportName { get; set; }
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public PMReportProjectStatus Status { get; set; }
        public ProjectHealthEnum ProjectHealth { get; set; }
        public long PMId { get; set; }
        public string PmName { get; set; }
        public string Note { get; set; }
    }
}
