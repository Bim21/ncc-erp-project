using Abp.Authorization;
using Abp.Configuration;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using NccCore.Uitls;
using Newtonsoft.Json;
using ProjectManagement.APIs.PMReportProjectIssues;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.APIs.ProjectUsers;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.APIs.ResourceRequests.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.NccCore.Helper;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using ProjectManagement.Services.ResourceManager;
using ProjectManagement.Services.ResourceManager.Dto;
using ProjectManagement.Services.ResourceService.Dto;
using ProjectManagement.Users;
using ProjectManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests
{
    [AbpAuthorize]
    public class ResourceRequestAppService : ProjectManagementAppServiceBase
    {
        private readonly ProjectUserAppService _projectUserAppService;
        private readonly PMReportProjectIssueAppService _pMReportProjectIssueAppService;
        private readonly IUserAppService _userAppService;
        private readonly ResourceManager _resourceManager;

        private readonly UserManager _userManager;
        private ISettingManager _settingManager;
        private KomuService _komuService;

        public ResourceRequestAppService(
            ProjectUserAppService projectUserAppService,
            PMReportProjectIssueAppService pMReportProjectIssueAppService,
            KomuService komuService,
            UserManager userManager,
            ResourceManager resourceManager,
            ISettingManager settingManager,
            IUserAppService userAppService)
        {
            _projectUserAppService = projectUserAppService;
            _pMReportProjectIssueAppService = pMReportProjectIssueAppService;
            _komuService = komuService;
            _resourceManager = resourceManager;
            _settingManager = settingManager;
            _userAppService = userAppService;
            _userManager = userManager;
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
                            PMNote = x.PMNote,
                            DMNote = x.DMNote,
                            TimeNeed = x.TimeNeed,
                            TimeDone = x.TimeDone.Value
                        });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ViewAllResourceRequest,
        PermissionNames.PmManager_ResourceRequest_ViewAllResourceRequest)]
        public async Task<GridResult<GetResourceRequestDto>> GetAllPaging(InputGetAllRequestResourceDto input)
        {
            IQueryable<GetResourceRequestDto> query = null;

            var queryRequest = WorkScope.GetAll<ResourceRequest>();

            if (input.SkillIds != null && input.SkillIds.Any())
            {
                var querySkill = WorkScope.GetAll<ResourceRequestSkill>()
                                          .Where(p => input.SkillIds.Contains(p.SkillId));

                query = from request in queryRequest
                        join requestSkill in querySkill on request.Id equals requestSkill.ResourceRequestId
                        select new GetResourceRequestDto
                        {
                            DMNote = request.DMNote,
                            Id = request.Id,
                            IsRecruitmentSend = request.IsRecruitmentSend,
                            Level = request.Level,
                            Name = request.Name,
                            ProjectName = request.Project.Name,
                            PMNote = request.PMNote,
                            Priority = request.Priority,
                            ProjectId = request.ProjectId,
                            RecruitmentUrl = request.RecruitmentUrl,
                            TimeNeed = request.TimeNeed,
                            TimeDone = request.TimeDone,
                            Skills = request.ResourceRequestSkills.Select(p => new GetResourceRequestDto_SkillInfo() { SkillId = p.Id, SkillName = p.Skill.Name }).ToList(),
                            Status = request.Status,
                            PlannedDate = request.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.StartTime).FirstOrDefault(),
                            PlannedEmployee = request.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.User.FullName).FirstOrDefault(),
                            PlannedProjectUserId = request.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.Id).FirstOrDefault(),
                            RequestStartTime = request.CreationTime
                        };
            }
            else
            {
                query = from request in queryRequest
                        select new GetResourceRequestDto
                        {
                            DMNote = request.DMNote,
                            Id = request.Id,
                            IsRecruitmentSend = request.IsRecruitmentSend,
                            Level = request.Level,
                            Name = request.Name,
                            ProjectName = request.Project.Name,
                            PMNote = request.PMNote,
                            Priority = request.Priority,
                            ProjectId = request.ProjectId,
                            RecruitmentUrl = request.RecruitmentUrl,
                            TimeNeed = request.TimeNeed,
                            TimeDone = request.TimeDone,
                            Skills = request.ResourceRequestSkills.Select(p => new GetResourceRequestDto_SkillInfo() { SkillId = p.Id, SkillName = p.Skill.Name }).ToList(),
                            Status = request.Status,
                            PlannedDate = request.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.StartTime).FirstOrDefault(),
                            PlannedEmployee = request.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.User.FullName).FirstOrDefault(),
                            PlannedProjectUserId = request.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.Id).FirstOrDefault(),
                            RequestStartTime = request.CreationTime
                        };
            }

            //Sorting Result
            var sortText = input.Sort.EmptyIfNull().ToLower();

            if (input.SortDirection == SortDirection.ASC)
            {
                switch (sortText)
                {
                    case "priority":
                        {
                            query = query.OrderBy(p => p.Priority);
                            break;
                        }
                    case "project":
                        {
                            query = query.OrderBy(p => p.ProjectName);
                            break;
                        }
                    case "level":
                        {
                            query = query.OrderBy(p => p.Level);
                            break;
                        }
                    case "timeneed":
                        {
                            query = query.OrderBy(p => p.TimeNeed);
                            break;
                        }
                    case "timerequest":
                        {
                            query = query.OrderBy(p => p.RequestStartTime);
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                switch (sortText)
                {
                    case "priority":
                        {
                            query = query.OrderByDescending(p => p.Priority);
                            break;
                        }
                    case "project":
                        {
                            query = query.OrderByDescending(p => p.ProjectName);
                            break;
                        }
                    case "level":
                        {
                            query = query.OrderByDescending(p => p.Level);
                            break;
                        }
                    case "timeneed":
                        {
                            query = query.OrderByDescending(p => p.TimeNeed);
                            break;
                        }
                    case "timerequest":
                        {
                            query = query.OrderByDescending(p => p.RequestStartTime);
                            break;
                        }
                    default:
                        break;
                }
            }

            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        public async Task<List<ResourceRequestSelectListDto>> GetSkills()
        {
            var query = WorkScope.GetAll<Skill>()
                                 .Select(p => new ResourceRequestSelectListDto()
                                 {
                                     Id = p.Id,
                                     Name = p.Name
                                 });

            return await query.ToListAsync();
        }

        [HttpGet]
        public async Task<List<ResourceRequestSelectListDto>> GetLevels()
        {
            await Task.CompletedTask;

            var result = Enum.GetValues(typeof(UserLevel_ResourceRequest))
                             .Cast<UserLevel_ResourceRequest>()
                             .Select(p => new ResourceRequestSelectListDto()
                             {
                                 Id = p.GetHashCode(),
                                 Name = p.ToString()
                             })
                             .ToList();

            return result;
        }

        [HttpGet]
        public async Task<List<ResourceRequestSelectListDto>> GetPriorities()
        {
            await Task.CompletedTask;

            var result = Enum.GetValues(typeof(Priority))
                             .Cast<Priority>()
                             .Select(p => new ResourceRequestSelectListDto()
                             {
                                 Id = p.GetHashCode(),
                                 Name = p.ToString()
                             })
                             .ToList();

            return result;
        }

        [HttpGet]
        public async Task<List<ResourceRequestSelectListDto>> GetStatuses()
        {
            await Task.CompletedTask;

            var result = Enum.GetValues(typeof(ResourceRequestStatus))
                             .Cast<ResourceRequestStatus>()
                             .Select(p => new ResourceRequestSelectListDto()
                             {
                                 Id = p.GetHashCode(),
                                 Name = p.ToString()
                             })
                             .ToList();

            return result;
        }



        [HttpGet]
        public async Task<List<GetResourceRequestDto>> GetAllRequestByProjectId(long projectId)
        {
            var query = WorkScope.GetAll<ResourceRequest>()
                                 .Where(p => p.ProjectId == projectId)
                                 .OrderByDescending(p => p.CreationTime)
                                 .Select(p => new GetResourceRequestDto()
                                 {
                                     DMNote = p.DMNote,
                                     Id = p.Id,
                                     IsRecruitmentSend = p.IsRecruitmentSend,
                                     Level = p.Level,
                                     Name = p.Name,
                                     ProjectName = p.Project.Name,
                                     PMNote = p.PMNote,
                                     Priority = p.Priority,
                                     ProjectId = p.ProjectId,
                                     RecruitmentUrl = p.RecruitmentUrl,
                                     TimeNeed = p.TimeNeed,
                                     TimeDone = p.TimeDone,
                                     Skills = p.ResourceRequestSkills.Select(p => new GetResourceRequestDto_SkillInfo() { SkillId = p.Id, SkillName = p.Skill.Name }).ToList(),
                                     Status = p.Status,
                                     PlannedDate = p.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.StartTime).FirstOrDefault(),
                                     PlannedEmployee = p.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.User.FullName).FirstOrDefault(),
                                     PlannedProjectUserId = p.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.Id).FirstOrDefault(),
                                     RequestStartTime = p.CreationTime
                                 });

            return await query.ToListAsync();
        }

        [HttpGet]
        public async Task<GetResourceRequestDto> GetResourceRequestById(long requestId)
        {
            var query = WorkScope.GetAll<ResourceRequest>()
                     .Where(p => p.Id == requestId)
                     .Select(p => new GetResourceRequestDto()
                     {
                         DMNote = p.DMNote,
                         Id = p.Id,
                         IsRecruitmentSend = p.IsRecruitmentSend,
                         Level = p.Level,
                         Name = p.Name,
                         ProjectName = p.Project.Name,
                         PMNote = p.PMNote,
                         Priority = p.Priority,
                         ProjectId = p.ProjectId,
                         RecruitmentUrl = p.RecruitmentUrl,
                         TimeNeed = p.TimeNeed,
                         TimeDone = p.TimeDone,
                         Skills = p.ResourceRequestSkills.Select(p => new GetResourceRequestDto_SkillInfo() { SkillId = p.Id, SkillName = p.Skill.Name }).ToList(),
                         Status = p.Status,
                         PlannedDate = p.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.StartTime).FirstOrDefault(),
                         PlannedEmployee = p.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.User.FullName).FirstOrDefault(),
                         PlannedProjectUserId = p.ProjectUsers.OrderByDescending(p => p.UserId).Select(x => x.Id).FirstOrDefault(),
                         RequestStartTime = p.CreationTime
                     });

            return await query.FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ResourceRequestDto> CreateRequestByProject(ResourceRequestDto model)
        {
            model.Status = ResourceRequestStatus.APPROVE;

            var project = await WorkScope.GetAll<Project>().FirstOrDefaultAsync(x => x.Id == model.ProjectId);
            if (project == null)
                throw new UserFriendlyException("Project doesn't exist");

            if (!model.SkillIds.Any())
                throw new UserFriendlyException("Select at least 1 skill");

            model.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ResourceRequest>(model));

            //Create ResourceRequestSkill, amount based on Skills selected
            for (int j = 0; j < model.SkillIds.Count(); j++)
            {
                var skillModel = new ResourceRequestSkill()
                {
                    IsDeleted = false,
                    ResourceRequestId = model.Id,
                    SkillId = model.SkillIds[j],
                    Quantity = 1
                };

                await WorkScope.InsertAsync(skillModel);
            }

            SendKomuNotify(model.Name, project.Name, model.Status);

            return model;
        }

        [HttpPost]
        public async Task<ResourceRequestDto> UpdateRequestByProject(ResourceRequestDto model)
        {
            var project = await WorkScope.GetAll<Project>().FirstOrDefaultAsync(x => x.Id == model.ProjectId);
            if (project == null)
            {
                throw new UserFriendlyException("Project doesn't exist");
            }

            var resourceRequest = await WorkScope.GetAsync<ResourceRequest>(model.Id);

            var requestPlan = await WorkScope.GetAll<ProjectUser>().AnyAsync(p => p.ResourceRequestId == resourceRequest.Id);
            if (!requestPlan && resourceRequest.ProjectId != model.ProjectId)
                throw new UserFriendlyException("Cannot change project of a planned resource request");

            //Update skill
            var oldSkillIdsList = WorkScope.GetAll<ResourceRequestSkill>()
                                                .Where(p => p.ResourceRequestId == model.Id)
                                                .Select(p => p.SkillId).ToList();

            var skillIdsToAdd = model.SkillIds.Where(p => !oldSkillIdsList.Contains(p));
            var skillIdsToRemove = oldSkillIdsList.Where(p => !model.SkillIds.Contains(p));

            foreach (var skillId in skillIdsToAdd)
            {
                var skillModel = new ResourceRequestSkill()
                {
                    ResourceRequestId = model.Id,
                    SkillId = skillId,
                    Quantity = 1
                };

                await WorkScope.InsertAsync(skillModel);
            }

            foreach (var skillId in skillIdsToRemove)
            {
                await WorkScope.DeleteAsync<ResourceRequestSkill>(skillId);
            }

            resourceRequest.ProjectId = model.ProjectId;
            resourceRequest.DMNote = model.DMNote;
            resourceRequest.PMNote = model.PMNote;
            resourceRequest.TimeNeed = model.TimeNeed;
            resourceRequest.Status = model.Status;
            resourceRequest.Level = model.Level;
            resourceRequest.Priority = model.Priority;

            await WorkScope.UpdateAsync(resourceRequest);

            SendKomuNotify(model.Name, project.Name, model.Status);

            return model;
        }

        [HttpPost]
        public async Task DeleteRequestByProject(long resourceRequestId)
        {
            await Delete(resourceRequestId);
        }

        [HttpPost]
        public async Task<ResourceRequestCancelDto> CancelRequestByProject(ResourceRequestCancelDto model)
        {
            return await Cancel(model);
        }

        [HttpPost]
        public async Task<ResourceRequestNoteUpdateDto> UpdateRequestPmNote(ResourceRequestNoteUpdateDto model)
        {
            var resourceRequest = WorkScope.Get<ResourceRequest>(model.ResourceRequestId);
            if (resourceRequest == null)
                throw new UserFriendlyException("Request doesn't exist");

            resourceRequest.PMNote = model.PMNote;

            await WorkScope.UpdateAsync(resourceRequest);

            return model;
        }

        [HttpPost]
        public async Task<ResourceRequestNoteUpdateDto> UpdateRequestHpmNote(ResourceRequestNoteUpdateDto model)
        {
            var resourceRequest = WorkScope.Get<ResourceRequest>(model.ResourceRequestId);
            if (resourceRequest == null)
                throw new UserFriendlyException("Request doesn't exist");

            resourceRequest.DMNote = model.HPMNote;

            await WorkScope.UpdateAsync(resourceRequest);

            return model;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_GetSkillDetail,
            PermissionNames.PmManager_ResourceRequest_GetSkillDetail)]
        public async Task<List<GetSkillDetailDto>> GetSkillDetail(long resourceRequestId)
        {
            var query = WorkScope.GetAll<ResourceRequestSkill>().Where(x => x.ResourceRequestId == resourceRequestId)
                                    .Select(x => new GetSkillDetailDto
                                    {
                                        Id = x.Id,
                                        ResourceRequestId = x.ResourceRequestId,
                                        ResourceRequestName = x.ResourceRequest.Name,
                                        SkillId = x.SkillId,
                                        SkillName = x.Skill.Name,
                                        Quantity = x.Quantity

                                    });
            return await query.ToListAsync();
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
            var isExistProjectUser = await WorkScope
                .GetAll<ProjectUser>()
                .AnyAsync(x =>
                            x.ProjectId == input.ProjectId &&
                            x.UserId == input.UserId &&
                            x.Status == input.Status &&
                            x.StartTime.Date == input.StartTime.Date &&
                            x.ProjectRole == x.ProjectRole &&
                            x.AllocatePercentage == input.AllocatePercentage);
            if (isExistProjectUser)
            {
                throw new UserFriendlyException("User already exist in project !");
            }

            var resourceRequest = await WorkScope
                .GetAsync<ResourceRequest>((long)input.ResourceRequestId);
            if (input.StartTime.Date < resourceRequest.TimeNeed.Date)
            {
                throw new UserFriendlyException("Start date must be greater than request date !");
            }

            var pmReportActive = await WorkScope
                .GetAll<PMReport>()
                .FirstOrDefaultAsync(x => x.IsActive);
            if (pmReportActive == null)
            {
                throw new UserFriendlyException("Can't find any active reports !");
            }

            input.ProjectId = resourceRequest.ProjectId;
            input.PMReportId = pmReportActive.Id;
            input.Status = ProjectUserStatus.Future;
            input.IsFutureActive = true;
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectUser>(input));

            if (input.Status == ProjectUserStatus.Present)
            {
                await ChangeProjectUserStatus(input.Id, input.ProjectId, input.UserId);
            }

            var pmKomuId = await _userAppService.UpdateKomuId(AbpSession.UserId.Value);
            var pmUserName = string.Empty;
            if (!pmKomuId.HasValue)
            {
                pmUserName = UserManager.GetUserById(AbpSession.UserId.Value).UserName;
            }
            var employee = UserManager.GetUserById(input.UserId);
            employee.UserName = UserHelper.GetUserName(employee.EmailAddress) ?? employee.UserName;
            var projectName = WorkScope.Get<Project>(input.ProjectId)?.Name;

            var komuMessage = new StringBuilder();
            komuMessage.Append($"Từ ngày **{input.StartTime:dd/MM/yyyy}**, ");
            komuMessage.Append($"PM {(pmKomuId.HasValue ? "<@" + pmKomuId + ">" : "**" + pmUserName + "**")} request ");
            komuMessage.Append($"**{employee.UserName}** làm việc ở dự án ");
            komuMessage.Append($"**{projectName}**");
            await _komuService.NotifyToChannel(new KomuMessage
            {
                CreateDate = DateTimeUtils.GetNow(),
                Message = komuMessage.ToString(),
            },
            ChannelTypeConstant.PM_CHANNEL);
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
            var users = WorkScope.GetAll<User>().Where(x => x.IsActive && x.UserType != UserType.FakeUser)
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


        //[HttpPost]
        //[AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource,
        //PermissionNames.PmManager_ResourceRequest_AvailableResource)]
        //public async Task<GridResult<AvailableResourceDto>> AvailableResource(GridParam input, DateTime? startTime, long? skillId)
        //{
        //    input.SearchText = Regex.Replace(input.SearchText, @"\s+", " ");
        //    var skill = input.FilterItems.Where(x => x.PropertyName == "skill").FirstOrDefault();
        //    if (skill != null)
        //    {
        //        skillId = long.Parse(skill.Value.ToString());
        //        input.FilterItems.Remove(skill);
        //    }
        //    var projectUsers = WorkScope.GetAll<ProjectUser>()
        //                       .Where(x => x.Project.Status != ProjectStatus.Potential 
        //                       && x.Project.Status != ProjectStatus.Closed && x.Status == ProjectUserStatus.Present 
        //                       && x.IsFutureActive)
        //                       .Select(x => new
        //                       {
        //                           UserId = x.UserId,
        //                           ProjectId = x.ProjectId,
        //                           ProjectName = x.Project.Name,
        //                           AllocatePercentage = x.AllocatePercentage,
        //                           Status = x.Status,
        //                           ProjectRole = x.ProjectRole,
        //                           StartTime = x.StartTime,
        //                       });
        //    var userSkills = WorkScope.GetAll<UserSkill>().Include(x => x.Skill);
        //    var userPlanFuture = WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Future && x.IsFutureActive)
        //               .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed);
        //    var filterProjectName = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectName") : null;
        //    var filterprojectUserPlans = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectUserPlans") : null;
        //    var projectUserPresent = projectUsers.Where(x => x.Status == ProjectUserStatus.Present).OrderByDescending(x => x.StartTime);
        //    var users = WorkScope.GetAll<User>().Where(x => x.IsActive).Where(x => x.UserType != UserType.FakeUser)
        //                        .Select(x => new AvailableResourceDto
        //                        {
        //                            UserId = x.Id,
        //                            UserType = x.UserType,
        //                            FullName = x.Name + " " + x.Surname,
        //                            NormalFullName = x.Surname + " " + x.Name,
        //                            EmailAddress = x.EmailAddress,
        //                            Branch = x.Branch,
        //                            UserLevel = x.UserLevel,
        //                            AvatarPath = "/avatars/" + x.AvatarPath,
        //                            DateStartPool = projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id) != null ?
        //                            projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id).StartTime : x.CreationTime,
        //                            WorkingProjects = projectUsers.Where(y => y.UserId == x.Id && y.AllocatePercentage > 0)
        //                            .Select(x => new WorkingProjectDto
        //                            {
        //                                projectId = x.ProjectId,
        //                                ProjectName = x.ProjectName,
        //                                ProjectRole = x.ProjectRole,
        //                                StartTime = x.StartTime
        //                            }).ToList(),
        //                            Used = (projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) > 0) ? projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) : 0,
        //                            ProjectUserPlans = userPlanFuture.Where(pu => pu.UserId == x.Id).Select(p => new ProjectUserPlan
        //                            {
        //                                ProjectName = p.Project.Name,
        //                                StartTime = p.StartTime.Date,
        //                                AllocatePercentage = p.AllocatePercentage
        //                            }).ToList(),
        //                            ListSkills = userSkills.Where(uk => uk.UserId == x.Id).Select(uk => new Skills.Dto.SkillDto
        //                            {
        //                                Id = uk.SkillId,
        //                                Name = uk.Skill.Name
        //                            }).ToList(),
        //                            PoolNote = x.PoolNote,
        //                            StarRate = x.StarRate,
        //                            TotalFreeDay = WorkScope.GetAll<ProjectUser>().Where(y => y.UserId == x.Id).Any(y => y.Project.Status == ProjectStatus.InProgress && y.Status == ProjectUserStatus.Present && y.AllocatePercentage >= 100) ? //điều kiện đang có trong 1 dự dán
        //                            0 :
        //                            WorkScope.GetAll<ProjectUser>().Where(z => z.UserId == x.Id).Any(z => z.Status == ProjectUserStatus.Present && z.AllocatePercentage == 0) ? //điều kiện đã được release ra khỏi dự án
        //                            (DateTime.Now - WorkScope.GetAll<ProjectUser>().Where(k => k.UserId == x.Id && k.Status == ProjectUserStatus.Present && k.AllocatePercentage == 0).OrderByDescending(k => k.StartTime).Select(k => k.StartTime).FirstOrDefault()).Days //lấy ra ngày release gần nhất
        //                            : (DateTime.Now - x.CreationTime).Days //lấy ra ngày tạo
        //                            ,
        //                        }).Where(x => !skillId.HasValue || userSkills.Where(y => y.UserId == x.UserId).Select(y => y.SkillId).Contains(skillId.Value));
        //    if (filterProjectName != null)
        //    {
        //        string searchByProject = filterProjectName.Value.ToString();
        //        input.FilterItems.Remove(filterProjectName);
        //        var listIdContainsProjectName = projectUsers.Where(x => x.ProjectName.Contains(searchByProject));
        //        users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
        //    }
        //    if (filterprojectUserPlans != null)
        //    {
        //        string searchByprojectUserPlans = filterprojectUserPlans.Value.ToString();
        //        input.FilterItems.Remove(filterprojectUserPlans);
        //        var listIdContainsProjectName = userPlanFuture.Where(x => x.Project.Name.Contains(searchByprojectUserPlans));
        //        users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
        //    }
        //    return await users.GetGridResult(users, input);
        //}


        [HttpPost]
        [AbpAuthorize]
        public async Task<GridResult<GetAllResourceDto>> GetVendorResource(InputGetResourceDto input)
        {
            return await _resourceManager.GetResources(input, true);
        }

        [HttpPost]
        [AbpAuthorize]
        public async Task<GridResult<GetAllResourceDto>> GetAllResource(InputGetResourceDto input)
        {
            return await _resourceManager.GetResources(input, false);
        }


        [HttpPost]
        [AbpAuthorize]
        public async Task<GridResult<GetAllPoolResourceDto>> GetAllPoolResource(InputGetResourceDto input)
        {
            return await _resourceManager.GetAllPoolResource(input);
        }


        [HttpDelete]
        [AbpAuthorize]
        public async Task CancelResourcePlan(long projectUserId)
        {
            var pu = await WorkScope.GetAll<ProjectUser>()
                .Include(s => s.User)
                .Include(s => s.Project)
                .Include(s => s.Project.PM)
                .Where(s => s.Id == projectUserId)
                .Select(s => new
                {
                    ProjectUser = s,
                    ProjectCode = s.Project.Code,
                    ProjectName = s.Project.Name,
                    EmployeeName = s.User.Name + " " + s.User.Surname,
                    PMName = s.Project.PM.Name + " " + s.Project.PM.Surname,
                    InOutString = s.AllocatePercentage > 0 ? "vào dự án" : "ra dự án"
                }).FirstOrDefaultAsync();

            var projectUser = pu.ProjectUser;

            if (projectUser.Status != ProjectUserStatus.Future)
            {
                throw new UserFriendlyException(String.Format("projectUser with id {0} is not future!", projectUser.Id));
            }
            if (projectUser.CreatorUserId != AbpSession.UserId.Value)
            {
                bool allowCancelAnyPlan = await PermissionChecker.IsGrantedAsync(PermissionNames.DeliveryManagement_ResourceRequest_CancelAnyPlanResource);
                if (!allowCancelAnyPlan)
                {
                    throw new UserFriendlyException(String.Format("You don't have permission to cancel resource plan of other people!"));
                }
            }

            await WorkScope.DeleteAsync(projectUser);

            if (!pu.ProjectCode.Equals(AppConsts.CHO_NGHI_PROJECT_CODE, StringComparison.OrdinalIgnoreCase))
            {
                var komuMessage = new StringBuilder();
                komuMessage.Append($"PM **{pu.PMName}** đã **HỦY** plan: ");
                komuMessage.Append($"**{pu.EmployeeName}** {pu.InOutString } **{pu.ProjectName}** ");
                komuMessage.Append($"từ ngày **{projectUser.StartTime:dd/MM/yyyy}**, ");

                await _komuService.NotifyToChannel(new KomuMessage
                {
                    CreateDate = DateTimeUtils.GetNow(),
                    Message = komuMessage.ToString(),
                },
                ChannelTypeConstant.PM_CHANNEL);
            }

        }

        [HttpPut]
        public async Task updateUserPoolNote(UpdateUserPoolNoteDto input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            user.PoolNote = input.Note;
            await WorkScope.UpdateAsync<User>(user);
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
        public async Task<PlanUserDto> PlanUser(PlanUserDto input)
        {
            var projectUsers = WorkScope
                .GetAll<ProjectUser>()
                .Where(x =>
                            x.Project.Status != ProjectStatus.Potential &&
                            x.Project.Status != ProjectStatus.Closed &&
                            x.Status == ProjectUserStatus.Future &&
                            x.IsFutureActive);

            var pmReportActive = await WorkScope
                .GetAll<PMReport>()
                .FirstOrDefaultAsync(x => x.IsActive);
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            var isExistProjectUser = projectUsers
                .Any(x =>
                        x.ProjectId == input.ProjectId &&
                        x.UserId == input.UserId &&
                        x.StartTime == input.StartTime);
            if (isExistProjectUser)
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

            var project = WorkScope.Get<Project>(input.ProjectId);
            if (!project.Code.Equals(AppConsts.CHO_NGHI_PROJECT_CODE))
            {
                var pmKomuId = await _userAppService.UpdateKomuId(AbpSession.UserId.Value);
                var pmUserName = string.Empty;
                if (!pmKomuId.HasValue)
                {
                    pmUserName = UserManager.GetUserById(AbpSession.UserId.Value).UserName;
                }
                var employee = UserManager.GetUserById(input.UserId);
                employee.UserName = UserHelper.GetUserName(employee.EmailAddress) ?? employee.UserName;


                var komuMessage = new StringBuilder();
                komuMessage.Append($"Từ ngày **{input.StartTime:dd/MM/yyyy}**, ");
                komuMessage.Append($"PM {(pmKomuId.HasValue ? "<@" + pmKomuId + ">" : "**" + pmUserName + "**")} request ");
                komuMessage.Append($"**{employee.UserName}** làm việc ở dự án ");
                komuMessage.Append($"**{project.Name}**");
                await _komuService.NotifyToChannel(new KomuMessage
                {
                    CreateDate = DateTimeUtils.GetNow(),
                    Message = komuMessage.ToString(),
                },
                ChannelTypeConstant.PM_CHANNEL);
            }
            return input;
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
        public async Task<ResourceRequestDto> Create(ResourceRequestDto model)
        {
            model.Status = ResourceRequestStatus.APPROVE;

            var project = await WorkScope.GetAll<Project>().FirstOrDefaultAsync(x => x.Id == model.ProjectId);
            if (project == null)
                throw new UserFriendlyException("Project doesn't exist");

            if (model.Quantity <= 0)
                throw new UserFriendlyException("Quantity must be equal or greater than 1");

            if (!model.SkillIds.Any())
                throw new UserFriendlyException("Select at least 1 skill");

            //Create ResourceRequest, amount based on quantity
            for (int i = 0; i < model.Quantity; i++)
            {
                var resourceRequestModel = new ResourceRequest()
                {
                    DMNote = model.DMNote,
                    Level = model.Level,
                    IsRecruitmentSend = false,
                    Name = model.Name,
                    PMNote = model.PMNote,
                    Priority = model.Priority,
                    ProjectId = model.ProjectId,
                    RecruitmentUrl = string.Empty,
                    Status = model.Status,
                    TimeDone = model.TimeDone,
                    TimeNeed = model.TimeNeed
                };

                var newRequestId = await WorkScope.InsertAndGetIdAsync(resourceRequestModel);

                //Create ResourceRequestSkill, amount based on Skills selected
                for (int j = 0; j < model.SkillIds.Count(); j++)
                {
                    var skillModel = new ResourceRequestSkill()
                    {
                        IsDeleted = false,
                        ResourceRequestId = newRequestId,
                        SkillId = model.SkillIds[j],
                        Quantity = 1
                    };

                    await WorkScope.InsertAsync(skillModel);
                }

                SendKomuNotify(model.Name, project.Name, model.Status);
            }

            return model;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_Update, PermissionNames.PmManager_ResourceRequest_Update)]
        public async Task<GetResourceRequestDto> Update(ResourceRequestDto model)
        {
            var project = await WorkScope.GetAll<Project>().FirstOrDefaultAsync(x => x.Id == model.ProjectId);
            if (project == null)
            {
                throw new UserFriendlyException("Project doesn't exist");
            }

            var resourceRequest = await WorkScope.GetAsync<ResourceRequest>(model.Id);

            var requestPlan = await WorkScope.GetAll<ProjectUser>().AnyAsync(p => p.ResourceRequestId == resourceRequest.Id);
            if (!requestPlan && resourceRequest.ProjectId != model.ProjectId)
                throw new UserFriendlyException("Cannot change project of a planned resource request");

            //Update skill
            var oldSkillIdsList = WorkScope.GetAll<ResourceRequestSkill>()
                                                .Where(p => p.ResourceRequestId == model.Id)
                                                .Select(p => p.SkillId).ToList();

            var skillIdsToAdd = model.SkillIds.Where(p => !oldSkillIdsList.Contains(p));
            var skillIdsToRemove = oldSkillIdsList.Where(p => !model.SkillIds.Contains(p));

            foreach (var skillId in skillIdsToAdd)
            {
                var skillModel = new ResourceRequestSkill()
                {
                    ResourceRequestId = model.Id,
                    SkillId = skillId,
                    Quantity = 1
                };

                await WorkScope.InsertAsync(skillModel);
            }

            foreach (var skillId in skillIdsToRemove)
            {
                await WorkScope.DeleteAsync<ResourceRequestSkill>(skillId);
            }

            resourceRequest.ProjectId = model.ProjectId;
            resourceRequest.DMNote = model.DMNote;
            resourceRequest.PMNote = model.PMNote;
            resourceRequest.TimeNeed = model.TimeNeed;
            resourceRequest.Status = model.Status;
            resourceRequest.Level = model.Level;
            resourceRequest.Priority = model.Priority;

            await WorkScope.UpdateAsync(resourceRequest);

            SendKomuNotify(model.Name, project.Name, model.Status);

            return await GetResourceRequestById(model.Id);
        }

        [HttpPost]
        public async Task<ResourceRequestCancelDto> Cancel(ResourceRequestCancelDto model)
        {
            var resourceRequest = await WorkScope.GetAsync<ResourceRequest>(model.Id);
            if (resourceRequest == null)
                throw new UserFriendlyException("Request doesn't exist");

            resourceRequest.Status = ResourceRequestStatus.CANCELLED;

            await WorkScope.UpdateAsync(resourceRequest);

            SendKomuNotify(resourceRequest.Name, resourceRequest.Project.Name, resourceRequest.Status);

            return model;
        }

        private async void SendKomuNotify(string requestName, string projectName, ResourceRequestStatus requestStatus)
        {
            var user = await WorkScope.GetAsync<User>(AbpSession.UserId.Value);

            var userName = UserHelper.GetUserName(user.EmailAddress);

            if (user != null && !user.KomuUserId.HasValue)
            {
                user.KomuUserId = await _komuService.GetKomuUserId(new KomuUserDto { Username = userName ?? user.UserName }, ChannelTypeConstant.KOMU_USER);
                await WorkScope.UpdateAsync<User>(user);
            }

            var projectUri = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri);

            var link = $"{projectUri.Replace("-api", String.Empty)}app/resource-request";

            var message = new StringBuilder();
            switch (requestStatus)
            {
                case ResourceRequestStatus.DONE:
                    {
                        message.Append($"Request **{requestName}** cho dự án **{projectName}** ");
                        message.AppendLine($"đã được {(user.KomuUserId.HasValue ? "<@" + user.KomuUserId + ">" : "**" + user.UserName + "**")} chuyển sang trạng thái hoàn thành.");
                    }
                    break;
                case ResourceRequestStatus.CANCELLED:
                    {
                        message.Append($"Request **{requestName}** cho dự án **{projectName}** ");
                        message.AppendLine($"đã được huỷ bởi {(user.KomuUserId.HasValue ? "<@" + user.KomuUserId + ">" : "**" + user.UserName + "**")}.");
                    }
                    break;
                case ResourceRequestStatus.APPROVE:
                    {
                        message.AppendLine($"PM {(user.KomuUserId.HasValue ? "<@" + user.KomuUserId.ToString() + ">" : "**" + (userName ?? user.UserName) + "**")} đã tạo mới request **{requestName}** cho dự án **{projectName}**.");
                        message.AppendLine(link);
                    }
                    break;
                case ResourceRequestStatus.PENDING:
                default:
                    return;
            }

            await _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = userName ?? user.UserName,
                Message = message.ToString(),
                CreateDate = DateTimeUtils.GetNow(),
            }, ChannelTypeConstant.PM_CHANNEL);
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_Delete, PermissionNames.PmManager_ResourceRequest_Delete)]
        public async Task Delete(long resourceRequestId)
        {
            var resourceRequest = await WorkScope.GetAsync<ResourceRequest>(resourceRequestId);
            if (resourceRequest == null)
                throw new UserFriendlyException("Request doesnt exist");

            var projectUser = resourceRequest.ProjectUsers.Count > 0;
            if (projectUser)
                throw new UserFriendlyException("Resource Request can not delete !");

            var resourceRequestSkills = WorkScope.GetAll<ResourceRequestSkill>().Where(p => p.ResourceRequestId == resourceRequestId);
            foreach (var requestSkill in resourceRequestSkills)
            {
                await WorkScope.SoftDeleteAsync(requestSkill);
            }

            await WorkScope.SoftDeleteAsync(resourceRequest);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_CreateSkill, PermissionNames.PmManager_ResourceRequest_CreateSkill)]
        public async Task<ResourceRequestSkillDto> CreateSkill(ResourceRequestSkillDto input)
        {
            var isExist = await WorkScope.GetAll<ResourceRequestSkill>().AnyAsync(x => x.SkillId == input.SkillId && x.ResourceRequestId == input.ResourceRequestId);
            if (isExist)
                throw new UserFriendlyException("Can not create !");
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ResourceRequestSkill>(input));
            return input;
        }
        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_DeleteSkill, PermissionNames.PmManager_ResourceRequest_DeleteSkill)]
        public async Task DeleteSkill(long resourceRequestSkillId)
        {
            var resourceRequestSkill = await WorkScope.GetAll<ResourceRequestSkill>().AnyAsync(x => x.Id == resourceRequestSkillId);
            if (!resourceRequestSkill)
                throw new UserFriendlyException("Resource Request Skill can not delete !");

            await WorkScope.DeleteAsync<ResourceRequestSkill>(resourceRequestSkillId);
        }

        [HttpGet]
        public async Task<ResourceRequestPlanDto> GetResourceRequestPlan(long projectUserId)
        {
            await Task.CompletedTask;
            var projectUser = WorkScope.Get<ProjectUser>(projectUserId);
            if (projectUser == null)
                return null;
            else
                return new ResourceRequestPlanDto()
                {
                    ProjectUserId = projectUser.Id,
                    UserId = projectUser.UserId,
                    UserName = WorkScope.Get<User>(projectUser.UserId).FullName,
                    JoinDate = projectUser.StartTime,
                    ResourceRequestId = projectUser.ResourceRequestId
                };
        }

        [HttpPost]
        public async Task<ResourceRequestPlanDto> CreateResourceRequestPlan(ResourceRequestPlanDto model)
        {
            var resourceRequest = WorkScope.Get<ResourceRequest>((long)model.ResourceRequestId);

            if (resourceRequest == null)
                throw new UserFriendlyException("Invalid resource request");

            var reportProject = WorkScope.GetAll<PMReportProject>().Where(p => p.ProjectId == resourceRequest.ProjectId).FirstOrDefault();

            var projectUser = new ProjectUser()
            {
                UserId = model.UserId,
                ProjectId = resourceRequest.ProjectId,
                ProjectRole = ProjectUserRole.DEV,
                AllocatePercentage = 100,
                StartTime = resourceRequest.TimeNeed,
                Status = ProjectUserStatus.Future,
                IsExpense = false,
                ResourceRequestId = model.ResourceRequestId,
                PMReportId = reportProject.PMReportId,
                IsFutureActive = true,
                IsPool = false
            };

            model.ProjectUserId = await WorkScope.InsertAndGetIdAsync(projectUser);

            return model;
        }

        [HttpPost]
        public async Task<ResourceRequestPlanDto> UpdateResourceRequestPlan(ResourceRequestPlanDto model)
        {
            var projectUser = WorkScope.Get<ProjectUser>(model.ProjectUserId);

            if (projectUser == null)
                throw new UserFriendlyException($"Project user not found with id : {model.ProjectUserId}");

            projectUser.UserId = model.UserId;
            projectUser.StartTime = model.JoinDate;

            await WorkScope.UpdateAsync(projectUser);

            return model;
        }

        [HttpDelete]
        public async Task DeleteResourceRequestPlan(long id)
        {
            var projectUser = WorkScope.Get<ProjectUser>(id);

            if (projectUser == null)
                throw new UserFriendlyException("Invalid plan");

            await WorkScope.SoftDeleteAsync(projectUser);
        }

        [HttpPost]
        public async Task<List<ResourceRequestPlanUserDto>> GetResourceRequestPlanUser(string keyword)
        {
            var query = WorkScope.GetAll<User>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim().ToLower();

                query = query.Where(p => p.UserName.ToLower().Contains(keyword)
                                        || p.Surname.ToLower().Contains(keyword)
                                        || p.FullName.ToLower().Contains(keyword)
                                        || p.EmailAddress.ToLower().Contains(keyword));
            }

            var result = await query.Select(p => new ResourceRequestPlanUserDto()
            {
                UserId = p.Id,
                Email = p.EmailAddress,
                Name = p.Name,
                Surname = p.Surname,
                Fullname = p.FullName
            }).ToListAsync();

            return result;
        }

        #region PRIVATE API
        private async Task ChangeProjectUserStatus(long projectUserId, long projectId, long userId)
        {
            var projectUsers = await WorkScope
                    .GetAll<ProjectUser>()
                    .Where(x =>
                                x.Id != projectUserId &&
                                x.ProjectId == projectId &&
                                x.UserId == userId &&
                                x.Status == ProjectUserStatus.Present)
                    .ToListAsync();
            foreach (var item in projectUsers)
            {
                item.Status = ProjectUserStatus.Past;
                await WorkScope.UpdateAsync(item);
            }
        }
        #endregion
    }
}
