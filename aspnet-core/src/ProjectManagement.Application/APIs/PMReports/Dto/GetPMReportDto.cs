using Abp.Application.Services.Dto;
using NccCore.Anotations;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReports.Dto
{
    public class GetPMReportDto : EntityDto<long>
    {
        [ApplySearchAttribute]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [ApplySearchAttribute]
        public int Year { get; set; }
        public PMReportType Type { get; set; }
        public int NumberOfProject { get; set; }
        public PMReportStatus PMReportStatus { get; set; }

    }
}
