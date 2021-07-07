using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.HRM.Dto
{
    public class CreateUserHRMDto : EntityDto<long>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string UserCode { get; set; }
    }
}
