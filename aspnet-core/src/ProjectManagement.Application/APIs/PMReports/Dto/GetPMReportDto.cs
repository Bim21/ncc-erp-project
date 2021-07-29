using Abp.Application.Services.Dto;
using NccCore.Anotations;
using System.Collections.Generic;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReports.Dto
{
    public class GetPMReportDto : EntityDto<long>
    {
        [ApplySearchAttribute]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Year { get; set; }
        public PMReportType Type { get; set; }
        public int NumberOfProject { get; set; }
        public List<CountProjectHealth> CountProjectHeath { get; set; }
        public PMReportStatus PMReportStatus { get; set; }
        public string Note { get; set; }

    }

    public class CountProjectHealth
    {
        public int Number { get; set; }
        public ProjectHealth ProjectHealth { get; set; }
    }
}
