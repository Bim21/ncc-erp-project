using Abp.Authorization;
using Abp.Configuration;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using NccCore.Uitls;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.APIs.Timesheets.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Entities;
using ProjectManagement.Services.Finance;
using ProjectManagement.Services.Finance.Dto;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.TimesheetProjects
{
    [AbpAuthorize]
    public class TimesheetProjectAppService : ProjectManagementAppServiceBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly FinanceService _financeService;
        private ISettingManager _settingManager;
        private KomuService _komuService;

        public TimesheetProjectAppService(IWebHostEnvironment environment, FinanceService financeService, 
            KomuService komuService, ISettingManager settingManager)
        {
            _hostingEnvironment = environment;
            _financeService = financeService;
            _komuService = komuService;
            _settingManager = settingManager;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_GetAllByproject)]
        public async Task<List<GetTimesheetProjectDto>> GetAllByProject(long projectId)
        {
            var query = from ts in WorkScope.GetAll<Timesheet>()
                        join tsp in WorkScope.GetAll<TimesheetProject>().Where(x => x.ProjectId == projectId)
                        on ts.Id equals tsp.TimesheetId
                        select new GetTimesheetProjectDto
                        {
                            Id = tsp.Id,
                            TimeSheetName = $"T{ts.Month}/{ts.Year}",
                            ProjectId = tsp.ProjectId,
                            TimesheetFile = tsp.FilePath,
                            Note = tsp.Note,
                            HistoryFile = tsp.HistoryFile
                        };
            return await query.ToListAsync();
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_ViewInvoice)]
        public async Task<List<GetDetailInvoiceDto>> ViewInvoice(long timesheetId)
        {
            var query = from c in WorkScope.GetAll<Client>()
                        join tsp in WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId && x.Timesheet.IsActive)
                        on c.Id equals tsp.Project.ClientId
                        group tsp by new { c.Id, c.Name } into pp
                        select new GetDetailInvoiceDto
                        {
                            ClientId = pp.Key.Id,
                            ClientName = pp.Key.Name,
                            TotalProject = pp.Count()
                        };
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_CreateInvoice)]
        public async Task<MergeInvoiceDto> CreateInvoice(MergeInvoiceDto input)
        {
            var timesheet = await WorkScope.GetAsync<Timesheet>(input.TimesheetId);

            if (timesheet.CreatedInvoice)
                throw new UserFriendlyException("Invoice created !");

            var timesheetProject = WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == input.TimesheetId && x.Timesheet.IsActive);
            var query = WorkScope.GetAll<Client>().Where(x => timesheetProject.Select(p => p.Project.ClientId).Contains(x.Id))
                .Select(x => new
                {
                    ClientId = x.Id,
                    ClientName = x.Name,
                    ClientCode = x.Code,
                    Month = timesheetProject.Where(p => p.Project.ClientId == x.Id).Select(m => m.Timesheet.Month).FirstOrDefault(),
                    Year = timesheetProject.Where(p => p.Project.ClientId == x.Id).Select(m => m.Timesheet.Year).FirstOrDefault(),
                    TimesheetProject = timesheetProject.Where(p => p.Project.ClientId == x.Id).Select(p => new
                    {
                        ProjectName = p.Project.Name,
                        FileId = p.Id,
                        FilePath = p.FilePath
                    }).ToList()
                });

            var createInvoice = new List<CreateInvoiceDto>();
            foreach (var isMerge in input.MergeInvoice)
            {
                if (isMerge.isMergeInvoice)
                {
                    var client = query.Where(c => c.ClientId == isMerge.ClientId);
                    var projectName = new StringBuilder();
                    foreach (var item in client)
                    {
                        foreach (var p in item.TimesheetProject)
                        {
                            projectName.Append($"{p.ProjectName}_");
                        }
                        var invoice = new CreateInvoiceDto
                        {
                            Name = $"Invoice {item.Month}/{item.Year}",
                            ClientName = item.ClientName,
                            Project = $"{projectName}",
                            AccountCode = item.ClientCode,
                            TotalPrice = 0,
                            Status = InvoiceStatus.New,
                            Note = null,
                            Detail = item.TimesheetProject.Select(x => new InvoiceDetailDto
                            {
                                ProjectName = x.ProjectName,
                                FileId = x.FileId,
                                LinkFile = x.FilePath
                            }).ToList()
                        };
                        createInvoice.Add(invoice);
                    }
                }
                else
                {
                    var client = query.Where(c => c.ClientId == isMerge.ClientId);

                    foreach (var item in client)
                    {
                        foreach (var p in item.TimesheetProject)
                        {
                            var invoice = new CreateInvoiceDto
                            {
                                Name = $"Invoice {item.Month}/{item.Year}",
                                ClientName = item.ClientName,
                                Project = p.ProjectName,
                                AccountCode = item.ClientCode,
                                TotalPrice = 0,
                                Status = InvoiceStatus.New,
                                Note = null,
                                Detail = new List<InvoiceDetailDto>
                                    {
                                        new InvoiceDetailDto
                                        {
                                            ProjectName = p.ProjectName,
                                            FileId = p.FileId,
                                            LinkFile = p.FilePath
                                        }
                                    }
                            };
                            createInvoice.Add(invoice);
                        }
                    }
                }
            }
            var rs = await _financeService.CreateInvoiceToFinance(createInvoice);
            if (rs == null)
                throw new UserFriendlyException("Error creating Invoice");

            timesheet.CreatedInvoice = true;
            timesheet.IsActive = false;
            await WorkScope.UpdateAsync(timesheet);
            return input;
        }

        [HttpGet]
        public async Task<List<GetProjectDto>> GetAllProjectForDropDown(long timesheetId)
        {
            var timesheetProject = await WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId).Select(x => x.ProjectId).ToListAsync();

            var query = WorkScope.GetAll<Project>().Where(x => !timesheetProject.Contains(x.Id))
                .Where(x => x.Status != ProjectStatus.Potential && x.Status != ProjectStatus.Closed)
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

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_GetAllProjectTimesheetByTimesheet, 
            PermissionNames.Timesheet_TimesheetProject_ViewOnlyme, 
            PermissionNames.Timesheet_TimesheetProject_ViewOnlyActiveProject,
            PermissionNames.Timesheet_TimesheetProject_ViewProjectBillInfomation)]
        public async Task<GridResult<GetTimesheetDetailDto>> GetAllProjectTimesheetByTimesheet(GridParam input, long timesheetId)
        {
            var viewAll = PermissionChecker.IsGranted(PermissionNames.Timesheet_TimesheetProject_GetAllProjectTimesheetByTimesheet);
            var viewonlyme = PermissionChecker.IsGranted(PermissionNames.Timesheet_TimesheetProject_ViewOnlyme);
            var viewActiveProject = PermissionChecker.IsGranted(PermissionNames.Timesheet_TimesheetProject_ViewOnlyActiveProject);
            var viewProjectBillInfo = PermissionChecker.IsGranted(PermissionNames.Timesheet_TimesheetProject_ViewProjectBillInfomation);

            var query = (from tsp in WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId)
                        join p in WorkScope.GetAll<Project>() on tsp.ProjectId equals p.Id
                        join pr in WorkScope.GetAll<PMReportProject>().Where(x => x.PMReport.IsActive) on p.Id equals pr.ProjectId
                        join c in WorkScope.GetAll<Client>() on p.ClientId equals c.Id
                        join u in WorkScope.GetAll<User>() on p.PMId equals u.Id
                        where viewAll || (viewonlyme ? p.PMId == AbpSession.UserId.Value : !viewActiveProject || p.Status != ProjectStatus.Potential && p.Status != ProjectStatus.Closed)
                        select new GetTimesheetDetailDto
                        {
                            Id = tsp.Id,
                            ProjectId = tsp.ProjectId,
                            TimesheetId = tsp.TimesheetId,
                            ProjectName = p.Name,
                            PmId = u.Id,
                            PmUserType = u.UserType,
                            PmAvatarPath = "/avatars/" + u.AvatarPath,
                            PmBranch = u.Branch,
                            PmEmailAddress = u.EmailAddress,
                            PmFullName = u.Name + " " + u.Surname,
                            PmUserName = u.UserName,
                            ClientId = c.Id,
                            ClientName = c.Name,
                            File = tsp.FilePath,
                            ProjectBillInfomation = !viewProjectBillInfo ? "" : tsp.ProjectBillInfomation,
                            Note = tsp.Note,
                            IsSendReport = pr.Status,
                            HistoryFile = tsp.HistoryFile,
                            HasFile = !string.IsNullOrEmpty(tsp.FilePath)
                        }).OrderByDescending(x => x.ClientId);

            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_Create)]
        public async Task<TimesheetProjectDto> Create(TimesheetProjectDto input)
        {
            var billInfomation = new StringBuilder();

            var timesheet = await WorkScope.GetAsync<Timesheet>(input.TimesheetId);
            if (!timesheet.IsActive)
            {
                throw new UserFriendlyException("Timesheet not active !");
            }

            var isExist = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId);
            if (isExist)
                throw new UserFriendlyException($"TimesheetProject with ProjectId {input.ProjectId} already exist in Timesheet !");

            var projectUserBills = WorkScope.GetAll<ProjectUserBill>().Where(x => x.ProjectId == input.ProjectId && x.isActive && x.Project.IsCharge)
                                .Select(x => new
                                {
                                    FullName = x.User.FullName,
                                    BillRole = x.BillRole,
                                    BillRate = x.BillRate
                                });

            foreach (var b in projectUserBills)
            {
                billInfomation.Append($"<b>{b.FullName}</b> - {b.BillRole} - {b.BillRate} <br>");
            }

            input.ProjectBillInfomation = $"{billInfomation}";
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<TimesheetProject>(input));

            return input;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet)]
        public async Task<List<ProjectDto>> GetAllRemainProjectInTimesheet(long timesheetId)
        {
            var timesheetProjects = WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId).Select(x => x.ProjectId);
            var query = WorkScope.GetAll<Project>().Where(x => !timesheetProjects.Contains(x.Id) && x.IsCharge == true && x.Status != ProjectStatus.Potential && x.Status != ProjectStatus.Closed)
                                .Select(x => new ProjectDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Code = x.Code,
                                    ProjectType = x.ProjectType,
                                    StartTime = x.StartTime.Date,
                                    EndTime = x.EndTime.Value.Date,
                                    Status = x.Status
                                });

            return await query.ToListAsync();
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_Update)]
        public async Task<TimesheetProjectDto> Update(TimesheetProjectDto input)
        {
            var timesheet = await WorkScope.GetAsync<Timesheet>(input.TimesheetId);
            var timeSheetProject = await WorkScope.GetAsync<TimesheetProject>(input.Id);
            var isExist = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.Id != input.Id && (x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId));
            if (isExist)
                throw new UserFriendlyException($"TimesheetProject with ProjectId {input.ProjectId} already exist in Timesheet !");

            if (!timesheet.IsActive)
            {
                throw new UserFriendlyException("Timesheet not active !");
            }

            if(string.IsNullOrEmpty(input.ProjectBillInfomation))
                input.ProjectBillInfomation = timeSheetProject.ProjectBillInfomation;
            ObjectMapper.Map<TimesheetProjectDto, TimesheetProject>(input, timeSheetProject);
            await WorkScope.GetRepo<TimesheetProject, long>().UpdateAsync(timeSheetProject);
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_Delete)]
        public async Task Delete(long timesheetProjectId)
        {
            var timeSheetProject = await WorkScope.GetAll<TimesheetProject>().Include(x => x.Timesheet).FirstOrDefaultAsync(x => x.Id == timesheetProjectId);

            if (!timeSheetProject.Timesheet.IsActive)
            {
                throw new UserFriendlyException("Timesheet not active !");
            }

            if (timeSheetProject.FilePath != null)
            {
                throw new UserFriendlyException("Timesheet already has attachments, cannot be deleted !");
            }

            await WorkScope.DeleteAsync(timeSheetProject);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_UploadFileTimesheetProject)]
        public async Task UpdateFileTimeSheetProject([FromForm] FileInputDto input)
        {
            String path = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads", "timesheets");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var timesheetProject = await WorkScope.GetAll<TimesheetProject>().Include(x => x.Project).Include(x => x.Timesheet)
                                        .Where(x => x.Id == input.TimesheetProjectId).FirstOrDefaultAsync();

            if(!timesheetProject.Timesheet.IsActive)
            {
                throw new UserFriendlyException("Timesheet not active !");
            }
            
            var now = DateTimeUtils.GetNow();
            var user = await WorkScope.GetAsync<User>(AbpSession.UserId.Value);
            var historyFile = new StringBuilder();
            string message;
            string alias;
            var ListAttach = new List<attachment>();
            var projectUri = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri);
            var komuUserNameSetting = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUserNames);
            var komuUserNames = komuUserNameSetting.Split(";").ToList();
            komuUserNames.RemoveAt(komuUserNames.Count-1);

            if (input != null && input.File != null && input.File.Length > 0)
            {
                string fileName = input.File.FileName;
                string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                if (FileExtension == "xlsx" || FileExtension == "xltx" || FileExtension == "docx")
                {
                    var filePath = timesheetProject.Timesheet.Year + "-" + timesheetProject.Timesheet.Month + "_" + timesheetProject.Project.Code + "_" + fileName;
                    if (timesheetProject.FilePath != null && timesheetProject.FilePath != fileName)
                    {
                        File.Delete(Path.Combine(path, timesheetProject.FilePath));

                        timesheetProject.FilePath = null;
                        await WorkScope.UpdateAsync(timesheetProject);
                    }

                    using (var stream = System.IO.File.Create(Path.Combine(path, filePath)))
                    {
                        await input.File.CopyToAsync(stream);
                        timesheetProject.FilePath = filePath;
                    }
                }
                else
                {
                    throw new UserFriendlyException(String.Format("Only accept files xlsx, xltx, docx !"));
                }

                // thêm history upload file
                historyFile.Append($"{now.ToString("yyyy/MM/dd HH:mm")} {user.UserName} upload {timesheetProject.FilePath}<br>");
                // nhắc nhở komu
                message = $"Chào bạn lúc {now.ToString("yyyy/MM/dd HH:mm")} có {user.UserName} upload {timesheetProject.FilePath} vào project " +
                            $"\"{timesheetProject.Project.Name}\" trong đợt timesheet \"{timesheetProject.Timesheet.Name}\".";
                alias = "Nhắc việc NCC";
                ListAttach.Add(new attachment
                {
                    title = "Mời bạn click vào đây để xem chi tiết công việc nhé.",
                    titlelink = $"{projectUri}/app/list-project-detail/timesheet-tab?id={timesheetProject.ProjectId}"
                });
            }
            else
            {                        
                historyFile.Append($"{now.ToString("yyyy/MM/dd HH:mm")} {user.UserName} delete {timesheetProject.FilePath}<br>");
                message = $"Chào bạn lúc {now.ToString("yyyy/MM/dd HH:mm")} có {user.UserName} delete {timesheetProject.FilePath} vào project " +
                            $"\"{timesheetProject.Project.Name}\" trong đợt timesheet \"{timesheetProject.Timesheet.Name}\".";
                alias = "Nhắc việc NCC";
                ListAttach.Add(new attachment
                {
                    title = "Mời bạn click vào đây để xem chi tiết công việc nhé.",
                    titlelink = $"{projectUri}/app/list-project-detail/timesheet-tab?id={timesheetProject.ProjectId}"
                });

                File.Delete(Path.Combine(path, timesheetProject.FilePath));
                timesheetProject.FilePath = null;
            }

            await _komuService.ProcessKomu(ListAttach, message, alias, komuUserNames);
            timesheetProject.HistoryFile += historyFile;
            await WorkScope.UpdateAsync(timesheetProject);
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_DownloadFileTimesheetProject)]
        public async Task<object> DownloadFileTimesheetProject(long timesheetProjectId)
        {
            var timesheetProject = await WorkScope.GetAsync<TimesheetProject>(timesheetProjectId);

            String path = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads", "timesheets");

            var filePath = Path.Combine(path, timesheetProject.FilePath);

            var data = await System.IO.File.ReadAllBytesAsync(filePath);

            return new
            {
                FileName = timesheetProject.FilePath,
                Data = data
            };
        }
    }
}
