using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Clients.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Clients
{
    [AbpAuthorize]
    public class ClientAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        [AbpAuthorize(PermissionNames.Admin_Client_ViewAll)]
        public async Task<GridResult<ClientDto>> GetAllPaging(GridParam input)
        {
            var query = WorkScope.GetAll<Client>()
                .Select(s => new ClientDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                });
            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        public async Task<List<ClientDto>> GetAll()
        {
            var query = WorkScope.GetAll<Client>()
              .Select(s => new ClientDto
              {
                  Id = s.Id,
                  Name = s.Name,
                  Code = s.Code
              });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Admin_Client_Create)]
        public async Task<ClientDto> Create(ClientDto input)
        {
            var isExist = await WorkScope.GetAll<Client>().AnyAsync(x => x.Name == input.Name || x.Code == input.Code);

            if (isExist)
                throw new UserFriendlyException(String.Format("Name or Code already exist !"));

            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<Client>(input));
            return input;
        }
        [HttpPut]
        [AbpAuthorize(PermissionNames.Admin_Client_Edit)]
        public async Task<ClientDto> Update(ClientDto input)
        {
            var client = await WorkScope.GetAsync<Client>(input.Id);           

            var isExist = await WorkScope.GetAll<Client>().AnyAsync(x => x.Id != input.Id && (x.Name == input.Name || x.Code == input.Code));

            if (isExist)
                throw new UserFriendlyException(String.Format("Name or Code already exist !"));

            await WorkScope.UpdateAsync(ObjectMapper.Map<ClientDto, Client>(input, client));
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.Admin_Client_Delete)]
        public async Task Delete(long clientId)
        {
            var hasProject = await WorkScope.GetAll<Project>().AnyAsync(x => x.ClientId == clientId);
            if (hasProject)
                throw new UserFriendlyException("Client already has a project !");

            await WorkScope.DeleteAsync<Client>(clientId);
        }
    }
}
