using Abp.Authorization;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.APIs.ProjectCheckLists.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ProjectCheckLists
{
    public class ProjectCheckListAppService : ProjectManagementAppServiceBase
    {
        [AbpAuthorize(PermissionNames.SaoDo_ProjectChecklist_Create)]
        public async Task<ProjectCheckListDto> Create(ProjectCheckListDto input)
        {
            var isExist = await WorkScope.GetAll<ProjectCheckList>()
                .AnyAsync(x => x.ProjectId == input.ProjectId && x.CheckListItemId == input.CheckListItemId);
            if (isExist)
            {
                throw new UserFriendlyException("Project with Id '" + input.ProjectId + "' created with item with id '" + input.CheckListItemId + "'");
            }
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectCheckList>(input));
            return input;
        }

        [AbpAuthorize(PermissionNames.SaoDo_ProjectChecklist_Delete)]
        public async Task Delete(long ProjectId, long CheckListItemId)
        {
            var isExist = await WorkScope.GetAll<ProjectCheckList>()
               .Where(x => x.ProjectId == ProjectId && x.CheckListItemId == CheckListItemId).FirstOrDefaultAsync();
            if (isExist == default)
            {
                throw new UserFriendlyException("Project with Id '" + ProjectId + "' does not exist with item with id '" + CheckListItemId + "'");
            }
            await WorkScope.DeleteAsync(isExist);
        }

        [AbpAuthorize(PermissionNames.SaoDo_ProjectChecklist_ReverseActive)]
        public async Task ReverseActive(long ProjectId, long CheckListItemId)
        {
            var isExist = await WorkScope.GetAll<ProjectCheckList>()
               .Where(x => x.ProjectId == ProjectId && x.CheckListItemId == CheckListItemId).FirstOrDefaultAsync();
            if (isExist == default)
            {
                throw new UserFriendlyException("Project with Id '" + ProjectId + "' does not exist with item with id '" + CheckListItemId + "'");
            }
            isExist.IsActive = !isExist.IsActive;
            await WorkScope.UpdateAsync(isExist);
        }

        [AbpAuthorize(PermissionNames.SaoDo_ProjectChecklist_AddByProjectType)]
        public async Task<List<ProjectCheckListDto>> AddByProjectType(ProjectType input)
        {
            var projectChecklists = await (from p in WorkScope.GetAll<Project>()
                                             .Where(x => x.ProjectType == input && x.Status != ProjectStatus.Closed && x.Status != ProjectStatus.Potential)
                                           from ci in WorkScope.GetAll<CheckListItem>()
                                           select new ProjectCheckListDto
                                           {
                                               CheckListItemId = ci.Id,
                                               ProjectId = p.Id,
                                               IsActive = true
                                           }).ToListAsync();
            foreach (var i in projectChecklists)
            {
                i.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectCheckList>(i));
            }
            return projectChecklists;
        }

    }
}
