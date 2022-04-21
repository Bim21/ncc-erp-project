using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using ProjectManagement.APIs.TimeSheetProjectBills;
using ProjectManagement.APIs.TimeSheetProjectBills.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.Services.ProjectTimesheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.ProjectUserBills
{
    [AbpAuthorize]
    public class ProjectUserBillAppService : ProjectManagementAppServiceBase
    {
        private ProjectTimesheetManager timesheetManager;


        public ProjectUserBillAppService(ProjectTimesheetManager timesheetManager)
        {
            this.timesheetManager = timesheetManager;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo)]
        public async Task<List<GetProjectUserBillDto>> GetAllByProject(long projectId)
        {
            var query = WorkScope.GetAll<ProjectUserBill>()
                .Where(x => x.ProjectId == projectId)
                .OrderByDescending(x => x.CreationTime)
                        .Select(x => new GetProjectUserBillDto
                        {
                            Id = x.Id,
                            UserId = x.UserId,
                            UserName = x.User.Name,
                            ProjectId = x.ProjectId,
                            ProjectName = x.Project.Name,
                            BillRole = x.BillRole,
                            BillRate = x.BillRate,
                            StartTime = x.StartTime.Date,
                            EndTime = x.EndTime.Value.Date,
                            //CurrencyName = x.Project.Currency.Name,
                            Note = x.Note,
                            shadowNote = x.shadowNote,
                            isActive = x.isActive,
                            AvatarPath = x.User.AvatarPath,
                            FullName = x.User.FullName,
                            Branch = x.User.Branch,
                            EmailAddress = x.User.EmailAddress,
                            UserType = x.User.UserType
                        });
            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_LastInvoiceNumber_View)]
        public async Task<byte?> GetLastInvoiceNumber(long projectId)
        {
            var lastInvoiceNumber = await WorkScope.GetAll<Project>()
             .Where(s => s.Id == projectId)
             .Select(s => s.LastInvoiceNumber)
             .FirstOrDefaultAsync();
            return lastInvoiceNumber;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_LastInvoiceNumber_Edit)]
        public async Task<byte?> UpdateLastInvoiceNumber(UpdateLastInvoiceNumberDto input)
        {
            var project = await WorkScope.GetAsync<Project>(input.ProjectId);
            var entity = ObjectMapper.Map(input, project);
            var lastInvoiceNumber = await WorkScope.UpdateAsync(entity);

            return lastInvoiceNumber.LastInvoiceNumber;
        }

        [HttpGet]
        [AbpAuthorize]
        public async Task<ProjectRateDto> GetRate(long projectId)
        {
            var query = WorkScope.GetAll<Project>().Include(x => x.Currency).Where(x => x.Id == projectId)
                                .Select(x => new ProjectRateDto
                                {
                                    IsCharge = x.IsCharge,
                                    ChargeType = x.ChargeType,
                                    CurrencyName = x.Currency.Name

                                });
            return await query.FirstOrDefaultAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Create,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Create,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Create)]
        public async Task<ProjectUserBillDto> Create(ProjectUserBillDto input)
        {
            if (input.EndTime.HasValue && input.StartTime.Date > input.EndTime.Value.Date)
                throw new UserFriendlyException($"Start date cannot be greater than end date !");

            var duplicatedPUB = await WorkScope.GetAll<ProjectUserBill>()
                .Where(s => s.UserId == input.UserId)
                .Where(s => s.ProjectId == input.ProjectId)
                .Select(s => new { s.User.FullName, s.BillRole, s.isActive, s.BillRate })
                .FirstOrDefaultAsync();

            if (duplicatedPUB != default)
            {
                throw new UserFriendlyException($"Already exist: {duplicatedPUB.FullName} - {duplicatedPUB.BillRole} - {duplicatedPUB.BillRate} - Active: {duplicatedPUB.isActive}");
            }
            var project = await WorkScope.GetAll<Project>()
                .Where(s => s.Id == input.ProjectId)
                .Select(s => new Project
                {
                    CurrencyId = s.CurrencyId,
                    ChargeType = s.ChargeType
                })
                .FirstOrDefaultAsync();

            var entity = ObjectMapper.Map<ProjectUserBill>(input);

            input.Id = await WorkScope.InsertAndGetIdAsync(entity);

            await this.timesheetManager.CreateTimesheetProjectBill(entity, project);

            return input;
        }


        [HttpPut]
        [AbpAuthorize(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Edit,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Edit,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Edit)]
        public async Task<ProjectUserBillDto> Update(ProjectUserBillDto input)
        {
            var projectUserBill = await WorkScope.GetAsync<ProjectUserBill>(input.Id);

            if (input.EndTime.HasValue && input.StartTime.Date > input.EndTime.Value.Date)
                throw new UserFriendlyException($"Start date cannot be greater than end date !");

            var entity = ObjectMapper.Map(input, projectUserBill);
            await WorkScope.UpdateAsync(entity);

            await this.timesheetManager.UpdateTimesheetProjectBill(entity);
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete,
            PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Delete,
            PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Delete)]
        public async Task Delete(long projectUserBillId)
        {
            var projectUserBill = await WorkScope.GetAsync<ProjectUserBill>(projectUserBillId);

            await WorkScope.DeleteAsync(projectUserBill);
        }
    }
}
