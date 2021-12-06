using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Users.Dto
{
    public class UpdateStarRateFromTimesheetDto
    {
        public string UserCode { get; set; }
        public string EmailAddress { get; set; }
        public int StarRate { get; set; }
    }
}
