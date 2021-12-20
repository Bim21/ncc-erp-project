﻿using Abp.Authorization;
using Abp.Configuration;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using NccCore.Uitls;
using OfficeOpenXml;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.APIs.TimeSheetProjectBills;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.APIs.Timesheets.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Entities;
using ProjectManagement.Net.MimeTypes;
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
        private readonly string templateFolder = Path.Combine("wwwroot", "template");
        private TimeSheetProjectBillAppService _timeSheetProjectBillAppService;

        public TimesheetProjectAppService(IWebHostEnvironment environment, FinanceService financeService,
            KomuService komuService, ISettingManager settingManager, TimeSheetProjectBillAppService timeSheetProjectBillAppService)
        {
            _hostingEnvironment = environment;
            _financeService = financeService;
            _komuService = komuService;
            _settingManager = settingManager;
            _timeSheetProjectBillAppService = timeSheetProjectBillAppService;
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
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_ExportInvoice)]

        public async Task<FileBase64Dto> ExportInvoice(InvoiceExcelDto invoiceExcelDto)
        {
            try
            {
                var templateFilePath = Path.Combine(templateFolder, "InvoiceUserTemplate.xlsx");
                var listProject = await WorkScope.GetAll<Project>().Where(x => invoiceExcelDto.ProjectId.Contains(x.Id)).Include(x => x.Client).Include(x=>x.Currency).ToListAsync();
                var defaultWorkingHours = Convert.ToInt32(await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.DefaultWorkingHours));
                var invoiceUserBilling = new List<TimeSheetProjectBillExcelDto>();

                string allProjectName = "";
                IDictionary<long, string> allCurrencyDic = new Dictionary<long, string>();
                string allCurrency = "";

                foreach (var project in listProject)
                {
                    var listUserBillProject = new List<TimeSheetProjectBillExcelDto>();
                    switch (project?.ChargeType)
                    {
                        case ChargeType.Daily:
                             listUserBillProject = await WorkScope.GetAll<TimesheetProjectBill>()
                                                                .Where(x => x.TimesheetId == invoiceExcelDto.TimesheetId && x.ProjectId == project.Id && x.IsActive)
                                                                .OrderByDescending(x => x.CreationTime)
                                                                .Select(x => new TimeSheetProjectBillExcelDto
                                                                {
                                                                    FullName = x.User.FullName,
                                                                    WorkingTime = x.WorkingTime,
                                                                    BillRate = x.BillRate,
                                                                    LineTotal = x.WorkingTime * x.BillRate
                                                                }).ToListAsync();
                            break;
                        case ChargeType.Monthly:
                             listUserBillProject = await WorkScope.GetAll<TimesheetProjectBill>()
                                                                .Include(x => x.TimeSheet)
                                                                .Where(x => x.TimesheetId == invoiceExcelDto.TimesheetId && x.ProjectId == project.Id && x.IsActive)
                                                                .OrderByDescending(x => x.CreationTime)
                                                                .Select(x => new TimeSheetProjectBillExcelDto
                                                                {
                                                                    FullName = x.User.FullName,
                                                                    WorkingTime = x.WorkingTime,
                                                                    BillRate = (double)(x.BillRate / x.TimeSheet.TotalWorkingDay),
                                                                    LineTotal = x.WorkingTime * (double)(x.BillRate / x.TimeSheet.TotalWorkingDay)
                                                                }).ToListAsync();
                            break;
                        case ChargeType.Hours:
                            listUserBillProject = await WorkScope.GetAll<TimesheetProjectBill>()
                                                                .Include(x => x.TimeSheet)
                                                                .Where(x => x.TimesheetId == invoiceExcelDto.TimesheetId && x.ProjectId == project.Id && x.IsActive)
                                                                .OrderByDescending(x => x.CreationTime)
                                                                .Select(x => new TimeSheetProjectBillExcelDto
                                                                {
                                                                    FullName = x.User.FullName,
                                                                    WorkingTime = x.WorkingTime,
                                                                    BillRate = x.BillRate * defaultWorkingHours,
                                                                    LineTotal = x.WorkingTime * x.BillRate * defaultWorkingHours
                                                                }).ToListAsync();
                            break;
                    }
                    invoiceUserBilling.AddRange(listUserBillProject);
                }

                
                using (var memoryStream = new MemoryStream(File.ReadAllBytes(templateFilePath)))
                {
                    using (var excelPackageIn = new ExcelPackage(memoryStream))
                    {
                        var invoiceSheet = excelPackageIn.Workbook.Worksheets[0];
                        var companySetupSheet = excelPackageIn.Workbook.Worksheets[1];

                        invoiceSheet.PrinterSettings.FitToHeight = 0;

                        invoiceSheet.Cells["E2"].Value = listProject[0].Client?.Name;
                        foreach(var project in listProject)
                        {
                            allProjectName += project.Name + "\n";
                            if(project.Currency != null)
                            {
                                allCurrencyDic[project.CurrencyId.Value] = project.Currency.Name;
                            }
                        }
                        invoiceSheet.Cells["D6:E6"].Value = allProjectName;
                        invoiceSheet.Cells["D6:E6"].Style.WrapText = true;
                        invoiceSheet.Cells["D7:E7"].Value = listProject[0].Client?.Address;
                        invoiceSheet.Cells["B3"].Value = DateTime.Now.Date;
                        invoiceSheet.Cells["B4"].Value = $"BILLING PERIOD: {DateTime.Now.Month}/{DateTime.Now.Year}";
                        var invoiceDetailTable = invoiceSheet.Tables.First();
                        var invoiceDetailTableStart = invoiceDetailTable.Address.Start;

                        if (invoiceUserBilling.Count > 0)
                        {
                            invoiceSheet.InsertRow(invoiceDetailTableStart.Row + 1, invoiceUserBilling.Count - 1, invoiceDetailTableStart.Row + invoiceUserBilling.Count);
                            invoiceSheet.Names["InvoiceNetTotal"].Formula = "=SUM(InvoiceDetails[LINE TOTAL])-Discount";

                            int d = 0;
                            for (int i = invoiceDetailTableStart.Row + 1; i <= invoiceDetailTable.Address.End.Row; i++)
                            {
                                for (int j = invoiceDetailTable.Address.Start.Column; j <= invoiceDetailTable.Address.End.Column; j++)
                                {
                                    //add the cell data to the List
                                    switch (j)
                                    {
                                        case 2:
                                            invoiceSheet.Cells[i, j].Value = invoiceUserBilling[d].FullName;
                                            break;
                                        case 3:
                                            invoiceSheet.Cells[i, j].Value = invoiceUserBilling[d].WorkingTime;
                                            break;
                                        case 4:
                                            invoiceSheet.Cells[i, j].Value = invoiceUserBilling[d].BillRate;
                                            break;
                                        default:
                                            invoiceSheet.Cells[i, j].Value = invoiceUserBilling[d].LineTotal;
                                            break;
                                    }
                                }
                                d++;
                            }
                        }
                        #region Fill dat into Company Setup sheet
                        foreach (KeyValuePair<long, string> kvp in allCurrencyDic)
                            allCurrency += kvp.Value + "\n";
                        companySetupSheet.Cells["C14"].Value = allCurrency;
                        invoiceSheet.Cells["C14"].Style.WrapText = true;
                        #endregion
                        var fileBytes = excelPackageIn.GetAsByteArray();
                        string fileBase64 = Convert.ToBase64String(fileBytes);
                      
                        return new FileBase64Dto
                        {
                            FileName = $"{listProject[0].Name.Replace("/", "").Replace(":", "").Replace(" ", "_")}_{DateTime.Now}_.xlsx",
                            FileType = MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet,
                            Base64 = fileBase64
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
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
                    ProjectType = x.ProjectType,
                    StartTime = x.StartTime.Date,
                    EndTime = x.EndTime.Value.Date,
                    Status = x.Status,
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
            var filterItem = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName.Contains("isComplete") && (bool)x.Value == false) : null;
            if (filterItem != null)
            {
                input.FilterItems.Remove(filterItem);
            }
            var viewAll = PermissionChecker.IsGranted(PermissionNames.Timesheet_TimesheetProject_GetAllProjectTimesheetByTimesheet);
            var viewonlyme = PermissionChecker.IsGranted(PermissionNames.Timesheet_TimesheetProject_ViewOnlyme);
            var viewActiveProject = PermissionChecker.IsGranted(PermissionNames.Timesheet_TimesheetProject_ViewOnlyActiveProject);
            var viewProjectBillInfo = PermissionChecker.IsGranted(PermissionNames.Timesheet_TimesheetProject_ViewProjectBillInfomation);
            //var timsheetProjectBills = WorkScope.GetAll<TimesheetProjectBill>().Where(x => x.TimesheetId == timesheetId);

            //foreach (var item in timsheetProjectBills)
            //{
            //}
            //var projectBillInfomation = 

            var query = (from tsp in WorkScope.GetAll<TimesheetProject>()
                                              .Where(x => x.TimesheetId == timesheetId)
                                              .Where(x => filterItem == null || x.IsComplete != true)
                         join p in WorkScope.GetAll<Project>() on tsp.ProjectId equals p.Id
                         //join pr in WorkScope.GetAll<PMReportProject>().Where(x => x.PMReport.IsActive) on p.Id equals pr.ProjectId
                         join c in WorkScope.GetAll<Client>() on p.ClientId equals c.Id into ps
                         from c in ps.DefaultIfEmpty()
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
                             ProjectBillInfomation = viewProjectBillInfo ? tsp.ProjectBillInfomation : "" ,
                             Note = tsp.Note,
                             //IsSendReport = pr.Status,
                             HistoryFile = tsp.HistoryFile,
                             HasFile = !string.IsNullOrEmpty(tsp.FilePath),
                             IsComplete = tsp.IsComplete,
                         }).OrderByDescending(x => x.ClientId);
            //foreach (var item in query)
            //{
            //    item.ProjectBillInfomation
            //}
            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheet_TimesheetProject_Create)]
        public async Task<TimesheetProjectDto> Create(TimesheetProjectDto input)
        {
            var billInfomation = new StringBuilder();
            var projectType = await WorkScope.GetAsync<Project>(input.ProjectId);
            if(projectType.ProjectType==ProjectType.TRAINING) throw new UserFriendlyException("The training project is not suitable !");
            var timesheet = await WorkScope.GetAsync<Timesheet>(input.TimesheetId);
            if (!timesheet.IsActive)
            {
                throw new UserFriendlyException("Timesheet not active !");
            }

            var isExist = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId);
            if (isExist)
                throw new UserFriendlyException($"TimesheetProject with ProjectId {input.ProjectId} already exist in Timesheet !");
            var projectUserBills = WorkScope.GetAll<ProjectUserBill>()
                .Include(x => x.User)
                .Where(x => x.ProjectId == input.ProjectId && (!x.EndTime.HasValue || x.EndTime.Value.Date > timesheet.CreationTime.Date || (x.EndTime.Value.Month >= timesheet.Month && x.EndTime.Value.Year >= timesheet.Year)));
            var timesheetProjectBills = await WorkScope.GetAll<TimesheetProjectBill>()
                .Where(x => x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId)
                .ToListAsync();

            var deleteTimesheetProjectBills = await WorkScope.GetAll<TimesheetProjectBill>().Where(x => x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId && !projectUserBills.Select(x => x.UserId).Contains(x.UserId)).ToListAsync();
            foreach (var item in deleteTimesheetProjectBills)
            {
                await WorkScope.DeleteAsync<TimesheetProjectBill>(item.Id);
            }
            //input.ProjectBillInfomation = $"{billInfomation}";
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<TimesheetProject>(input));

            await _timeSheetProjectBillAppService.UpdateFromProjectUserBill(input.ProjectId, input.TimesheetId);

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

            if (string.IsNullOrEmpty(input.ProjectBillInfomation))
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
            var timesheetProjectBills = await WorkScope.GetAll<TimesheetProjectBill>()
                .Where(x => x.TimesheetId == timeSheetProject.TimesheetId && x.ProjectId == timeSheetProject.ProjectId)
                .ToListAsync();
            foreach(var tsProjectBill in timesheetProjectBills)
            {
                await WorkScope.DeleteAsync(tsProjectBill);
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

            if (!timesheetProject.Timesheet.IsActive)
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
            komuUserNames.RemoveAt(komuUserNames.Count - 1);

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
