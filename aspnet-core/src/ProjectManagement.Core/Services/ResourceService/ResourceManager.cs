using Abp.Dependency;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.IoC;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Entities;
using ProjectManagement.Services.ResourceManager.Dto;
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
    public class ResourceManager : ITransientDependency
    {
        private readonly IWorkScope _workScope;
        private readonly IAbpSession _abpSession;

        public ResourceManager(IWorkScope workScope, IAbpSession abpSession)
        {
            _workScope = workScope;
            _abpSession = abpSession;
        }

        public IQueryable<ProjectOfUserDto> QueryProjectsOfUser()
        {
            return _workScope.GetAll<ProjectUser>()
                .Where(x => x.User.UserType != UserType.FakeUser)
                .Select(x => new ProjectOfUserDto
                {
                    Id = x.Id,
                    ProjectId = x.ProjectId,
                    ProjectName = x.Project.Name,
                    ProjectRole = x.ProjectRole,
                    PmName = x.Project.PM.Name + " " + x.Project.PM.Surname,
                    AllocatePercentage = x.AllocatePercentage,
                    StartTime = x.StartTime,
                    PUStatus = x.Status,
                    UserId = x.UserId,
                    ProjectStatus = x.Project.Status,
                    IsPool = x.IsPool,
                });
        }

        public IQueryable<UserOfProjectDto> QueryUsersOfPRoject()
        {
            return _workScope.GetAll<ProjectUser>()
                .Select(x => new UserOfProjectDto
                {
                    ProjectId = x.ProjectId,
                    Status = x.Status,
                    UserLevel = x.User.UserLevel,
                    AllocatePercentage = x.AllocatePercentage,
                    AvatarPath = x.User.AvatarPath,
                    Branch = x.User.Branch,
                    EmailAddress = x.User.EmailAddress,
                    FullName = x.User.FullName,
                    Id = x.Id,
                    ProjectRole = x.ProjectRole,
                    StartTime = x.StartTime,
                    UserType = x.User.UserType,
                    UserId = x.UserId
                });
        }

        public async Task<List<UserOfProjectDto>> GetCurrentUsersByProjectId(long projectId)
        {
            return await QueryUsersOfPRoject()
                .Where(s => s.ProjectId == projectId)
                .Where(s => s.Status == ProjectUserStatus.Present)
                .Where(s => s.AllocatePercentage > 0)
                .OrderBy(s => s.ProjectRole)
                .ThenByDescending(s => s.StartTime)
                .ToListAsync();
        }


        public IQueryable<UserOfProjectDto> QueryWorkingUsersOfPRoject(long projectId)
        {
            var queryPu = _workScope.GetAll<ProjectUser>()
                .Where(s => s.ProjectId == projectId)
                .OrderBy(s => s.ProjectRole)
                .ThenByDescending(s => s.StartTime);

            var queryUser = from u in _workScope.GetAll<User>()
                      .Where(x => x.UserType != UserType.FakeUser)
                            join pu in queryPu on u.Id equals pu.UserId
                            select new UserOfProjectDto
                            {
                                ProjectId = pu.ProjectId,
                                AvatarPath = u.AvatarPath,
                                FullName = u.Name + " " + u.Surname,
                                EmailAddress = u.EmailAddress,
                                Branch = u.Branch,
                                IsAvtive = u.IsActive,
                                UserLevel = u.UserLevel,
                                UserType = u.UserType,
                                AllocatePercentage = pu.AllocatePercentage,
                                IsPool = pu.IsPool,
                                ProjectRole = pu.ProjectRole,
                                StartTime = pu.StartTime,
                                Status = pu.Status,
                            };
            return queryUser;
        }


        public async Task<List<ProjectOfUserDto>> GetWorkingProjectsByUserId(long userId)
        {
            return await QueryProjectsOfUser()
                .Where(s => s.UserId == userId)
                .Where(s => s.PUStatus == ProjectUserStatus.Present)
                .Where(s => s.AllocatePercentage > 0)
                .Where(s => s.ProjectStatus == ProjectStatus.InProgress)
                .OrderBy(s => s.ProjectRole)
                .ThenByDescending(s => s.StartTime)
                .ToListAsync();
        }


        public async Task<List<ResourceHisotryDto>> GetResourceHistory(long userId)
        {
            return await _workScope.GetAll<ProjectUser>().Where(s => s.UserId == userId)
                   .Where(sa => sa.Status == ProjectUserStatus.Past || (sa.Status == ProjectUserStatus.Present & sa.AllocatePercentage <= 0))
                   .Select(pu => new ResourceHisotryDto
                   {
                       ProjectId = pu.ProjectId,
                       ProjectName = pu.Project.Name,
                       ProjectRole = pu.ProjectRole,
                       allowcatePercentage = pu.AllocatePercentage,
                       StartTime = pu.StartTime,
                       Status = pu.Status,
                   })
                   .OrderByDescending(s => s.StartTime)
                   .ToListAsync();
        }


        public async Task<List<ResourceHisotryDto>> AddUserToProject(AddResourceToProjectDto input)
        {
            var activeWeeklyReportId = _workScope.GetAll<PMReport>()
                .Where(s => s.IsActive == true)
                .Select(s => s.Id).FirstOrDefault();
            var currentProjects = _workScope.GetAll<ProjectUser>()
                .Where(s => s.UserId == input.UserId)
                .Where(s => s.Status == ProjectUserStatus.Present);






            if (currentProjects.Any())
            {
                foreach (var pu in currentProjects)
                {
                    pu.Status = ProjectUserStatus.Past;
                    pu.AllocatePercentage = 0;
                    await _workScope.UpdateAsync(pu);
                }
            }

            var newPu = new ProjectUser
            {
                UserId = input.UserId,
                ProjectId = input.ProjectId,
                Status = ProjectUserStatus.Present,
                AllocatePercentage = 100,
                StartTime = input.StartTime,
                CreationTime = DateTime.Now,
                PMReportId = activeWeeklyReportId,
                Note = input.Note,
            };
          
            await _workScope.InsertAsync(newPu);

            return null;
        }




        public async Task<List<PlanedResourceDto>> GetAllPlanedResource()
        {
            return await _workScope.GetAll<ProjectUser>()
                .Where(x => x.Status == ProjectUserStatus.Future)
                .Select(x => new PlanedResourceDto
                {
                    ProjectName = x.Project.Name,
                    StartTime = x.StartTime,
                    AllocatePercentage = x.AllocatePercentage,
                    ProjectRole = x.ProjectRole.ToString(),
                    FullName = x.User.FullName,
                    UserType = x.User.UserType,
                    Branch = x.User.Branch,
                    AvatarPath = x.User.AvatarPath,
                    EmailAddress = x.User.EmailAddress,
                    Note = x.Note,
                }).ToListAsync();
        }



        public IQueryable<PlanedResourceDto> QueryPlansOfUser()
        {
            return _workScope.GetAll<ProjectUser>()
            .Where(x => x.Status == ProjectUserStatus.Future)
            .Select(x => new PlanedResourceDto
            {
                ProjectName = x.Project.Name,
                StartTime = x.StartTime,
                AllocatePercentage = x.AllocatePercentage,
                ProjectRole = x.ProjectRole.ToString(),
                FullName = x.User.FullName,
                UserType = x.User.UserType,
                Branch = x.User.Branch,
                AvatarPath = x.User.AvatarPath,
                EmailAddress = x.User.EmailAddress,
                Note = x.Note,
            });
        }

        public async Task PlanResourceToProject(InputPlanResourceDto input)
        {
            var newPlanResource = new ProjectUser
            {
                AllocatePercentage = 100,
                Status = ProjectUserStatus.Future,
                CreationTime = DateTime.Now,
                CreatorUserId = _abpSession.UserId,
                ProjectId = input.ProjectId,
                StartTime = input.StartTime,
                Note = input.Note
            };
            _workScope.Insert(newPlanResource);

        }


        public async Task ConfirmPlanOfUser(ConfirmJoinProjectDto input)
        {
            var activeWeeklyReportId = _workScope.GetAll<PMReport>()
              .Where(s => s.IsActive == true)
              .Select(s => s.Id).FirstOrDefault();
            var currentUserPlans = _workScope.GetAll<ProjectUser>()
                .Where(s => s.UserId == input.UserId);
            var confirmPu = await _workScope.GetAll<ProjectUser>()
                .Where(s => s.Id == input.projectUserId).FirstOrDefaultAsync();
            if (currentUserPlans.Any())
            {
                foreach (var currentPlan in currentUserPlans)
                {
                    currentPlan.Status = ProjectUserStatus.Past;
                    currentPlan.PMReportId = activeWeeklyReportId;
                    await _workScope.UpdateAsync(currentPlan);
                }
            }
            confirmPu.Status = ProjectUserStatus.Present;
            //confirmPu.StartTime = input.startTime;
            confirmPu.PMReportId = activeWeeklyReportId;
            await _workScope.UpdateAsync(confirmPu);
        }


        public async Task EditProjectUserPlan(long projectUserId, EditProjectUserDto input)
        {
            var projectUser = _workScope.GetAll<ProjectUser>()
                 .Where(s => s.Id == projectUserId).FirstOrDefault();
            projectUser.ProjectId = input.ProjectId;
            projectUser.AllocatePercentage = input.AllocatePercentage;
            projectUser.StartTime = input.StartTime;
            projectUser.Note = input.Note;
            await _workScope.UpdateAsync(projectUser);
        }


        public IQueryable<GetAllResourceDto> QueryAllResource(InputGetResourceDto input, bool isVendor)
        {
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
                           Branch = x.Branch,
                           UserLevel = x.UserLevel,
                           AvatarPath = "/avatars/" + x.AvatarPath,
                           StarRate = x.StarRate,
                           Used = x.ProjectUsers
                            .Where(p => p.Project.Status != ProjectStatus.Potential
                            && p.Project.Status != ProjectStatus.Closed
                            && p.Status == ProjectUserStatus.Present
                            && p.IsFutureActive)
                            .Sum(y => y.AllocatePercentage),
                           UserSkills = x.UserSkills.Select(s => new UserSkillDto
                           {
                               SkillId = s.SkillId,
                               SkillName = s.Skill.Name
                           }).ToList(),
                           ProjectUserPlans = x.ProjectUsers
                           .Where(pu => pu.Status == ProjectUserStatus.Future && pu.IsFutureActive)
                           .Where(pu => pu.Project.Status != ProjectStatus.Potential && pu.Project.Status != ProjectStatus.Closed)
                           .Select(p => new ProjectUserPlans
                           {
                               ProjectUserId = p.Id,
                               ProjectName = p.Project.Name,
                               StartTime = p.StartTime.Date,
                               AllocatePercentage = p.AllocatePercentage
                           })
                           .ToList(),
                           WorkingProjects = x.ProjectUsers
                            .Where(s => s.Status == ProjectUserStatus.Present
                            && s.AllocatePercentage > 0
                            && s.Project.Status != ProjectStatus.Potential
                            && s.Project.Status != ProjectStatus.Closed)
                            .Select(p => new WorkingProjectDto
                            {
                                projectId = p.ProjectId,
                                ProjectName = p.Project.Name,
                                ProjectRole = p.ProjectRole,
                                StartTime = p.StartTime
                            }).ToList(),
                       });

            if (input.SkillIds != null && !input.SkillIds.IsEmpty())
            {
                //OR
                var qSkillUserIds = _workScope.GetAll<UserSkill>()
                   .Where(s => input.SkillIds.Contains(s.SkillId))
                   .Select(s => s.UserId);

                //AND
                if (input.IsAndCondition)
                {
                    qSkillUserIds = _workScope.GetAll<UserSkill>()
                    .Where(s => input.SkillIds[0] == s.SkillId)
                    .Select(s => s.UserId);

                    for (var i = 1; i < input.SkillIds.Count(); i++)
                    {
                        qSkillUserIds = from userId in qSkillUserIds
                                    join userId2 in (_workScope.GetAll<UserSkill>()
                                    .Where(s => input.SkillIds[i] == s.SkillId)
                                    .Select(s => s.UserId)) on userId equals userId2
                                    select userId;
                    }

                }

                quser = from u in quser
                        join userId in qSkillUserIds on u.UserId equals userId
                        select u;

            }
            return quser;
        }



    }
}
