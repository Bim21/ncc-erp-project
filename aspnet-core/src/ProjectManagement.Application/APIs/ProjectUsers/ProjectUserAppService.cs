using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Configuration;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using NccCore.Uitls;
using Newtonsoft.Json;
using ProjectManagement.APIs.PMReportProjects.Dto;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.NccCore.Helper;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using ProjectManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ProjectUsers
{
    [AbpAuthorize]
    public class ProjectUserAppService : ProjectManagementAppServiceBase
    {
        private ISettingManager _settingManager;
        private KomuService _komuService;
        public ProjectUserAppService(
            KomuService komuService, ISettingManager settingManager)
        {
            _komuService = komuService;
            _settingManager = settingManager;
        }
        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_ViewAllByProject, PermissionNames.DeliveryManagement_ProjectUser_ViewAllByProject)]
        public async Task<List<GetProjectUserDto>> GetAllByProject(long projectId, bool viewHistory)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.ProjectId == projectId && x.IsFutureActive)
                        .Where(x => viewHistory || x.Status != ProjectUserStatus.Past && (x.Status == ProjectUserStatus.Present ? x.AllocatePercentage > 0 : true))
                        .Where(x => x.User.UserType != UserType.FakeUser)
                        .OrderByDescending(x => x.CreationTime)
                        .Select(x => new GetProjectUserDto
                        {
                            Id = x.Id,
                            UserId = x.UserId,
                            FullName = x.User.FullName,
                            ProjectId = x.ProjectId,
                            ProjectName = x.Project.Name,
                            ProjectRole = x.ProjectRole.ToString(),
                            AllocatePercentage = x.AllocatePercentage,
                            StartTime = x.StartTime,
                            Status = x.Status.ToString(),
                            IsExpense = x.IsExpense,
                            ResourceRequestId = x.ResourceRequestId,
                            PMReportId = x.PMReportId,
                            IsFutureActive = x.IsFutureActive,
                            AvatarPath = "/avatars/" + x.User.AvatarPath,
                            Branch = x.User.Branch,
                            EmailAddress = x.User.EmailAddress,
                            UserName = x.User.UserName,
                            UserType = x.User.UserType,
                            Note = x.Note
                        });
            return await query.ToListAsync();
        }

        [HttpGet]
        public async Task<List<UserDto>> GetAllProjectUserInProject(long projectId)
        {
            var projectUser = WorkScope.GetAll<ProjectUser>().Where(x => x.ProjectId == projectId && x.Status != ProjectUserStatus.Past).Select(x => x.UserId);

            var query = WorkScope.GetAll<User>().Where(x => x.IsActive && projectUser.Contains(x.Id))
                        .Select(x => new UserDto
                        {
                            Id = x.Id,
                            FullName = x.FullName,
                            AvatarPath = "/avatars/" + x.AvatarPath,
                            UserType = x.UserType,
                            UserLevel = x.UserLevel,
                            Branch = x.Branch,
                            EmailAddress = x.EmailAddress,
                            UserName = x.UserName,
                        });
            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_ViewDetailProjectUser,
            PermissionNames.DeliveryManagement_ProjectUser_ViewDetailProjectUser)]
        public async Task<GetProjectUserDto> Get(long projectUserId)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.Id == projectUserId)
                                .Select(x => new GetProjectUserDto
                                {
                                    Id = x.Id,
                                    UserId = x.UserId,
                                    FullName = x.User.FullName,
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
                                    IsFutureActive = x.IsFutureActive,
                                    Note = x.Note
                                });
            return await query.FirstOrDefaultAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_Create, PermissionNames.DeliveryManagement_ProjectUser_Create)]
        public async Task<ProjectUserDto> Create(ProjectUserDto model)
        {
            var isExistProjectUser = await WorkScope.GetAll<ProjectUser>().AnyAsync(x => x.ProjectId == model.ProjectId && x.UserId == model.UserId
                                    && x.Status == model.Status && x.StartTime.Date == model.StartTime.Date && x.ProjectRole == x.ProjectRole
                                    && x.AllocatePercentage == model.AllocatePercentage);
            if (isExistProjectUser)
                throw new UserFriendlyException("User already exist in project !");
            if (model.Status == ProjectUserStatus.Past)
                throw new UserFriendlyException("Can't add people to the past !");
            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");
            var userIsActive = await WorkScope.GetAll<User>().AnyAsync(x => x.Id == model.UserId && x.IsActive);
            if (!userIsActive)
                throw new UserFriendlyException("Can't add people is inactive !");
            model.IsFutureActive = true;
            model.PMReportId = pmReportActive.Id;
            model.Status = model.StartTime.Date > DateTime.Now.Date ? ProjectUserStatus.Future : ProjectUserStatus.Present;
            model.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectUser>(model));

            if (model.Status == ProjectUserStatus.Present)
            {
                var projectUsers = await WorkScope.GetAll<ProjectUser>().Where(x => x.Id != model.Id && x.ProjectId == model.ProjectId && x.UserId == model.UserId && x.Status == ProjectUserStatus.Present).ToListAsync();
                foreach (var item in projectUsers)
                {
                    item.Status = ProjectUserStatus.Past;
                    await WorkScope.UpdateAsync(item);
                }
            }
            var project = await WorkScope.GetAll<Project>().FirstOrDefaultAsync(x => x.Id == model.ProjectId);
            if (project == null)
                throw new UserFriendlyException("Project doesn't exist");
            var pm = await WorkScope.GetAsync<User>(AbpSession.UserId.Value);
            var user = await WorkScope.GetAsync<User>(model.UserId);
            var pmUserName = UserHelper.GetUserName(pm.EmailAddress);
            var userName = UserHelper.GetUserName(user.EmailAddress);
            if (pm != null && string.IsNullOrEmpty(pm.KomuUserId.ToString()))
            {
                pm.KomuUserId = await _komuService.GetKomuUserId(new KomuUserDto { Username = pmUserName ?? pm.UserName }, ChannelTypeConstant.KOMU_USER);
                await WorkScope.UpdateAsync<User>(pm);
            }
            var message = new StringBuilder();
            if (model.AllocatePercentage == 0)
                message.AppendLine($"Từ ngày **{model.StartTime:dd/MM/yyyy}**, PM {(!string.IsNullOrEmpty(pm.KomuUserId.ToString()) ? "<@" + pm.KomuUserId + ">" : "**" + pmUserName ?? pm.UserName + "**")} release **{userName ?? user.UserName}** ra khỏi dự án **{project.Name}**.");
            else
                message.AppendLine($"Từ ngày **{model.StartTime:dd/MM/yyyy}**, PM {(!string.IsNullOrEmpty(pm.KomuUserId.ToString()) ? "<@" + pm.KomuUserId + ">" : "**" + pmUserName ?? pm.UserName + "**")} request **{userName ?? user.UserName}** làm việc ở dự án **{project.Name}**.");
            await _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = pmUserName ?? pm.UserName,
                Message = message.ToString(),
                CreateDate = DateTimeUtils.GetNow(),
            }, ChannelTypeConstant.PM_CHANNEL);
            return model;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_Update, PermissionNames.DeliveryManagement_ProjectUser_Update)]
        public async Task<ProjectUserDto> Update(ProjectUserDto model)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(model.Id);

            if (projectUser.Status == ProjectUserStatus.Past)
                throw new UserFriendlyException("Can't edit people in the past !");

            if (model.Status == ProjectUserStatus.Past)
                throw new UserFriendlyException("Can't edit people to the past !");

            if (model.ResourceRequestId != null && model.StartTime.Date < DateTime.Now.Date)
            {
                throw new UserFriendlyException("Can't add user at past time !");
            }

            if (projectUser.Status == ProjectUserStatus.Future && model.Status == ProjectUserStatus.Present)
            {
                var projectUsers = await WorkScope.GetAll<ProjectUser>().Where(x => x.Id != model.Id && x.ProjectId == model.ProjectId && x.UserId == model.UserId && x.Status == ProjectUserStatus.Present).ToListAsync();
                foreach (var item in projectUsers)
                {
                    item.Status = ProjectUserStatus.Past;
                    await WorkScope.UpdateAsync(item);
                }
            }

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            model.IsFutureActive = true;
            model.PMReportId = pmReportActive.Id;
            await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectUserDto, ProjectUser>(model, projectUser));

            return model;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_Delete, PermissionNames.DeliveryManagement_ProjectUser_Delete)]
        public async Task Delete(long projectUserId)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(projectUserId);

            await WorkScope.DeleteAsync(projectUser);
        }
    }
}
