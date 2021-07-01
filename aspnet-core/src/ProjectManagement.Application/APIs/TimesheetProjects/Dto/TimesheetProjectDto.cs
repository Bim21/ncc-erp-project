using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.TimesheetProjects.Dto
{
    [AutoMapTo(typeof(TimesheetProject))]
    public class TimesheetProjectDto : EntityDto<long>
    {
        public long ProjectId { get; set; }
        public long TimesheetId { get; set; }
        public string Note { get; set; }
        public string ProjectBillInfomation { get; set; }
    }
}
