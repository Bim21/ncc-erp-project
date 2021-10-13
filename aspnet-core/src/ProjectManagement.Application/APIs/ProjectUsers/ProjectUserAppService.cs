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
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
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
        public async Task<ProjectUserDto> Create(ProjectUserDto input)
        {
            var isExist = await WorkScope.GetAll<ProjectUser>().AnyAsync(x => x.ProjectId == input.ProjectId && x.UserId == input.UserId
                                    && x.Status == input.Status && x.StartTime.Date == input.StartTime.Date && x.ProjectRole == x.ProjectRole
                                    && x.AllocatePercentage == input.AllocatePercentage);
            if (isExist)
                throw new UserFriendlyException("User already exist in project !");

            if (input.Status == ProjectUserStatus.Past)
                throw new UserFriendlyException("Can't add people to the past !");

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            input.IsFutureActive = true;
            input.PMReportId = pmReportActive.Id;
            input.Status = input.StartTime.Date > DateTime.Now.Date ? ProjectUserStatus.Future : ProjectUserStatus.Present;
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectUser>(input));

            if (input.Status == ProjectUserStatus.Present)
            {
                var projectUsers = await WorkScope.GetAll<ProjectUser>().Where(x => x.Id != input.Id && x.ProjectId == input.ProjectId && x.UserId == input.UserId && x.Status == ProjectUserStatus.Present).ToListAsync();
                foreach (var item in projectUsers)
                {
                    item.Status = ProjectUserStatus.Past;
                    await WorkScope.UpdateAsync(item);
                }

            }
            //Komu bot nhắn tin đến nhóm
            
            var login = new LoginDto
            {
                password = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.PasswordBot),
                user = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.UserBot)
            };
            var response = await _komuService.Login(login);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var DecryptContent = JsonConvert.DeserializeObject<LoginJsonPrase>(responseContent);
                //get name project
                var query = WorkScope.GetAll<Project>().Where(x => x.Id == input.ProjectId)
                                    .Select(x => new GetProjectDto
                                    {
                                        Name = x.Name,
                                    });
                var result = await query.FirstOrDefaultAsync();
                var nameProject = result.Name;
                var room = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuRoom);
                var now = DateTimeUtils.GetNow();
                var admin = await WorkScope.GetAsync<User>(AbpSession.UserId.Value);
                var user = await WorkScope.GetAsync<User>(input.UserId);
                var message=string.Empty;
                var startTime = $"{input.StartTime.Day}/{input.StartTime.Month}/{input.StartTime.Year}";
                if (input.AllocatePercentage==0)
                {
                    message= $"Từ ngày {startTime}, PM {admin.UserName} release {user.UserName} ra khỏi dự án {nameProject}."; 
                }
                else
                {
                    message = $"Từ ngày {startTime}, PM {admin.UserName} request {user.UserName} làm việc ở dự án {nameProject}.";
                }  
                var alias = "Nhắc việc NCC";
                var postMessage = new PostMessage
                {
                    channel = room,
                    text = message.ToString(),
                    alias = alias
                };
                await _komuService.PostMessage(postMessage, DecryptContent.data);

                await _komuService.Logout(DecryptContent.data);
            }
            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_ProjectUser_Update, PermissionNames.DeliveryManagement_ProjectUser_Update)]
        public async Task<ProjectUserDto> Update(ProjectUserDto input)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(input.Id);

            if (projectUser.Status == ProjectUserStatus.Past)
                throw new UserFriendlyException("Can't edit people in the past !");

            if (input.Status == ProjectUserStatus.Past)
                throw new UserFriendlyException("Can't edit people to the past !");

            if (input.ResourceRequestId != null && input.StartTime.Date < DateTime.Now.Date)
            {
                throw new UserFriendlyException("Can't add user at past time !");
            }

            if (projectUser.Status == ProjectUserStatus.Future && input.Status == ProjectUserStatus.Present)
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

            input.IsFutureActive = true;
            input.PMReportId = pmReportActive.Id;
            await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectUserDto, ProjectUser>(input, projectUser));

            return input;
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
