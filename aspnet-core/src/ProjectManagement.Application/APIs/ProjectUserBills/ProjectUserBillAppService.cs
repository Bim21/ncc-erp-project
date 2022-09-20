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
            var isViewRate = await IsGrantedAsync(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Rate_View);
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
                            AccountName = x.AccountName,
                            BillRole = x.BillRole,
                            BillRate = isViewRate ? x.BillRate : 0,
                            StartTime = x.StartTime.Date,
                            EndTime = x.EndTime.Value.Date,
                            //CurrencyName = x.Project.Currency.Name,
                            Note = x.Note,
                            shadowNote = x.shadowNote,
                            isActive = x.isActive,
                            AvatarPath = x.User.AvatarPath,
                            FullName = x.User.FullName,
                            Branch = x.User.BranchOld,
                            BranchColor = x.User.Branch.Color,
                            BranchDisplayName = x.User.Branch.DisplayName,
                            EmailAddress = x.User.EmailAddress,
                            UserType = x.User.UserType,
                            ChargeType = x.ChargeType.HasValue ? x.ChargeType : x.Project.ChargeType,
                        });
            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize]
        public async Task<long> GetLastInvoiceNumber(long projectId)
        {
            return await WorkScope.GetAll<Project>()
             .Where(s => s.Id == projectId)
             .Select(s => s.LastInvoiceNumber)
             .FirstOrDefaultAsync();
        }


        private async Task<Project> GetProjectById(long projectId)
        {
            return await WorkScope.GetAll<Project>().FirstOrDefaultAsync(x => x.Id == projectId);
        }

        [HttpGet]
        public async Task<List<SubInvoiceDto>> GetAllPosibleSubProject(long projectId)
        {
            var mainProject = WorkScope.GetAll<Project>().GroupBy(p => p.ParentInvoiceId).Select(x => new
            {
                ProjectId = x.Key,
            });
            var clientId = WorkScope.GetAll<Project>().Where(p => p.Id == projectId ).Select(p => p.ClientId).FirstOrDefault();
            return await WorkScope.GetAll<Project>()
                .Where(p => p.ClientId == clientId && !p.ParentInvoiceId.HasValue && p.Id != projectId)
                .Where(x => !mainProject.Any(p => p.ProjectId == x.Id))
                .Select(p => new SubInvoiceDto
                {
                    ProjectId = p.Id,
                    ProjectName = p.Name
                }).ToListAsync();
        }
        [HttpGet]
        public async Task<List<SubInvoiceDto>> GetAllPosibleMainProject(long projectId)
        {
            var clientId = WorkScope.GetAll<Project>().Where(p => p.Id == projectId).Select(p => p.ClientId).FirstOrDefault();
            return await WorkScope.GetAll<Project>().Where(p => p.ClientId == clientId && p.Id != projectId).Select(p => new SubInvoiceDto
            {
                ProjectId = p.Id,
                ProjectName = p.Name
            }).ToListAsync();
        }
        [HttpPut]
        [AbpAuthorize(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_LastInvoiceNumber_Edit)]
        public async Task<long> UpdateLastInvoiceNumber(UpdateLastInvoiceNumberDto input)
        {
            var project = await GetProjectById(input.ProjectId);
            if (project != null)
            {
                project.LastInvoiceNumber = input.LastInvoiceNumber;
                var projectUpdate = await WorkScope.UpdateAsync<Project>(project);
                return projectUpdate.LastInvoiceNumber;
            }

            return default;
        }

        [HttpGet]
        [AbpAuthorize]
        public async Task<float> GetDiscount(long projectId)
        {
            return await WorkScope.GetAll<Project>()
             .Where(s => s.Id == projectId)
             .Select(s => s.Discount)
             .FirstOrDefaultAsync();
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Discount_Edit)]
        public async Task<float> UpdateDiscount(UpdateDiscountDto input)
        {
            var project = await GetProjectById(input.ProjectId);
            if (project != null)
            {
                project.Discount = input.Discount;
                var projectUpdate = await WorkScope.UpdateAsync<Project>(project);
                return projectUpdate.Discount;
            }

            return default;

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

            var isEditNote = await IsGrantedAsync(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Note_Edit);
            if (!isEditNote)
            {
                input.Note = projectUserBill.Note;
            }
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

        [HttpPut]
        [AbpAuthorize(PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Note_Edit)]
        public async Task<UpdateNoteDto> UpdateNote(UpdateNoteDto input)
        {
            var projectUserBill = await WorkScope.GetAsync<ProjectUserBill>(input.Id);

            var entity = ObjectMapper.Map(input, projectUserBill);

            await WorkScope.UpdateAsync(entity);

            return input;
        }
        #region Integrate Finfast
        [HttpGet]
        public async Task<List<SubInvoiceDto>> GetAllProjectCanUsing(long projectId)
        {
            return await WorkScope.GetAll<Project>()
                .Where(s => !s.ParentInvoiceId.HasValue && s.Id != projectId && !WorkScope.GetAll<Project>().Where(x => x.ParentInvoiceId == s.Id).Any())
                .Select(x => new SubInvoiceDto
                {
                    ProjectId = x.Id,
                    ProjectName = x.Name
                }).ToListAsync();
        }
        [HttpGet]
        public async Task<ParentInvoiceDto> GetParentInvoiceByProject(long projectId)
        {
            var project = await WorkScope.GetAll<Project>()
                .Where(s => s.Id == projectId)
                .Select(s => new ParentInvoiceDto
                {
                    ProjectId = s.Id,
                    ParentId = s.ParentInvoiceId,
                    SubInvoices = WorkScope.GetAll<Project>().Where(s => s.ParentInvoiceId == projectId).Select(x => new SubInvoiceDto
                    {
                        ProjectId = x.Id,
                        ProjectName = x.Name
                    }).ToList()
                }).FirstOrDefaultAsync();
            return project;
        }
        [HttpPost]
        public async Task<string> AddSubInvoice(AddSubInvoiceDto input)
        {
            var subInvoice = await WorkScope.GetAsync<Project>(input.SubInvoiceId);
            subInvoice.ParentInvoiceId = input.ParentInvoiceId;
            await CurrentUnitOfWork.SaveChangesAsync();
            return "Added Successfullly";
        }
        [HttpPost]
        public async Task<string> AddSubInvoices(AddSubInvoicesDto input)
        {
            var listProject = new List<Project>();
            var subProjects = await WorkScope.GetAll<Project>().Where(x => x.ParentInvoiceId == input.ParentInvoiceId).Where(x => !input.SubInvoiceIds.Any(s => s == x.Id)).ToListAsync();
            foreach (var subProject in subProjects)
            {
                subProject.ParentInvoiceId = null;
                listProject.Add(subProject);
            }
            foreach(var projectId in input.SubInvoiceIds)
            {
                var subInvoice = await WorkScope.GetAsync<Project>(projectId);
                subInvoice.ParentInvoiceId = input.ParentInvoiceId;
                listProject.Add(subInvoice);
            }
            await WorkScope.UpdateRangeAsync(listProject);
            return $"Added {input.SubInvoiceIds.Count} sub to main project";
        }
        [HttpGet]
        public async Task<List<SubInvoiceDto>> GetSubInoviceByProjectId(long projectId)
        {
            return await WorkScope.GetAll<Project>()
                .Where(s => s.ParentInvoiceId == projectId)
                .Select(s =>new SubInvoiceDto
                {
                    ProjectId= s.Id,
                    ProjectName = s.Name
                })
                .ToListAsync();
        }
        [HttpGet]
        public async Task OutParentInvoice(long subInvoiceId)
        {
            var subInvoice = await WorkScope.GetAsync<Project>(subInvoiceId);
            subInvoice.ParentInvoiceId = null;
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
