using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class AvailableResourceDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Projects { get; set; }
        public byte Used { get; set; }
    }
}
