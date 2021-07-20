using Abp.Domain.Entities;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.AuditResultPeoples.Dto
{
    public class GetAuditResultPeopleDto : Entity<long>
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Note { get; set; }
        public long? CuratorId { get; set; }
        public string CuratorName { get; set; }
        public bool IsPass { get; set; }
        public long CheckListItemId { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string AvatarPath { get; set; }
        public UserType UserType { get; set; }
        public Branch Branch { get; set; }
    }
}
