using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class ResourceRequestUserDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public byte Undisposed { get; set; }
    }
}
