using Abp.AutoMapper;
using Abp.Domain.Entities;
using ProjectManagement.Entities;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.CheckListItems.Dto
{
    [AutoMapTo(typeof(CheckListItemMandatory))]
    public class CheckListItemMandatoryDto: Entity<long>
    {
        public long CheckListItemId { get; set; }
        public ProjectType ProjectType { get; set; }
    }
}
