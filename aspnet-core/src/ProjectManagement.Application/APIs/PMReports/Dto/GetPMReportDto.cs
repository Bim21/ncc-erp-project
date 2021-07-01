using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReports.Dto
{
    public class GetPMReportDto : EntityDto<long>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Year { get; set; }
        public PMReportType Type { get; set; }
        public int NumberOfProject { get; set; }
    }
}
