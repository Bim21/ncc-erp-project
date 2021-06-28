using Abp.Application.Services.Dto;
using NccCore.Anotations;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.Timesheets.Dto
{
    public class GetTimesheetDto : EntityDto<long>
    {
        [ApplySearchAttribute]
        public string Name { get; set; }
        [ApplySearchAttribute]
        public int Month { get; set; }
        [ApplySearchAttribute]
        public int Year { get; set; }
        public TimesheetStatus Status { get; set; }
        public long TotalProject { get; set; }
        public long TotalTimesheet { get; set; }
    }
}
