using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.PMReportProjects.Dto;
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
        public async Task<List<GetProjectUserDto>> GetAllByProject(long projectId, bool viewHistory)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.ProjectId == projectId)
                        .Where(x => viewHistory || x.Status != ProjectUserStatus.Past)
                        .OrderByDescending(x => x.CreationTime)
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

            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_ViewDetailProjectUser)]
        public async Task<GetProjectUserDto> Get(long projectUserId)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.Id == projectUserId)
                                .Select(x => new GetProjectUserDto
                                {
                                    Id = x.Id,
                                    UserId = x.UserId,
                                    UserName = x.User.FullName,
                                    ProjectId = x.ProjectId,
                                    ProjectName = x.Project.Name,
                                    ProjectRole = x.ProjectRole.ToString(),
                                    AllocatePercentage = x.AllocatePercentage,
                                    StartTime = x.StartTime.Date,
                                    Status = x.Status.ToString(),
                                    IsExpense = x.IsExpense,
                                    ResourceRequestId = x.ResourceRequestId,
                                    ResourceRequestName = x.ResourceRequest.Name,
                                    PMReportId = x.PMReportId,
                                    PMReportName = x.PMReport.Name,
                                    IsFutureActive = x.IsFutureActive
                                });
            return await query.FirstOrDefaultAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_Create)]
        public async Task<ProjectUserDto> Create(ProjectUserDto input)
        {
            var isExist = await WorkScope.GetAll<ProjectUser>().AnyAsync(x => x.ProjectId == input.ProjectId && x.UserId == input.UserId && x.AllocatePercentage == input.AllocatePercentage);
            if (isExist)
                throw new UserFriendlyException("User already exist in project !");

            if(input.Status == ProjectUserStatus.Past)
                throw new UserFriendlyException("Can't add people to the past !");

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            input.PMReportId = pmReportActive.Id;
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectUser>(input));

            var projectUsers = await WorkScope.GetAll<ProjectUser>().Where(x => x.Id != input.Id && x.ProjectId == input.ProjectId && x.UserId == input.UserId && x.Status == ProjectUserStatus.Present).ToListAsync();
            foreach(var item in projectUsers)
            {
                item.Status = ProjectUserStatus.Past;
                await WorkScope.UpdateAsync(item);
            }

            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_Update)]
        public async Task<ProjectUserDto> Update(ProjectUserDto input)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(input.Id);

            if (input.Status == ProjectUserStatus.Past)
                throw new UserFriendlyException("Can't edit people to the past !");

            if(projectUser.Status == ProjectUserStatus.Future && input.Status == ProjectUserStatus.Present)
            {
                var projectUsers = await WorkScope.GetAll<ProjectUser>().Where(x => x.Id != input.Id && x.ProjectId == input.ProjectId && x.UserId == input.UserId && x.Status == ProjectUserStatus.Present).ToListAsync();
                foreach (var item in projectUsers)
                {
                    item.Status = ProjectUserStatus.Past;
                    await WorkScope.UpdateAsync(item);
                }
            }

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            input.PMReportId = pmReportActive.Id;
            await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectUserDto, ProjectUser>(input, projectUser));

            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_Delete)]
        public async Task Delete(long projectUserId)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(projectUserId);

            await WorkScope.DeleteAsync(projectUser);
        }
    }
}
