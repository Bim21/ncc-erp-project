using Abp;
using Abp.Application.Services;
using Abp.Dependency;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.IoC;
using NccCore.Paging;
using NccCore.Uitls;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants;
using ProjectManagement.Entities;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using ProjectManagement.Services.ResourceManager.Dto;
using ProjectManagement.Services.ResourceRequestService;
using ProjectManagement.Services.ResourceRequestService.Dto;
using ProjectManagement.Services.ResourceService.Dto;
using ProjectManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.ResourceManager
{
    public class ResourceManager : ApplicationService
    {
        private readonly IWorkScope _workScope;
        private readonly KomuService _komuService;
        private readonly UserManager _userManager;
        private readonly ResourceRequestManager _resourceRequestManager;

        public ResourceManager(IWorkScope workScope, IAbpSession abpSession,
            ResourceRequestManager resourceRequestManager,
            KomuService komuService, UserManager userManager)
        {
            _workScope = workScope;
            _komuService = komuService;
            _userManager = userManager;
            _resourceRequestManager = resourceRequestManager;
        }

        public IQueryable<ProjectOfUserDto> QueryProjectsOfUser()
        {
            return _workScope.GetAll<ProjectUser>()
                .Select(x => new ProjectOfUserDto
                {
                    ProjectName = x.Project.Name,
                    PmName = x.Project.PM.FullName,
                    StartTime = x.StartTime
                });
        }


        public async Task<List<UserOfProjectDto>> GetWorkingUsersOfProject(long projectId)
        {
            return await QueryUsersOfProject(projectId)
                .Where(s => s.ProjectId == projectId)
                .Where(s => s.PUStatus == ProjectUserStatus.Present)
                .Where(s => s.AllocatePercentage > 0)
                .OrderBy(s => s.PUStatus)
                .ThenByDescending(s => s.StartTime)
                .ToListAsync();
        }


        public IQueryable<UserOfProjectDto> QueryUsersOfProject(long projectId)
        {
            var queryPu = _workScope.GetAll<ProjectUser>()
                .Where(s => s.ProjectId == projectId)
                .Where(s => s.User.UserType != UserType.FakeUser)
            .Select(s => new UserOfProjectDto
            {
                Id = s.Id,
                ProjectId = s.ProjectId,
                UserId = s.UserId,
                AvatarPath = s.User.AvatarPath,
                FullName = s.User.FullName,
                EmailAddress = s.User.EmailAddress,
                Branch = s.User.BranchOld,
                UserLevel = s.User.UserLevel,
                UserType = s.User.UserType,
                AllocatePercentage = s.AllocatePercentage,
                IsPool = s.IsPool,
                ProjectRole = s.ProjectRole,
                StartTime = s.StartTime,
                StarRate = s.User.StarRate,
                Note = s.Note,
                PUStatus = s.Status,
                PMReportId = s.PMReportId,
                BranchColor = s.User.Branch.Color,
                BranchDisplayName = s.User.Branch.DisplayName,
                UserSkills = s.User.UserSkills.Select(s => new UserSkillDto
                {
                    SkillId = s.SkillId,
                    SkillName = s.Skill.Name
                }).ToList()
            })
            .OrderByDescending(s => s.PUStatus == ProjectUserStatus.Present && s.AllocatePercentage > 0)
            .ThenByDescending(s => s.StartTime);

            return queryPu;
        }


        public IQueryable<ProjectOfUserDto> QueryWorkingProjectsOfUser(long userId)
        {

            return _workScope.GetAll<ProjectUser>()
                .Where(s => s.UserId == userId)
                .Where(s => s.Status == ProjectUserStatus.Present)
                .Where(s => s.AllocatePercentage > 0)
                .Where(s => s.Project.Status == ProjectStatus.InProgress)
                .OrderBy(s => s.ProjectRole)
                .ThenByDescending(s => s.StartTime)
                .Select(x => new ProjectOfUserDto
                {
                    ProjectName = x.Project.Name,
                    PmName = x.Project.PM.FullName,
                    StartTime = x.StartTime,
                    IsPool = x.IsPool
                });
        }


        public async Task<KomuProjectInfoDto> GetKomuProjectInfo(long projectId)
        {
            return await _workScope.GetAll<Project>()
                .Where(s => s.Id == projectId)
                .Select(s => new KomuProjectInfoDto
                {
                    Status = s.Status,
                    ProjectId = projectId,
                    ProjectCode = s.Code,
                    ProjectName = s.Name,
                    PM = new KomuUserInfoDto
                    {
                        FullName = s.PM.FullName,
                        KomuUserId = s.PM.KomuUserId,
                        UserId = s.PMId,
                        UserName = s.PM.UserName
                    }

                }).FirstOrDefaultAsync();
        }

        public async Task<long> GetActiveReportId()
        {
            return await _workScope.GetAll<PMReport>()
                .Where(s => s.IsActive == true)
                .OrderByDescending(s => s.Id)
                .Select(s => s.Id).FirstOrDefaultAsync();
        }

        public async Task<StringBuilder> ReleaseUserFromAllWorkingProjectsByHRM(KomuUserInfoDto employee, long activeReportId, string outProjectNote)
        {            
            var sbKomuMessage = new StringBuilder();
            var currentPUs = await _workScope.GetAll<ProjectUser>()
             .Include(s => s.Project)
             .Where(s => s.UserId == employee.UserId)
             .Where(s => s.Status == ProjectUserStatus.Present)
             .Where(s => s.AllocatePercentage > 0)
             .ToListAsync();

            if (currentPUs.IsEmpty())
            {
                return sbKomuMessage;
            }


            foreach (var pu in currentPUs)
            {
                //release user from current project
                pu.PMReportId = activeReportId;
                pu.Status = ProjectUserStatus.Past;

                await _workScope.UpdateAsync(pu);

                var outPU = new ProjectUser
                {
                    IsPool = pu.IsPool,
                    UserId = pu.UserId,
                    ProjectId = pu.ProjectId,
                    Status = ProjectUserStatus.Present,
                    AllocatePercentage = 0,
                    StartTime = DateTimeUtils.GetNow(),
                    PMReportId = activeReportId,
                    ProjectRole = pu.ProjectRole,
                    Note = outProjectNote,
                };
                await _workScope.InsertAsync(outPU);
                sbKomuMessage.AppendLine($"{DateTimeUtils.ToString(outPU.StartTime)}: **HRM Tool** " +
                    $"released {employee.KomuAccountInfo} from **{pu.Project.Name}** {CommonUtil.ProjectUserWorkTypeKomu(pu.IsPool)}");
            }

            return sbKomuMessage;
        }



        private async Task<StringBuilder> releaseUserFromAllWorkingProjects(KomuUserInfoDto sessionUser, KomuUserInfoDto employee,
            KomuProjectInfoDto projectToJoin, long activeReportId, bool isPresentPool, bool allowConfirmMoveEmployeeToOtherProject , ProjectUser futureU)
        {
            var sbKomuMessage = new StringBuilder();
            var currentPUs = await _workScope.GetAll<ProjectUser>()
             .Include(s => s.Project)
             .Where(s => s.UserId == employee.UserId)
             .Where(s => s.Status == ProjectUserStatus.Present)
             .Where(s => s.Project.Status == ProjectStatus.InProgress)
             .Where(s => s.AllocatePercentage > 0)
             .ToListAsync();
            if (!allowConfirmMoveEmployeeToOtherProject)
            {
                foreach (var pu in currentPUs)
                {
                    if (pu.IsPool == false)
                    {
                        throw new UserFriendlyException("This user is working offical in other project, so he/she can't work TEMP in other project");
                    }
                }
            }
            if (!currentPUs.IsEmpty())
            {
                var isPoolInProjectToJoin = currentPUs
                .Where(s => s.ProjectId == projectToJoin.ProjectId)
                .Where(s => s.AllocatePercentage > 0)
                .Select(s => s.IsPool)
                .FirstOrDefault();

                if (isPoolInProjectToJoin != default && isPoolInProjectToJoin == isPresentPool)
                {
                   throw new UserFriendlyException("This user is already working on this project!");
                }

                foreach (var pu in currentPUs)
                {
                    //release user from current project
                    pu.PMReportId = activeReportId;
                    pu.Status = ProjectUserStatus.Past;

                    await _workScope.UpdateAsync(pu);

                    var outPU = new ProjectUser
                    {
                        IsPool = pu.IsPool,
                        UserId = pu.UserId,
                        ProjectId = pu.ProjectId,
                        Status = ProjectUserStatus.Present,
                        AllocatePercentage = 0,
                        StartTime = DateTimeUtils.MinDate(futureU.StartTime, DateTimeUtils.GetNow()),
                        PMReportId = activeReportId,
                        ProjectRole = pu.ProjectRole,
                        Note = $"added to project {projectToJoin.ProjectName} {CommonUtil.ProjectUserWorkType(pu.IsPool)} by {sessionUser.FullName}",
                    };
                    await _workScope.InsertAsync(outPU);
                    sbKomuMessage.AppendLine($"{DateTimeUtils.ToString(outPU.StartTime)}: {sessionUser.KomuAccountInfo} " +
                        $"released {employee.KomuAccountInfo} from {pu.Project.Name} {CommonUtil.ProjectUserWorkTypeKomu(pu.IsPool)}");
                }
            }

            return sbKomuMessage;
        }

        public async Task<ProjectUser> CreatePresentProjectUserAndNofity(AddResourceToProjectDto input, bool allowConfirmMoveEmployeeToOtherProject)
        {
            var activeReportId = await GetActiveReportId();

            var sessionUser = await getSessionKomuUserInfo();
            var employee = await getKomuUserInfo(input.UserId);
            var projectToJoin = await GetKomuProjectInfo(input.ProjectId);

            if (projectToJoin.Status == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("You can not add user to closed project");
            }
            var joinPU = new ProjectUser
            {
                IsPool = input.IsPool,
                UserId = input.UserId,
                ProjectId = input.ProjectId,
                Status = ProjectUserStatus.Present,
                AllocatePercentage = 100,
                StartTime = input.StartTime,
                PMReportId = activeReportId,
                ProjectRole = input.ProjectRole,
            };
            var sbKomuMessage = await releaseUserFromAllWorkingProjects(sessionUser, employee, projectToJoin, activeReportId, input.IsPool, allowConfirmMoveEmployeeToOtherProject, joinPU);

            await _workScope.InsertAsync(joinPU);

            if (projectToJoin.ProjectCode == AppConsts.CHO_NGHI_PROJECT_CODE)
            {
                await _userManager.DeactiveUser(employee.UserId);
            }
            nofityCreatePresentPU(joinPU, sbKomuMessage, sessionUser, employee, projectToJoin);

            return joinPU;
        }

        private void nofityCreatePresentPU(ProjectUser joinPU, StringBuilder sbKomuMessage, KomuUserInfoDto sessionUser, KomuUserInfoDto employee, KomuProjectInfoDto project)
        {
            sbKomuMessage.AppendLine();
            sbKomuMessage.Append($"{sessionUser.KomuAccountInfo} confirmed {employee.KomuAccountInfo} ");
            sbKomuMessage.Append($"work in {project.KomuProjectInfo} {CommonUtil.ProjectUserWorkTypeKomu(joinPU.IsPool)} ");
            sbKomuMessage.Append($"on {DateTimeUtils.ToString(joinPU.StartTime)}");

            SendKomu(sbKomuMessage, project.ProjectCode);

        }

        public async Task<ProjectUserExt> ConfirmJoinProject(long projectUserId, DateTime startTime, bool allowConfirmMoveEmployeeToOtherProject)
        {
            var confirmPUExt = await GetPUExt(projectUserId);


            if (confirmPUExt == null || confirmPUExt.PU == null)
            {
                throw new UserFriendlyException("Not found ProjectUser with Id " + projectUserId);
            }

            var futurePU = confirmPUExt.PU;
            if (futurePU.Status != ProjectUserStatus.Future || futurePU.AllocatePercentage <= 0)
            {
                throw new UserFriendlyException($"Invalid ProjectUser with Id {projectUserId}:  Status != FUTURE or AllocatePercentage <= 0");
            }

            if (startTime.Date > DateTimeUtils.GetNow().Date)
            {
                throw new UserFriendlyException($"Start Time must be less than or equal today");
            }
            var employee = await getKomuUserInfo(futurePU.UserId);
            var project = await GetKomuProjectInfo(futurePU.ProjectId);
            var activeReportId = await GetActiveReportId();
            var sessionUser = await getSessionKomuUserInfo();
            var resourceRequest = _workScope.GetAll<ResourceRequest>()
               .Where(s => s.Id == futurePU.ResourceRequestId)
               .FirstOrDefault();

            if (resourceRequest != null)
            {
                resourceRequest.Status = ResourceRequestStatus.DONE;
                resourceRequest.TimeDone = DateTimeUtils.GetNow();
                await _workScope.UpdateAsync(resourceRequest);

                var listRequestDto = await _resourceRequestManager.IQGetResourceRequest()
                 .Where(s => s.Id == resourceRequest.Id)
                 .FirstOrDefaultAsync();

                nofityKomuDoneResourceRequest(listRequestDto, sessionUser, project);
            }

          
            var sbKomuMessage = await releaseUserFromAllWorkingProjects(sessionUser, employee, project, activeReportId, futurePU.IsPool, allowConfirmMoveEmployeeToOtherProject , futurePU);

            futurePU.Status = ProjectUserStatus.Present;
            futurePU.StartTime = startTime;
            futurePU.PMReportId = activeReportId;

            await _workScope.UpdateAsync(confirmPUExt.PU);

            if (project.ProjectCode == AppConsts.CHO_NGHI_PROJECT_CODE)
            {
                await _userManager.DeactiveUser(employee.UserId);
            }

            nofityCreatePresentPU(futurePU, sbKomuMessage, sessionUser, employee, project);


            return confirmPUExt;

        }

        public void nofityKomuDoneResourceRequest(GetResourceRequestDto listRequestDto, KomuUserInfoDto sessionUser, KomuProjectInfoDto project)
        {
            StringBuilder setDoneKomuMessage = new StringBuilder();
            setDoneKomuMessage.AppendLine($"{sessionUser.KomuAccountInfo} set DONE for request:");
            setDoneKomuMessage.AppendLine($"{listRequestDto.KomuInfo()} ");
            setDoneKomuMessage.AppendLine("");

            SendKomu(setDoneKomuMessage, project.ProjectCode);
        }


        public async Task<ProjectUser> ValidateUserWorkingInThisProject(long userId, long projectId)
        {
            var userWorkingInThisProject = await _workScope.GetAll<ProjectUser>()
                 .Where(s => s.UserId == userId)
                 .Where(s => s.ProjectId == projectId)
                 .Where(s => s.Status == ProjectUserStatus.Present)
                 .Where(s => s.AllocatePercentage > 0)
                 .Where(s => s.Project.Status == ProjectStatus.InProgress).FirstOrDefaultAsync();

            if (userWorkingInThisProject == null)
            {
                throw new UserFriendlyException("This user is not working in this Project, so you can't plan him out project");
            }
            return userWorkingInThisProject;
        }

        public async Task<ProjectUser> ConfirmOutProject(ConfirmOutProjectDto input)
        {
            var outPUExt = await GetPUExt(input.ProjectUserId);
            await ValidateUserWorkingInThisProject(outPUExt.PU.UserId, outPUExt.PU.ProjectId);

            if (outPUExt == null || outPUExt.PU == null)
            {
                throw new UserFriendlyException("Not found ProjectUser with Id " + input.ProjectUserId);
            }

            var outPU = outPUExt.PU;
            if (outPU.Status != ProjectUserStatus.Future || outPU.AllocatePercentage > 0)
            {
                throw new UserFriendlyException($"Invalid ProjectUser with Id {outPU.Id}:  Status != FUTURE or AllocatePercentage > 0");
            }

            if (input.StartTime.Date > DateTimeUtils.GetNow().Date)
            {
                throw new UserFriendlyException($"Start Time must be less than or equal today");
            }

            var activeReportId = await GetActiveReportId();
            var curentPUs = await _workScope.GetAll<ProjectUser>()
                .Where(x => x.ProjectId == outPU.ProjectId)
                .Where(x => x.Status == ProjectUserStatus.Present)
                .Where(x => x.UserId == outPU.UserId)
                .ToListAsync();

            foreach (var curentPU in curentPUs)
            {
                curentPU.Status = ProjectUserStatus.Past;
                curentPU.PMReportId = activeReportId;
                await _workScope.UpdateAsync(curentPU);
            }

            outPU.Status = ProjectUserStatus.Present;
            outPU.PMReportId = activeReportId;
            outPU.StartTime = input.StartTime;
            await _workScope.UpdateAsync(outPU);

            var sessionUser = await getSessionKomuUserInfo();
            var employee = await getKomuUserInfo(outPU.UserId);
            var project = await GetKomuProjectInfo(outPU.ProjectId);
            var sbKomuMessage = new StringBuilder();

            sbKomuMessage.Append($"{DateTimeUtils.ToString(outPU.StartTime)}: {sessionUser.KomuAccountInfo} ");
            sbKomuMessage.Append($"released {employee.KomuAccountInfo} from {project.KomuProjectInfo} {CommonUtil.ProjectUserWorkTypeKomu(outPU.IsPool)}");

            SendKomu(sbKomuMessage, project.ProjectCode);

            return outPU;
        }





        /// <summary>
        /// Release one working user from project
        /// if release date > now: plan release
        /// else: confirm release
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ProjectUser> ReleaseWorkingUserFromProject(ReleaseUserToPoolDto input)
        {
            var presentPU = await GetPUExt(input.ProjectUserId);

            if (presentPU == null)
            {
                throw new UserFriendlyException($"Not found ProjectUser with Id {input.ProjectUserId}");
            }
            if (presentPU.PU.Status != ProjectUserStatus.Present)
            {
                throw new UserFriendlyException($"Employee {presentPU.Employee.FullName} is not working in project {presentPU.Project.ProjectName}. So you can't release him/her from this project.");
            }

            var activeReportId = await GetActiveReportId();


            if (input.ReleaseDate.Date <= DateTimeUtils.GetNow())
            {
                presentPU.PU.Status = ProjectUserStatus.Past;
                presentPU.PU.PMReportId = activeReportId;

                await _workScope.UpdateAsync(presentPU.PU);
            }
            var releasePU = new ProjectUser
            {
                UserId = presentPU.PU.UserId,
                IsPool = presentPU.PU.IsPool,
                Status = input.ReleaseDate.Date <= DateTimeUtils.GetNow() ? ProjectUserStatus.Present : ProjectUserStatus.Future,
                ProjectId = presentPU.PU.ProjectId,
                ProjectRole = presentPU.PU.ProjectRole,
                PMReportId = activeReportId,
                AllocatePercentage = 0,
                StartTime = input.ReleaseDate,
                IsFutureActive = false,
                Note = input.Note,
            };
            await _workScope.InsertAsync(releasePU);

            var sessionUser = await getSessionKomuUserInfo();
            var sbKomuMessage = new StringBuilder();

            sbKomuMessage.Append($"{sessionUser.KomuAccountInfo} {CommonUtil.PUStatusToPlanConfirmKomu(releasePU.Status)} release  {presentPU.Employee.KomuAccountInfo} ");
            sbKomuMessage.Append($"from {presentPU.Project.KomuProjectInfo} {CommonUtil.ProjectUserWorkTypeKomu(releasePU.IsPool)} ");
            sbKomuMessage.Append($"on {DateTimeUtils.ToString(releasePU.StartTime)}");

            SendKomu(sbKomuMessage, presentPU.Project.ProjectCode);

            return releasePU;

        }

        public IQueryable<UserOfProjectDto> QueryPlansOfProject(long projectId)
        {
            var q = QueryUsersOfProject(projectId)
                .Where(x => x.PUStatus == ProjectUserStatus.Future)
                .OrderByDescending(x => x.StartTime);

            return q;

        }


        public async Task<ProjectUser> AddFuturePUAndNofity(InputPlanResourceDto input)
        {
            var pu = await AddFuturePU(input);
            await NotifyAddFuturePU(pu);
            return pu;
        }

        public async Task<ProjectUser> AddFuturePU(InputPlanResourceDto input)
        {
            var activeReportId = await GetActiveReportId();
            var pu = new ProjectUser
            {
                AllocatePercentage = input.AllocatePercentage,
                Status = ProjectUserStatus.Future,
                UserId = input.UserId,
                ProjectId = input.ProjectId,
                StartTime = input.StartTime,
                IsPool = input.IsPool,
                PMReportId = activeReportId,
                Note = input.Note,
                ProjectRole = input.ProjectRole,
                IsFutureActive = false
            };
            await _workScope.InsertAsync(pu);
            return pu;
        }

        public async Task NotifyAddFuturePU(ProjectUser pu)
        {
            var sbKomuMessage = new StringBuilder();
            var sessionUser = await getSessionKomuUserInfo();
            var employee = await getKomuUserInfo(pu.UserId);
            var project = await GetKomuProjectInfo(pu.ProjectId);

            sbKomuMessage.Append($"{sessionUser.KomuAccountInfo} planned {employee.KomuAccountInfo} ");
            sbKomuMessage.Append($"{CommonUtil.JoinOrOutProject(pu.AllocatePercentage)} {project.KomuProjectInfo} {CommonUtil.ProjectUserWorkTypeKomu(pu.IsPool)} ");
            sbKomuMessage.Append($"on {DateTimeUtils.ToString(pu.StartTime)}");

            SendKomu(sbKomuMessage, project.ProjectCode);
        }

        public async Task<ProjectUserExt> GetPUExt(long id)
        {
            return await _workScope.GetAll<ProjectUser>()
                 .Where(s => s.Id == id)
                 .Include(s => s.Project)
                 .Include(s => s.User)
                 .Select(s => new ProjectUserExt
                 {
                     PU = s,
                     Project = new KomuProjectInfoDto
                     {
                         ProjectId = s.ProjectId,
                         ProjectCode = s.Project.Code,
                         ProjectName = s.Project.Name,
                         PM = new KomuUserInfoDto
                         {
                             FullName = s.Project.PM.FullName,
                             KomuUserId = s.Project.PM.KomuUserId,
                             UserId = s.Project.PMId,
                             UserName = s.Project.PM.UserName
                         }
                     },
                     Employee = new KomuUserInfoDto
                     {
                         FullName = s.User.FullName,
                         KomuUserId = s.User.KomuUserId,
                         UserId = s.UserId,
                         UserName = s.User.UserName
                     }

                 })
                 .FirstOrDefaultAsync();
        }

        public async Task<ProjectUserExt> DeleteFuturePUAndNotify(long projectUserId)
        {
            var puExt = await GetPUExt(projectUserId);

            if (puExt == null || puExt.PU == null)
            {
                throw new UserFriendlyException($"Not found ProjectUser with Id {projectUserId}");
            }
            if (puExt.PU.Status != ProjectUserStatus.Future)
            {
                throw new UserFriendlyException($"ProjectUser with id {projectUserId} is not FUTURE");
            }

            await _workScope.DeleteAsync(puExt.PU);

            var sessionUser = await getSessionKomuUserInfo();
            var sbKomuMessage = new StringBuilder();

            sbKomuMessage.Append($"{sessionUser.KomuAccountInfo} **CANCEL PLAN: ** {puExt.Employee.KomuAccountInfo} ");
            sbKomuMessage.Append($"{CommonUtil.JoinOrOutProject(puExt.PU.AllocatePercentage)} {puExt.Project.KomuProjectInfo} ");
            sbKomuMessage.Append($"{CommonUtil.ProjectUserWorkTypeKomu(puExt.PU.IsPool)} on {DateTimeUtils.ToString(puExt.PU.StartTime)}");

            SendKomu(sbKomuMessage, puExt.Project.ProjectCode);

            return puExt;
        }



        public async Task EditProjectUserPlan(EditProjectUserDto input)
        {
            var projectUser = _workScope.GetAll<ProjectUser>()
                 .Where(s => s.Id == input.ProjectUserId).FirstOrDefault();
            projectUser.ProjectId = input.ProjectId;
            projectUser.AllocatePercentage = input.AllocatePercentage;
            projectUser.StartTime = input.StartTime;
            projectUser.Note = input.Note;
            projectUser.IsPool = input.IsPool;
            projectUser.ProjectRole = input.ProjectRole;
            await _workScope.UpdateAsync(projectUser);
        }


        public async Task<IQueryable<GetAllResourceDto>> QueryAllResource(InputGetResourceDto input, bool isVendor)
        {
            var activeReportId = await GetActiveReportId();
            var quser = _workScope.All<User>()
                       .Where(x => x.IsActive)
                       .Where(x => x.UserType != UserType.FakeUser)
                       .Where(u => isVendor ? u.UserType == UserType.Vendor : u.UserType != UserType.Vendor)
                       .Select(x => new GetAllResourceDto
                       {
                           UserId = x.Id,
                           UserType = x.UserType,
                           FullName = x.Name + " " + x.Surname,
                           NormalFullName = x.Surname + " " + x.Name,
                           EmailAddress = x.EmailAddress,
                           Branch = x.BranchOld,
                           BranchColor = x.Branch.Color,
                           BranchDisplayName = x.Branch.DisplayName,
                           BranchId = x.BranchId,
                           UserLevel = x.UserLevel,
                           AvatarPath = x.AvatarPath,
                           StarRate = x.StarRate,
                           UserSkills = x.UserSkills.Select(s => new UserSkillDto
                           {
                               SkillId = s.SkillId,
                               SkillName = s.Skill.Name
                           }).ToList(),

                           PlanProjects = x.ProjectUsers
                           .Where(pu => pu.Status == ProjectUserStatus.Future)
                           .Where(pu => pu.Project.Status != ProjectStatus.Closed)
                           .Where(pu => pu.PMReportId == activeReportId)
                           .Select(pu => new ProjectOfUserDto
                           {
                               Id = pu.Id,
                               ProjectId = pu.ProjectId,
                               ProjectName = pu.Project.Name,
                               ProjectRole = pu.ProjectRole,
                               PmName = pu.Project.PM.Name,
                               StartTime = pu.StartTime,
                               IsPool = pu.IsPool,
                               AllocatePercentage = pu.AllocatePercentage
                           })
                           .ToList(),

                           WorkingProjects = x.ProjectUsers
                            .Where(s => s.Status == ProjectUserStatus.Present
                            && s.AllocatePercentage > 0
                            && s.Project.Status != ProjectStatus.Closed)
                            .Select(pu => new ProjectOfUserDto
                            {
                                ProjectId = pu.Id,
                                ProjectName = pu.Project.Name,
                                ProjectRole = pu.ProjectRole,
                                ProjectStatus = pu.Project.Status,
                                PmName = pu.Project.PM.Name,
                                StartTime = pu.StartTime,
                                IsPool = pu.IsPool
                            })
                           .ToList(),

                       });

            return quser;
        }

        public IQueryable<long> queryUserIdsHaveAnySkill(List<long> skillIds)
        {
            if (skillIds == null || skillIds.IsEmpty())
            {
                throw new Exception("skillIds null or empty");
            }
            return _workScope.GetAll<UserSkill>()
                   .Where(s => skillIds.Contains(s.SkillId))
                   .Select(s => s.UserId);
        }


        public async Task<List<long>> getUserIdsHaveAllSkill(List<long> skillIds)
        {
            if (skillIds == null || skillIds.IsEmpty())
            {
                throw new Exception("skillIds null or empty");
            }

            var result = await _workScope.GetAll<UserSkill>()
                    .Where(s => skillIds[0] == s.SkillId)
                    .Select(s => s.UserId)
                    .Distinct()
                    .ToListAsync();

            if (result == null || result.IsEmpty())
            {
                return new List<long>();
            }

            for (var i = 1; i < skillIds.Count(); i++)
            {
                var userIds = await _workScope.GetAll<UserSkill>()
                    .Where(s => skillIds[i] == s.SkillId)
                    .Select(s => s.UserId)
                    .Distinct()
                    .ToListAsync();

                result = result.Intersect(userIds).ToList();

                if (result == null || result.IsEmpty())
                {
                    return new List<long>();
                }
            }

            return result;


        }

        public async Task<GridResult<GetAllPoolResourceDto>> GetAllPoolResource(InputGetResourceDto input)
        {
            var now = DateTimeUtils.GetNow().Date;
            var activeReportId = await GetActiveReportId();

            var workingUserIds = await _workScope.All<ProjectUser>()
                .Where(s => s.Status == ProjectUserStatus.Present)
                .Where(s => !s.IsPool)
                .Where(s => s.AllocatePercentage > 0)
                .Where(s => s.Project.Status == ProjectStatus.InProgress)
                .Select(s => s.UserId)
                .Distinct()
                .ToListAsync();

            var quser = _workScope.All<User>()
                       .Where(x => x.IsActive)
                       .Where(x => x.UserType != UserType.FakeUser)
                       .Where(x => x.UserType != UserType.Vendor)
                       .Where(s => !workingUserIds.Contains(s.Id))
                       .Select(u => new GetAllPoolResourceDto
                       {
                           UserId = u.Id,
                           UserType = u.UserType,
                           FullName = u.Name + " " + u.Surname,
                           EmailAddress = u.EmailAddress,
                           Branch = u.BranchOld,
                           BranchId = u.BranchId,
                           BranchColor = u.Branch.Color,
                           BranchDisplayName = u.Branch.DisplayName,
                           UserLevel = u.UserLevel,
                           AvatarPath = u.AvatarPath,
                           StarRate = u.StarRate,
                           PoolNote = u.PoolNote,
                           UserSkills = u.UserSkills.Select(s => new UserSkillDto
                           {
                               SkillId = s.SkillId,
                               SkillName = s.Skill.Name
                           }).ToList(),

                           PlannedProjects = u.ProjectUsers
                           .Where(pu => pu.Status == ProjectUserStatus.Future)
                           .Where(pu => pu.Project.Status != ProjectStatus.Closed)
                           .Where(pu => pu.PMReportId == activeReportId)
                           .Select(pu => new ProjectOfUserDto
                           {
                               ProjectName = pu.Project.Name,
                               ProjectId = pu.ProjectId,
                               ProjectRole = pu.ProjectRole,
                               ProjectStatus = pu.Project.Status,
                               PmName = pu.Project.PM.Name,
                               StartTime = pu.StartTime,
                               Id = pu.Id,
                               AllocatePercentage = pu.AllocatePercentage,
                               IsPool = pu.IsPool
                           })
                           .ToList(),

                           PoolProjects = u.ProjectUsers
                           .Where(pu => pu.Status == ProjectUserStatus.Present)
                           .Where(pu => pu.IsPool)
                           .Where(pu => pu.AllocatePercentage > 0)
                           .Where(pu => pu.Project.Status != ProjectStatus.Closed)
                           .Select(pu => new ProjectOfUserDto
                           {
                               ProjectId = pu.ProjectId,
                               ProjectName = pu.Project.Name,
                               ProjectRole = pu.ProjectRole,
                               PmName = pu.Project.PM.Name,
                               StartTime = pu.StartTime,
                               Id = pu.Id,
                           })
                           .ToList(),

                           LastReleaseDate = u.ProjectUsers.Where(x => x.Status == ProjectUserStatus.Present)
                            .Where(x => x.AllocatePercentage <= 0)
                            .OrderByDescending(x => x.StartTime)
                            .Select(x => x.StartTime).FirstOrDefault(),

                           UserCreationTime = u.CreationTime,
                       });


            if (input.SkillIds == null || input.SkillIds.IsEmpty())
            {

                return await quser.GetGridResult(quser, input);
            }
            if (input.SkillIds.Count() == 1 || !input.IsAndCondition)
            {
                var querySkillUserIds = queryUserIdsHaveAnySkill(input.SkillIds).Distinct();
                quser = from u in quser
                        join userId in querySkillUserIds on u.UserId equals userId
                        select u;


                return await quser.GetGridResult(quser, input);
            }
            var userIdsHaveAllSkill = await getUserIdsHaveAllSkill(input.SkillIds);
            quser = quser.Where(s => userIdsHaveAllSkill.Contains(s.UserId));
            return await quser.GetGridResult(quser, input);

        }


        public async Task<GridResult<GetAllResourceDto>> GetResources(InputGetResourceDto input, bool isVendor)
        {
            var query = await QueryAllResource(input, isVendor);
            if (input.SkillIds == null || input.SkillIds.IsEmpty())
            {
                return await query.GetGridResult(query, input);
            }
            if (input.SkillIds.Count() == 1 || !input.IsAndCondition)
            {
                var querySkillUserIds = queryUserIdsHaveAnySkill(input.SkillIds).Distinct();
                query = from u in query
                        join userId in querySkillUserIds on u.UserId equals userId
                        select u;

                return await query.GetGridResult(query, input);
            }

            var userIdsHaveAllSkill = await getUserIdsHaveAllSkill(input.SkillIds);
            query = query.Where(s => userIdsHaveAllSkill.Contains(s.UserId));

            return await query.GetGridResult(query, input);
        }

        public void SendKomu(StringBuilder komuMessage, string projectCode)
        {
            if (!projectCode.Equals(AppConsts.CHO_NGHI_PROJECT_CODE, StringComparison.OrdinalIgnoreCase))
            {
                 _komuService.NotifyToChannel(new KomuMessage
                {
                    CreateDate = DateTimeUtils.GetNow(),
                    Message = komuMessage.ToString(),
                },
                ChannelTypeConstant.PM_CHANNEL);
            }
        }

        public async Task<KomuUserInfoDto> getSessionKomuUserInfo()
        {
            return await getKomuUserInfo(AbpSession.UserId.Value);

        }
        private async Task<KomuUserInfoDto> getKomuUserInfo(long userId)
        {
            return await _workScope.GetAll<User>()
                .Where(s => s.Id == userId)
                .Select(s => new KomuUserInfoDto
                {
                    FullName = s.FullName,
                    UserId = s.Id,
                    KomuUserId = s.KomuUserId,
                    UserName = s.UserName
                }).FirstOrDefaultAsync();

        }

        public async Task<KomuUserInfoDto> GetKomuUserInfo(string emailAddress)
        {
            return await _workScope.GetAll<User>()
                .Where(s => s.NormalizedEmailAddress == emailAddress.ToUpper().Trim())
                .Select(s => new KomuUserInfoDto
                {
                    FullName = s.FullName,
                    UserId = s.Id,
                    KomuUserId = s.KomuUserId,
                    UserName = s.UserName
                }).FirstOrDefaultAsync();

        }

        public ProjectOfUserDto MapToProjectOfUserDto(ProjectUser pu)
        {
            return new ProjectOfUserDto
            {
                AllocatePercentage = pu.AllocatePercentage,
                Id = pu.Id,
                IsPool = pu.IsPool,
                PmName = pu.Project.PM.FullName,
                PMReportId = pu.PMReportId,
                ProjectId = pu.ProjectId,
                ProjectName = pu.Project.PM.FullName,
                ProjectRole = pu.ProjectRole,
                ProjectStatus = pu.Project.Status,
                StartTime = pu.StartTime,
                UserId = pu.UserId,
                Status = pu.Status,
                ProjectType = pu.Project.ProjectType,
            };
        }

        public UserOfProjectDto MapToUserofProjectDto(ProjectUser pu)
        {
            return new UserOfProjectDto
            {
                Id = pu.Id,
                ProjectId = pu.ProjectId,
                UserId = pu.UserId,
                AvatarPath = pu.User.AvatarPath != null ? pu.User.AvatarPath : "",
                FullName = pu.User.FullName,
                EmailAddress = pu.User.EmailAddress,
                Branch = pu.User.BranchOld,
                IsAvtive = pu.User.IsActive,
                UserLevel = pu.User.UserLevel,
                UserType = pu.User.UserType,
                AllocatePercentage = pu.AllocatePercentage,
                IsPool = pu.IsPool,
                ProjectRole = pu.ProjectRole,
                StartTime = pu.StartTime,
                PUStatus = pu.Status,
                StarRate = pu.User.StarRate,
                UserSkills = pu.User.UserSkills.Select(s => new UserSkillDto
                {
                    SkillId = s.SkillId,
                    SkillName = s.Skill.Name
                }).ToList(),
                Note = pu.Note,
            };
        }

        public async Task<List<UserOfProjectDto>> ResourceChangesDuringTheWeek(long projectId, long pmReportId)
        {
            return await _workScope.GetAll<ProjectUser>()
                .Where(x => x.ProjectId == projectId)
                .Where(x => x.PMReportId == pmReportId)
                .Where(x => x.Status != ProjectUserStatus.Future)
                .Where(x => x.User.UserType != UserType.FakeUser)
                .OrderByDescending(x => x.CreationTime)
                .Select(x => MapToUserofProjectDto(x))
                .ToListAsync();
        }

        public async Task<List<UserOfProjectDto>> ResourceChangesInTheFuture(long projectId, long pmReportId)
        {
            return await _workScope.GetAll<ProjectUser>()
                .Where(x => x.ProjectId == projectId)
                .Where(x => x.PMReportId == pmReportId)
                .Where(x => x.Status == ProjectUserStatus.Future)
                .Where(x => x.User.UserType != UserType.FakeUser)
                .OrderByDescending(x => x.CreationTime)
                .Select(x => MapToUserofProjectDto(x))
                .ToListAsync();
        }

        /// <summary>
        /// Release all working user from project
        /// Called when close project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<ProjectUser>> ReleaseAllWorkingUserFromProject(long projectId)
        {

            var presentPUs = await _workScope.GetAll<ProjectUser>()
                .Where(s => s.ProjectId == projectId)
                .Where(s => s.Status == ProjectUserStatus.Present)
                .Where(s => s.User.UserType != UserType.FakeUser)
                .Where(s => s.AllocatePercentage > 0)
                .ToListAsync();

            if (presentPUs == null || presentPUs.IsEmpty())
            {
                return null;
            }

            var activeReportId = await GetActiveReportId();
            foreach (var pu in presentPUs)
            {
                pu.Status = ProjectUserStatus.Past;
                pu.PMReportId = activeReportId;
            }
            await CurrentUnitOfWork.SaveChangesAsync();

            foreach (var pu in presentPUs)
            {
                pu.Id = 0;
                pu.AllocatePercentage = 0;
                pu.Status = ProjectUserStatus.Present;
                pu.StartTime = DateTimeUtils.GetNow();
                pu.Note = "Be released by closing project";
                await _workScope.InsertAndGetIdAsync(pu);
            }

            return presentPUs;
        }

    }
}
