﻿
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using NccCore.Uitls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using ProjectManagement.APIs.PMReports.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Configuration;
using ProjectManagement.Entities;
using ProjectManagement.NccCore.BackgroundJob;
using System;
using System.Linq;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReports
{
    public class PMReportAppService : ProjectManagementAppServiceBase
    {
        private readonly IBackgroundJobManager _backgroundJobManager;
        public PMReportAppService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }
        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_ViewAll)]
        public async Task<GridResult<GetPMReportDto>> GetAllPaging(GridParam input)
        {
            var pmReportProject = WorkScope.GetAll<PMReportProject>();
            var query = WorkScope.GetAll<PMReport>()
                .OrderByDescending(x => x.CreationTime)
                .Select(x => new GetPMReportDto
            {
                Id = x.Id,
                Name = x.Name,
                Year = x.Year,
                IsActive = x.IsActive,
                Type = x.Type,
                PMReportStatus = x.PMReportStatus,
                NumberOfProject = pmReportProject.Where(y => y.PMReportId == x.Id).Count()
            });
            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_Create)]
        public async Task<CreatePMReportDto> Create(CreatePMReportDto input)
        {
            var isExist = await WorkScope.GetAll<PMReport>().AnyAsync(x => x.Name == input.Name && x.Type == input.Type && x.Year == input.Year);
            if (isExist)
                throw new UserFriendlyException("PM Report already exist!");

            var activeReport = await WorkScope.GetAll<PMReport>().Where(x => x.IsActive).FirstOrDefaultAsync();
            if (activeReport != null)
            {
                activeReport.IsActive = false;
                await WorkScope.UpdateAsync(activeReport);
            }

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<PMReport>(input));

            var userInFuture = WorkScope.GetAll<ProjectUser>().Where(x => x.PMReportId == (activeReport != null ? activeReport.Id : 0) && x.Status == ProjectUserStatus.Future && x.IsFutureActive);
            foreach (var item in userInFuture)
            {
                item.IsFutureActive = false;
                await WorkScope.UpdateAsync(item);

                var projectUser = new ProjectUser
                {
                    UserId = item.UserId,
                    ProjectId = item.ProjectId,
                    ProjectRole = item.ProjectRole,
                    AllocatePercentage = item.AllocatePercentage,
                    StartTime = item.StartTime,
                    Status = item.Status,
                    IsExpense = item.IsExpense,
                    ResourceRequestId = item.ResourceRequestId,
                    PMReportId = input.Id,
                    IsFutureActive = true
                };
                await WorkScope.InsertAsync(projectUser);
            }

            var pmReportProjectInProcess = from prp in WorkScope.GetAll<PMReportProject>().Where(x => x.PMReportId == (activeReport != null ? activeReport.Id : 0))
                                           join prpi in WorkScope.GetAll<PMReportProjectIssue>().Where(x => x.Status == PMReportProjectIssueStatus.InProgress)
                                           on prp.Id equals prpi.PMReportProjectId
                                           select new
                                           {
                                               ProjectId = prp.ProjectId,
                                               Issue = prpi
                                           };

            var projectActive = await WorkScope.GetAll<Project>().Where(x => x.Status != ProjectStatus.Closed).ToListAsync();
            foreach (var item in projectActive)
            {

                var pmReportProject = new PMReportProject
                {
                    PMReportId = input.Id,
                    ProjectId = item.Id,
                    Status = PMReportProjectStatus.Draft,
                    ProjectHealth = ProjectHealth.Green,
                    PMId = item.PMId,
                    Note = null
                };
                pmReportProject.Id = await WorkScope.InsertAndGetIdAsync(pmReportProject);

                var listInProcess = pmReportProjectInProcess.Where(x => x.ProjectId == item.Id).Select(x => x.Issue);
                if (listInProcess.Count() > 0)
                {
                    foreach (var issue in listInProcess)
                    {
                        var pmReportProjectIssue = new PMReportProjectIssue
                        {
                            PMReportProjectId = pmReportProject.Id,
                            CreationTime = issue.CreationTime,
                            Description = issue.Description,
                            Impact = issue.Impact,
                            Critical = issue.Critical,
                            Source = issue.Source,
                            Solution = issue.Solution,
                            MeetingSolution = issue.MeetingSolution,
                            Status = issue.Status
                        };
                        await WorkScope.InsertAsync(pmReportProjectIssue);
                    }
                }
            }

            // back ground job update status can send of pmreport
            var now = DateTimeUtils.GetNow();
            var canSendDay = Int32.Parse(await SettingManager.GetSettingValueAsync(AppSettingNames.CanSendDay));
            var canSendHour = Int32.Parse(await SettingManager.GetSettingValueAsync(AppSettingNames.CanSendHour));
            var expiredDay = Int32.Parse(await SettingManager.GetSettingValueAsync(AppSettingNames.ExpiredDay));
            var expiredHour = Int32.Parse(await SettingManager.GetSettingValueAsync(AppSettingNames.ExpiredHour));

            if (!input.CanSendTime.HasValue)
            {
                input.CanSendTime = SettingToDate(canSendDay, canSendHour, now);
            }

            if (!input.ExpiredTime.HasValue)
            {
                input.ExpiredTime = SettingToDate(expiredDay, expiredHour, now);
            }

            if (input.CanSendTime > input.ExpiredTime)
            {
                throw new UserFriendlyException("CanSendTime can't not greater than ExpiredTime!");
            }
            var pmReport = await WorkScope.GetAsync<PMReport>(input.Id);

            // nếu qua thời điểm cantime thì ko cần tạo job, tương tự expired
            if (input.ExpiredTime < now)
            {
                pmReport.PMReportStatus = PMReportStatus.Expired;
                await WorkScope.UpdateAsync(pmReport);
                return input;
            }
            else
            {
                // add background job
                await _backgroundJobManager.EnqueueAsync<PMReportBackgroundJob, PMReportBackgroundJobArgs>(new PMReportBackgroundJobArgs
                {
                    PMReportId = input.Id,
                    PMReportStatus = PMReportStatus.Expired
                }, BackgroundJobPriority.High, TimeSpan.FromHours((input.ExpiredTime.Value - DateTimeUtils.GetNow()).TotalHours));

                if (input.CanSendTime < now)
                {
                    pmReport.PMReportStatus = PMReportStatus.CanSend;
                    await WorkScope.UpdateAsync(pmReport);
                }
                else
                {
                    // add background job
                    await _backgroundJobManager.EnqueueAsync<PMReportBackgroundJob, PMReportBackgroundJobArgs>(new PMReportBackgroundJobArgs
                    {
                        PMReportId = input.Id,
                        PMReportStatus = PMReportStatus.CanSend
                    }, BackgroundJobPriority.High, TimeSpan.FromHours((input.CanSendTime.Value - DateTimeUtils.GetNow()).TotalHours));
                }
            }           

            return input;
        }

        private DateTime SettingToDate(int day, int hour, DateTime timeline)
        {
            var result = timeline.AddDays(day - (int)timeline.DayOfWeek).AddHours(hour - timeline.Hour);
            return result;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_Update)]
        public async Task<PMReportDto> Update(PMReportDto input)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(input.Id);

            var isExist = await WorkScope.GetAll<PMReport>().AnyAsync(x => x.Id != input.Id && x.Name == input.Name && x.Type == input.Type && x.Year == input.Year);
            if (isExist)
                throw new UserFriendlyException("PM Report already exist !");

            if(input.IsActive != pmReport.IsActive)
            {
                throw new UserFriendlyException("Report status cannot be edited !");
            }

            await WorkScope.UpdateAsync(ObjectMapper.Map<PMReportDto, PMReport>(input, pmReport));
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_Delete)]
        public async Task Delete(long pmReportId)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(pmReportId);

            var hasPmReportProject = await WorkScope.GetAll<PMReportProject>().AnyAsync(x => x.PMReportId == pmReportId);
            if (hasPmReportProject)
                throw new UserFriendlyException("PM Report has PmReportProject !");

            await WorkScope.DeleteAsync(pmReport);
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_CloseReport)]
        public async Task<string> CloseReport(long pmReportId)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(pmReportId);

            // phạt nếu chưa gửi report
            var pmReportProjects = WorkScope.GetAll<PMReportProject>()
                .Where(x => x.PMReportId == pmReportId && x.Status == PMReportProjectStatus.Draft 
                && x.PMReport.Type == PMReportType.Weekly);
            foreach(var i in pmReportProjects)
            {
                i.IsPunish = PunishStatus.High;
                await WorkScope.UpdateAsync(i);
            }

            var newPmReport = new CreatePMReportDto
            {
                Name = pmReport.Name + " (1)",
                Year = DateTime.Now.Year,
                IsActive = true,
                Type = PMReportType.Weekly
            };
            await Create(newPmReport);

            return $"{pmReport.Name} locked, new PmReport with name {pmReport.Name} (1) created";
        }
    }
}
