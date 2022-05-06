using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NccCore.Anotations;
using NccCore.IoC;
using NccCore.Paging;
using ProjectManagement.Entities;
using ProjectManagement.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ClientEnum;

namespace ProjectManagement.APIs.Clients.Dto
{
    [AutoMapTo(typeof(Client))]
    public class ClientDto : EntityDto<long>
    {
        [ApplySearchAttribute]
        public string Name { get; set; }
        [ApplySearchAttribute]
        public string Code { get; set; }
        public string Address { get; set; }
        public InvoiceDateSetting InvoiceDateSetting { get; set; }
        public byte PaymentDueBy { get; set; }
        public float TransferFee { get; set; }

        public string PaymentDueByToString
        {
            get
            {
                var dataPaymentDueBy = new DataPaymentDueBy();
                return dataPaymentDueBy.ListDataPaymentDueBy.Find(x => x.Value == PaymentDueBy).Label.ToString();
            }
        }
        public string InvoiceDateSettingToString
        {
            get
            {
                var dataInvoiceDate = new DataInvoiceDate();
                return dataInvoiceDate.ListDataInvoiceDate.Find(x => x.Value == InvoiceDateSetting).Label.ToString();
            }
        }
    }
}
