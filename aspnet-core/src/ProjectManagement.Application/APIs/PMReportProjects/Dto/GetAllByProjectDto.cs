
using Abp.Domain.Entities;

namespace ProjectManagement.APIs.PMReportProjects.Dto
{
    public class GetAllByProjectDto : Entity<long>
    {
        public long ReportId { get; set; }
        public string PMReportName { get; set; }
        public string Note { get; set; }
    }
}
