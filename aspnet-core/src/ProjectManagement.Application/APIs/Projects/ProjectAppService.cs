using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.Projects
{
    [AbpAuthorize]
    public class ProjectAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewAll, PermissionNames.PmManager_Project_ViewonlyMe)]
        public async Task<GridResult<GetProjectDto>> GetAllPaging(GridParam input)
        {
            bool isViewAll = await PermissionChecker.IsGrantedAsync(PermissionNames.PmManager_Project_ViewAll);
            var filterStatus = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "status") : null;
            int valueStatus = -1;
            if (filterStatus != null)
            {
                valueStatus = Convert.ToInt32(filterStatus.Value);
                input.FilterItems.Remove(filterStatus);
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
                            PmId = p.PMId,
                            PmName = p.PM.Name,
                            PmFullName = p.PM.FullName,
                            PmAvatarPath = "/avatars/" + p.PM.AvatarPath,
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
                                    PmId = x.PMId,
                                    PmName = x.PM.Name,
                                    PmFullName = x.PM.FullName,
                                    PmUserName = x.PM.UserName,
                                    PmEmailAddress = x.PM.EmailAddress,
                                    PmAvatarPath = "/avatars/" + x.PM.AvatarPath,
                                    PmBranch = x.PM.Branch,
                                    PmUserType = x.PM.UserType,
                                    CurrencyId = x.CurrencyId,
                                    CurrencyName = x.Currency.Name
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

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_Create)]
        public async Task<ProjectDto> Create(ProjectDto input)
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

            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_Project_Update)]
        public async Task<ProjectDto> Update(ProjectDto input)
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

            await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectDto, Project>(input, project));
            return input;
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
        public async Task<GridResult<GetTrainingProjectDto>> GetAllTrainingProjectPaging(GridParam input)
        {
            var filterStatus = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "status") : null;
            int valueStatus = -1;
            if (filterStatus != null)
            {
                valueStatus = Convert.ToInt32(filterStatus.Value);
                input.FilterItems.Remove(filterStatus);
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
                            PmFullName = p.PM.FullName,
                            PmAvatarPath = "/avatars/" + p.PM.AvatarPath,
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
        [HttpPost]
        public async Task<TrainingProjectDto> CreateTrainingProject(TrainingProjectDto input)
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

            return input;
        }
        [HttpPut]
        public async Task<TrainingProjectDto> UpdateTrainingProject(TrainingProjectDto input)
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
            await WorkScope.UpdateAsync(ObjectMapper.Map<TrainingProjectDto, Project>(input, project));
            return input;
        }
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
                                    PmAvatarPath = "/avatars/" + x.PM.AvatarPath,
                                    PmBranch = x.PM.Branch,
                                    PmUserType = x.PM.UserType,
                                });
            return await query.FirstOrDefaultAsync();
        }
        #endregion

        #region PAGE PRODUCT PROJECT
        [HttpPost]
        public async Task<GridResult<ProductProjectDto>> GetAllProductProjectPaging(GridParam input)
        {
            var filterStatus = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName == "status") : null;
            int valueStatus = -1;
            if (filterStatus != null)
            {
                valueStatus = Convert.ToInt32(filterStatus.Value);
                input.FilterItems.Remove(filterStatus);
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
                            PmName = p.PM.Name,
                            PmFullName = p.PM.FullName,
                            PmAvatarPath = "/avatars/" + p.PM.AvatarPath,
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
                                    PmAvatarPath = "/avatars/" + x.PM.AvatarPath,
                                    PmBranch = x.PM.Branch,
                                    PmUserType = x.PM.UserType,
                                });
            return await query.FirstOrDefaultAsync();
        }
        #endregion
    }
}
