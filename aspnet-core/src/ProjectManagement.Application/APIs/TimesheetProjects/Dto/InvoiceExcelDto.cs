using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.TimesheetProjects.Dto
{
    public class InvoiceExcelDto
    {
        public long TimesheetId { get; set; }
        public List<long> ProjectIds { get; set; }
        public string OptionExportInvoice { get; set; }
    }
    public class InvoiceExcelProject : FullAuditedEntity<long>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public long? ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
        public long? CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public Currency Currency { get; set; }
        public bool? IsCharge { get; set; }
        public ChargeType? ChargeType { get; set; }

        public float Discount { get; set; }
    }
}
