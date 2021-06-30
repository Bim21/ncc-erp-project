using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ProjectUsers
{
    public class ProjectUserAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_ViewAllByProject)]
        public async Task<GridResult<GetProjectUserDto>> GetAllByProject(GridParam input, long projectId, bool viewHistory)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.ProjectId == projectId)
                        .Where(x => viewHistory || x.Status == ProjectUserStatus.Present)
                        .Select(x => new GetProjectUserDto
                        {
                            Id = x.Id,
                            UserId = x.UserId,
                            UserName = x.User.Name,
                            ProjectId = x.ProjectId,
                            ProjectName = x.Project.Name,
                            ProjectRole = x.ProjectRole.ToString(),
                            AllocatePercentage = x.AllocatePercentage,
                            StartTime = x.StartTime,
                            Status = x.Status.ToString(),
                            IsExpense = x.IsExpense,
                            ResourceRequestId = x.ResourceRequestId,
                            PMReportId = x.PMReportId,
                            IsFutureActive = x.IsFutureActive
                        });

            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        public async Task<ProjectUserDto> Create(ProjectUserDto input)
        {
            var isExist = await WorkScope.GetAll<ProjectUser>().AnyAsync(x => x.ProjectId == input.ProjectId && x.UserId == input.UserId);
            if (isExist)
                throw new UserFriendlyException("User already exist in project !");

            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectUser>(input));
            return input;
        }

        [HttpPut]
        public async Task<ProjectUserDto> Update(ProjectUserDto input)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(input.Id);

            await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectUserDto, ProjectUser>(input, projectUser));
            return input;
        }

        [HttpDelete]
        public async Task Delete(long projectUserId)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(projectUserId);

            await WorkScope.DeleteAsync(projectUser);
        }
    }
}
