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

            var query = from p in WorkScope.GetAll<Project>().Where(x => isViewAll || x.PMId == AbpSession.UserId.Value)
                        join rp in WorkScope.GetAll<PMReportProject>().Where(x => x.PMReport.IsActive) on p.Id equals rp.ProjectId into lst
                        from l in lst.DefaultIfEmpty()
                        select new GetProjectDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Code = p.Code,
                            ProjectType = p.ProjectType.ToString(),
                            StartTime = p.StartTime.Date,
                            EndTime = p.EndTime.Value.Date,
                            Status = p.Status,
                            ClientId = p.ClientId,
                            ClientName = p.Client.Name,
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
                            TimeSendReport = l.TimeSendReport
                        };
            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        public async Task<List<GetProjectDto>> GetAll()
        {
            var query = WorkScope.GetAll<Project>().Where(x=>x.Status != ProjectStatus.Potential && x.Status != ProjectStatus.Closed)
                .Select(x => new GetProjectDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    ProjectType = x.ProjectType.ToString(),
                    StartTime = x.StartTime.Date,
                    EndTime = x.EndTime.Value.Date,
                    Status = x.Status,
                    ClientId = x.ClientId,
                    ClientName = x.Client.Name,
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
            var query = WorkScope.GetAll<Project>().Where(x => x.Id == projectId)
                                .Select(x => new GetProjectDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Code = x.Code,
                                    ProjectType = x.ProjectType.ToString(),
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
                                    PmUserType = x.PM.UserType
                                });
            return await query.FirstOrDefaultAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewDetail)]
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
            var allproject = await WorkScope.GetAll<Project>().Select(x=>x.Id).ToListAsync();
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
    }
}
