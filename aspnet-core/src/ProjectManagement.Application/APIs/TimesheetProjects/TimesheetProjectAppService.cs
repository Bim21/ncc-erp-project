using Abp.Authorization;
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
using ProjectManagement.APIs.TimeSheetProjectBills.Dto;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.APIs.Timesheets.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants;
using ProjectManagement.Entities;
using ProjectManagement.Helper;
using ProjectManagement.NccCore.Helper;
using ProjectManagement.Net.MimeTypes;
using ProjectManagement.Services.Finance;
using ProjectManagement.Services.Finance.Dto;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using ProjectManagement.Services.ProjectTimesheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;
using ProjectManagement.Services.Timesheet;
using ProjectManagement.Services.Timesheet.Dto;
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
        private readonly ProjectTimesheetManager _timesheetManager;
        private readonly TimesheetService _timesheetService;

        public TimesheetProjectAppService(IWebHostEnvironment environment, FinanceService financeService,
            KomuService komuService, ISettingManager settingManager, ProjectTimesheetManager timesheetManager,
            TimesheetService timesheetService)
        {
            _hostingEnvironment = environment;
            _financeService = financeService;
            _komuService = komuService;
            _settingManager = settingManager;
            _timesheetManager = timesheetManager;
            _timesheetService = timesheetService;
        }

        [HttpGet]
        [AbpAuthorize]
        public async Task<List<GetTimesheetProjectDto>> GetAllByProject(long projectId)
        {
            var query = WorkScope.GetAll<TimesheetProject>()
                        .Where(x => x.ProjectId == projectId)
                        .OrderByDescending(x =>x.CreationTime)
                        .Select(tsp => new GetTimesheetProjectDto
                        {
                            Id = tsp.Id,
                            TimeSheetName = $"T{tsp.Timesheet.Month}/{tsp.Timesheet.Year}",
                            ProjectId = tsp.ProjectId,
                            TimesheetFile = tsp.FilePath,
                            Note = tsp.Note,
                            HistoryFile = tsp.HistoryFile
                        });
            return await query.ToListAsync();
        }

        [HttpGet]
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

        //[HttpPost]
        //[AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_ExportInvoice)]
        //public async Task<MergeInvoiceDto> CreateInvoice(MergeInvoiceDto input)
        //{
        //    var timesheet = await WorkScope.GetAsync<Timesheet>(input.TimesheetId);

        //    if (timesheet.CreatedInvoice)
        //        throw new UserFriendlyException("Invoice created !");

        //    var timesheetProject = WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == input.TimesheetId && x.Timesheet.IsActive);
        //    var query = WorkScope.GetAll<Client>().Where(x => timesheetProject.Select(p => p.Project.ClientId).Contains(x.Id))
        //        .Select(x => new
        //        {
        //            ClientId = x.Id,
        //            ClientName = x.Name,
        //            ClientCode = x.Code,
        //            Month = timesheetProject.Where(p => p.Project.ClientId == x.Id).Select(m => m.Timesheet.Month).FirstOrDefault(),
        //            Year = timesheetProject.Where(p => p.Project.ClientId == x.Id).Select(m => m.Timesheet.Year).FirstOrDefault(),
        //            TimesheetProject = timesheetProject.Where(p => p.Project.ClientId == x.Id).Select(p => new
        //            {
        //                ProjectName = p.Project.Name,
        //                FileId = p.Id,
        //                FilePath = p.FilePath
        //            }).ToList()
        //        });

        //    var createInvoice = new List<CreateInvoiceDto>();
        //    foreach (var isMerge in input.MergeInvoice)
        //    {
        //        if (isMerge.isMergeInvoice)
        //        {
        //            var client = query.Where(c => c.ClientId == isMerge.ClientId);
        //            var projectName = new StringBuilder();
        //            foreach (var item in client)
        //            {
        //                foreach (var p in item.TimesheetProject)
        //                {
        //                    projectName.Append($"{p.ProjectName}_");
        //                }
        //                var invoice = new CreateInvoiceDto
        //                {
        //                    Name = $"Invoice {item.Month}/{item.Year}",
        //                    ClientName = item.ClientName,
        //                    Project = $"{projectName}",
        //                    AccountCode = item.ClientCode,
        //                    TotalPrice = 0,
        //                    Status = InvoiceStatus.New,
        //                    Note = null,
        //                    Detail = item.TimesheetProject.Select(x => new InvoiceDetailDto
        //                    {
        //                        ProjectName = x.ProjectName,
        //                        FileId = x.FileId,
        //                        LinkFile = x.FilePath
        //                    }).ToList()
        //                };
        //                createInvoice.Add(invoice);
        //            }
        //        }
        //        else
        //        {
        //            var client = query.Where(c => c.ClientId == isMerge.ClientId);

        //            foreach (var item in client)
        //            {
        //                foreach (var p in item.TimesheetProject)
        //                {
        //                    var invoice = new CreateInvoiceDto
        //                    {
        //                        Name = $"Invoice {item.Month}/{item.Year}",
        //                        ClientName = item.ClientName,
        //                        Project = p.ProjectName,
        //                        AccountCode = item.ClientCode,
        //                        TotalPrice = 0,
        //                        Status = InvoiceStatus.New,
        //                        Note = null,
        //                        Detail = new List<InvoiceDetailDto>
        //                            {
        //                                new InvoiceDetailDto
        //                                {
        //                                    ProjectName = p.ProjectName,
        //                                    FileId = p.FileId,
        //                                    LinkFile = p.FilePath
        //                                }
        //                            }
        //                    };
        //                    createInvoice.Add(invoice);
        //                }
        //            }
        //        }
        //    }
        //    var rs = await _financeService.CreateInvoiceToFinance(createInvoice);
        //    if (rs == null)
        //        throw new UserFriendlyException("Error creating Invoice");

        //    timesheet.CreatedInvoice = true;
        //    timesheet.IsActive = false;
        //    await WorkScope.UpdateAsync(timesheet);
        //    return input;
        //}

        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_ExportInvoice)]
        public async Task<FileBase64Dto> ExportInvoice(InvoiceExcelDto invoiceExcelDto)
        {
            try
            {
                var templateFilePath = Path.Combine(templateFolder, "InvoiceUserTemplate.xlsx");
                var listProject = await WorkScope.GetAll<Project>().Where(x => invoiceExcelDto.ProjectId.Contains(x.Id)).Include(x => x.Client).Include(x => x.Currency).ToListAsync();
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
                        foreach (var project in listProject)
                        {
                            allProjectName += project.Name + "\n";
                            if (project.Currency != null)
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
                        string fileName = string.Empty;
                        if (listProject.Count() > 1)
                        {
                            fileName = FilesHelper.SetFileName(listProject[0].Client.Name);
                        }
                        else
                        {
                            fileName = FilesHelper.SetFileName(listProject[0].Name);
                        }
                        return new FileBase64Dto
                        {
                            FileName = fileName,
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
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail)]
        public async Task<GridResult<GetTimesheetDetailDto>> GetAllProjectTimesheetByTimesheet(GridParam input, long timesheetId)
        {
            var filterItem = input.FilterItems != null ? input.FilterItems.FirstOrDefault(x => x.PropertyName.Contains("isComplete") && (bool)x.Value == false) : null;
            if (filterItem != null)
            {
                input.FilterItems.Remove(filterItem);
            }
            var allowViewBillRate = PermissionChecker.IsGranted(PermissionNames.Timesheets_TimesheetDetail_ViewBillRate);
            var allowViewAllTSProject = PermissionChecker.IsGranted(PermissionNames.Timesheets_TimesheetDetail_ViewAll);

            var query = (from tsp in WorkScope.GetAll<TimesheetProject>()
                                              .Where(x => x.TimesheetId == timesheetId)
                                              .Where(x => filterItem == null || x.IsComplete != true)
                                              .Where(x => !allowViewAllTSProject ? x.Project.PMId == AbpSession.UserId : true)
                         select new GetTimesheetDetailDto
                         {
                             Id = tsp.Id,
                             ProjectId = tsp.ProjectId,
                             TimesheetId = tsp.TimesheetId,
                             ProjectName = tsp.Project.Name,
                             PmId = tsp.Project.PMId,
                             PmUserType = tsp.Project.PM.UserType,
                             PmAvatarPath = tsp.Project.PM.AvatarPath,
                             PmBranch = tsp.Project.PM.Branch,
                             PmEmailAddress = tsp.Project.PM.EmailAddress,
                             PmFullName = tsp.Project.PM.Name + " " + tsp.Project.PM.Surname,
                             ClientId = tsp.Project.ClientId,
                             ClientName = tsp.Project.Client != null ? tsp.Project.Client.Name : "NULL",
                             File = tsp.FilePath,
                             ProjectBillInfomation = WorkScope.GetAll<TimesheetProjectBill>()
                                                    .Where(x => x.TimesheetId == tsp.TimesheetId && x.ProjectId == tsp.ProjectId && x.IsActive)
                                                    .Select(x => new TimesheetProjectBillInfoDto
                                                    {
                                                        FullName = x.User.FullName,
                                                        BillRate = allowViewBillRate ? x.BillRate : 0,
                                                        BillRole = x.BillRole,
                                                        WorkingTime = x.WorkingTime,
                                                        Description = x.Note
                                                    }).ToList(),
                             Note = tsp.Note,
                             HistoryFile = tsp.HistoryFile,
                             HasFile = !string.IsNullOrEmpty(tsp.FilePath),
                             IsComplete = tsp.IsComplete,
                             RequireTimesheetFile = tsp.Project.RequireTimesheetFile,
                             Currency = tsp.Project.Currency.Name,
                             ChargeType = tsp.Project.ChargeType,
                         }).OrderByDescending(x => x.ClientId);
            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_AddProjectToTimesheet)]
        public async Task<Object> Create(TimesheetProjectDto input)
        {
            var isExist = await WorkScope.GetAll<TimesheetProject>()
               .AnyAsync(x => x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId);
            if (isExist)
                throw new UserFriendlyException($"ProjectId {input.ProjectId} already exist in this Timesheet.");

            var isCharedProject = await WorkScope.GetAll<Project>()
                .Where(s => s.Id == input.ProjectId)
                .Select(s => s.IsCharge)
                .FirstOrDefaultAsync();

            if (isCharedProject != true)
            {
                throw new UserFriendlyException("You can't add No-chagred project to timesheet");
            }

            var timesheet = await _timesheetManager.GetTimesheetById(input.TimesheetId);
            if (timesheet == default || !timesheet.IsActive)
            {
                throw new UserFriendlyException($"The timesheet Id {input.TimesheetId} is not exist or not active");
            }

            await _timesheetManager.DeleteTimesheetProjectBill(input.ProjectId, timesheet.Id);

            var listPUB = await _timesheetManager.GetListProjectUserBillDto(timesheet.Year, timesheet.Month, input.ProjectId);

            if (listPUB == null || listPUB.IsEmpty())
            {
                throw new UserFriendlyException($"Project has no Bill Account!");
            }

            var timesheetProject = new TimesheetProject
            {
                ProjectId = input.ProjectId,
                TimesheetId = input.TimesheetId,
                IsComplete = false,
            };

            await WorkScope.InsertAsync(timesheetProject);

            var listTimesheetProjectBill = new List<TimesheetProjectBill>();

            foreach (var pub in listPUB)
            {
                var projectId = pub.ProjectId;
                var timesheetProjectBill = new TimesheetProjectBill
                {
                    ProjectId = projectId,
                    TimesheetId = timesheet.Id,
                    UserId = pub.UserId,
                    BillRole = pub.BillRole,
                    BillRate = pub.BillRate,
                    StartTime = pub.StartTime,
                    EndTime = pub.EndTime,
                    IsActive = true
                };
                listTimesheetProjectBill.Add(timesheetProjectBill);

            }

            await WorkScope.InsertRangeAsync(listTimesheetProjectBill);

            return new
            {
                TimesheetProject = timesheetProject,
                ListTimesheetProjectBill = listTimesheetProjectBill
            };
        }



        [HttpGet]
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail)]
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
        public async Task<SetTimesheetProjectCompleteDto> SetComplete(SetTimesheetProjectCompleteDto input)
        {
            var timesheetProject = await WorkScope.GetAsync<TimesheetProject>(input.Id);
            timesheetProject.IsComplete = input.IsComplete;
            await CurrentUnitOfWork.SaveChangesAsync();
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_Delete)]
        public async Task Delete(long timesheetProjectId)
        {
            var timeSheetProject = await WorkScope.GetAll<TimesheetProject>()
                .Include(x => x.Timesheet)
                .FirstOrDefaultAsync(x => x.Id == timesheetProjectId);

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

            foreach (var tsProjectBill in timesheetProjectBills)
            {
                tsProjectBill.IsDeleted = true;
            }
            timeSheetProject.IsDeleted = true;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_UploadTimesheetFile)]
        public async Task UpdateFileTimeSheetProject([FromForm] FileInputDto input)
        {
            String path = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads", "timesheets");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var timesheetProject = await WorkScope.GetAll<TimesheetProject>()
                .Include(x => x.Project)
                .Include(x => x.Timesheet)
                .Where(x => x.Id == input.TimesheetProjectId)
                .FirstOrDefaultAsync();

            if (!timesheetProject.Timesheet.IsActive)
            {
                throw new UserFriendlyException("Timesheet not active !");
            }

            var now = DateTimeUtils.GetNow();
            var user = await WorkScope.GetAsync<User>(AbpSession.UserId.Value);
            var userName = UserHelper.GetUserName(user.EmailAddress);
            if (user != null && !user.KomuUserId.HasValue)
            {
                user.KomuUserId = await _komuService.GetKomuUserId(new KomuUserDto { Username = userName ?? user.UserName }, ChannelTypeConstant.KOMU_USER);
                await WorkScope.UpdateAsync(user);
            }
            var historyFile = new StringBuilder();
            var message = new StringBuilder();
            var projectUri = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri);
            var titlelink = $"{projectUri}/app/list-project-detail/timesheet-tab?id={timesheetProject.ProjectId}";

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
                message.AppendLine($"Chào bạn lúc **{now.ToString("yyyy/MM/dd HH:mm")}** có **{userName ?? user.UserName}** upload **{timesheetProject.FilePath}** vào project " +
                            $"\"**{timesheetProject.Project.Name}**\" trong đợt timesheet \"**{timesheetProject.Timesheet.Name}**\".");
            }
            else
            {
                historyFile.Append($"{now.ToString("yyyy/MM/dd HH:mm")} {user.UserName} delete {timesheetProject.FilePath}<br>");
                message.AppendLine($"Chào bạn lúc **{now.ToString("yyyy/MM/dd HH:mm")}** có **{userName ?? user.UserName}** delete **{timesheetProject.FilePath}** vào project " +
                           $"\"**{timesheetProject.Project.Name}**\" trong đợt timesheet \"**{timesheetProject.Timesheet.Name}**\".");

                File.Delete(Path.Combine(path, timesheetProject.FilePath));
                timesheetProject.FilePath = null;
            }

            var komuUserNameSetting = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUserNames);
            var komuUserNames = komuUserNameSetting.Split(";").ToList();
            komuUserNames.RemoveAt(komuUserNames.Count - 1);
            message.AppendLine(titlelink);
            foreach (var username in komuUserNames)
            {
                await _komuService.NotifyToChannel(new KomuMessage
                {
                    UserName = username,
                    Message = message.ToString(),
                }, ChannelTypeConstant.USER_ONLY);
            }
            timesheetProject.HistoryFile += historyFile;
            await WorkScope.UpdateAsync(timesheetProject);
        }

        [HttpGet]
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

        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_UpdateNote)]
        public async Task<IActionResult> UpdateNote(UpdateTsProjectNoteDto input)
        {
            var projectTimesheet = await WorkScope.GetAll<TimesheetProject>().FirstOrDefaultAsync(x => x.Id == input.Id);
            if (projectTimesheet != null)
            {
                projectTimesheet.Note = input.Note;
                await WorkScope.UpdateAsync<TimesheetProject>(projectTimesheet);
                return new OkObjectResult("Update Note Success!");
            }
            return new BadRequestObjectResult("Not Found Timesheet Project!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPM()
        {
            var pms = await WorkScope.GetAll<Project>()
                .Select(u => new
                {
                    Id = u.PMId,
                    UserName = u.PM.UserName,
                    Name = u.PM.Name,
                    Surname = u.PM.Surname,
                    EmailAddress = u.PM.EmailAddress,
                    FullName = u.PM.FullName,
                    AvatarPath = u.PM.AvatarPath,
                    UserType = u.PM.UserType,
                    UserLevel = u.PM.UserLevel,
                    Branch = u.PM.Branch,
                })
                .Distinct()
                .ToListAsync();
            return new OkObjectResult(pms);
        }

        public async Task<TimesheetChartDto> GetBillInfoChart(long projectId, DateTime? fromDate, DateTime? toDate)
        {
            DateTime endDate = toDate.HasValue ? toDate.Value : DateTimeUtils.GetNow().AddMonths(-1);
            DateTime startDate = fromDate.HasValue ? fromDate.Value : endDate.AddMonths(-6);
            var toYear = endDate.Year;
            var toMonth = endDate.Month;
            var fromYear = startDate.Year;
            var fromMonth = startDate.Month;

            var mapTimesheet = await WorkScope.GetAll<TimesheetProjectBill>()
                .Where(s => s.ProjectId == projectId)
                .Where(s => s.TimeSheet.Year > fromYear || (s.TimeSheet.Year == fromYear && s.TimeSheet.Month >= fromMonth))
                .Where(s => s.TimeSheet.Year < toYear || (s.TimeSheet.Year == toYear && s.TimeSheet.Month <= toMonth))
                .GroupBy(s => new { s.TimesheetId, s.TimeSheet.Year, s.TimeSheet.Month, s.TimeSheet.TotalWorkingDay })
                .Select(s => new
                {
                    s.Key.Year,
                    s.Key.Month,
                    TotalWorkingDay = s.Key.TotalWorkingDay.HasValue ? s.Key.TotalWorkingDay : 22,
                    ManDay = s.Sum(x => x.WorkingTime),
                }).ToDictionaryAsync(s => s.Year + "-" + s.Month);

            var listMonth = DateTimeUtils.GetListMonth(startDate, endDate);
            var listManDay = new List<double>();
            var listManMonth = new List<double>();
            foreach (var date in listMonth)
            {
                var key = date.Year + "-" + date.Month;
                var manDay = mapTimesheet.ContainsKey(key) ? mapTimesheet[key].ManDay : 0;
                listManDay.Add(manDay);

                var manMonth = mapTimesheet.ContainsKey(key) ? Math.Round((double)(manDay / mapTimesheet[key].TotalWorkingDay), 2) : 0;
                listManMonth.Add(manMonth);
            }

            var result = new TimesheetChartDto()
            {
                Labels = listMonth.Select(s => DateTimeUtils.GetMonthName(s)).ToList(),
                ManDays = listManDay,
                ManMonths = listManMonth
            };

            return result;
        }

        [HttpPost]
        private async Task<TimesheetTaxDto> GetTimesheetDetailForTaxInTimesheetTool(InputTimesheetTaxDto input)
        {
            return await _timesheetService.GetTimesheetDetailForTax(input);
        }

        private ExcelWorksheet CopySheet(ExcelWorkbook workbook, string existingWorksheetName, string newWorksheetName)
        {
            ExcelWorksheet worksheet = workbook.Worksheets.Copy(existingWorksheetName, newWorksheetName);
            return worksheet;
        }

        private ExcelPackage ExportSheetInvoice(
          ExcelPackage excelPackageIn,
          List<Project> listProject,
          List<InvoiceDto> listTimesheetDetailOfUser)
        {
            try
            {
                string allProjectName = "";
                IDictionary<long, string> allCurrencyDic = new Dictionary<long, string>();

                var invoiceSheet = excelPackageIn.Workbook.Worksheets[0];
                var countListProject = listProject.Count;
                int currentRowInvoice = 15;
                double sumLineTotal = 0;
                var invoiceDetailTable = invoiceSheet.Tables.First();
                var invoiceDetailTableStart = invoiceDetailTable.Address.Start;
                invoiceSheet.InsertRow(invoiceDetailTableStart.Row + 1, listTimesheetDetailOfUser.Count - 1, invoiceDetailTableStart.Row + listTimesheetDetailOfUser.Count);

                foreach (var timesheetDetailOfUser in listTimesheetDetailOfUser)
                {
                    //index start table in excel template
                    var nameSheetDetailTimesheet = timesheetDetailOfUser.FullName.Replace(" ", "") + "_" + timesheetDetailOfUser.ProjectName.Replace(" ", "");
                    if (countListProject == 1)
                    {
                        nameSheetDetailTimesheet = timesheetDetailOfUser.FullName.Replace(" ", "");
                    }
                    //Fill data sheet invoice
                    invoiceSheet.Cells[currentRowInvoice, 2].Formula = "=HYPERLINK(\"#'" + nameSheetDetailTimesheet + "'!A1\",\"" + timesheetDetailOfUser.FullName + "\")";
                    invoiceSheet.Cells[currentRowInvoice, 3].Value = timesheetDetailOfUser.ProjectName;
                    invoiceSheet.Cells[currentRowInvoice, 4].Value = timesheetDetailOfUser.WorkingTimeDay;
                    invoiceSheet.Cells[currentRowInvoice, 5].Value = timesheetDetailOfUser.BillRate;
                    invoiceSheet.Cells[currentRowInvoice, 6].Value = timesheetDetailOfUser.LineTotal;
                    sumLineTotal += timesheetDetailOfUser.LineTotal;
                    currentRowInvoice++;
                }
                invoiceSheet.PrinterSettings.FitToHeight = 0;
                invoiceSheet.Cells["E2"].Value = listProject[0].Client?.Name;
                foreach (var project in listProject)
                {
                    allProjectName += project.Name + "\n";
                    if (project.Currency != null)
                    {
                        allCurrencyDic[project.CurrencyId.Value] = project.Currency.Name;
                    }
                }
                invoiceSheet.Cells["D6:E6"].Value = allProjectName;
                invoiceSheet.Cells["D6:E6"].Style.WrapText = true;
                invoiceSheet.Cells["D7:E7"].Value = listProject[0].Client?.Address;
                invoiceSheet.Cells["B3"].Value = DateTime.Now.Date;
                invoiceSheet.Cells["B4"].Value = $"BILLING PERIOD: {DateTime.Now.Month}/{DateTime.Now.Year}";
                invoiceSheet.Names["InvoiceNetTotal"].Formula = $"={sumLineTotal}-Discount";

                if (countListProject == 1)
                {
                    invoiceSheet.DeleteColumn(3);
                }
                return excelPackageIn;
            }

            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        private ExcelPackage ExportSheetCompany(
           ExcelPackage excelPackageIn,
           List<Project> listProject)
        {
            try
            {
                string allProjectName = "";
                IDictionary<long, string> allCurrencyDic = new Dictionary<long, string>();
                string allCurrency = "";
                foreach (var project in listProject)
                {
                    allProjectName += project.Name + "\n";
                    if (project.Currency != null)
                    {
                        allCurrencyDic[project.CurrencyId.Value] = project.Currency.Name;
                    }
                }

                var companySetupSheet = excelPackageIn.Workbook.Worksheets[1];

                #region Fill dat into Company Setup sheet
                foreach (KeyValuePair<long, string> kvp in allCurrencyDic)
                    allCurrency += kvp.Value + "\n";
                companySetupSheet.Cells["C14"].Value = allCurrency;
                #endregion

                return excelPackageIn;
            }

            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        private ExcelPackage ExportSheetTimesheetDetail(
          ExcelPackage excelPackageIn,
          List<Project> listProject,
          List<InvoiceDto> listTimesheetDetailOfUser)
        {
            try
            {
                var countListProject = listProject.Count;
                foreach (var timesheetDetailOfUser in listTimesheetDetailOfUser)
                {
                    int currentRow = 3;
                    //index start table in excel template
                    var nameSheetDetailTimesheet = timesheetDetailOfUser.FullName.Replace(" ", "") + "_" + timesheetDetailOfUser.ProjectName.Replace(" ", "");
                    if (countListProject == 1)
                    {
                        nameSheetDetailTimesheet = timesheetDetailOfUser.FullName.Replace(" ", "");
                    }
                    var sheetDetailTimesheet = CopySheet(excelPackageIn.Workbook, "Detail", nameSheetDetailTimesheet);
                    sheetDetailTimesheet.Cells["B1:E1"].Value = $"TIMESHEET DETAIL OF PROJECT {timesheetDetailOfUser.ProjectName.ToUpper()} - {timesheetDetailOfUser.FullName.ToUpper()}"; ;
                    sheetDetailTimesheet.Cells["B1:E1"].Style.WrapText = true;

                    sheetDetailTimesheet.InsertRow(currentRow, timesheetDetailOfUser.ListTimesheetDetail.Count - 1);
                    foreach (var timesheetDetail in timesheetDetailOfUser.ListTimesheetDetail)
                    {
                        //Fill data sheet timesheet detail
                        sheetDetailTimesheet.Cells[currentRow, 2].Value = timesheetDetail.DateAtView;
                        sheetDetailTimesheet.Cells[currentRow, 3].Value = timesheetDetail.WorkingTime;
                        sheetDetailTimesheet.Cells[currentRow, 4].Value = timesheetDetail.TaskName;
                        sheetDetailTimesheet.Cells[currentRow, 5].Value = timesheetDetail.Note;
                        currentRow++;
                    }
                }

                return excelPackageIn;
            }

            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_ExportInvoiceForTax)]
        public async Task<FileBase64Dto> ExportInvoiceForTax(InvoiceExcelDto input)
        {
            try
            {
                var templateFilePath = Path.Combine(templateFolder, "InvoiceUserTemplateForTax.xlsx");
                var listProject = await WorkScope.GetAll<Project>()
                    .Where(x => input.ProjectId.Contains(x.Id))
                    .Include(x => x.Client)
                    .Include(x => x.Currency)
                      .Select(x => new Project
                      {
                          Id = x.Id,
                          Code = x.Code,
                          Name = x.Name,
                          Currency = x.Currency,
                          CurrencyId = x.CurrencyId,
                          Client = x.Client,
                      }).ToListAsync();

                var listTimesheetDetailOfUser = await GetInvoiceProjectTimesheet(input, listProject);

                using (var memoryStream = new MemoryStream(File.ReadAllBytes(templateFilePath)))
                {
                    using (var excelPackageIn = new ExcelPackage(memoryStream))
                    {

                        ExportSheetInvoice(excelPackageIn, listProject, listTimesheetDetailOfUser);
                        ExportSheetTimesheetDetail(excelPackageIn, listProject, listTimesheetDetailOfUser);
                        ExportSheetCompany(excelPackageIn, listProject);

                        var fileBytes = excelPackageIn.GetAsByteArray();
                        string fileBase64 = Convert.ToBase64String(fileBytes);
                        string fileName = string.Empty;
                        if (listProject.Count() > 1)
                        {
                            fileName = FilesHelper.SetFileName(listProject[0].Client.Name);
                        }
                        else
                        {
                            fileName = FilesHelper.SetFileName(listProject[0].Name);
                        }

                        return new FileBase64Dto
                        {
                            FileName = fileName,
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

        private async Task<ResultInvoice> ProjectExportForTax(List<Project> listProject, long timesheetId)
        {
            var defaultWorkingHours = Convert.ToInt32(await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.DefaultWorkingHours));
            var listUserBillProject = new List<InvoiceDto>();
            var invoiceUserBilling = new List<InvoiceDto>();
            List<string> listProjectCode = new List<string> { };
            foreach (var project in listProject)
            {
                listUserBillProject = await (from pub in WorkScope.GetAll<ProjectUserBill>()
                                             join tpb in WorkScope.GetAll<TimesheetProjectBill>() on pub.ProjectId equals tpb.ProjectId
                                             join t in WorkScope.GetAll<Timesheet>() on tpb.TimesheetId equals t.Id
                                             where pub.UserId == tpb.UserId
                                             where tpb.TimesheetId == timesheetId && tpb.ProjectId == project.Id && tpb.IsActive
                                             orderby tpb.CreationTime descending
                                             select new
                                             {
                                                 StartTime = pub.StartTime.Date,
                                                 EndTime = pub.EndTime.Value.Date,
                                                 tpb,
                                                 TotalWorkingDay = t.TotalWorkingDay
                                             })
                                              .Select(x => new InvoiceDto
                                              {
                                                  FullName = x.tpb.User.FullName,
                                                  EmailAddress = x.tpb.User.EmailAddress,
                                                  ProjectName = project.Name,
                                                  ProjectCode = project.Code,
                                                  BillRate = calculateBillRate(x.tpb.BillRate, (ChargeType)project.ChargeType, defaultWorkingHours, x.TotalWorkingDay),
                                                  WorkingTimeDay = x.tpb.WorkingTime,
                                                  StartTime = x.StartTime,
                                                  EndTime = x.EndTime,
                                              }).ToListAsync();
                invoiceUserBilling.AddRange(listUserBillProject);
                listProjectCode.Add(project.Code);
            }

            var resultProjectInvoice = new ResultInvoice
            {
                ListInvoice = invoiceUserBilling,
                ListProjectCode = listProjectCode,
            };

            return resultProjectInvoice;
        }

        private static float calculateBillRate(float billRate, ChargeType chargeType, int defaultWorkingHours, float? totalWorkingDay)
        {
            if (chargeType == ChargeType.Hours)
            {
                return billRate * defaultWorkingHours;
            }
            else if (chargeType == ChargeType.Monthly)
            {
                return billRate / totalWorkingDay.Value;
            }
            else
            {
                return billRate;
            }
        }

        private async Task<List<InvoiceDto>> GetInvoiceProjectTimesheet(InvoiceExcelDto input, List<Project> listProject)
        {
            var timesheet = await WorkScope.GetAll<Timesheet>().
                Where(s => s.Id == input.TimesheetId)
                .Select(s => new Timesheet
                {
                    Year = s.Year,
                    Month = s.Month
                })
                .FirstOrDefaultAsync();

            var resultProjectInvoice = await ProjectExportForTax(listProject, input.TimesheetId);
            var listProjectInvoice = resultProjectInvoice.ListInvoice;
            var listProjectCode = resultProjectInvoice.ListProjectCode;

            var inputTimesheetTaxDto = new InputTimesheetTaxDto
            {
                Year = timesheet.Year,
                Month = timesheet.Month,
                ProjectCodes = listProjectCode
            };

            var timesheetDetailOfUserFromTimesheetTool = await GetTimesheetDetailForTaxInTimesheetTool(inputTimesheetTaxDto);

            return ListTimesheetDetailOfUserInProject(listProjectInvoice, timesheetDetailOfUserFromTimesheetTool, timesheet);
        }

        private List<InvoiceDto> ListTimesheetDetailOfUserInProject(List<InvoiceDto> listProjectInvoice, TimesheetTaxDto timesheetDetailOfUserFromTimesheetTool, Timesheet timesheet)
        {
            //Detail timesheet of user in project
            var listTimesheet = timesheetDetailOfUserFromTimesheetTool.ListTimesheet;
            var listWorkingDay = timesheetDetailOfUserFromTimesheetTool.ListWorkingDay;
            List<DateTime> listWorkingDayProjectUserBill = new List<DateTime> { };

            var listTimesheetDetailOfUser = new List<InvoiceDto>();

            foreach (var projectInvoice in listProjectInvoice)
            {
                var timesheetDetailOfUser = new InvoiceDto { };

                var firstDayOfMonth = DateTimeUtils.FirstDayOfMonth(new DateTime(timesheet.Year, timesheet.Month, 1));
                var lastDayOfMonth = DateTimeUtils.LastDayOfMonth(new DateTime(timesheet.Year, timesheet.Month, 1));

                var startBillDate = new DateTime(Math.Max(firstDayOfMonth.Ticks, projectInvoice.StartTime.Ticks));
                var endTime = projectInvoice.EndTime.HasValue ? projectInvoice.EndTime.Value : lastDayOfMonth;
                var endBillDate = new DateTime(Math.Min(lastDayOfMonth.Ticks, endTime.Ticks));

                listWorkingDayProjectUserBill = listWorkingDay
                                                .Where(x => x.Date >= startBillDate.Date && x.Date <= endBillDate.Date)
                                                .ToList();
                var listTimesheetDetailByProjectAndUser = listTimesheet
                                                        .Where(x => x.EmailAddress == projectInvoice.EmailAddress)
                                                        .Where(x => x.ProjectCode == projectInvoice.ProjectCode)
                                                        .OrderBy(x => x.DateAt).ToList();

                float workingTimeHourDB = projectInvoice.WorkingTimeDay * 8;
                //Timesheet detail of user in project
                var listTimesheetDetail = TimesheetDetailOfUserInProject(listWorkingDayProjectUserBill, listTimesheetDetailByProjectAndUser, workingTimeHourDB);

                timesheetDetailOfUser.FullName = projectInvoice.FullName;
                timesheetDetailOfUser.ProjectName = projectInvoice.ProjectName;
                timesheetDetailOfUser.WorkingTimeDay = projectInvoice.WorkingTimeDay;
                timesheetDetailOfUser.BillRate = projectInvoice.BillRate;
                timesheetDetailOfUser.ListTimesheetDetail = listTimesheetDetail;
                listTimesheetDetailOfUser.Add(timesheetDetailOfUser);
            }

            return listTimesheetDetailOfUser;
        }

        private List<TimesheetDetailDto> TimesheetDetailOfUserInProject(
            List<DateTime> listWorkingDayProjectUserBill,
            List<TimesheetDetailDto> listTimesheetDetailByProjectAndUser,
            float workingTimeHourDB)
        {

            var listTimesheetDetail = new List<TimesheetDetailDto>();

            float totalWorkingHour = 0;
            string taskName = "";
            string note = "";
            float workingHour = 8;
            DateTime dateAtLast = DateTime.Today;

            foreach (var workingDayProjectUserBill in listWorkingDayProjectUserBill)
            {
                if (workingTimeHourDB == totalWorkingHour) break;
                workingHour = Math.Min((workingTimeHourDB - totalWorkingHour), 8);
                var timesheetByProjectAndUser = listTimesheetDetailByProjectAndUser.Where(x => x.DateAt == workingDayProjectUserBill.Date).FirstOrDefault();

                if (timesheetByProjectAndUser != null)
                {
                    taskName = timesheetByProjectAndUser.TaskName;
                    note = timesheetByProjectAndUser.Note;
                    dateAtLast = timesheetByProjectAndUser.DateAt;
                }
                else
                {
                    taskName = listTimesheetDetailByProjectAndUser.Where(x => x.DateAt.Date == dateAtLast.Date).Select(x => x.TaskName).FirstOrDefault();
                    note = "";
                }

                totalWorkingHour += workingHour;
                var timesheetDetail = new TimesheetDetailDto
                {
                    DateAtView = workingDayProjectUserBill.ToString("dd/MM/yyyy"),
                    WorkingTime = workingHour,
                    TaskName = taskName,
                    Note = note,
                };
                listTimesheetDetail.Add(timesheetDetail);
            }

            return listTimesheetDetail;
        }
    }
}
