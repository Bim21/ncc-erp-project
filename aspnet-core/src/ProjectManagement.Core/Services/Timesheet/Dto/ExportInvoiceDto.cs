using NccCore.Uitls;
using ProjectManagement.Entities;
using ProjectManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ProjectManagement.Constants.Enum.ClientEnum;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.Timesheet.Dto
{
    public class InvoiceData
    {
        public InvoiceGeneralInfo Info { get; set; }
        public List<TimesheetUser> TimesheetUsers { get; set; }
        public List<string> ProjectCodes { get; set; }

        private bool IsInvoiceHaveOneProject()
        {
            return TimesheetUsers.Select(s => s.ProjectName).Distinct().Count() == 1;
        }

        private string ProjectName()
        {
            return TimesheetUsers.Select(s => s.ProjectName).FirstOrDefault();
        }
        public string CurrencyName()
        {
            return TimesheetUsers.Select(s => s.CurrencyName).FirstOrDefault();
        }
        public string ExportFileName()
        {
            string projectName = IsInvoiceHaveOneProject() ? "_" + ProjectName() : "";
            var date = new DateTime(Info.Year, Info.Month, 1);

            return FilesHelper.SetFileName($"{Info.ClientName}{projectName}_Invoice{Info.InvoiceNumber}_{date.ToString("yyyyMM")}");
        }
    }

    public class InvoiceGeneralInfo
    {
        public string ClientName { get; set; }
        public float TransferFee { get; set; }
        public float Discount { get; set; }
        public long InvoiceNumber { get; set; }

        public string ClientAddress { get; set; }
        public string PaymentInfo { get; set; }
        public byte PaymentDueBy { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public InvoiceDateSetting InvoiceDateSetting { get; set; }

        public string[] PaymentInfoArr()
        {
            return PaymentInfo.Split("\n");
        }
        public string InvoiceDateStr()
        {
            var date = new DateTime(Year, Month, 1).AddMonths(1);
            if (InvoiceDateSetting == InvoiceDateSetting.LastDateThisMonth)
            {
                date = date.AddDays(-1);
            }

            return DateTimeUtils.FormatDateToInvoice(date);
        }

        public string PaymentDueByStr()
        {
            var date = new DateTime(Year, Month, 1).AddMonths(2).AddDays(-1);//last date of next month
            if (PaymentDueBy >= 1)
            {
                try
                {
                    date = new DateTime(Year, Month, PaymentDueBy).AddMonths(1);//on the date of next month
                }
                catch
                {
                    date = new DateTime(Year, Month, 15).AddMonths(1);//on the date of next month
                }

            }

            return DateTimeUtils.FormatDateToInvoice(date);
        }
    }

    public class TimesheetUser
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public float WorkingDay { get; set; }
        public ChargeType ChargeType { get; set; }
        public string CurrencyName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public float BillRate { get; set; }
        public int DefaultWorkingHours { get; set; }
        public ExportInvoiceMode Mode { get; set; }
        public float TimesheetWorkingDay { get; set; }
        public float BillRateDisplay => (Mode == ExportInvoiceMode.MontlyToDaily && ChargeType == ChargeType.Monthly) ? BillRate / TimesheetWorkingDay : BillRate;
        public float WorkingDayDisplay
        {
            get
            {
                if ((Mode == ExportInvoiceMode.MontlyToDaily && ChargeType == ChargeType.Monthly) || ChargeType == ChargeType.Daily)
                {
                    return WorkingDay;
                }

                if (ChargeType == ChargeType.Hours)
                {
                    return WorkingDay * DefaultWorkingHours;
                }

                return WorkingDay / TimesheetWorkingDay;
            }
        }
        public string ChargeTypeDisplay
        {
            get
            {
                if ((Mode == ExportInvoiceMode.MontlyToDaily && ChargeType == ChargeType.Monthly) || ChargeType == ChargeType.Daily)
                {
                    return "Day";
                }

                if (ChargeType == ChargeType.Hours)
                {
                    return "Hours";
                }

                return "Month";
            }
        }
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
                    return WorkingDay * BillRate * DefaultWorkingHours;
                }

                return WorkingDay * BillRate / TimesheetWorkingDay;
            }
        }
    }

    public class TimesheetTaxDto
    {
        public List<DateTime> ListWorkingDay { get; set; }
        public List<TimesheetDetail> ListTimesheet { get; set; }
    }

    public class TimesheetDetailUser
    {
        public int ProjectNumber { get; set; }
        public string FullName { get; set; }
        public string ProjectName { get; set; }
        public List<TimesheetDetail> TimesheetDetails { get; set; }
    }
    public class TimesheetDetail
    {
        public DateTime DateAt { get; set; }
        public float ManDay { get; set; }
        public string TaskName { get; set; }
        public string Note { get; set; }
        public string EmailAddress { get; set; }
        public string ProjectCode { get; set; }
    }
    public enum ExportInvoiceMode : byte
    {
        Normal = 0,
        MontlyToDaily = 1
    }
}
