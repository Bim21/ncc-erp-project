using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.Finance.Dto
{
    public class CreateInvoiceDto
    {
        public string Name { get; set; }
        public string ClientName { get; set; }
        public string Project { get; set; }
        public string AccountCode { get; set; }
        public double TotalPrice { get; set; }
        public InvoiceStatus Status { get; set; }
        public string Note { get; set; }
        public List<InvoiceDetailDto> Detail { get; set; }
    }

    public class InvoiceDetailDto
    {
        public string ProjectName { get; set; }
        public long FileId { get; set; }
        public string LinkFile { get; set; }
    }
}
