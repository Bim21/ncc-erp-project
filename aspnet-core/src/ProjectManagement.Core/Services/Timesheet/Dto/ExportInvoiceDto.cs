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
        public float Discount { get; set; }
        public float TransferFee { get; set; }
        public long InvoiceNumber { get; set; }
    }

    public class ResultExportInvoice
    {
        public string ProjectName { get; set; }
        public List<ExportInvoiceDto> ListExportInvoice { get; set; }
    }




    public class ResultExportInvoiceDto
    {
       

        public List<TimesheetUserDto> ListExportInvoice { get; set; }
    }


    public class ExportProjectDto
    {
        public long ProjectId { get; set; }
        public string  ProjectName { get; set; }
        public string ClientName { get; set; }
        public float TransferFee { get; set; }
        public float Discount { get; set; }

    }

    public class TimesheetUserDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string ProjectName { get; set; }
        public float WorkingDay { get; set; }
        public ChargeType ChargeType { get; set; }
        public string CurrencyName { get; set; }
        //public DateTime StartTime { get; set; }
        //public DateTime? EndTime { get; set; }
        public float BillRate { get; set; }

        public ExportInvoiceMode Mode { get; set; }
        public float TimesheetWorkingDay { get; set; }

        public float Discount { get; set; }
        public float TransferFee { get; set; }
        public long InvoiceNumber { get; set; }

        public string ClientAddress { get; set; }
        public string PaymentInfo { get; set; }
        public string PaymentDueBy { get; set; }

        public float BillRateDisplay => Mode == ExportInvoiceMode.Normal ? BillRate : BillRate / TimesheetWorkingDay;
        public double LineTotal
        {
            get
            {
                if (ChargeType == ChargeType.Daily)
                {
                    return WorkingDay * BillRate;
                }

                if (ChargeType == ChargeType.Hours)
                {
                    return WorkingDay * BillRate * 8;
                }

                return WorkingDay * BillRate / TimesheetWorkingDay;
            }
        }
    }

    public enum ExportInvoiceMode : byte
    {
        Normal = 0,
        MontlyTodaily = 1
    }
}
