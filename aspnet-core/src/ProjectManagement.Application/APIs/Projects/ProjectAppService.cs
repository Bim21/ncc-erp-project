﻿using Abp.Authorization;
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
    public class ProjectAppService : ProjectManagementAppServiceBase
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        public ProjectAppService(                    
                   UserManager userManager,
                   RoleManager roleManager)
        {            
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_ViewAll, PermissionNames.PmManager_Project_ViewonlyMe)]
        public async Task<GridResult<GetProjectDto>> GetAllPaging(GridParam input)
        {
            bool isViewAll = await PermissionChecker.IsGrantedAsync(PermissionNames.PmManager_Project_ViewAll);

            var query = from p in WorkScope.GetAll<Project>()
                        where isViewAll || p.PMId == AbpSession.UserId.Value
                        select new GetProjectDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Code = p.Code,
                            ProjectType = p.ProjectType.ToString(),
                            StartTime = p.StartTime.Date,
                            EndTime = p.EndTime.Value.Date,
                            Status = p.Status.ToString(),
                            ClientId = p.ClientId,
                            ClientName = p.Client.Name,
                            IsCharge = p.IsCharge,
                            PmId = p.PMId,
                            PmName = p.PM.Name,
                        };
            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        public async Task<List<GetProjectDto>> GetAll()
        {
            var query = WorkScope.GetAll<Project>().Where(x => x.Status != ProjectStatus.Potential && x.Status != ProjectStatus.Closed)
                .Select(x => new GetProjectDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    ProjectType = x.ProjectType.ToString(),
                    StartTime = x.StartTime.Date,
                    EndTime = x.EndTime.Value.Date,
                    Status = x.Status.ToString(),
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
                                    Status = x.Status.ToString(),
                                    ClientId = x.ClientId,
                                    ClientName = x.Client.Name,
                                    IsCharge = x.IsCharge,
                                    PmId = x.PMId,
                                    PmName = x.PM.Name
                                });
            return await query.FirstOrDefaultAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_Project_Create)]
        public async Task<ProjectDto> Create(ProjectDto input)
        {
            var isExist = await WorkScope.GetAll<Project>().AnyAsync(x => x.Name == input.Name || x.Code == input.Code);

            if (isExist)
                throw new UserFriendlyException("Name or Code already exist !");

            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<Project>(input));

            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.PmManager_Project_Update)]
        public async Task<ProjectDto> Update(ProjectDto input)
        {
            var project = await WorkScope.GetAsync<Project>(input.Id);

            var isExist = await WorkScope.GetAll<Project>().AnyAsync(x => x.Id != input.Id && (x.Name == input.Name || x.Code == input.Code));

            if (isExist)
                throw new UserFriendlyException("Name or Code already exist !");

            await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectDto, Project>(input, project));
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.PmManager_Project_Delete)]
        public async Task Delete(long projectID)
        {
            var project = await WorkScope.GetAsync<Project>(projectID);
            if (project == null)
                throw new UserFriendlyException($"Project with id = {projectID} not exist !");

            await WorkScope.DeleteAsync(project);
        }
    }
}
