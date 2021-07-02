
using Abp.Authorization;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.APIs.AuditResults.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.AuditResults
{
    public class AuditResultAppService:ProjectManagementAppServiceBase
    {
        [AbpAuthorize(PermissionNames.SaoDo_AuditResult_Create)]
        public async Task<AuditResultDto> Create(AuditResultDto input)
        {
            var isExist = await WorkScope.GetAll<AuditResult>()
                            .AnyAsync(x=>x.AuditSessionId == input.AuditSessionId && x.ProjectId == input.ProjectId);
            if (isExist)
            {
                throw new UserFriendlyException("Audit Result already exists.");
            }
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<AuditResult>(input));
            return input;
        }
        [AbpAuthorize(PermissionNames.SaoDo_AuditResult_Update)]
        public async Task<AuditResultDto> Update(AuditResultDto input)
        {
            var isExist = await WorkScope.GetAll<AuditResult>()
                            .AnyAsync(x => x.AuditSessionId == input.AuditSessionId && x.ProjectId == input.ProjectId && x.Id != input.Id);
            if (isExist)
            {
                throw new UserFriendlyException("Audit Result already exists.");
            }
            await WorkScope.UpdateAsync(ObjectMapper.Map<AuditResult>(input));
            return input;
        }
        [AbpAuthorize(PermissionNames.SaoDo_AuditResult_Delete)]
        public async Task Delete(long id)
        {
            await WorkScope.DeleteAsync<AuditResult>(id);
        }
    }
}
