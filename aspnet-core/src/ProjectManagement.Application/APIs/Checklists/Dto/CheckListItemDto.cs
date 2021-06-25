using Abp.AutoMapper;
using Abp.Domain.Entities;
using NccCore.Anotations;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.APIs.Checklists.Dto
{
    [AutoMapTo(typeof(CheckListItem))]
    public class CheckListItemDto : Entity<long>
    {
        [ApplySearchAttribute]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Code { get; set; }
        public long CategoryId { get; set; }
        [ApplySearchAttribute]
        public string Title { get; set; }   // get from category
        [MaxLength(10000)]
        [ApplySearchAttribute]
        public string Description { get; set; }
        public List<ProjectType> Mandatorys { get; set; }
        [MaxLength(255)]
        public string AuditTarget { get; set; }
        [MaxLength(255)]
        [ApplySearchAttribute]
        public string PersonInCharge { get; set; }
        public string Note { get; set; }

    }
}
