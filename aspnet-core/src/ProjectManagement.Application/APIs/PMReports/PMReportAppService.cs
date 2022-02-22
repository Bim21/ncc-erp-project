
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using NccCore.Uitls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using ProjectManagement.APIs.PMReportProjectIssues.Dto;
using ProjectManagement.APIs.PMReportProjects.Dto;
using ProjectManagement.APIs.PMReports.Dto;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Entities;
using ProjectManagement.NccCore.BackgroundJob;
using ProjectManagement.Services.Timesheet;
using ProjectManagement.Services.Timesheet.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReports
{
    [AbpAuthorize]
    public class PMReportAppService : ProjectManagementAppServiceBase
    {
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly TimesheetService _timesheetService;
        public PMReportAppService(IBackgroundJobManager backgroundJobManager, TimesheetService timesheetService)
        {
            _backgroundJobManager = backgroundJobManager;
            _timesheetService = timesheetService;
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
                    NumberOfProject = pmReportProject.Where(y => y.PMReportId == x.Id).Count(),
                    CountProjectHeath = new List<CountProjectHealth> {
                    new CountProjectHealth
                    {
                        ProjectHealth = ProjectHealth.Green,
                        Number = pmReportProject.Where(y => y.PMReportId == x.Id).Count(x=>x.ProjectHealth == ProjectHealth.Green),
                    },
                    new CountProjectHealth
                    {
                        ProjectHealth = ProjectHealth.Red,
                        Number = pmReportProject.Where(y => y.PMReportId == x.Id).Count(x=>x.ProjectHealth == ProjectHealth.Red),
                    },
                    new CountProjectHealth
                    {
                        ProjectHealth = ProjectHealth.Yellow,
                        Number = pmReportProject.Where(y => y.PMReportId == x.Id).Count(x=>x.ProjectHealth == ProjectHealth.Yellow),
                    },
                },
                    Note = x.Note,
                });
            return await query.GetGridResult(query, input);
        }

        [HttpGet]
        public async Task<List<CollectTimesheetDto>> CollectTimesheet(long pmReportId, DateTime startTime, DateTime endTime)
        {
            var pmReportProjects = await WorkScope.GetAll<PMReportProject>()
                .Include(p => p.Project)
                .Include(p => p.Project.PM)
                .Where(x => x.PMReportId == pmReportId)
                .Select(s => new
                {
                    pmReportProject = s,
                    projectCode = s.Project.Code,
                    projectName = s.Project.Name,
                    pmName = s.Project.PM.Name + " " + s.Project.PM.Surname
                 })
                .ToListAsync();

            var listProjectCode = pmReportProjects
                .Select(pro=>pro.projectCode).ToList();

            var listTimsheetByProjectCode = await _timesheetService.getTimesheetByListProjectCode(listProjectCode, startTime, endTime);
            
            Dictionary<String, TotalWorkingTimeOfWeekDto> mapProjectCodeToTimesheet = listTimsheetByProjectCode
                .GroupBy(s => s.ProjectCode)
                .ToDictionary(x => x.Key, s => s.FirstOrDefault());
           
            var result = new List<CollectTimesheetDto>();
            foreach (var pmReport in pmReportProjects)
            {
                TotalWorkingTimeOfWeekDto timesheet = mapProjectCodeToTimesheet.ContainsKey(pmReport.projectCode) ? mapProjectCodeToTimesheet[pmReport.projectCode] : default;
                if(timesheet != null)
                {
                    pmReport.pmReportProject.TotalNormalWorkingTime = timesheet.NormalWorkingTime;
                    pmReport.pmReportProject.TotalOverTime = timesheet.OverTime;
                    await WorkScope.UpdateAsync(pmReport.pmReportProject);
                    result.Add(new CollectTimesheetDto
                    {
                        NormalWorkingTime = timesheet.NormalWorkingTime,
                        OverTime = timesheet.OverTime,
                        ProjectCode = pmReport.projectCode,
                        ProjectName = pmReport.projectName,
                        PMName = pmReport.pmName,
                        Note = "Success"
                    });
                }
                else
                {
                    result.Add(new CollectTimesheetDto
                    {                        
                        ProjectCode = pmReport.projectCode,
                        ProjectName = pmReport.projectName,
                        PMName = pmReport.pmName,
                        Note = "Not found in Timesheet tool"
                    });

                }               

            }

            return result;
        }

        public async Task<List<PMReportDto>> GetAll()
        {
            var query = WorkScope.GetAll<PMReport>()
                .Select(x => new PMReportDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Year = x.Year,
                    IsActive = x.IsActive,
                    Type = x.Type,
                    PMReportStatus = x.PMReportStatus,
                    Note = x.Note,
                });
            return await query.ToListAsync();
        }

        public async Task<UpdateNoteDto> UpdateNote(UpdateNoteDto input)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(input.Id);
            if (!pmReport.IsActive)
            {
                throw new UserFriendlyException("Report has been closed !");
            }
            pmReport.Note = input.Note;
            await WorkScope.UpdateAsync(pmReport);
            return input;
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

            if (input.IsActive != pmReport.IsActive)
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
            foreach (var i in pmReportProjects)
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

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_StatisticsReport)]
        public async Task<ReportStatisticsDto> StatisticsReport(long pmReportId, DateTime startDate)
        {
            var changeInFuture = new List<TotalFutureUseDto>();

            var pmReport = await WorkScope.GetAsync<PMReport>(pmReportId);

            var issues = await (from p in WorkScope.GetAll<PMReportProjectIssue>().Where(x => x.PMReportProject.PMReportId == pmReportId)
                                select new GetPMReportProjectIssueDto
                                {
                                    Id = p.Id,
                                    PMReportProjectId = p.PMReportProjectId,
                                    ProjectName = p.PMReportProject.Project.Name,
                                    Description = p.Description,
                                    Impact = p.Impact,
                                    Critical = p.Critical.ToString(),
                                    Source = p.Source.ToString(),
                                    Solution = p.Solution,
                                    MeetingSolution = p.MeetingSolution,
                                    Status = p.Status.ToString(),
                                    ProjectHealth = p.PMReportProject.ProjectHealth,
                                    CreatedAt = p.CreationTime
                                }).ToListAsync();

            var projectUser = WorkScope.GetAll<ProjectUser>().Include(x => x.Project)
                .Where(x => x.Status != ProjectUserStatus.Past && x.IsFutureActive && x.Project.Status != ProjectStatus.Closed);
            var futureUser = projectUser.Where(x => x.StartTime.Date <= startDate.Date && x.Status == ProjectUserStatus.Future);
            var presentUser = projectUser.Where(x => x.Status == ProjectUserStatus.Present && x.IsFutureActive);

            var changeUse = from p in presentUser
                            join f in futureUser on p.UserId equals f.UserId
                            where p.ProjectId == f.ProjectId
                            select new
                            {
                                Present = p,
                                Future = f
                            };

            foreach (var item in changeUse)
            {
                if (item.Present.AllocatePercentage != item.Future.AllocatePercentage)
                {
                    changeInFuture.Add(new TotalFutureUseDto
                    {
                        UserId = item.Present.UserId,
                        ProjectId = item.Present.ProjectId,
                        Total = item.Future.AllocatePercentage - item.Present.AllocatePercentage
                    });
                }
            }

            var totalPercentageFuture = futureUser.Where(x => !changeInFuture.Select(r => r.UserId).Contains(x.Id) && !changeInFuture.Select(r => r.ProjectId).Contains(x.ProjectId));


            var query = from u in WorkScope.GetAll<User>().ToList()
                        join pu in projectUser on u.Id equals pu.UserId into pUser
                        join c in changeInFuture on u.Id equals c.UserId into change
                        join t in totalPercentageFuture on u.Id equals t.UserId into newFuture
                        select new
                        {
                            UserId = u.Id,
                            FullName = u.Name + " " + u.Surname,
                            Avatar = "/avatars/" + u.AvatarPath,
                            UserType = u.UserType,
                            Branch = u.Branch,
                            UserEmail = u.EmailAddress,
                            TotalInTheWeek = pUser.Where(x => x.PMReportId == pmReportId && x.Status == ProjectUserStatus.Present).Sum(x => x.AllocatePercentage),
                            TotalInTheFuture = presentUser.Where(x => x.UserId == u.Id).Sum(x => x.AllocatePercentage) + change.Sum(x => x.Total) + newFuture.Sum(x => x.AllocatePercentage)
                        };

            var result = new ReportStatisticsDto
            {
                Note = pmReport.Note,
                Issues = issues,
                ResourceInTheWeek = query.OrderByDescending(x => x.TotalInTheWeek).Select(x => new ProjectUserStatistic
                {
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Avatar = x.Avatar,
                    UserType = x.UserType,
                    Branch = x.Branch,
                    Email = x.UserEmail,
                    AllocatePercentage = x.TotalInTheWeek
                }).ToList(),
                ResourceInTheFuture = query.OrderByDescending(x => x.TotalInTheFuture).Select(x => new ProjectUserStatistic
                {
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Avatar = x.Avatar,
                    UserType = x.UserType,
                    Branch = x.Branch,
                    Email = x.UserEmail,
                    AllocatePercentage = x.TotalInTheFuture
                }).ToList()
            };
            return result;
        }

        [AbpAuthorize(PermissionNames.DeliveryManagement_PMReport_Get)]
        public async Task<PMReportDto> Get(long id)
        {
            var pmReport = await WorkScope.GetAsync<PMReport>(id);
            return new PMReportDto
            {
                Id = pmReport.Id,
                IsActive = pmReport.IsActive,
                Name = pmReport.Name,
                Note = pmReport.Note,
                PMReportStatus = pmReport.PMReportStatus,
                Type = pmReport.Type,
                Year = pmReport.Year
            };
        }
    }
}
