using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.Timesheets.Dto
{
    [AutoMapTo(typeof(Timesheet))]
    public class TimesheetDto : EntityDto<long>
    {
        public string Name { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
