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
        private readonly UserManager _userManager;
        private ISettingManager _settingManager;
        private KomuService _komuService;

        public ResourceRequestAppService(
            ProjectUserAppService projectUserAppService,
            PMReportProjectIssueAppService pMReportProjectIssueAppService,
            KomuService komuService,
            UserManager userManager,
            ISettingManager settingManager,
            IUserAppService userAppService)
        {
            _projectUserAppService = projectUserAppService;
            _pMReportProjectIssueAppService = pMReportProjectIssueAppService;
            _komuService = komuService;
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
                            StatusName = x.Status.ToString(),
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
        public async Task<GridResult<GetResourceRequestDto>> GetAllPaging(GridParam input, string order = "PROJECT")
        {
            var projectUser = WorkScope.GetAll<ProjectUser>();
            var SumSkillByResourceRequest = WorkScope.GetAll<ResourceRequestSkill>()
                .GroupBy(x => x.ResourceRequestId)
                .Select(x => new
                {
                    ResourceRequestId = x.Key,
                    Sum = x.Sum(y => y.Quantity)
                });

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
                UserSkills = WorkScope.GetAll<ResourceRequestSkill>().Where(z => z.ResourceRequestId == x.Id).Select(z => new GetSkillDetailDto
                {
                    SkillId = z.SkillId,
                    SkillName = z.Skill.Name,
                    Quantity = z.Quantity,
                    ResourceRequestId = z.ResourceRequestId,
                    ResourceRequestName = z.ResourceRequest.Name
                }).OrderBy(x => x.SkillName).ToList(),
                KeySkill = WorkScope.GetAll<ResourceRequestSkill>().Where(z => z.ResourceRequestId == x.Id).OrderBy(z => z.Skill.Name).FirstOrDefault().Skill.Name,
                KeyQuantity = WorkScope.GetAll<ResourceRequestSkill>().Where(z => z.ResourceRequestId == x.Id).OrderBy(z => z.Skill.Name).FirstOrDefault().Quantity,
                SumSkill = SumSkillByResourceRequest.Where(h => h.ResourceRequestId == x.Id).FirstOrDefault().Sum,
                PlannedNumberOfPersonnel = projectUser.Where(y => y.ProjectId == x.ProjectId && y.ResourceRequestId == x.Id).Count()
            });
            if (order == "TIMENEED")
            {
                query = query.OrderBy(x => x.TimeNeed);
            }
            else if (order == "SKILL")
            {
                //query = (query.Where(x => x.KeyQuantity > 0).OrderBy(x => x.KeySkill).ThenByDescending(x=>x.KeyQuantity).AsEnumerable().Union(query.Where(x => x.KeyQuantity == null))).AsQueryable();
                query = query.OrderBy(x => x.KeySkill == null).ThenBy(x => x.KeySkill).ThenByDescending(x => x.KeyQuantity);
            }
            else
            {
                query = query.OrderByDescending(x => x.ProjectName);
            }


            return await query.GetGridResult(query, input);
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

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource,
            PermissionNames.PmManager_ResourceRequest_AvailableResource)]
        public async Task<GridResult<AvailableResourceDto>> AvailableResource(GridParam input, DateTime? startTime, long? skillId)
        {
            input.SearchText = Regex.Replace(input.SearchText, @"\s+", " ");
            var skill = input.FilterItems.Where(x => x.PropertyName == "skill").FirstOrDefault();
            if (skill != null)
            {
                skillId = long.Parse(skill.Value.ToString());
                input.FilterItems.Remove(skill);
            }
            var projectUsers = WorkScope.GetAll<ProjectUser>()
                               .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed && x.Status == ProjectUserStatus.Present && x.IsFutureActive)
                               .Select(x => new
                               {
                                   UserId = x.UserId,
                                   ProjectId = x.ProjectId,
                                   ProjectName = x.Project.Name,
                                   AllocatePercentage = x.AllocatePercentage,
                                   Status = x.Status,
                                   ProjectRole = x.ProjectRole,
                                   StartTime = x.StartTime,
                               });
            var userSkills = WorkScope.GetAll<UserSkill>().Include(x => x.Skill);
            var userPlanFuture = WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Future && x.IsFutureActive)
                       .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed);
            var filterProjectName = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectName") : null;
            var filterprojectUserPlans = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectUserPlans") : null;
            var projectUserPresent = projectUsers.Where(x => x.Status == ProjectUserStatus.Present).OrderByDescending(x => x.StartTime);
            var users = WorkScope.GetAll<User>().Where(x => x.IsActive).Where(x => x.UserType != UserType.FakeUser)
                                .Select(x => new AvailableResourceDto
                                {
                                    UserId = x.Id,
                                    UserType = x.UserType,
                                    FullName = x.Name + " " + x.Surname,
                                    NormalFullName = x.Surname + " " + x.Name,
                                    EmailAddress = x.EmailAddress,
                                    Branch = x.Branch,
                                    UserLevel = x.UserLevel,
                                    AvatarPath = "/avatars/" + x.AvatarPath,
                                    DateStartPool = projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id) != null ?
                                    projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id).StartTime : x.CreationTime,
                                    WorkingProjects = projectUsers.Where(y => y.UserId == x.Id && y.AllocatePercentage > 0)
                                    .Select(x => new WorkingProjectDto
                                    {
                                        projectId = x.ProjectId,
                                        ProjectName = x.ProjectName,
                                        ProjectRole = x.ProjectRole,
                                        StartTime = x.StartTime
                                    }).ToList(),
                                    Used = (projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) > 0) ? projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) : 0,
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
                                    PoolNote = x.PoolNote,
                                    StarRate = x.StarRate,
                                    TotalFreeDay = WorkScope.GetAll<ProjectUser>().Where(y => y.UserId == x.Id).Any(y => y.Project.Status == ProjectStatus.InProgress && y.Status == ProjectUserStatus.Present && y.AllocatePercentage >= 100) ? //điều kiện đang có trong 1 dự dán
                                    0 :
                                    WorkScope.GetAll<ProjectUser>().Where(z => z.UserId == x.Id).Any(z => z.Status == ProjectUserStatus.Present && z.AllocatePercentage == 0) ? //điều kiện đã được release ra khỏi dự án
                                    (DateTime.Now - WorkScope.GetAll<ProjectUser>().Where(k => k.UserId == x.Id && k.Status == ProjectUserStatus.Present && k.AllocatePercentage == 0).OrderByDescending(k => k.StartTime).Select(k => k.StartTime).FirstOrDefault()).Days //lấy ra ngày release gần nhất
                                    : (DateTime.Now - x.CreationTime).Days //lấy ra ngày tạo
                                    ,
                                }).Where(x => !skillId.HasValue || userSkills.Where(y => y.UserId == x.UserId).Select(y => y.SkillId).Contains(skillId.Value));
            if (filterProjectName != null)
            {
                string searchByProject = filterProjectName.Value.ToString();
                input.FilterItems.Remove(filterProjectName);
                var listIdContainsProjectName = projectUsers.Where(x => x.ProjectName.Contains(searchByProject));
                users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
            }
            if (filterprojectUserPlans != null)
            {
                string searchByprojectUserPlans = filterprojectUserPlans.Value.ToString();
                input.FilterItems.Remove(filterprojectUserPlans);
                var listIdContainsProjectName = userPlanFuture.Where(x => x.Project.Name.Contains(searchByprojectUserPlans));
                users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
            }
            return await users.GetGridResult(users, input);
        }





        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource,
                   PermissionNames.PmManager_ResourceRequest_AvailableResource)]
        public async Task<GridResult<AvailableResourceDto>> GetAllResource(GridParam input, long? skillId)
        {
            input.SearchText = Regex.Replace(input.SearchText, @"\s+", " ");
            var skill = input.FilterItems.Where(x => x.PropertyName == "skill").FirstOrDefault();
            if (skill != null)
            {
                skillId = long.Parse(skill.Value.ToString());
                input.FilterItems.Remove(skill);
            }
            var projectUsers = WorkScope.GetAll<ProjectUser>()
                               .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed && x.Status == ProjectUserStatus.Present && x.IsFutureActive)
                               .Select(x => new
                               {
                                   UserId = x.UserId,
                                   ProjectId = x.ProjectId,
                                   ProjectName = x.Project.Name,
                                   AllocatePercentage = x.AllocatePercentage,
                                   Status = x.Status,
                                   StartTime = x.StartTime,
                                   ProjectRole = x.ProjectRole
                               });
            var userSkills = WorkScope.GetAll<UserSkill>().Include(x => x.Skill);
            var userPlanFuture = WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Future && x.IsFutureActive)
                       .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed);
            var filterProjectName = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectName") : null;
            var filterprojectUserPlans = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectUserPlans") : null;
            var projectUserPresent = projectUsers.Where(x => x.Status == ProjectUserStatus.Present).OrderByDescending(x => x.StartTime);
            var users = WorkScope.GetAll<User>().Where(x => x.IsActive).Where(x => x.UserType != UserType.FakeUser)
                                .Where(u => u.UserType != UserType.Vendor)
                                .Select(x => new AvailableResourceDto
                                {
                                    UserId = x.Id,
                                    UserType = x.UserType,
                                    FullName = x.Name + " " + x.Surname,
                                    NormalFullName = x.Surname + " " + x.Name,
                                    EmailAddress = x.EmailAddress,
                                    Branch = x.Branch,
                                    UserLevel = x.UserLevel,
                                    AvatarPath = "/avatars/" + x.AvatarPath,
                                    DateStartPool = projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id) != null ?
                                    projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id).StartTime : x.CreationTime,
                                    WorkingProjects = projectUsers.Where(y => y.UserId == x.Id && y.AllocatePercentage > 0)
                                    .Select(x => new WorkingProjectDto
                                    {
                                        projectId = x.ProjectId,
                                        ProjectName = x.ProjectName,
                                        ProjectRole = x.ProjectRole,
                                        StartTime = x.StartTime
                                    }).ToList(),
                                    Used = (projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) > 0) ? projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) : 0,
                                    ProjectUserPlans = userPlanFuture.Where(pu => pu.UserId == x.Id).Select(p => new ProjectUserPlan
                                    {
                                        ProjectUserId = p.Id,
                                        ProjectName = p.Project.Name,
                                        StartTime = p.StartTime.Date,
                                        AllocatePercentage = p.AllocatePercentage
                                    }).ToList(),
                                    ListSkills = userSkills.Where(uk => uk.UserId == x.Id).Select(uk => new Skills.Dto.SkillDto
                                    {
                                        Id = uk.SkillId,
                                        Name = uk.Skill.Name
                                    }).ToList(),
                                    PoolNote = x.PoolNote,
                                    StarRate = x.StarRate,
                                    TotalFreeDay = WorkScope.GetAll<ProjectUser>().Where(y => y.UserId == x.Id).Any(y => y.Project.Status == ProjectStatus.InProgress && y.Status == ProjectUserStatus.Present && y.AllocatePercentage >= 100) ? //điều kiện đang có trong 1 dự dán
                                    0 :
                                    WorkScope.GetAll<ProjectUser>().Where(z => z.UserId == x.Id).Any(z => z.Status == ProjectUserStatus.Present && z.AllocatePercentage == 0) ? //điều kiện đã được release ra khỏi dự án
                                    (DateTime.Now - WorkScope.GetAll<ProjectUser>().Where(k => k.UserId == x.Id && k.Status == ProjectUserStatus.Present && k.AllocatePercentage == 0).OrderByDescending(k => k.StartTime).Select(k => k.StartTime).FirstOrDefault()).Days //lấy ra ngày release gần nhất
                                    : (DateTime.Now - x.CreationTime).Days //lấy ra ngày tạo
                                    ,
                                }).Where(x => !skillId.HasValue || userSkills.Where(y => y.UserId == x.UserId).Select(y => y.SkillId).Contains(skillId.Value));
            if (filterProjectName != null)
            {
                string searchByProject = filterProjectName.Value.ToString();
                input.FilterItems.Remove(filterProjectName);
                var listIdContainsProjectName = projectUsers.Where(x => x.ProjectName.Contains(searchByProject));
                users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
            }
            if (filterprojectUserPlans != null)
            {
                string searchByprojectUserPlans = filterprojectUserPlans.Value.ToString();
                input.FilterItems.Remove(filterprojectUserPlans);
                var listIdContainsProjectName = userPlanFuture.Where(x => x.Project.Name.Contains(searchByprojectUserPlans));
                users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
            }
            return await users.GetGridResult(users, input);
        }



        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource,
                          PermissionNames.PmManager_ResourceRequest_AvailableResource)]
        public async Task<GridResult<AvailableResourceDto>> GetAllPoolResource(GridParam input, long? skillId)
        {
            input.SearchText = Regex.Replace(input.SearchText, @"\s+", " ");
            var skill = input.FilterItems.Where(x => x.PropertyName == "skill").FirstOrDefault();
            if (skill != null)
            {
                skillId = long.Parse(skill.Value.ToString());
                input.FilterItems.Remove(skill);
            }
            var projectUsers = WorkScope.GetAll<ProjectUser>()
                               .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed && x.Status == ProjectUserStatus.Present && x.IsFutureActive)
                               .Select(x => new
                               {
                                   UserId = x.UserId,
                                   ProjectId = x.ProjectId,
                                   ProjectName = x.Project.Name,
                                   AllocatePercentage = x.AllocatePercentage,
                                   Status = x.Status,
                                   StartTime = x.StartTime,
                                   ProjectRole = x.ProjectRole
                               });
            var userSkills = WorkScope.GetAll<UserSkill>().Include(x => x.Skill);
            var userPlanFuture = WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Future && x.IsFutureActive)
                       .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed);
            var filterProjectName = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectName") : null;
            var filterprojectUserPlans = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectUserPlans") : null;
            var projectUserPresent = projectUsers.Where(x => x.Status == ProjectUserStatus.Present).OrderByDescending(x => x.StartTime);
            var users = WorkScope.GetAll<User>().Where(x => x.IsActive).Where(x => x.UserType != UserType.FakeUser)
                                .Where(u => u.UserType != UserType.Vendor)
                                .Select(x => new AvailableResourceDto
                                {
                                    UserId = x.Id,
                                    UserType = x.UserType,
                                    FullName = x.Name + " " + x.Surname,
                                    NormalFullName = x.Surname + " " + x.Name,
                                    EmailAddress = x.EmailAddress,
                                    Branch = x.Branch,
                                    UserLevel = x.UserLevel,
                                    AvatarPath = "/avatars/" + x.AvatarPath,
                                    DateStartPool = projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id) != null ?
                                    projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id).StartTime : x.CreationTime,
                                    WorkingProjects = projectUsers.Where(y => y.UserId == x.Id && y.AllocatePercentage > 0)
                                    .Select(x => new WorkingProjectDto
                                    {
                                        projectId = x.ProjectId,
                                        ProjectName = x.ProjectName,
                                        ProjectRole = x.ProjectRole,
                                        StartTime = x.StartTime
                                    }).ToList(),
                                    Used = (projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) > 0) ? projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) : 0,
                                    ProjectUserPlans = userPlanFuture.Where(pu => pu.UserId == x.Id).Select(p => new ProjectUserPlan
                                    {
                                        ProjectUserId = p.Id,
                                        ProjectName = p.Project.Name,
                                        StartTime = p.StartTime.Date,
                                        AllocatePercentage = p.AllocatePercentage
                                    }).ToList(),
                                    ListSkills = userSkills.Where(uk => uk.UserId == x.Id).Select(uk => new Skills.Dto.SkillDto
                                    {
                                        Id = uk.SkillId,
                                        Name = uk.Skill.Name
                                    }).ToList(),
                                    PoolNote = x.PoolNote,
                                    StarRate = x.StarRate,
                                    TotalFreeDay = WorkScope.GetAll<ProjectUser>().Where(y => y.UserId == x.Id).Any(y => y.Project.Status == ProjectStatus.InProgress && y.Status == ProjectUserStatus.Present && y.AllocatePercentage >= 100) ? //điều kiện đang có trong 1 dự dán
                                    0 :
                                    WorkScope.GetAll<ProjectUser>().Where(z => z.UserId == x.Id).Any(z => z.Status == ProjectUserStatus.Present && z.AllocatePercentage == 0) ? //điều kiện đã được release ra khỏi dự án
                                    (DateTime.Now - WorkScope.GetAll<ProjectUser>().Where(k => k.UserId == x.Id && k.Status == ProjectUserStatus.Present && k.AllocatePercentage == 0).OrderByDescending(k => k.StartTime).Select(k => k.StartTime).FirstOrDefault()).Days //lấy ra ngày release gần nhất
                                    : (DateTime.Now - x.CreationTime).Days //lấy ra ngày tạo
                                    ,
                                }).Where(x => !skillId.HasValue || userSkills.Where(y => y.UserId == x.UserId).Select(y => y.SkillId).Contains(skillId.Value))
                                .Where(x => x.Used <= 0);
            if (filterProjectName != null)
            {
                string searchByProject = filterProjectName.Value.ToString();
                input.FilterItems.Remove(filterProjectName);
                var listIdContainsProjectName = projectUsers.Where(x => x.ProjectName.Contains(searchByProject));
                users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
            }
            if (filterprojectUserPlans != null)
            {
                string searchByprojectUserPlans = filterprojectUserPlans.Value.ToString();
                input.FilterItems.Remove(filterprojectUserPlans);
                var listIdContainsProjectName = userPlanFuture.Where(x => x.Project.Name.Contains(searchByprojectUserPlans));
                users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
            }
            return await users.GetGridResult(users, input);
        }




        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource,
                                 PermissionNames.PmManager_ResourceRequest_AvailableResource)]
        public async Task<GridResult<AvailableResourceDto>> GetVendorResource(GridParam input, long? skillId)
        {
            input.SearchText = Regex.Replace(input.SearchText, @"\s+", " ");
            var skill = input.FilterItems.Where(x => x.PropertyName == "skill").FirstOrDefault();
            if (skill != null)
            {
                skillId = long.Parse(skill.Value.ToString());
                input.FilterItems.Remove(skill);
            }
            var projectUsers = WorkScope.GetAll<ProjectUser>()
                               .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed && x.Status == ProjectUserStatus.Present && x.IsFutureActive)
                               .Select(x => new
                               {
                                   UserId = x.UserId,
                                   ProjectId = x.ProjectId,
                                   ProjectName = x.Project.Name,
                                   AllocatePercentage = x.AllocatePercentage,
                                   Status = x.Status,
                                   ProjectRole = x.ProjectRole,
                                   StartTime = x.StartTime,
                               });
            var userSkills = WorkScope.GetAll<UserSkill>().Include(x => x.Skill);
            var userPlanFuture = WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Future && x.IsFutureActive)
                       .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed);
            var filterProjectName = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectName") : null;
            var filterprojectUserPlans = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "projectUserPlans") : null;
            var projectUserPresent = projectUsers.Where(x => x.Status == ProjectUserStatus.Present).OrderByDescending(x => x.StartTime);
            var users = WorkScope.GetAll<User>().Where(x => x.IsActive).Where(x => x.UserType != UserType.FakeUser)
                                .Where(u => u.UserType == UserType.Vendor)
                                .Select(x => new AvailableResourceDto
                                {
                                    UserId = x.Id,
                                    UserType = x.UserType,
                                    FullName = x.Name + " " + x.Surname,
                                    NormalFullName = x.Surname + " " + x.Name,
                                    EmailAddress = x.EmailAddress,
                                    Branch = x.Branch,
                                    UserLevel = x.UserLevel,
                                    AvatarPath = "/avatars/" + x.AvatarPath,
                                    DateStartPool = projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id) != null ?
                                    projectUserPresent.FirstOrDefault(y => y.AllocatePercentage == 0 && y.UserId == x.Id).StartTime : x.CreationTime,
                                    WorkingProjects = projectUsers.Where(y => y.UserId == x.Id && y.AllocatePercentage > 0)
                                    .Select(x => new WorkingProjectDto
                                    {
                                        projectId = x.ProjectId,
                                        ProjectName = x.ProjectName,
                                        ProjectRole = x.ProjectRole,
                                        StartTime = x.StartTime
                                    }).ToList(),
                                    Used = (projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) > 0) ? projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage) : 0,
                                    ProjectUserPlans = userPlanFuture.Where(pu => pu.UserId == x.Id).Select(p => new ProjectUserPlan
                                    {
                                        ProjectUserId = p.Id,
                                        ProjectName = p.Project.Name,
                                        StartTime = p.StartTime.Date,
                                        AllocatePercentage = p.AllocatePercentage
                                    }).ToList(),
                                    ListSkills = userSkills.Where(uk => uk.UserId == x.Id).Select(uk => new Skills.Dto.SkillDto
                                    {
                                        Id = uk.SkillId,
                                        Name = uk.Skill.Name
                                    }).ToList(),
                                    PoolNote = x.PoolNote,
                                    StarRate = x.StarRate,
                                    TotalFreeDay = WorkScope.GetAll<ProjectUser>().Where(y => y.UserId == x.Id).Any(y => y.Project.Status == ProjectStatus.InProgress && y.Status == ProjectUserStatus.Present && y.AllocatePercentage >= 100) ? //điều kiện đang có trong 1 dự dán
                                    0 :
                                    WorkScope.GetAll<ProjectUser>().Where(z => z.UserId == x.Id).Any(z => z.Status == ProjectUserStatus.Present && z.AllocatePercentage == 0) ? //điều kiện đã được release ra khỏi dự án
                                    (DateTime.Now - WorkScope.GetAll<ProjectUser>().Where(k => k.UserId == x.Id && k.Status == ProjectUserStatus.Present && k.AllocatePercentage == 0).OrderByDescending(k => k.StartTime).Select(k => k.StartTime).FirstOrDefault()).Days //lấy ra ngày release gần nhất
                                    : (DateTime.Now - x.CreationTime).Days //lấy ra ngày tạo
                                    ,
                                }).Where(x => !skillId.HasValue || userSkills.Where(y => y.UserId == x.UserId).Select(y => y.SkillId).Contains(skillId.Value));

            if (filterProjectName != null)
            {
                string searchByProject = filterProjectName.Value.ToString();
                input.FilterItems.Remove(filterProjectName);
                var listIdContainsProjectName = projectUsers.Where(x => x.ProjectName.Contains(searchByProject));
                users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
            }
            if (filterprojectUserPlans != null)
            {
                string searchByprojectUserPlans = filterprojectUserPlans.Value.ToString();
                input.FilterItems.Remove(filterprojectUserPlans);
                var listIdContainsProjectName = userPlanFuture.Where(x => x.Project.Name.Contains(searchByprojectUserPlans));
                users = users.Where(x => listIdContainsProjectName.Where(y => y.UserId == x.UserId).Any());
            }
            return await users.GetGridResult(users, input);
        }


        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_CancelResource)]
        public async Task CancelResourcePlan(long projectUserId)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(projectUserId);

            await WorkScope.DeleteAsync(projectUser);

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
            if (!project.Code.Equals("CHO_NGHI"))
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
            model.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ResourceRequest>(model));
            var projectUri = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri);
            var project = await WorkScope.GetAll<Project>().FirstOrDefaultAsync(x => x.Id == model.ProjectId);
            if (project == null)
                throw new UserFriendlyException("Project doesn't exist");
            var user = await WorkScope.GetAsync<User>(AbpSession.UserId.Value);
            var userName = UserHelper.GetUserName(user.EmailAddress);
            if (user != null && !user.KomuUserId.HasValue)
            {
                user.KomuUserId = await _komuService.GetKomuUserId(new KomuUserDto { Username = userName ?? user.UserName }, ChannelTypeConstant.KOMU_USER);
                await WorkScope.UpdateAsync<User>(user);
            }
            var link = $"{projectUri.Replace("-api", String.Empty)}app/resource-request";
            var message = new StringBuilder();
            message.AppendLine($"PM {(user.KomuUserId.HasValue ? "<@" + user.KomuUserId.ToString() + ">" : "**" + (userName ?? user.UserName) + "**")} đã tạo mới request **{model.Name}** cho dự án **{project.Name}**.");
            message.AppendLine(link);
            await _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = userName ?? user.UserName,
                Message = message.ToString(),
                CreateDate = DateTimeUtils.GetNow(),
            }, ChannelTypeConstant.PM_CHANNEL);
            return model;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_Update, PermissionNames.PmManager_ResourceRequest_Update)]
        public async Task<ResourceRequestDto> Update(ResourceRequestDto model)
        {
            var resourceRequest = await WorkScope
                .GetAsync<ResourceRequest>(model.Id);
            await WorkScope.UpdateAsync(ObjectMapper.Map(model, resourceRequest));

            if (model.Status == ResourceRequestStatus.PENDING ||
                    model.Status == ResourceRequestStatus.APPROVE)
            {
                return model;
            }

            var projectUri = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri);
            var project = await WorkScope
                .GetAll<Project>()
                .FirstOrDefaultAsync(x => x.Id == model.ProjectId);
            if (project == null)
            {
                throw new UserFriendlyException("Project doesn't exist");
            }
            var user = await WorkScope.GetAsync<User>(AbpSession.UserId.Value);
            user.KomuUserId = await _userAppService.UpdateKomuId(user.Id);
            if (!user.KomuUserId.HasValue)
            {
                user.UserName = UserHelper.GetUserName(user.EmailAddress) ?? user.UserName;
            }

            var message = new StringBuilder();
            var titlelink = $"{projectUri.Replace("-api", String.Empty)}app/resource-request";
            if (model.Status == ResourceRequestStatus.DONE)
            {
                message.Append($"Request **{model.Name}** cho dự án **{project.Name}** ");
                message.AppendLine($"đã được {(user.KomuUserId.HasValue ? "<@" + user.KomuUserId + ">" : "**" + user.UserName + "**")} chuyển sang trạng thái hoàn thành.");
            }
            else
            {
                message.Append($"Request **{model.Name}** cho dự án **{project.Name}** ");
                message.AppendLine($"đã được huỷ bởi {(user.KomuUserId.HasValue ? "<@" + user.KomuUserId + ">" : "**" + user.UserName + "**")}.");
            }

            message.AppendLine(titlelink);
            await _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = user.UserName,
                Message = message.ToString(),
                CreateDate = DateTimeUtils.GetNow(),
            },
            ChannelTypeConstant.PM_CHANNEL);
            return model;
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
