
using Abp.Authorization;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.ProjectMilestones.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.ProjectMilestones
{
    public class ProjectMilestoneAppService : ProjectManagementAppServiceBase
    {
        [AbpAuthorize(PermissionNames.PmManager_ProjectMilestone_ViewAll)]
        public async Task<GridResult<ProjectMilestoneDto>> GetAllPaging(GridParam input)
        {
            var query = WorkScope.GetAll<ProjectMilestone>()
                        .Select(x => new ProjectMilestoneDto
                        {
                            Id = x.Id,
                            Description = x.Description,
                            Flag = x.Flag,
                            Name = x.Name,
                            Note = x.Note,
                            ProjectId = x.ProjectId,
                            Status = x.Status,
                            UATTimeEnd = x.UATTimeEnd,
                            UATTimeStart = x.UATTimeStart
                        });
            return await query.GetGridResult(query, input);
        }

        [AbpAuthorize(PermissionNames.PmManager_ProjectMilestone_Create)]
        public async Task<ProjectMilestoneDto> Create(ProjectMilestoneDto input)
        {
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectMilestone>(input));
            return input;
        }

        [AbpAuthorize(PermissionNames.PmManager_ProjectMilestone_Update)]
        public async Task<ProjectMilestoneDto> Update(ProjectMilestoneDto input)
        {
            var isExist = await WorkScope.GetAsync<ProjectMilestone>(input.Id);
            ObjectMapper.Map(input, isExist);
            await WorkScope.UpdateAsync(isExist);
            return input;
        }

        [AbpAuthorize(PermissionNames.PmManager_ProjectMilestone_Delete)]
        public async Task Delete(long id)
        {
            await WorkScope.DeleteAsync<ProjectMilestone>(id);
        }
    }
}
