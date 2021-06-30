using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.ProjectUserBills
{
    //public class ProjectUserBillAppService : ProjectManagementAppServiceBase
    //{
    //    [HttpGet]
    //    [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_GetAllPaging)]
    //    public async Task<GridResult<GetProjectUserBillDto>> GetAllPaging(GridParam input)
    //    {
    //        var query = WorkScope.GetAll<ProjectUserBill>()
    //                    .Select(x => new GetProjectUserBillDto
    //                    {
    //                        ProjectId = x.ProjectId,
    //                        ProjectName = x.Project.Name,
    //                        UserId = x.UserId,
    //                        UserName = x.User.FullName,
    //                        BillRole = x.BillRole,
    //                        BillRate = x.BillRate,
    //                        StartTime = x.StartTime.Date,
    //                        EndTime = x.EndTime.Value.Date,
    //                        Currency = x.Currency
    //                    });
    //        return await query.GetGridResult(query, input);
    //    }

    //    [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_GetAllByproject)]
    //    public async Task<List<GetProjectUserBillDto>> GetAllByProject(long projectId)
    //    {
    //        var query = WorkScope.GetAll<ProjectUserBill>().Where(x => x.ProjectId == projectId)
    //                    .Select(x => new GetProjectUserBillDto
    //                    {
    //                        ProjectId = x.ProjectId,
    //                        ProjectName = x.Project.Name,
    //                        UserId = x.UserId,
    //                        UserName = x.User.FullName,
    //                        BillRole = x.BillRole,
    //                        BillRate = x.BillRate,
    //                        StartTime = x.StartTime.Date,
    //                        EndTime = x.EndTime.Value.Date,
    //                        Currency = x.Currency
    //                    });
    //        return await query.ToListAsync();
    //    }

    //    [HttpPost]
    //    [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_Create)]
    //    public async Task<ProjectUserBillDto> Create(ProjectUserBillDto input)
    //    {
    //        var isExist = await WorkScope.GetAll<ProjectUserBill>().AnyAsync(x => x.ProjectId == input.ProjectId && x.UserId == input.UserId);
    //        if(isExist)
    //            throw new UserFriendlyException($"Project User Bill with ProjectId: {input.ProjectId}, UserId: {input.UserId} already exist !");

    //        if(input.StartTime.Date > input.EndTime.Value.Date)
    //            throw new UserFriendlyException($"Start date cannot be greater than end date !");

    //        await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<ProjectUserBill>(input));

    //        return input;
    //    }

    //    [HttpPut]
    //    [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_Update)]
    //    public async Task<ProjectUserBillDto> Update(ProjectUserBillDto input)
    //    {
    //        var projectUserBill = await WorkScope.GetAsync<ProjectUserBill>(input.Id);

    //        var isExist = await WorkScope.GetAll<ProjectUserBill>().AnyAsync(x => x.Id != input.Id && x.ProjectId == input.ProjectId && x.UserId == input.UserId);
    //        if (isExist)
    //            throw new UserFriendlyException($"Project User Bill with ProjectId: {input.ProjectId}, UserId: {input.UserId} already exist !");

    //        if (input.StartTime.Date > input.EndTime.Value.Date)
    //            throw new UserFriendlyException($"Start date cannot be greater than end date !");

    //        await WorkScope.UpdateAsync(ObjectMapper.Map<ProjectUserBillDto,ProjectUserBill>(input, projectUserBill));

    //        return input;
    //    }

    //    [HttpDelete]
    //    [AbpAuthorize(PermissionNames.PmManager_ProjectUserBill_Delete)]
    //    public async Task Delete(long projectUserBillId)
    //    {
    //        var projectUserBill = await WorkScope.GetAsync<ProjectUserBill>(projectUserBillId);

    //        await WorkScope.DeleteAsync(projectUserBill);
    //    }
    //}
}
