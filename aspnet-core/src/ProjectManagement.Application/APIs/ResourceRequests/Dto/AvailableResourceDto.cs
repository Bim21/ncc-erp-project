using System.Collections.Generic;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class AvailableResourceDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string AvatarPath { get; set; }
        public UserType UserType { get; set; }
        public Branch Branch { get; set; }
        public List<string> Projects { get; set; }
        public byte Used { get; set; }
    }
}
