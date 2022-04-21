using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.Timesheet.Dto
{
    public class ExportInvoiceDto
    {
        public string ProjectCode { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string ProjectName { get; set; }
        public float WorkingTimeDay { get; set; }
        public ChargeType ChargeType { get; set; }
        public string CurrencyName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public float BillRate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public double LineTotal
        {
            get
            {
                return WorkingTimeDay * BillRate;
            }
        }
    }

    public class ResultExportInvoice
    {

        public string ProjectName { get; set; }
        public List<ExportInvoiceDto> ListExportInvoice { get; set; }
    }
}
