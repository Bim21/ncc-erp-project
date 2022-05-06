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
    public class PaymentDueByDto
    {
        public int Value { get; set; }
        public string Label { get; set; }
    }

    public class DataPaymentDueBy
    {
        public List<PaymentDueByDto> ListDataPaymentDueBy
        {
            get
            {
                List<PaymentDueByDto> paymentDueBys = new List<PaymentDueByDto>();
                paymentDueBys.Add(new PaymentDueByDto() { Value = 0, Label = "Last date next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 15, Label = "15th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 1, Label = "1st next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 2, Label = "2nd next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 3, Label = "3rd next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 4, Label = "4th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 5, Label = "5th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 6, Label = "6th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 7, Label = "7th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 8, Label = "8th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 9, Label = "9th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 10, Label = "10th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 11, Label = "11th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 12, Label = "12th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 13, Label = "13th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 14, Label = "14th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 16, Label = "16th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 17, Label = "17th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 18, Label = "18th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 19, Label = "19th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 20, Label = "20th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 21, Label = "21st next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 22, Label = "22nd next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 23, Label = "23rd next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 24, Label = "24th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 25, Label = "25th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 26, Label = "26th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 27, Label = "27th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 28, Label = "28th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 29, Label = "29th next month" });
                paymentDueBys.Add(new PaymentDueByDto() { Value = 30, Label = "30th next month" });

                return paymentDueBys;
            }
        }
    }




}
