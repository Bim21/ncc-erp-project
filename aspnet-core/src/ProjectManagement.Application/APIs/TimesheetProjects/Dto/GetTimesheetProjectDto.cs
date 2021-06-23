using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.TimesheetProjects.Dto
{
    public class GetTimesheetProjectDto : EntityDto<long>
    {
        public long ProjectId { get; set; }
        public string TimesheetFile { get; set; }
        public long TimesheetId { get; set; }
        public string TimeSheet { get; set; }
        public string Note { get; set; }
    }
}
