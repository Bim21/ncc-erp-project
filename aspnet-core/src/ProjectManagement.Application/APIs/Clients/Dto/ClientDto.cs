using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NccCore.IoC;
using NccCore.Paging;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Clients.Dto
{
    [AutoMapTo(typeof(Client))]
    public class ClientDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
