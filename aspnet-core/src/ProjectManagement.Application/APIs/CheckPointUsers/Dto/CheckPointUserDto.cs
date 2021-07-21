﻿using Abp.AutoMapper;
using Abp.Domain.Entities;
using NccCore.Anotations;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.CheckPointUsers.Dto
{
    [AutoMapTo(typeof(CheckPointUser))]
    public class CheckPointUserDto : Entity<long>
    {
        public long? ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public string ReviewerAvatar { get; set; }
        public long? UserId { get; set; }
        [ApplySearchAttribute]
        public string UserName { get; set; }
        [ApplySearchAttribute]
        public string UserEmail { get; set; }
        public string UserAvatar { get; set; }
        public CheckPointUserType Type { get; set; }    
        public CheckPointUserStatus Status { get; set; }
        public int? Score { get; set; }
        public string Note { get; set; }

        //public bool PhaseStatus { get; set; }

    }
}
