﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    [AutoMapTo(typeof(ResourceRequest))]
    public class ResourceRequestDto : EntityDto<long>
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public DateTime TimeNeed { get; set; }
        public ResourceRequestStatus Status { get; set; }
        public DateTime TimeDone { get; set; }
        public string Note { get; set; }
    }
}