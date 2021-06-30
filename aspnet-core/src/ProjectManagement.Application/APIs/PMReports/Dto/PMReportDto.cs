using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReports.Dto
{
    [AutoMapTo(typeof(PMReport))]
    public class PMReportDto : EntityDto<long>
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public int Year { get; set; }
        public PMReportType Type { get; set; }
    }
}
