using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.APIs.AuditResultPeoples.Dto
{
    public class GetAuditResultPeopleDto : Entity<long>
    {
        public string UserName { get; set; }
        public string Note { get; set; }
        public string CuratorName { get; set; }
        public bool IsPass { get; set; }
        public long CheckListItemId { get; set; }

    }
}
