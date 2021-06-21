using Abp.Domain.Entities.Auditing;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Entities
{
        public class CheckListItemMandatory : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(CheckListItemId))]
        public CheckListItem CheckListItem { get; set; }
        public long CheckListItemId { get; set; }

        public ProjectType ProjectType { get; set; }
    }
}
