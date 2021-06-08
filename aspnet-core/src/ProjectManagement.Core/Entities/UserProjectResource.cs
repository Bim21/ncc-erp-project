using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ProjectManagement.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static ProjectManagement.Constants.StatusEnum;

namespace ProjectManagement.Entities
{
   public class UserProjectResource:FullAuditedEntity<long>
    {
        
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public long ProjectId { get; set; }

        public DateTime JoinTime { set; get; }
        public bool Active { set; get; }
        public int Allocate { set; get; }
        public int  WillUse { set; get; }
        public DateTime? WillUseTime { set; get; }
        public JoinType JoinType { set; get; }
        public TypeUserProject Type { set; get; }
        public RoleUserProject Role { set; get; }

    }
    public enum RoleUserProject
    {
        Member = 0,
        PM = 1
       
    }
}
