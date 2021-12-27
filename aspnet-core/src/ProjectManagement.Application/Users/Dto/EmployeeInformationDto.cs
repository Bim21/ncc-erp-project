using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Users.Dto
{
    public class EmployeeInformationDto
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
        public string WorkingTime { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleType { get; set; }
        public string Branch { get; set; }
        public List<ProjectDTO> ProjectDtos { get; set; }
    }

    public class ProjectDTO
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string PmName { get; set; }
        public DateTime StartTime { get; set; }
        public string ProjectRole { get; set; }
    }
}
