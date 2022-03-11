using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using NccCore.Uitls;
using ProjectManagement.APIs.PMReportProjectIssues.Dto;
using ProjectManagement.APIs.PMReportProjects.Dto;
using ProjectManagement.APIs.PMReports.Dto;
using ProjectManagement.APIs.ProjectUsers;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.Services.Timesheet;
using ProjectManagement.Services.Timesheet.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReportProjects
{
    [AbpAuthorize]
    public class PMReportProjectAppService : ProjectManagementAppServiceBase
    {
        private readonly TimesheetService _timesheetService;

        public PMReportProjectAppService(TimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_GetAllByPmReport,
            PermissionNames.PmManager_PMReportProject_GetAllByPmReport)]
        public async Task<List<GetPMReportProjectDto>> GetAllByPmReport(long pmReportId,string projectType="OUTSOURCING")
        {
            var query = WorkScope.GetAll<PMReportProject>()
                .Where(x => x.PMReportId == pmReportId && x.Project.ProjectType != ProjectType.NoBill)
                .WhereIf(projectType == "PRODUCT", x => x.Project.ProjectType == ProjectType.PRODUCT)
                .WhereIf(projectType == "TRAINING",x => x.Project.ProjectType == ProjectType.TRAINING)
                .WhereIf(projectType == "OUTSOURCING", x => x.Project.ProjectType != ProjectType.TRAINING && x.Project.ProjectType != ProjectType.PRODUCT)
                .Select(x => new GetPMReportProjectDto
                {
                    Id = x.Id,
                    PMReportId = x.PMReportId,
                    PMReportName = x.PMReport.Name,
                    ProjectId = x.ProjectId,
                    ProjectName = x.Project.Name,
                    Status = x.Status.ToString(),
                    ProjectHealth = x.ProjectHealth.ToString(),
                    PMId = x.PMId,
                    PmName = x.PM.Name,
                    Note = x.Note,
                    PmBranch = x.PM.Branch,
                    PmEmailAddress = x.PM.EmailAddress,
                    PmAvatarPath = x.PM.AvatarPath,
                    PmFullName = x.PM.Name + " " + x.PM.Surname,
                    PmUserName = x.PM.UserName,
                    PmUserType = x.PM.UserType,
                    Seen = x.Seen,
                    TotalNormalWorkingTime = x.TotalNormalWorkingTime,
                    TotalOverTime = x.TotalOverTime
                }).OrderBy(x => x.PmFullName); 
            return await query.ToListAsync();
        }

        [HttpGet]
        public async Task<object> GetInfoProject(long pmReportProjectId)
        {
            var projectUser = WorkScope.GetAll<ProjectUser>().Where(x => x.Status == ProjectUserStatus.Present && x.AllocatePercentage > 0);
            var projectUserBill = WorkScope.GetAll<ProjectUserBill>().Where(x => x.isActive);

            var query = WorkScope.GetAll<PMReportProject>().Where(x => x.Id == pmReportProjectId)
                                        .Select(x => new
                                        {
                                            ProjectName = x.Project.Name,
                                            ClientName = x.Project.Client.Name,
                                            PmName = x.PM.FullName,
                                            TotalBill = projectUserBill.Where(b => b.ProjectId == x.ProjectId).Count(),
                                            TotalResource = projectUser.Where(r => r.ProjectId == x.ProjectId).Count(),
                                            TotalNormalWorkingTime = x.TotalNormalWorkingTime,
                                            TotalOverTime = x.TotalOverTime
                                        });

            return await query.FirstOrDefaultAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek, 
            PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesInTheFuture)]
        public async Task<List<CurrentResourceDto>> GetCurrentResourceOfProject(long projectId)
        {
            var totalPercent = from pu in WorkScope.GetAll<ProjectUser>().Where(x => x.Project.Status != ProjectStatus.Closed)
                               .Where(x => x.Status == ProjectUserStatus.Present && x.IsFutureActive)
                               select new
                               {
                                   UserId = pu.UserId,
                                   TotalPercent = pu.AllocatePercentage
                               };

            var projectUsers = WorkScope.GetAll<ProjectUser>()
                                .Where(x => x.ProjectId == projectId)
                                .Where(x => x.Status == ProjectUserStatus.Present && x.IsFutureActive && x.AllocatePercentage > 0)
                                .Where(x => x.User.UserType != UserType.FakeUser)
                                .Select(x => new CurrentResourceDto
                                { 
                                    FullName = x.User.FullName,
                                    ProjectRole = x.ProjectRole.ToString(),
                                    AllocatePercentage = x.AllocatePercentage,
                                    TotalPercent = totalPercent.Where(t => t.UserId == x.UserId).Sum(x => x.TotalPercent)
                                });
            return await projectUsers.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_GetWorkingTimeFromTimesheet,
            PermissionNames.PmManager_PMReportProject_GetWorkingTimeFromTimesheet)]
        public async Task<TotalWorkingTimeOfWeekDto> GetWorkingTimeFromTimesheet(long pmReportProjectId, DateTime startTime, DateTime endTime)
        {
            var pmReportProject = await WorkScope.GetAll<PMReportProject>()
                .Include(x => x.Project)
                .FirstOrDefaultAsync(x => x.Id == pmReportProjectId);

            var result = await _timesheetService.GetWorkingHourFromTimesheet(pmReportProject.Project.Code, startTime, endTime);

            pmReportProject.TotalNormalWorkingTime = result.NormalWorkingTime;
            pmReportProject.TotalOverTime = result.OverTime;

            await WorkScope.UpdateAsync(pmReportProject);

            return new TotalWorkingTimeOfWeekDto 
            { 
                NormalWorkingTime = result.NormalWorkingTime,
                OverTime = result.OverTime 
            };
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_GetAllByPmReport,
            PermissionNames.PmManager_PMReportProject_GetAllByPmReport)]
        public async Task<GetResultpmReportProjectIssue> ProblemsOfTheWeekForReport(long ProjectId, long pmReportId)
        {
            var pmReportProject = await WorkScope.GetAll<PMReportProject>().Where(x => x.ProjectId == ProjectId && x.PMReportId == pmReportId).FirstOrDefaultAsync();
            var query = from prpi in WorkScope.GetAll<PMReportProjectIssue>()
                         .Where(x => x.PMReportProject.ProjectId == ProjectId && x.PMReportProject.PMReportId == pmReportId)
                         .OrderByDescending(x => x.CreationTime)
                        select new GetPMReportProjectIssueDto
                        {
                            Id = prpi.Id,
                            PMReportProjectId = prpi.PMReportProjectId,
                            Description = prpi.Description,
                            Impact = prpi.Impact,
                            Critical = prpi.Critical.ToString(),
                            Source = prpi.Source.ToString(),
                            Solution = prpi.Solution,
                            MeetingSolution = prpi.MeetingSolution,
                            Status = prpi.Status.ToString(),
                            CreatedAt = prpi.CreationTime
                        };

            var result = query.Select(x => new GetResultpmReportProjectIssue
            {
                PmReportProjectId = pmReportProject.Id,
                ProjectHealth = pmReportProject.ProjectHealth,
                Result = query.ToList()
            });

            return await result.FirstOrDefaultAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_UpdatePmReportProjectHealth,
            PermissionNames.PmManager_PMReportProject_UpdatePmReportProjectHealth)]
        public async Task UpdateHealth(long pmReportProjectId, ProjectHealth projectHealth)
        {
            var pmReportProject = await WorkScope.GetAll<PMReportProject>().Include(x => x.PMReport)
                                        .Where(x => x.Id == pmReportProjectId).FirstOrDefaultAsync();

            if(!pmReportProject.PMReport.IsActive)
            {
                throw new UserFriendlyException("Report has been closed !");
            }

            pmReportProject.ProjectHealth = projectHealth;
            await WorkScope.UpdateAsync(pmReportProject);
        }

        [HttpGet]
        public async Task<List<GetPMReportProjectDto>> GetAllPmReportProjectForDropDown()
        {
            var query = WorkScope.GetAll<PMReportProject>().Include(x => x.PM)
                              .Where(x => x.PMReport.IsActive)
                              .Select(x => new GetPMReportProjectDto
                              {
                                  Id = x.Id,
                                  PMReportId = x.PMReportId,
                                  PMReportName = x.PMReport.Name,
                                  ProjectId = x.ProjectId,
                                  ProjectName = x.Project.Name,
                                  Status = x.Status.ToString(),
                                  ProjectHealth = x.ProjectHealth.ToString(),
                                  PMId = x.PMId,
                                  Note = x.Note,
                                  PmName = x.PM.Name,
                                  PmAvatarPath = "/avatars/" + x.PM.AvatarPath,
                                  PmEmailAddress = x.PM.EmailAddress,
                                  PmFullName = x.PM.FullName,
                                  PmUserName = x.PM.UserName,
                                  PmBranch = x.PM.Branch,
                                  PmUserType = x.PM.UserType
                              });

            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek,
            PermissionNames.PmManager_PMReportProject_ResourceChangesDuringTheWeek)]
        public async Task<List<GetProjectUserDto>> ResourceChangesDuringTheWeek(long projectId, long pmReportId)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.ProjectId == projectId && x.PMReportId == pmReportId )
                            .Where(x => x.Status == ProjectUserStatus.Present).OrderByDescending(x => x.CreationTime)
                            .Where(x => x.User.UserType != UserType.FakeUser)
                            .Select(x => new GetProjectUserDto
                            {
                                Id = x.Id,
                                UserId = x.UserId,
                                FullName = x.User.FullName,
                                ProjectId = x.ProjectId,
                                ProjectName = x.Project.Name,
                                ProjectRole = x.ProjectRole.ToString(),
                                AllocatePercentage = x.AllocatePercentage,
                                StartTime = x.StartTime.Date,
                                Status = x.Status.ToString(),
                                IsExpense = x.IsExpense,
                                ResourceRequestId = x.ResourceRequestId,
                                ResourceRequestName = x.ResourceRequest.Name,
                                PMReportId = x.PMReportId,
                                PMReportName = x.PMReport.Name,
                                IsFutureActive = x.IsFutureActive,
                                AvatarPath = "/avatars/" + x.User.AvatarPath,
                                EmailAddress = x.User.EmailAddress,
                                UserName = x.User.UserName,
                                Branch = x.User.Branch,
                                UserType = x.User.UserType,
                                Note = x.Note
                            });
            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesInTheFuture,
            PermissionNames.PmManager_PMReportProject_ResourceChangesInTheFuture)]
        public async Task<List<GetProjectUserDto>> ResourceChangesInTheFuture(long projectId, long pmReportId)
        {
            var query = WorkScope.GetAll<ProjectUser>().Where(x => x.ProjectId == projectId && x.PMReportId == pmReportId)
                            .Where(x => x.Status == ProjectUserStatus.Future).OrderByDescending(x => x.CreationTime)
                            .Select(x => new GetProjectUserDto
                            {
                                Id = x.Id,
                                UserId = x.UserId,
                                FullName = x.User.FullName,
                                ProjectId = x.ProjectId,
                                ProjectName = x.Project.Name,
                                ProjectRole = x.ProjectRole.ToString(),
                                AllocatePercentage = x.AllocatePercentage,
                                StartTime = x.StartTime.Date,
                                Status = x.Status.ToString(),
                                IsExpense = x.IsExpense,
                                ResourceRequestId = x.ResourceRequestId,
                                ResourceRequestName = x.ResourceRequest.Name,
                                PMReportId = x.PMReportId,
                                PMReportName = x.PMReport.Name,
                                IsFutureActive = x.IsFutureActive,
                                UserName = x.User.UserName,
                                AvatarPath = "/avatars/" + x.User.AvatarPath,
                                EmailAddress = x.User.EmailAddress,
                                UserType = x.User.UserType,
                                Branch = x.User.Branch,
                                Note = x.Note
                            });
            query = query.Where(x => x.UserType != UserType.FakeUser);
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_SendReport,
            PermissionNames.PmManager_PMReportProject_SendReport)]
        public async Task SendReport(long projectId, long pmReportId)
        {
            var pmReportProject = await WorkScope.GetAll<PMReportProject>().Include(x => x.PMReport)
                .Where(x => x.ProjectId == projectId && x.PMReportId == pmReportId).FirstOrDefaultAsync();
            if (pmReportProject.Status == PMReportProjectStatus.Sent)
                throw new UserFriendlyException("Report has been sent !");

            pmReportProject.Status = PMReportProjectStatus.Sent;
            pmReportProject.TimeSendReport = DateTimeUtils.GetNow();
            // phạt nhẹ nếu quá hạn
            if (pmReportProject.PMReport.PMReportStatus == PMReportStatus.Expired && pmReportProject.PMReport.Type == PMReportType.Weekly)
            {
                pmReportProject.IsPunish = PunishStatus.Low;
            }

            await WorkScope.UpdateAsync(pmReportProject);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_Create,
            PermissionNames.PmManager_PMReportProject_Create)]
        public async Task<PMReportProjectDto> Create(PMReportProjectDto input)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(input.PMReportId);
            if(!pmReport.IsActive)
            {
                throw new UserFriendlyException("PMReport is locked !");
            }

            var isExist = await WorkScope.GetAll<PMReportProject>().AnyAsync(x => x.PMReportId == input.PMReportId && x.ProjectId == input.ProjectId);
            if (isExist)
                throw new UserFriendlyException("PMReportProject already exist !");

            input.Status = PMReportProjectStatus.Draft;
            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<PMReportProject>(input));
            return input;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_Update,
            PermissionNames.PmManager_PMReportProject_Update)]
        public async Task<PMReportProjectDto> Update(PMReportProjectDto input)
        {
            var pmReportProject = await WorkScope.GetAsync<PMReportProject>(input.Id);

            if (!pmReportProject.PMReport.IsActive)
            {
                throw new UserFriendlyException("PMReport is locked !");
            }

            var isExist = await WorkScope.GetAll<PMReportProject>().AnyAsync(x => x.Id != input.Id && x.PMReportId == input.PMReportId && x.ProjectId == input.ProjectId);
            if (isExist)
                throw new UserFriendlyException("PMReportProject already exist !");

            await WorkScope.UpdateAsync(ObjectMapper.Map<PMReportProjectDto, PMReportProject>(input, pmReportProject));
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_Delete,
            PermissionNames.PmManager_PMReportProject_Delete)]
        public async Task Delete(long pmPeportProjectId)
        {
            var pmReportProject = await WorkScope.GetAsync<PMReportProject>(pmPeportProjectId);
            var pmReport = await WorkScope.GetAsync<PMReport>(pmReportProject.PMReportId);
            if (!pmReport.IsActive)
            {
                throw new UserFriendlyException("PMReport is locked !");
            }

            await WorkScope.DeleteAsync<PMReportProject>(pmPeportProjectId);
        }

        public async Task ReverseSeen(long pmReportProjectId)
        {
            var pmReportProject = await WorkScope.GetAsync<PMReportProject>(pmReportProjectId);
            pmReportProject.Seen = !pmReportProject.Seen;
            await WorkScope.UpdateAsync(pmReportProject);
        }


        [HttpGet]
        public async Task<List<GetAllByProjectDto>> GetAllByProject(long projectId)
        {
            var query = from p in WorkScope.GetAll<PMReport>()
                        join pp in WorkScope.GetAll<PMReportProject>().Where(x => x.ProjectId == projectId)
                        on p.Id equals pp.PMReportId into lst
                        from l in lst.DefaultIfEmpty()
                        orderby p.IsActive descending
                        orderby p.CreationTime descending
                        select new GetAllByProjectDto
                        {
                            ReportId = p.Id,
                            PmReportProjectId = l.Id,
                            PMReportName = p.Name,
                            Status = l.Status.ToString(),
                            IsActive = p.IsActive,
                            Note = l.Note,
                            ProjectHealth = l.ProjectHealth.ToString()
                        };
                        

            return await query.ToListAsync();
        }

        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReportProject_UpdateNote,
            PermissionNames.PmManager_PMReportProject_UpdateNote)]
        public async Task<UpdateNoteDto> UpdateNote(UpdateNoteDto input)
        {
            var pmReportProject = await WorkScope.GetAll<PMReportProject>().Include(x => x.PMReport).SingleOrDefaultAsync(x => x.Id == input.Id);

            if (!pmReportProject.PMReport.IsActive)
            {
                throw new UserFriendlyException("Report has been closed !");
            }

            pmReportProject.Note = input.Note;
            await WorkScope.UpdateAsync(pmReportProject);
            return input;
        }
    }
}
