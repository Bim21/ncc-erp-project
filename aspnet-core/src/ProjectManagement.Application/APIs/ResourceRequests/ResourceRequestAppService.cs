﻿using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.ProjectUsers;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.APIs.ResourceRequests.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.ResourceRequests
{
    public class ResourceRequestAppService : ProjectManagementAppServiceBase
    {
        private readonly ProjectUserAppService _projectUserAppService;

        public ResourceRequestAppService(ProjectUserAppService projectUserAppService)
        {
            _projectUserAppService = projectUserAppService;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ViewAllByProject)]
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
                            Note = x.Note,
                            TimeNeed = x.TimeNeed,
                            TimeDone = x.TimeDone.Value
                        });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ViewAllResourceRequest)]
        public async Task<GridResult<GetResourceRequestDto>> GetAllPaging(GridParam input)
        {
            var query = from rr in WorkScope.GetAll<ResourceRequest>()
                        join pu in WorkScope.GetAll<ProjectUser>() on rr.Id equals pu.ResourceRequestId
                        group pu by new { rr.Id, rr.Name, ProjectName = rr.Project.Name, rr.Status } into pp
                        select new GetResourceRequestDto
                        {
                            Id = pp.Key.Id,
                            Name = pp.Key.Name,
                            ProjectName = pp.Key.ProjectName,
                            Status = pp.Key.Status,
                            PlannedNumberOfPersonnel = pp.Count()
                        };
               
            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest)]
        public async Task<List<GetProjectUserDto>> ResourceRequestDetail(long resourceRequestId)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.ResourceRequestId == resourceRequestId)
                                    .Select(x => new GetProjectUserDto
                                    {
                                        Id = x.Id,
                                        UserId = x.UserId,
                                        UserName = x.User.Name,
                                        ProjectId = x.ProjectId,
                                        ProjectName = x.Project.Name,
                                        ProjectRole = x.ProjectRole.ToString(),
                                        AllocatePercentage = x.AllocatePercentage,
                                        StartTime = x.StartTime,
                                        Status = x.Status.ToString(),
                                        IsExpense = x.IsExpense,
                                        ResourceRequestId = x.ResourceRequestId,
                                        PMReportId = x.PMReportId,
                                        IsFutureActive = x.IsFutureActive
                                    });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AddUserToRequest)]
        public async Task<ProjectUserDto> AddUserToRequest(ProjectUserDto input)
        {
            var resourceRequest = await WorkScope.GetAsync<ResourceRequest>((long)input.ResourceRequestId);

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            var projectuser = new ProjectUser
            {
                UserId = input.UserId,
                ProjectId = resourceRequest.ProjectId,
                ResourceRequestId = resourceRequest.Id,
                ProjectRole = input.ProjectRole,
                StartTime = input.StartTime,
                Status = ProjectUserStatus.Future,
                IsExpense = true,
                IsFutureActive = false,
                PMReportId = pmReportActive.Id
            };
            input.Id = await WorkScope.InsertAndGetIdAsync(projectuser);
            return input;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest)]
        public async Task<List<ResourceRequestUserDto>> SearchAvailableUserForRequest(DateTime startDate)
        {
            if(startDate.Date <= DateTime.Now.Date)
            {
                throw new UserFriendlyException("The start date must be greater than the current time !");
            }

            var projectUsers = WorkScope.GetAll<ProjectUser>()
                                .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed)
                                .Where(x => x.StartTime.Date >= startDate.Date && x.Status == ProjectUserStatus.Present)
                                .Select(x => new
                                {
                                    UserId = x.UserId,
                                    StartTime = x.StartTime,
                                    AllocatePercentage = x.AllocatePercentage
                                });
            var users = WorkScope.GetAll<User>().Where(x => x.IsActive)
                                .Select(x => new ResourceRequestUserDto
                                {
                                    UserId = x.Id,
                                    UserName = x.FullName,
                                    StartTime = projectUsers.Where(y => y.UserId == x.Id).Select(y => y.StartTime).FirstOrDefault(),
                                    Undisposed = projectUsers.Any(y => y.UserId == x.Id) ? (byte)(100 - projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage)) : (byte)100
                                }).Where(x => x.Undisposed > 0);

            return await users.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource)]
        public async Task<GridResult<AvailableResourceDto>> AvailableResource(GridParam input)
        {
            var projectUsers = WorkScope.GetAll<ProjectUser>()
                               .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed && x.Status == ProjectUserStatus.Present)
                               .Select(x => new
                               {
                                   UserId = x.UserId,
                                   ProjectName = x.Project.Name,
                                   AllocatePercentage = x.AllocatePercentage
                               });
            var users = WorkScope.GetAll<User>().Where(x => x.IsActive)
                                .Select(x => new AvailableResourceDto
                                {
                                    UserId = x.Id,
                                    UserName = x.FullName,
                                    Projects = projectUsers.Where(y => y.UserId == x.Id).Select(x => x.ProjectName).ToList(),
                                    Used = (byte)projectUsers.Where(y => y.UserId == x.Id).Sum(y => y.AllocatePercentage)
                                });

            return await users.GetGridResult(users, input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_AvailableResourceFuture)]
        public async Task<GridResult<AvailableResourceFutureDto>> AvailableResourceFuture(GridParam input)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Future && !x.IsFutureActive)
                        .Select(x => new AvailableResourceFutureDto
                        {
                            Id = x.Id,
                            UserId = x.UserId,
                            UserName = x.User.Name,
                            Projectid = x.ProjectId,
                            ProjectName = x.Project.Name,
                            StartDate = x.StartTime.Date,
                            Use = x.AllocatePercentage
                        });
            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_PlanUser)]
        public async Task<ProjectUser> PlanUser(PlanUserDto input)
        {
            var projectUsers = WorkScope.GetAll<ProjectUser>()
                               .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed && 
                               x.Status == ProjectUserStatus.Present || (x.Status == ProjectUserStatus.Future && x.IsFutureActive))
                               .Select(x => new
                               {
                                   UserId = x.UserId,
                                   ProjectId = x.ProjectId,
                                   AllocatePercentage = x.AllocatePercentage,
                                   Status = x.Status,
                                   StartDate = x.StartTime,
                                   IsFutureActive = x.IsFutureActive
                               });

            var pmReportActive = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (pmReportActive == null)
                throw new UserFriendlyException("Can't find any active reports !");

            if(input.StartTime.Date <= DateTime.Now.Date)
                throw new UserFriendlyException("The start date must be greater than the current time !");

            if(projectUsers.Any(x => x.UserId == input.UserId && x.ProjectId == input.ProjectId && x.StartDate.Date == input.StartTime.Date))
                throw new UserFriendlyException($"Project User already exist in {input.StartTime.Date} !");

            var projectUser = new ProjectUser
            {
                UserId = input.UserId,
                ProjectId = input.ProjectId,
                ProjectRole = input.ProjectRole,
                StartTime = input.StartTime,
                Status = ProjectUserStatus.Future,
                IsExpense = input.IsExpense,
                IsFutureActive = false,
                PMReportId = pmReportActive.Id
            };
            projectUser.Id = await WorkScope.InsertAndGetIdAsync(projectUser);
            return projectUser;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ApproveUser)]
        public async Task<ProjectUserDto> ApproveUser(ProjectUserDto input)
        {
            input.IsFutureActive = true;
            input.Status = ProjectUserStatus.Present;

            await _projectUserAppService.Update(input);
            return input;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_RejectUser)]
        public async Task RejectUser(long projectUserId)
        {
            var projectUser = await WorkScope.GetAsync<ProjectUser>(projectUserId);

            await WorkScope.DeleteAsync(projectUser);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_Create)]
        public async Task<ResourceRequestDto> Create(ResourceRequestDto input)
        {
            var isExist = await WorkScope.GetAll<ResourceRequest>().AnyAsync(x => x.Name == input.Name && x.ProjectId == input.ProjectId && x.TimeNeed == input.TimeNeed);

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ResourceRequest>(input));
            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_Update)]
        public async Task<ResourceRequestDto> Update(ResourceRequestDto input)
        {
            var resourceRequest = await WorkScope.GetAsync<ResourceRequest>(input.Id);

            await WorkScope.UpdateAsync(ObjectMapper.Map<ResourceRequestDto, ResourceRequest>(input, resourceRequest));
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_Delete)]
        public async Task Delete(long resourceRequestId)
        {
            var resourceRequest = await WorkScope.GetAll<ProjectUser>().AnyAsync(x => x.ResourceRequestId == resourceRequestId);
            if (resourceRequest)
                throw new UserFriendlyException("Resource Request can not delete !");

            await WorkScope.DeleteAsync<ResourceRequest>(resourceRequestId);
        }
    }
}
