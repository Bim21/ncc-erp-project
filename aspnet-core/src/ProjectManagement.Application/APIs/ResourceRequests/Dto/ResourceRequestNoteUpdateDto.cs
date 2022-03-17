using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.ResourceRequests.Dto
{
    public class ResourceRequestNoteUpdateDto
    {
        public long ResourceRequestId { get; set; }

        public string PMNote { get; set; }

        public string HPMNote { get; set; }
    }
}
