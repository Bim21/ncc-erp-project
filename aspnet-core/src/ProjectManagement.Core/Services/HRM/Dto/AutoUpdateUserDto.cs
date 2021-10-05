using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.HRM.Dto
{
    public class AutoUpdateUserDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string UserCode { set; get; }
        public UserType UserType { get; set; }
        public UserLevel UserLevel { get; set; }
        public Branch? Branch { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
