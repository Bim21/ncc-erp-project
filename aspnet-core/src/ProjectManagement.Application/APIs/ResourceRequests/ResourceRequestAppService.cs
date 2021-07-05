using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
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
        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ResourceRequest_ViewAllByProject)]
        public async Task<List<GetResourceRequestDto>> GetAllByProject(long projectId)
        {
            var query = WorkScope.GetAll<ResourceRequest>().Where(x => x.ProjectId == projectId)
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
        public async Task<List<GetProjectUserDto>> ProjectUserResourceRequestDetail(long resourceRequestId)
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

        [HttpGet]
        public async Task<List<ResourceRequestUserDto>> GetAvailableUser(DateTime startDate)
        {
            var users = await WorkScope.GetAll<User>().Where(x => x.IsActive).ToListAsync();

            var query = (from u in users
                         join pu in WorkScope.GetAll<ProjectUser>().Include(x => x.Project)
                         .Where(x => x.Project.Status != ProjectStatus.Potential && x.Project.Status != ProjectStatus.Closed)
                         .Where(x => x.StartTime.Date <= startDate.Date && x.Status == ProjectUserStatus.Present)
                         on u.Id equals pu.UserId 
                         group pu by new { u.Id, u.FullName} into pp
                         select new ResourceRequestUserDto
                        {
                            UserId = pp.Key.Id,
                            UserName = pp.Key.FullName,
                            Undisposed = (byte)(100 - pp.Sum(x => x.AllocatePercentage))
                        }).Where(x => x.Undisposed > 0).AsQueryable();

            return query.ToList();
        }

        [HttpPost]
        public async Task<ResourceRequestDto> Create(ResourceRequestDto input)
        {
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ResourceRequest>(input));
            return input;
        }

        [HttpPut]
        public async Task<ResourceRequestDto> Update(ResourceRequestDto input)
        {
            var resourceRequest = await WorkScope.GetAsync<ResourceRequest>(input.Id);

            await WorkScope.UpdateAsync(ObjectMapper.Map<ResourceRequestDto, ResourceRequest>(input, resourceRequest));
            return input;
        }

        [HttpDelete]
        public async Task Delete(long resourceRequestId)
        {
            var resourceRequest = await WorkScope.GetAll<ProjectUser>().AnyAsync(x => x.ResourceRequestId == resourceRequestId);
            if (resourceRequest)
                throw new UserFriendlyException("Resource Request can not delete !");

            await WorkScope.DeleteAsync<ResourceRequest>(resourceRequestId);
        }
    }
}
