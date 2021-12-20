using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.TimesheetProjects.Dto
{
    public class InvoiceExcelDto
    {
        public long TimesheetId { get; set; }
        public List<long> ProjectId { get; set; }
    }
}
