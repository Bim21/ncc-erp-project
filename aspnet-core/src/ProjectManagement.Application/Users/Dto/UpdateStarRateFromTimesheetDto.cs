using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Users.Dto
{
    public class UpdateStarRateFromTimesheetDto
    {
        public string UserCode { get; set; }
        public string EmailAddress { get; set; }
        public int StarRate { get; set; }
        public UserLevel Level { get; set; }
        public UserType Type { get; set; }
    }
}
