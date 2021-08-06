﻿using Abp.Application.Services.Dto;
using NccCore.Anotations;
using System;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ProjectUserBills.Dto
{
    public class GetProjectUserBillDto  : EntityDto<long>
    {
        public long UserId { get; set; }
        [ApplySearchAttribute]
        public string UserName { get; set; }
        public long ProjectId { get; set; }
        [ApplySearchAttribute]
        public string ProjectName { get; set; }
        public string BillRole { get; set; }
        public float BillRate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Currency Currency { get; set; }
        public string Note { get; set; }
        public string shadowNote { get; set; }
        public bool isActive { get; set; }
        public string EmailAddress { get; set; }
        [ApplySearchAttribute]
        public string FullName { get; set; }
        public string AvatarPath { get; set; }
        public UserType UserType { get; set; }
        public Branch Branch { get; set; }
    }
}
