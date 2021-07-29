using Abp.Authorization;
using ProjectManagement.APIs.AuditResultPeoples.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.AuditResultPeoples
{
    [AbpAuthorize]
    public class AuditResultPeopleAppService : ProjectManagementAppServiceBase
    {
        [AbpAuthorize(PermissionNames.SaoDo_AuditResultPeople_Create)]
        public async Task<AuditResultPeopleDto> Create(AuditResultPeopleDto input)
        {
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<AuditResultPeople>(input));
            return input;
        }

        [AbpAuthorize(PermissionNames.SaoDo_AuditResultPeople_Delete)]
        public async Task Delete(long id)
        {
            await WorkScope.DeleteAsync<AuditResultPeople>(id);
        }

        [AbpAuthorize(PermissionNames.SaoDo_AuditResultPeople_Update)]
        public async Task<AuditResultPeopleDto> Update(AuditResultPeopleDto input)
        {
            var isExist = await WorkScope.GetAsync<AuditResultPeople>(input.Id);
            ObjectMapper.Map(input, isExist);
            await WorkScope.UpdateAsync(isExist);
            return input;
        }

    }
}
