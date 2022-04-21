using Abp.AutoMapper;
using ProjectManagement.Entities;

namespace ProjectManagement.APIs.ProjectUserBills.Dto
{
    [AutoMapTo(typeof(Project))]
    public class UpdateLastInvoiceNumberDto
    {
        public long ProjectId { get; set; }
        public byte? LastInvoiceNumber { get; set; }
    }
}
