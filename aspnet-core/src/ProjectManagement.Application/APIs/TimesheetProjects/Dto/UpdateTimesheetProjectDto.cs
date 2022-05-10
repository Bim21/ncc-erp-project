using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.TimesheetProjects.Dto
{
    public class UpdateTimesheetProjectDto
    {
        public long Id { get; set; }
        public float WorkingDay { get; set; }    
        public long InvoiceNumber { get; set; }
        public float Discount { get; set; }
        public float TransferFee { get; set; }
    }
}
