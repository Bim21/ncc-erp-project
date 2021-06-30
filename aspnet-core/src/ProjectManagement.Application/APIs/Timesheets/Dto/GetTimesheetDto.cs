using Abp.Application.Services.Dto;
using NccCore.Anotations;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

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
        public bool IsActive { get; set; }
        public long TotalProject { get; set; }
        public long TotalTimesheet { get; set; }
       
    }
}
