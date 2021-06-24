﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.Timesheets.Dto
{
    public class GetTimesheetDetailDto : EntityDto<long>
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public long PmId { get; set; }
        public string PmName { get; set; }
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public string File { get; set; }
        public string Note { get; set; }
    }
}