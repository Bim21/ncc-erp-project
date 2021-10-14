﻿using Abp.Authorization;
using Abp.Configuration;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using Newtonsoft.Json;
using ProjectManagement.APIs.PMReportProjectIssues;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.APIs.ProjectUsers;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.APIs.ResourceRequests.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests
{
    [AbpAuthorize]
    public class ResourceRequestAppService : ProjectManagementAppServiceBase
    {
        private readonly ProjectUserAppService _projectUserAppService;
        private readonly PMReportProjectIssueAppService _pMReportProjectIssueAppService;
        private ISettingManager _settingManager;
        private KomuService _komuService;

        public ResourceRequestAppService(ProjectUserAppService projectUserAppService, PMReportProjectIssueAppService pMReportProjectIssueAppService, KomuService komuService, ISettingManager settingManager)
        {
            _projectUserAppService = projectUserAppService;
            _pMReportProjectIssueAppService = pMReportProjectIssueAppService;
            _komuService = komuService;
            _settingManager = settingManager;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ViewAllByProject,
            PermissionNames.PmManager_ResourceRequest_ViewAllByProject)]
        public async Task<List<GetResourceRequestDto>> GetAllByProject(long projectId)
        {
            var query = WorkScope.GetAll<ResourceRequest>().Where(x => x.ProjectId == projectId).OrderByDescending(x => x.CreationTime)
                        .Select(x => new GetResourceRequestDto
                        {
                            Id = x.Id,
                            ProjectId = x.ProjectId,
                            ProjectName = x.Project.Name,
                            Name = x.Name,
                            Status = x.Status,
                            StatusName = x.Status.ToString(),
                            PMNote = x.PMNote,
                            DMNote =  x.DMNote,
                            TimeNeed = x.TimeNeed,
                            TimeDone = x.TimeDone.Value
                        });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ViewAllResourceRequest,
            PermissionNames.PmManager_ResourceRequest_ViewAllResourceRequest)]
        public async Task<GridResult<GetResourceRequestDto>> GetAllPaging(GridParam input)
        {
            var projectUser = WorkScope.GetAll<ProjectUser>();
            var query = WorkScope.GetAll<ResourceRequest>().Select(x => new GetResourceRequestDto
            {

                Id = x.Id,
                Name = x.Name,
                ProjectId = x.ProjectId,
                ProjectName = x.Project.Name,
                Status = x.Status,
                StatusName = x.Status.ToString(),
                TimeNeed = x.TimeNeed,
                TimeDone = x.TimeDone.Value,
                PMNote = x.PMNote,
                DMNote = x.DMNote,
                PlannedNumberOfPersonnel = projectUser.Where(y => y.ProjectId == x.ProjectId && y.ResourceRequestId == x.Id).Count()
            });

            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest,
            PermissionNames.PmManager_ResourceRequest_ViewDetailResourceRequest)]
        public async Task<List<GetProjectUserDto>> ResourceRequestDetail(long resourceRequestId)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.ResourceRequestId == resourceRequestId)
                                    .Where(x => x.User.UserType != UserType.FakeUser)
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

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AddUserToRequest,
            PermissionNames.PmManager_ResourceRequest_AddUserToRequest)]
        public async Task<ProjectUserDto> AddUserToRequest(ProjectUserDto input)
        {
            if (input.StartTime.Date < DateTime.Now.Date)
            {
                throw new UserFriendlyException("Can't add user at past time !");
            }

            var isExist = await WorkScope.GetAll<ProjectUser>().AnyAsync(x => x.ProjectId == input.ProjectId && x.UserId == input.UserId
                                   && x.Status == input.Status && x.StartTime.Date == input.StartTime.Date && x.ProjectRole == x.ProjectRole
                                   && x.AllocatePercentage == input.AllocatePercentage);
            if (isExist)
                throw new UserFriendlyException("User already exist in project !");

            var resourceRequest = await WorkScope.GetAsync<ResourceRequest>((long)input.ResourceRequestId);

            if (input.StartTime.Date < resourceRequest.TimeNeed.Date)
                throw new UserFriendlyException("Start date must be greater than request date !");

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            input.ProjectId = resourceRequest.ProjectId;
            input.PMReportId = pmReportActive.Id;
            input.Status = ProjectUserStatus.Future;
            input.IsFutureActive = true;
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
            return input;
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest,
            PermissionNames.PmManager_ResourceRequest_SearchAvailableUserForRequest)]
        public async Task<GridResult<ResourceRequestUserDto>> SearchAvailableUserForRequest(GridParam input, DateTime startDate)
        {
            if (startDate.Date < DateTime.Now.Date)
            {
                throw new UserFriendlyException("The start date must be greater than the current time !");
            }

            var projectUsers = WorkScope.GetAll<ProjectUser>()
                                .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed)
                                .Where(x => x.StartTime.Date <= startDate.Date && x.Status == ProjectUserStatus.Present && x.IsFutureActive)
                                .Select(x => new
                                {
                                    UserId = x.UserId,
                                    AllocatePercentage = x.AllocatePercentage
                                });
            var users = WorkScope.GetAll<User>().Where(x => x.IsActive&& x.UserType != UserType.FakeUser)
                                .Select(x => new ResourceRequestUserDto
                                {
                                    UserId = x.Id,
                                    UserName = x.UserName,
                                    UserType = x.UserType,
                                    EmailAddress = x.EmailAddress,
                                    Branch = x.Branch,
                                    FullName = x.Name + " " + x.Surname,
                                    AvatarPath = "/avatars/" + x.AvatarPath,
                                    Undisposed = projectUsers.Any(y => y.UserId == x.Id) ? (100 - projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage)) : 100
                                });

            return await users.GetGridResult(users, input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource,
            PermissionNames.PmManager_ResourceRequest_AvailableResource)]
        public async Task<GridResult<AvailableResourceDto>> AvailableResource(GridParam input, DateTime? startTime, long? skillId)
        {
            var projectUsers = WorkScope.GetAll<ProjectUser>()
                               .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed && x.Status == ProjectUserStatus.Present && x.IsFutureActive)
                               .Select(x => new
                               {
                                   UserId = x.UserId,
                                   ProjectId = x.ProjectId,
                                   ProjectName = x.Project.Name,
                                   AllocatePercentage = x.AllocatePercentage
                               });
            var userSkills = WorkScope.GetAll<UserSkill>().Include(x => x.Skill);
            var userPlanFuture = WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Future && x.IsFutureActive)
                       .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed);

            var users = WorkScope.GetAll<User>().Where(x => x.IsActive).Where(x => x.UserType != UserType.FakeUser)
                                .Select(x => new AvailableResourceDto
                                {
                                    UserId = x.Id,
                                    UserType = x.UserType,
                                    FullName = x.Name + " " + x.Surname,
                                    EmailAddress = x.EmailAddress,
                                    Branch = x.Branch,
                                    AvatarPath = "/avatars/" + x.AvatarPath,
                                    Projects = projectUsers.Where(y => y.UserId == x.Id && y.AllocatePercentage > 0)
                                    .Select(x => new ProjectBaseDto
                                    {
                                        ProjectId = x.ProjectId,
                                        ProjectName = x.ProjectName,
                                    }).ToList(),
                                    Used = (projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage)>0)? projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage):0,
                                    ProjectUserPlans = userPlanFuture.Where(pu => pu.UserId == x.Id).Select(p => new ProjectUserPlan
                                    {
                                        ProjectName = p.Project.Name,
                                        StartTime = p.StartTime.Date,
                                        AllocatePercentage = p.AllocatePercentage
                                    }).ToList(),
                                    ListSkills = userSkills.Where(uk => uk.UserId == x.Id).Select(uk => new Skills.Dto.SkillDto
                                    {
                                        Id = uk.SkillId,
                                        Name = uk.Skill.Name
                                    }).ToList(),
                                }).Where(x=> !skillId.HasValue || userSkills.Where(y => y.UserId == x.UserId).Select(y => y.SkillId).Contains(skillId.Value));
            return await users.GetGridResult(users, input);
        }

        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_GetProjectForDM,
        PermissionNames.PmManager_ResourceRequest_SearchAvailableUserForRequest)]
        public async Task<ProjectForDMDto> GetProjectForDM(long projectId, long pmReportId)
        {
            var problemsOfTheWeek = await _pMReportProjectIssueAppService.ProblemsOfTheWeek(projectId, pmReportId);
            var projectUsers = await WorkScope.GetAll<ProjectUser>()
                                .Include(x => x.User).Include(x => x.Project).ThenInclude(x => x.PM)
                                .Where(x => x.ProjectId == projectId).ToListAsync();
            var result = (from pu in projectUsers
                          group pu by new { pu.Project.Name, pu.ProjectId, pu.Project.PM.FullName } into pus
                          select new ProjectForDMDto
                          {
                              ProjectName = pus.Key.Name,
                              PMName = pus.Key.FullName,
                              ListUsers = pus
                              .Where(x => x.Status == ProjectUserStatus.Present)
                              .Where(x => x.AllocatePercentage > 0)
                              .Select(u => new UserBaseDto
                              {
                                  FullName = u.User.FullName,
                                  EmailAddress = u.User.EmailAddress,
                                  AvatarPath = "/avatars/" + u.User.AvatarPath,
                                  UserType = u.User.UserType,
                                  Branch = u.User.Branch
                              }).ToList(),
                              ProblemsOfTheWeek = problemsOfTheWeek
                          }).FirstOrDefault();
            return result;
        }
        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AvailableResourceFuture,
            PermissionNames.PmManager_ResourceRequest_AvailableResourceFuture)]
        public async Task<GridResult<AvailableResourceFutureDto>> AvailableResourceFuture(GridParam input)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Future && x.IsFutureActive)
                        .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed)
                        .Select(x => new AvailableResourceFutureDto
                        {
                            Id = x.Id,
                            UserId = x.UserId,
                            UserName = x.User.Name,
                            AvatarPath = "/avatars/" + x.User.AvatarPath,
                            Branch = x.User.Branch,
                            EmailAddress = x.User.EmailAddress,
                            FullName = x.User.Name + " " + x.User.Surname,
                            UserType = x.User.UserType,
                            Projectid = x.ProjectId,
                            ProjectName = x.Project.Name,
                            StartDate = x.StartTime.Date,
                            Use = x.AllocatePercentage
                        });
            query = query.Where(x => x.UserType != UserType.FakeUser);
            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_PlanUser,
            PermissionNames.PmManager_ResourceRequest_PlanUser)]
        public async Task<ProjectUser> PlanUser(PlanUserDto input)
        {
            var projectUsers = WorkScope.GetAll<ProjectUser>()
                               .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed &&
                               x.Status == ProjectUserStatus.Future && x.IsFutureActive);

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            if (input.StartTime.Date <= DateTime.Now.Date)
                throw new UserFriendlyException("The start date must be greater than the current time !");

            var isExist = projectUsers.Any(x => x.ProjectId == input.ProjectId && x.UserId == input.UserId && x.StartTime == input.StartTime);
            if (isExist)
            {
                throw new UserFriendlyException($"Project User already exist in {input.StartTime.Date} !");
            }

            var projectUser = new ProjectUser
            {
                UserId = input.UserId,
                ProjectId = input.ProjectId,
                ProjectRole = input.ProjectRole,
                AllocatePercentage = input.PercentUsage,
                StartTime = input.StartTime,
                Status = ProjectUserStatus.Future,
                IsExpense = input.IsExpense,
                IsFutureActive = true,
                PMReportId = pmReportActive.Id,
                Note = input.Note
            };
            input.Id = await WorkScope.InsertAndGetIdAsync(projectUser);

            return projectUser;
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ApproveUser,
            PermissionNames.PmManager_ResourceRequest_ApproveUser)]
        public async Task<ProjectUserDto> ApproveUser(ProjectUserDto input)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(input.Id);
            if (projectUser.Status != ProjectUserStatus.Future)
            {
                throw new UserFriendlyException("Can't approve request not in the future !");
            }

            input.Status = ProjectUserStatus.Present;
            input.ProjectId = projectUser.ProjectId;
            input.IsFutureActive = true;
            await _projectUserAppService.Update(input);

            var pu = WorkScope.GetAll<ProjectUser>().Where(x => x.Id != input.Id && x.ProjectId == input.ProjectId && x.UserId == input.UserId && x.Status == ProjectUserStatus.Present);
            foreach (var item in pu)
            {
                item.Status = ProjectUserStatus.Past;
                await WorkScope.UpdateAsync(item);
            }

            return input;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_RejectUser,
            PermissionNames.PmManager_ResourceRequest_RejectUser)]
        public async Task RejectUser(long projectUserId)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(projectUserId);

            await WorkScope.DeleteAsync(projectUser);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_Create, PermissionNames.PmManager_ResourceRequest_Create)]
        public async Task<ResourceRequestDto> Create(ResourceRequestDto input)
        {
            var isExist = await WorkScope.GetAll<ResourceRequest>().AnyAsync(x => x.Name == input.Name && x.ProjectId == input.ProjectId && x.TimeNeed == input.TimeNeed);

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ResourceRequest>(input));
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
                var projectUri = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri);
                //get name project
                var query = WorkScope.GetAll<Project>().Where(x => x.Id == input.ProjectId)
                                    .Select(x => new GetProjectDto
                                    {
                                        Name = x.Name,
                                    });
                var result = await query.FirstOrDefaultAsync();
                var nameProject = result.Name;
                //
                var room = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuRoom);
                var admin = await WorkScope.GetAsync<User>(AbpSession.UserId.Value);
                var message = $"PM {admin.UserName} đã tạo mới request {input.Name} cho dự án {nameProject}.";
                var alias = "Nhắc việc NCC";
                var ListAttach = new List<attachment>();
                ListAttach.Add(new attachment
                {
                    title = "Mời bạn click vào đây để xem chi tiết công việc nhé.",
                    titlelink = $"{projectUri.Replace("-api", String.Empty)}app/resource-request"
                });
                var postMessage = new PostMessage
                {
                    channel = room,
                    text = message.ToString(),
                    alias = alias,
                    attachments = ListAttach
                };
                await _komuService.PostMessage(postMessage, DecryptContent.data);

                await _komuService.Logout(DecryptContent.data);
            }
            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_Update, PermissionNames.PmManager_ResourceRequest_Update)]
        public async Task<ResourceRequestDto> Update(ResourceRequestDto input)
        {
            var resourceRequest = await WorkScope.GetAsync<ResourceRequest>(input.Id);

            await WorkScope.UpdateAsync(ObjectMapper.Map<ResourceRequestDto, ResourceRequest>(input, resourceRequest));
            //Komu bot nhắn tin đến nhóm
            if (input.Status == ResourceRequestStatus.DONE|| input.Status == ResourceRequestStatus.CANCELLED)
            {
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
                    var projectUri = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri);
                    //get name project
                    var query = WorkScope.GetAll<Project>().Where(x => x.Id == input.ProjectId)
                                        .Select(x => new GetProjectDto
                                        {
                                            Name = x.Name,
                                        });
                    var result = await query.FirstOrDefaultAsync();
                    var nameProject = result.Name;
                    //
                    var room = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuRoom);
                    var admin = await WorkScope.GetAsync<User>(AbpSession.UserId.Value);
                    var message = string.Empty;
                    if (input.Status == ResourceRequestStatus.DONE)
                    {
                        message = $"Request {input.Name} cho dự án {nameProject} đã được {admin.UserName} chuyển sang trạng thái hoàn thành.";
                    }
                    else if (input.Status == ResourceRequestStatus.CANCELLED)
                    {
                        message = $"Request {input.Name} cho dự án {nameProject} đã được huỷ bởi {admin.UserName}.";
                    }
                    var alias = "Nhắc việc NCC";
                    var ListAttach = new List<attachment>();
                    ListAttach.Add(new attachment
                    {
                        title = "Mời bạn click vào đây để xem chi tiết công việc nhé.",
                        titlelink = $"{projectUri.Replace("-api", String.Empty)}app/resource-request"
                    });
                    var postMessage = new PostMessage
                    {
                        channel = room,
                        text = message.ToString(),
                        alias = alias,
                        attachments = ListAttach
                    };
                    await _komuService.PostMessage(postMessage, DecryptContent.data);

                    await _komuService.Logout(DecryptContent.data);
                }
            }
            
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_Delete, PermissionNames.PmManager_ResourceRequest_Delete)]
        public async Task Delete(long resourceRequestId)
        {
            var resourceRequest = await WorkScope.GetAll<ProjectUser>().AnyAsync(x => x.ResourceRequestId == resourceRequestId);
            if (resourceRequest)
                throw new UserFriendlyException("Resource Request can not delete !");

            await WorkScope.DeleteAsync<ResourceRequest>(resourceRequestId);
        }
    }
}
