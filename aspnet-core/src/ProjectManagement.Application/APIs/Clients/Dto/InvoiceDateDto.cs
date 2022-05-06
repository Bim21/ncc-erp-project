using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NccCore.Anotations;
using NccCore.IoC;
using NccCore.Paging;
using ProjectManagement.Entities;
using ProjectManagement.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ClientEnum;

namespace ProjectManagement.APIs.Clients.Dto
{
    public class InvoiceDateDto
    {
        public InvoiceDateSetting Value { get; set; }
        public string Label { get; set; }
    }

    public class DataInvoiceDate
    {
        public List<InvoiceDateDto> ListDataInvoiceDate
        {
            get
            {
                List<InvoiceDateDto> invoiceDates = new List<InvoiceDateDto>();
                invoiceDates.Add(new InvoiceDateDto() { Value = InvoiceDateSetting.LastDateThisMonth, Label = "Last date this month" });
                invoiceDates.Add(new InvoiceDateDto() { Value = InvoiceDateSetting.FirstDateNextMonth, Label = "First date next month" });
                return invoiceDates;
            }
        }
    }
}
