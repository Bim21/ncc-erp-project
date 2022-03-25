﻿using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.APIs.ProjectUsers;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.Sessions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;
using ProjectManagement.Services.Timesheet;
using Abp.Domain.Repositories;
using Abp.Authorization.Users;
using ProjectManagement.APIs.ResourceRequests.Dto;
using Abp.Configuration;
using ProjectManagement.Configuration;

namespace ProjectManagement.APIs.Projects
{
    [AbpAuthorize]
    public class ProjectAppService : ProjectManagementAppServiceBase
    {
        private readonly IProjectUserAppService _projectUserAppService;
        private readonly RoleManager _roleManager;
        private readonly TimesheetService _timesheetService;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly ISettingManager _settingManager;
        public ProjectAppService(IProjectUserAppService projectUserAppService, TimesheetService timesheetService, IRepository<UserRole, long> userRoleRepository,
            RoleManager roleManager, ISettingManager settingManager)
        {
            _projectUserAppService = projectUserAppService;
            _timesheetService = timesheetService;
            _userRoleRepository = userRoleRepository;
            _roleManager = roleManager;
            _settingManager = settingManager;
        }
        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewAll, PermissionNames.PmManager_Project_ViewonlyMe)]
        public async Task<GridResult<GetProjectDto>> GetAllPaging(GridParam input)
        {
            bool isViewAll = await PermissionChecker.IsGrantedAsync(PermissionNames.PmManager_Project_ViewAll);
            var filterStatus = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "status") : null;
            var filterPmId = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "pmId" && Convert.ToInt64(x.Value) == -1) : null;
            int valueStatus = -1;
            if (filterStatus != null)
            {
                valueStatus = Convert.ToInt32(filterStatus.Value);
                input.FilterItems.Remove(filterStatus);
            }
            if (filterPmId != null)
            {
                input.FilterItems.Remove(filterPmId);
            }
            var query = from p in WorkScope.GetAll<Project>().Include(x => x.Currency)
                        .Where(x => isViewAll || x.PMId == AbpSession.UserId.Value)
                        .Where(x => x.ProjectType != ProjectType.TRAINING && x.ProjectType != ProjectType.PRODUCT)
                        .Where(x => filterStatus != null && valueStatus > -1 ? (valueStatus == 3 ? x.Status != ProjectStatus.Closed : x.Status == (ProjectStatus)valueStatus) : true)
                        join rp in WorkScope.GetAll<PMReportProject>().Where(x => x.PMReport.IsActive) on p.Id equals rp.ProjectId into lst
                        from l in lst.DefaultIfEmpty()
                        select new GetProjectDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Code = p.Code,
                            ProjectType = p.ProjectType,
                            StartTime = p.StartTime.Date,
                            EndTime = p.EndTime.Value.Date,
                            Status = p.Status,
                            ClientId = p.ClientId,
                            ClientName = p.Client.Name,
                            CurrencyId = p.CurrencyId,
                            CurrencyName = p.Currency.Name,
                            IsCharge = p.IsCharge,
                            ChargeType = p.ChargeType,
                            PmId = p.PMId,
                            PmName = p.PM.Name,
                            PmFullName = p.PM.FullName,
                            PmAvatarPath = p.PM.AvatarPath,
                            PmEmailAddress = p.PM.EmailAddress,
                            PmUserName = p.PM.UserName,
                            PmUserType = p.PM.UserType,
                            PmBranch = p.PM.Branch,
                            IsSent = l.Status,
                            TimeSendReport = l.TimeSendReport,
                            DateSendReport = l.TimeSendReport.Value.Date,
                            RequireTimesheetFile = p.RequireTimesheetFile
                        };
            return await query.GetGridResult(query, input);
        }



        [HttpGet]
        public async Task<List<ProjectInfoDto>> GetAllProjectInfo()
        {
            var query = WorkScope.GetAll<Project>()
                .Select(x => new ProjectInfoDto
                {
                    ProjectId = x.Id,
                    ProjectName = x.Name,
                    ProjectType = x.ProjectType,
                    ProjectStatus = x.Status,
                });
            return await query.ToListAsync();
        }

        [HttpGet]
        public async Task<List<GetProjectDto>> GetAll()
        {
            var query = WorkScope.GetAll<Project>()
                .Include(x => x.Currency)
                //.Where(x => x.Status != ProjectStatus.Potential && x.Status != ProjectStatus.Closed)
                .Select(x => new GetProjectDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    ProjectType = x.ProjectType,
                    StartTime = x.StartTime.Date,
                    EndTime = x.EndTime.Value.Date,
                    Status = x.Status,
                    ClientId = x.ClientId,
                    ClientName = x.Client.Name,
                    CurrencyId = x.CurrencyId,
                    CurrencyName = x.Currency.Name,
                    IsCharge = x.IsCharge,
                    ChargeType = x.ChargeType,
                    PmId = x.PMId,
                    PmName = x.PM.Name,
                });
            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewDetail)]
        public async Task<GetProjectDto> Get(long projectId)
        {
            var query = WorkScope.GetAll<Project>().Include(x => x.Currency).Where(x => x.Id == projectId)
                                .Select(x => new GetProjectDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Code = x.Code,
                                    ProjectType = x.ProjectType,
                                    StartTime = x.StartTime.Date,
                                    EndTime = x.EndTime.Value.Date,
                                    Status = x.Status,
                                    ClientId = x.ClientId,
                                    ClientName = x.Client.Name,
                                    IsCharge = x.IsCharge,
                                    ChargeType = x.ChargeType,
                                    PmId = x.PMId,
                                    PmName = x.PM.Name,
                                    PmFullName = x.PM.FullName,
                                    PmUserName = x.PM.UserName,
                                    PmEmailAddress = x.PM.EmailAddress,
                                    PmAvatarPath = x.PM.AvatarPath,
                                    PmBranch = x.PM.Branch,
                                    PmUserType = x.PM.UserType,
                                    CurrencyId = x.CurrencyId,
                                    CurrencyName = x.Currency.Name,
                                    RequireTimesheetFile = x.RequireTimesheetFile,

                                });
            return await query.FirstOrDefaultAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewProjectInfor)]
        public async Task<ProjectDetailDto> GetProjectDetail(long projectId)
        {
            return await WorkScope.GetAll<Project>().Where(x => x.Id == projectId)
                              .Select(x => new ProjectDetailDto
                              {
                                  ProjectId = x.Id,
                                  BriefDescription = x.BriefDescription,
                                  DetailDescription = x.DetailDescription,
                                  TechnologyUsed = x.TechnologyUsed,
                                  TechnicalProblems = x.TechnicalProblems,
                                  OtherProblems = x.OtherProblems,
                                  NewKnowledge = x.NewKnowledge
                              }).FirstOrDefaultAsync();
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_Project_UpdateProjectDetail)]
        public async Task<ProjectDetailDto> UpdateProjectDetail(ProjectDetailDto input)
        {
            var project = await WorkScope.GetAsync<Project>(input.ProjectId);

            await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectDetailDto, Project>(input, project));
            return input;
        }

        private async Task<string> UpdateRoleProject(long PmId)
        {
            //Set role PM for user when user set PM of project
            var role = await _roleManager.GetRoleByNameAsync(StaticRoleNames.Tenants.PM);
            var isRolePM = await (from ur in _userRoleRepository.GetAll()
                                  where ur.RoleId == role.Id
                                  where ur.UserId == PmId
                                  select ur.Id).FirstOrDefaultAsync();
            if (isRolePM == default)
            {
                await _userRoleRepository.InsertAsync(new UserRole
                {
                    RoleId = role.Id,
                    UserId = PmId
                });
            }
            return null;
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_Create)]
        private async Task<string> CreateProjectInTimesheet(ProjectDto input)
        {

            await UpdateRoleProject(input.PmId);

            var customerCode = await WorkScope.GetAll<Client>().Where(x => x.Id == input.ClientId).Select(x => x.Code).FirstOrDefaultAsync();
            var emailPM = await WorkScope.GetAll<User>().Where(x => x.Id == input.PmId).Select(x => x.EmailAddress).FirstOrDefaultAsync(); ;
            if (customerCode == default)
            {
                //Training + Product
                customerCode = "NCC";
            }
            var createProject = await _timesheetService.createProject(input.Name, input.Code, input.StartTime, input.EndTime,
                                                                       customerCode, input.ProjectType, emailPM);
            return createProject;
        }


        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_Update)]
        private async Task<string> UpdateProjectTimesheet(string code, long? PmId, ProjectStatus status)
        {
            var emailPM = "";
            if (PmId != null)
            {
                emailPM = await WorkScope.GetAll<User>().Where(x => x.Id == PmId).Select(x => x.EmailAddress).FirstOrDefaultAsync();
            }
            var updateProject = await _timesheetService.updateProject(code, emailPM, status);
            return updateProject;
        }


        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_Create)]
        public async Task<string> Create(ProjectDto input)
        {
            var isExist = await WorkScope.GetAll<Project>().AnyAsync(x => x.Name == input.Name || x.Code == input.Code);

            if (isExist)
                throw new UserFriendlyException("Name or Code already exist !");

            if (input.EndTime.HasValue && input.StartTime.Date > input.EndTime.Value.Date)
            {
                throw new UserFriendlyException("Start time cannot be greater than end time !");
            }

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<Project>(input));

            var projectCheckLists = await WorkScope.GetAll<CheckListItemMandatory>()
                                .Where(x => x.ProjectType == input.ProjectType)
                                .Select(x => new ProjectCheckList
                                {
                                    ProjectId = input.Id,
                                    CheckListItemId = x.CheckListItemId,
                                    IsActive = true,
                                }).ToListAsync();

            foreach (var i in projectCheckLists)
            {
                await WorkScope.InsertAsync(i);
            }

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            var pmReportProject = new PMReportProject
            {
                PMReportId = pmReportActive.Id,
                ProjectId = input.Id,
                Status = PMReportProjectStatus.Draft,
                ProjectHealth = ProjectHealth.Green,
                PMId = input.PmId,
                Note = null
            };
            await WorkScope.InsertAsync(pmReportProject);

            return await CreateProjectInTimesheet(input);
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_Project_Update)]
        public async Task<string> Update(ProjectDto input)
        {
            var allproject = await WorkScope.GetAll<Project>().Select(x => x.Id).ToListAsync();
            var project = await WorkScope.GetAsync<Project>(input.Id);

            var isExist = await WorkScope.GetAll<Project>().AnyAsync(x => x.Id != input.Id && (x.Name == input.Name || x.Code == input.Code));

            if (isExist)
                throw new UserFriendlyException("Name or Code already exist !");

            if (input.EndTime.HasValue && input.StartTime.Date > input.EndTime.Value.Date)
            {
                throw new UserFriendlyException("Start time cannot be greater than end time !");
            }


            if (input.Status == ProjectStatus.Closed)
            {
                var getProjectUserbyId = await _projectUserAppService.GetAllByProject(input.Id, false);
                foreach (var item in getProjectUserbyId)
                {
                    if (item.Status == "Present")
                    {
                        ProjectUserRole role;
                        Enum.TryParse(item.ProjectRole, out role);
                        var projectUser = new ProjectUserDto()
                        {
                            UserId = item.UserId,
                            ProjectId = item.ProjectId,
                            ProjectRole = role,
                            AllocatePercentage = 0,
                            StartTime = DateTime.Now,
                            Status = ProjectUserStatus.Present,
                            IsExpense = true,
                            PMReportId = item.PMReportId,
                            IsFutureActive = true,
                        };
                        await _projectUserAppService.Create(projectUser);
                    }
                }

            }

            ProjectStatus statusTemp = project.Status;
            long pmIdTemp = project.PMId;


            await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectDto, Project>(input, project));
            await UpdateRoleProject(input.PmId);
            var updateProject = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.UpdateProject);
            if (updateProject == "true" && input.Status != ProjectStatus.Potential && (input.Status != statusTemp || input.PmId != pmIdTemp))
            {
                return await UpdateProjectTimesheet(input.Code, input.PmId, input.Status);
            }

            return "update-only-project-tool";
        }


        public class ProjectUpdateStatus
        {
            public long Id { get; set; }

            public ProjectStatus Status { get; set; }

        }
        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_Update)]
        public async Task<string> UpdateStatusProject(ProjectUpdateStatus input)
        {
            var project = await WorkScope.GetAsync<Project>(input.Id);
            project.Status = input.Status;
            await WorkScope.UpdateAsync(ObjectMapper.Map<Project>(project));
            var updateProject = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.UpdateProject);
            if (updateProject == "true" && input.Status != ProjectStatus.Potential)
            {
                return await UpdateProjectTimesheet(project.Code, null, input.Status);
            }
            return "update-only-project-tool";
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.PmManager_Project_Delete)]
        public async Task Delete(long projectID)
        {
            var project = await WorkScope.GetAsync<Project>(projectID);

            var timesheetProject = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.ProjectId == projectID);
            if (timesheetProject)
                throw new UserFriendlyException($"Project has Timesheet Project !");

            var pmReportProject = await WorkScope.GetAll<PMReportProject>().AnyAsync(x => x.ProjectId == projectID);
            if (pmReportProject)
                throw new UserFriendlyException($"Project has Weekly Report !");

            var projectUser = await WorkScope.GetAll<ProjectUser>().AnyAsync(x => x.ProjectId == projectID);
            if (projectUser)
                throw new UserFriendlyException($"Project has Project User !");

            await WorkScope.DeleteAsync(project);
        }
        #region PAGE TRAINING PROJECT
        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewAll, PermissionNames.PmManager_Project_ViewonlyMe)]
        public async Task<GridResult<GetTrainingProjectDto>> GetAllTrainingProjectPaging(GridParam input)
        {
            var filterStatus = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "status") : null;
            var filterPmId = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "pmId" && Convert.ToInt64(x.Value) == -1) : null;
            int valueStatus = -1;
            if (filterStatus != null)
            {
                valueStatus = Convert.ToInt32(filterStatus.Value);
                input.FilterItems.Remove(filterStatus);
            }
            if (filterPmId != null)
            {
                input.FilterItems.Remove(filterPmId);
            }
            var query = from p in WorkScope.GetAll<Project>().Include(x => x.Currency)
                        .Where(x => x.ProjectType == ProjectType.TRAINING)
                        .Where(x => filterStatus != null && valueStatus > -1 ? (valueStatus == 3 ? x.Status != ProjectStatus.Closed : x.Status == (ProjectStatus)valueStatus) : true)
                        join rp in WorkScope.GetAll<PMReportProject>().Where(x => x.PMReport.IsActive) on p.Id equals rp.ProjectId into lst
                        from l in lst.DefaultIfEmpty()
                        select new GetTrainingProjectDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Code = p.Code,
                            StartTime = p.StartTime.Date,
                            EndTime = p.EndTime.Value.Date,
                            Status = p.Status,
                            PmId = p.PMId,
                            PmName = p.PM.Name,
                            ClientId = p.ClientId,
                            PmFullName = p.PM.FullName,
                            PmAvatarPath = p.PM.AvatarPath,
                            PmEmailAddress = p.PM.EmailAddress,
                            PmUserName = p.PM.UserName,
                            PmUserType = p.PM.UserType,
                            PmBranch = p.PM.Branch,
                            IsSent = l.Status,
                            TimeSendReport = l.TimeSendReport,
                            DateSendReport = l.TimeSendReport.Value.Date,
                            Evaluation = l.Note,
                        };
            return await query.GetGridResult(query, input);
        }
        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_Create)]
        public async Task<string> CreateTrainingProject(TrainingProjectDto input)
        {
            var isExist = await WorkScope.GetAll<Project>().AnyAsync(x => x.Name == input.Name || x.Code == input.Code || x.Id == input.Id);

            if (isExist)
                throw new UserFriendlyException("Name or Code already exist !");

            if (input.EndTime.HasValue && input.StartTime.Date > input.EndTime.Value.Date)
            {
                throw new UserFriendlyException("Start time cannot be greater than end time !");
            }
            input.ProjectType = ProjectType.TRAINING;
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<Project>(input));

            var projectCheckLists = await WorkScope.GetAll<CheckListItemMandatory>()
                                .Where(x => x.ProjectType == ProjectType.TRAINING)
                                .Select(x => new ProjectCheckList
                                {
                                    ProjectId = input.Id,
                                    CheckListItemId = x.CheckListItemId,
                                    IsActive = true,
                                }).ToListAsync();

            foreach (var i in projectCheckLists)
            {
                await WorkScope.InsertAsync(i);
            }

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            var pmReportProject = new PMReportProject
            {
                PMReportId = pmReportActive.Id,
                ProjectId = input.Id,
                Status = PMReportProjectStatus.Draft,
                ProjectHealth = ProjectHealth.Green,
                PMId = input.PmId,
                Note = null
            };
            await WorkScope.InsertAsync(pmReportProject);


            var trainingProject = new ProjectDto
            {
                Name = input.Name,
                Code = input.Code,
                ProjectType = ProjectType.TRAINING,
                PmId = input.PmId,
                StartTime = input.StartTime,
                EndTime = input.EndTime
            };

            return await CreateProjectInTimesheet(trainingProject);
        }
        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_Project_Update)]
        public async Task<string> UpdateTrainingProject(TrainingProjectDto input)
        {
            input.ProjectType = ProjectType.TRAINING;
            var project = await WorkScope.GetAsync<Project>(input.Id);

            var isExist = await WorkScope.GetAll<Project>().AnyAsync(x => x.Id != input.Id && (x.Name == input.Name || x.Code == input.Code));

            if (isExist)
                throw new UserFriendlyException("Name or Code already exist !");

            if (input.EndTime.HasValue && input.StartTime.Date > input.EndTime.Value.Date)
            {
                throw new UserFriendlyException("Start time cannot be greater than end time !");
            }


            ProjectStatus statusTemp = project.Status;
            long pmIdTemp = project.PMId;

            await WorkScope.UpdateAsync(ObjectMapper.Map<TrainingProjectDto, Project>(input, project));
            await UpdateRoleProject(input.PmId);
            var updateProject = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.UpdateProject);
            if (updateProject == "true" && input.Status != ProjectStatus.Potential && (input.Status != statusTemp || input.PmId != pmIdTemp))
            {
                return await UpdateProjectTimesheet(input.Code, input.PmId, input.Status);
            }

            return "update-only-project-tool";
        }
        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewDetail)]
        public async Task<GetTrainingProjectDto> GetDetailTrainingProject(long projectId)
        {
            var query = WorkScope.GetAll<Project>().Where(x => x.Id == projectId).Where(x => x.ProjectType == ProjectType.TRAINING)
                                .Select(x => new GetTrainingProjectDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Code = x.Code,
                                    StartTime = x.StartTime.Date,
                                    EndTime = x.EndTime.Value.Date,
                                    Status = x.Status,
                                    PmId = x.PMId,
                                    PmName = x.PM.Name,
                                    PmFullName = x.PM.FullName,
                                    PmUserName = x.PM.UserName,
                                    PmEmailAddress = x.PM.EmailAddress,
                                    PmAvatarPath = x.PM.AvatarPath,
                                    PmBranch = x.PM.Branch,
                                    PmUserType = x.PM.UserType,
                                });
            return await query.FirstOrDefaultAsync();
        }
        #endregion

        #region PAGE PRODUCT PROJECT
        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewAll, PermissionNames.PmManager_Project_ViewonlyMe)]
        public async Task<GridResult<ProductProjectDto>> GetAllProductProjectPaging(GridParam input)
        {
            var filterStatus = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "status") : null;
            var filterPmId = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "pmId" && Convert.ToInt64(x.Value) == -1) : null;
            int valueStatus = -1;
            if (filterStatus != null)
            {
                valueStatus = Convert.ToInt32(filterStatus.Value);
                input.FilterItems.Remove(filterStatus);
            }
            if (filterPmId != null)
            {
                input.FilterItems.Remove(filterPmId);
            }
            var query = from p in WorkScope.GetAll<Project>()
                        .Where(x => x.ProjectType == ProjectType.PRODUCT)
                        .Where(x => filterStatus != null && valueStatus > -1 ? (valueStatus == 3 ? x.Status != ProjectStatus.Closed : x.Status == (ProjectStatus)valueStatus) : true)
                        join rp in WorkScope.GetAll<PMReportProject>().Where(x => x.PMReport.IsActive) on p.Id equals rp.ProjectId into lst
                        from l in lst.DefaultIfEmpty()
                        select new ProductProjectDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Code = p.Code,
                            StartTime = p.StartTime.Date,
                            EndTime = p.EndTime.Value.Date,
                            Status = p.Status,
                            PmId = p.PMId,
                            ClientId = p.ClientId,
                            PmName = p.PM.Name,
                            PmFullName = p.PM.FullName,
                            PmAvatarPath = p.PM.AvatarPath,
                            PmEmailAddress = p.PM.EmailAddress,
                            PmUserName = p.PM.UserName,
                            PmUserType = p.PM.UserType,
                            PmBranch = p.PM.Branch,
                            IsSent = l.Status,
                            TimeSendReport = l.TimeSendReport,
                            DateSendReport = l.TimeSendReport.Value.Date
                        };
            return await query.GetGridResult(query, input);
        }
        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewDetail)]
        public async Task<ProductProjectDto> GetDetailProductProject(long projectId)
        {
            var query = WorkScope.GetAll<Project>().Where(x => x.Id == projectId).Where(x => x.ProjectType == ProjectType.PRODUCT)
                                .Select(x => new ProductProjectDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Code = x.Code,
                                    StartTime = x.StartTime.Date,
                                    EndTime = x.EndTime.Value.Date,
                                    Status = x.Status,
                                    PmId = x.PMId,
                                    PmName = x.PM.Name,
                                    PmFullName = x.PM.FullName,
                                    PmUserName = x.PM.UserName,
                                    PmEmailAddress = x.PM.EmailAddress,
                                    PmAvatarPath = x.PM.AvatarPath,
                                    PmBranch = x.PM.Branch,
                                    PmUserType = x.PM.UserType,
                                });
            return await query.FirstOrDefaultAsync();
        }
        #endregion
    }
}
