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
using ProjectManagement.APIs.TimeSheetProjectBills.Dto;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.APIs.Timesheets.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants;
using ProjectManagement.Entities;
using ProjectManagement.NccCore.Helper;
using ProjectManagement.Net.MimeTypes;
using ProjectManagement.Services.Finance;
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
                        .OrderByDescending(x => x.CreationTime)
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
                             ProjectCode = tsp.Project.Code,
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
                                                        Description = x.Note,
                                                        Currency = x.Currency.Name,
                                                        ChargeType = x.ChargeType
                                                    }).ToList(),
                             Note = tsp.Note,
                             HistoryFile = tsp.HistoryFile,
                             HasFile = !string.IsNullOrEmpty(tsp.FilePath),
                             IsComplete = tsp.IsComplete,
                             RequireTimesheetFile = tsp.Project.RequireTimesheetFile,
                             ProjectCurrency = tsp.Project.Currency.Name,
                             ProjectChargeType = tsp.Project.ChargeType,
                             InvoiceNumber = tsp.InvoiceNumber,
                             WorkingDay = tsp.WorkingDay,
                             Discount = tsp.Discount,
                             TransferFee = tsp.TransferFee,
                         }).OrderByDescending(x => x.ClientId);
            return await query.GetGridResult(query, input);
        }


        public async Task<TimesheetProject> CreateTimesheetProject(TimesheetProjectDto input, long invoiceNumber, float transferFee, float discount, float workingDay)
        {
            var timesheetProject = new TimesheetProject
            {
                ProjectId = input.ProjectId,
                TimesheetId = input.TimesheetId,
                IsComplete = false,
                InvoiceNumber = invoiceNumber,
                TransferFee = transferFee,
                Discount = discount,
                WorkingDay = workingDay
            };

            return await WorkScope.InsertAsync(timesheetProject);
        }


        [HttpPost]
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_AddProjectToTimesheet)]
        public async Task<Object> Create(TimesheetProjectDto input)
        {
            var isExist = await WorkScope.GetAll<TimesheetProject>()
               .AnyAsync(x => x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId);
            if (isExist)
                throw new UserFriendlyException($"ProjectId {input.ProjectId} already exist in this Timesheet.");

            var project = await WorkScope.GetAll<Project>()
                .Where(s => s.Id == input.ProjectId)
                .Select(s => new
                {
                    Project = s,
                    s.Client.TransferFee
                }).FirstOrDefaultAsync();

            if (project.Project.IsCharge != true || project.Project.Status != ProjectStatus.InProgress)
            {
                throw new UserFriendlyException($"Project Id {input.ProjectId} is No-charged or Not Inprogress. So, you can't add it to the timesheet");
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


            var invoiceNumber = project.Project.LastInvoiceNumber + 1;
            float discount = project.Project.Discount;
            float transferFee = project.TransferFee;

            var timesheetProject = await CreateTimesheetProject(input, invoiceNumber, transferFee, discount, (float)timesheet.TotalWorkingDay);

            project.Project.LastInvoiceNumber = invoiceNumber;

            var listTimesheetProjectBill = new List<TimesheetProjectBill>();

            foreach (var pub in listPUB)
            {
                var timesheetProjectBill = new TimesheetProjectBill
                {
                    ProjectId = pub.ProjectId,
                    TimesheetId = timesheet.Id,
                    UserId = pub.UserId,
                    BillRole = pub.BillRole,
                    BillRate = pub.BillRate,
                    StartTime = pub.StartTime,
                    EndTime = pub.EndTime,
                    IsActive = true,
                    ChargeType = project.Project.ChargeType,
                    CurrencyId = project.Project.CurrencyId,
                };
                listTimesheetProjectBill.Add(timesheetProjectBill);

            }

            await WorkScope.InsertRangeAsync(listTimesheetProjectBill);
            await CurrentUnitOfWork.SaveChangesAsync();
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

        private async Task<TimesheetTaxDto> GetTimesheetDetailForTaxInTimesheetTool(TimesheetDetailForTaxDto input)
        {
            return await _timesheetService.GetTimesheetDetailForTax(input);
        }

        private ExcelWorksheet CopySheet(ExcelWorkbook workbook, string existingWorksheetName, string newWorksheetName)
        {
            ExcelWorksheet worksheet = workbook.Worksheets.Copy(existingWorksheetName, newWorksheetName);
            return worksheet;
        }


        private async Task<InvoiceData> GetInvoiceData(InputExportInvoiceDto input)
        {
            var defaultWorkingHours = Convert.ToInt32(await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.DefaultWorkingHours));
            var result = new InvoiceData();

            var qtimesheetProject = WorkScope.All<TimesheetProject>()
                .Where(s => s.TimesheetId == input.TimesheetId)
                .Where(s => input.ProjectIds.Contains(s.ProjectId));

            var qtimesheetProjectBill = WorkScope.All<TimesheetProjectBill>()
                .Where(s => s.TimesheetId == input.TimesheetId)
                .Where(s => input.ProjectIds.Contains(s.ProjectId));

            result.Info = await qtimesheetProject
                .Select(s => new InvoiceGeneralInfo
                {
                    ClientAddress = s.Project.Client.Address,
                    ClientName = s.Project.Client.Name,
                    Discount = s.Discount,
                    InvoiceNumber = s.InvoiceNumber,
                    PaymentDueBy = s.Project.Client.PaymentDueBy,
                    PaymentInfo = s.Project.Currency.InvoicePaymentInfo,
                    TransferFee = s.TransferFee,
                    Year = s.Timesheet.Year,
                    Month = s.Timesheet.Month,
                    InvoiceDateSetting = s.Project.Client.InvoiceDateSetting
                }).FirstOrDefaultAsync();


            result.TimesheetUsers = await (from tpb in qtimesheetProjectBill
                                           from tp in qtimesheetProject
                                           where tpb.ProjectId == tp.ProjectId
                                           select new TimesheetUser
                                           {
                                               BillRate = tpb.BillRate,
                                               ChargeType = tpb.ChargeType.Value,
                                               CurrencyName = tpb.Currency.Name,
                                               FullName = tpb.User.FullName,
                                               Mode = input.Mode,
                                               DefaultWorkingHours = defaultWorkingHours,
                                               ProjectName = tpb.Project.Name,
                                               TimesheetWorkingDay = tp.WorkingDay,
                                               WorkingDay = tpb.WorkingTime,
                                               UserId = tpb.UserId,
                                               EmailAddress = tpb.User.EmailAddress,
                                               ProjectCode = tpb.Project.Code,
                                               EndTime = tpb.EndTime,
                                               StartTime = tpb.StartTime,
                                           }).ToListAsync();

            result.ProjectCodes = await qtimesheetProject.Select(x => x.Project.Code).ToListAsync();

            return result;
        }


        private void FillDataToExcelFileInvoiceSheet(ExcelPackage excelPackageIn, InvoiceData data)
        {
            var invoiceSheet = excelPackageIn.Workbook.Worksheets[0];

            var tsTable = invoiceSheet.Tables.First();
            var tsTableStart = tsTable.Address.Start;
            invoiceSheet.InsertRow(tsTableStart.Row + 1, data.TimesheetUsers.Count - 1, tsTableStart.Row + data.TimesheetUsers.Count);
            int rowIndex = tsTableStart.Row + 1;

            double sumLineTotal = 0;
            foreach (var tsUser in data.TimesheetUsers)
            {
                //Fill data sheet invoice
                invoiceSheet.Cells[rowIndex, 2].Value = tsUser.FullName;
                invoiceSheet.Cells[rowIndex, 3].Value = tsUser.ProjectName;
                invoiceSheet.Cells[rowIndex, 4].Value = tsUser.BillRateDisplay;
                invoiceSheet.Cells[rowIndex, 5].Value = tsUser.CurrencyName + "/" + tsUser.ChargeTypeDisplay;
                invoiceSheet.Cells[rowIndex, 6].Value = tsUser.WorkingDayDisplay;
                invoiceSheet.Cells[rowIndex, 7].Formula = $"={tsUser.BillRateDisplay}*{tsUser.WorkingDayDisplay}";
                sumLineTotal += tsUser.BillRateDisplay * tsUser.WorkingDayDisplay;
                rowIndex++;
            }

            var invoiceNetTotal = sumLineTotal + data.Info.TransferFee;
            var invoiceTotal = invoiceNetTotal - (data.Info.Discount * invoiceNetTotal) / 100;
            invoiceSheet.Cells["F6:I6"].Value = data.Info.ClientName;
            invoiceSheet.Cells["F7:I7"].Value = data.Info.ClientAddress;
            invoiceSheet.Cells["C2"].Value = data.Info.InvoiceNumber;
            invoiceSheet.Cells["B3"].Value = data.Info.InvoiceDateStr();
            invoiceSheet.Cells["B4"].Value = $"PAYMENT DUE BY: {data.Info.PaymentDueByStr()}";
            invoiceSheet.Names["TransferFee"].Value = data.Info.TransferFee;
            invoiceSheet.Names["InvoiceNetTotal"].Value = invoiceNetTotal;
            invoiceSheet.Names["DiscountLabel"].Value = $"Discount ({data.Info.Discount}%)";
            invoiceSheet.Names["Discount"].Value = (data.Info.Discount * invoiceNetTotal) / 100;
            invoiceSheet.Names["InvoiceTotal"].Value = invoiceTotal;
            invoiceSheet.Names["CurrencyInvoiceTotal"].Value = invoiceTotal;
            if (data.CurrencyName() == "USD")
            {
                invoiceSheet.Cells["F3:F4"].Value = "$";
            }
            else
            {
                invoiceSheet.Cells["I3:I4"].Value = data.CurrencyName();
            }
            //Fill data payment
            string[] arrPaymentInfo = data.Info.PaymentInfoArr();
            var arrPaymentInfoCount = arrPaymentInfo.Length;

            var indexPayment = invoiceSheet.Names["PaymentDetails"].Start.Row;
            invoiceSheet.InsertRow(indexPayment, arrPaymentInfoCount, indexPayment + arrPaymentInfoCount);

            invoiceSheet.Names["CurrencyTotal"].Value = $"{ data.CurrencyName()} TOTAL";

            for (int i = 0; i < arrPaymentInfoCount; i++)
            {
                invoiceSheet.Cells["B" + indexPayment].Value = arrPaymentInfo[i];
                indexPayment++;
            }
        }
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_ExportInvoice)]
        public async Task<FileBase64Dto> ExportInvoice(InputExportInvoiceDto input)
        {
            var data = await GetInvoiceData(input);

            var templateFilePath = Path.Combine(templateFolder, "Invoice.xlsx");

            using (var memoryStream = new MemoryStream(File.ReadAllBytes(templateFilePath)))
            {
                using (var excelPackageIn = new ExcelPackage(memoryStream))
                {
                    FillDataToExcelFileInvoiceSheet(excelPackageIn, data);
                    string fileBase64 = Convert.ToBase64String(excelPackageIn.GetAsByteArray());

                    return new FileBase64Dto
                    {
                        FileName = data.ExportFileName(),
                        FileType = MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet,
                        Base64 = fileBase64
                    };
                }
            }
        }
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_ExportInvoiceForTax)]
        public async Task<FileBase64Dto> ExportInvoiceForTax(InputExportInvoiceDto input)
        {
            var dataInvoice = await GetInvoiceData(input);
            var dataTimesheetDetail = await GetTimesheetDetailData(dataInvoice);

            var templateFilePath = Path.Combine(templateFolder, "InvoiceForTax.xlsx");

            using (var memoryStream = new MemoryStream(File.ReadAllBytes(templateFilePath)))
            {
                using (var excelPackageIn = new ExcelPackage(memoryStream))
                {
                    FillDataToExcelFileInvoiceSheet(excelPackageIn, dataInvoice);
                    FillDataToSheetTimesheetDetail(excelPackageIn, dataTimesheetDetail);
                    string fileBase64 = Convert.ToBase64String(excelPackageIn.GetAsByteArray());

                    return new FileBase64Dto
                    {
                        FileName = dataInvoice.ExportFileName(),
                        FileType = MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet,
                        Base64 = fileBase64
                    };
                }
            }
        }

        private async Task<List<TimesheetDetailUser>> GetTimesheetDetailData(InvoiceData invoiceData)
        {
            int month = invoiceData.Info.Month;
            int year = invoiceData.Info.Year;

            var timesheetDetailForTaxDto = new TimesheetDetailForTaxDto
            {
                Year = invoiceData.Info.Year,
                Month = invoiceData.Info.Month,
                ProjectCodes = invoiceData.ProjectCodes
            };

            var timesheetDetailForTax = await GetTimesheetDetailForTaxInTimesheetTool(timesheetDetailForTaxDto);

            var listTimesheet = timesheetDetailForTax.ListTimesheet;
            var listWorkingDay = timesheetDetailForTax.ListWorkingDay;

            var resultList = new List<TimesheetDetailUser>();
            List<DateTime> listBillDate = new List<DateTime> { };

            int countProject = invoiceData.ProjectCodes.Count();
            foreach (var tsProject in invoiceData.TimesheetUsers)
            {
                var timesheetDetailUser = new TimesheetDetailUser { };
                var tsDetails = listTimesheet
                                                        .Where(x => x.EmailAddress == tsProject.EmailAddress)
                                                        .Where(x => x.ProjectCode == tsProject.ProjectCode)
                                                        .OrderBy(x => x.DateAt).ToList();

                var firstDayOfMonth = DateTimeUtils.FirstDayOfMonth(new DateTime(year, month, 1));
                var lastDayOfMonth = DateTimeUtils.LastDayOfMonth(firstDayOfMonth);

                var startBillDate = new DateTime(Math.Max(firstDayOfMonth.Ticks, tsProject.StartTime.Ticks));
                var endTime = tsProject.EndTime.HasValue ? tsProject.EndTime.Value : lastDayOfMonth;
                var endBillDate = new DateTime(Math.Min(lastDayOfMonth.Ticks, endTime.Ticks));

                listBillDate = listWorkingDay
                                                .Where(x => x.Date >= startBillDate.Date && x.Date <= endBillDate.Date)
                                                .ToList();

                var timesheetDetails = TimesheetDetailOfUserInProject(listBillDate, tsDetails, tsProject.WorkingDay);
                timesheetDetailUser.FullName = tsProject.FullName;
                timesheetDetailUser.ProjectName = tsProject.ProjectName;
                timesheetDetailUser.ProjectNumber = countProject;
                timesheetDetailUser.TimesheetDetails = timesheetDetails;

                resultList.Add(timesheetDetailUser);
            }

            return resultList;
        }

        private void AddMoreBillDate(List<DateTime> listBillDate, double totalWorkingDay)
        {
            var maxBillDay = listBillDate.Count;
            if (totalWorkingDay <= maxBillDay)
            {
                return;
            }


            var firstBillDate = listBillDate.FirstOrDefault();

            var firstDateOfMonth = DateTimeUtils.FirstDayOfMonth(firstBillDate);
            var date = firstDateOfMonth;
            while (date.Month == firstDateOfMonth.Month || totalWorkingDay > maxBillDay)
            {
                if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
                {
                    listBillDate.Add(date);
                    maxBillDay += 1;
                }
                date = date.AddDays(1);
            }

            listBillDate = listBillDate.OrderBy(s => s.Ticks).ToList();

        }

        private List<TimesheetDetail> TimesheetDetailOfUserInProject(
                List<DateTime> listBillDate,
                List<TimesheetDetail> listTimesheetDetail,
                double totalWorkingDay
        )
        {

            AddMoreBillDate(listBillDate, totalWorkingDay);

            var resultList = new List<TimesheetDetail>();

            double remainDay = totalWorkingDay;

            double workingDay = 1;
            DateTime dateAtLast = DateTime.Today;

            foreach (var billDate in listBillDate)
            {
                if (remainDay <= 0) break;

                workingDay = Math.Min(remainDay, 1);
                remainDay -= workingDay;

                var tsDetail = listTimesheetDetail.Where(x => x.DateAt.Date == billDate.Date).FirstOrDefault();

                if (tsDetail == default)
                {
                    tsDetail = listTimesheetDetail.OrderBy(s => DateTimeUtils.DateDiffAbs(s.DateAt, billDate)).FirstOrDefault();
                }
                if (tsDetail != default)
                {
                    var timesheetDetail = new TimesheetDetail
                    {
                        DateAt = billDate,
                        ManDay = workingDay,
                        TaskName = tsDetail.TaskName,
                        Note = tsDetail.Note,
                    };
                    resultList.Add(timesheetDetail);
                }

            }

            return resultList;
        }

        private ExcelPackage FillDataToSheetTimesheetDetail(
                ExcelPackage excelPackageIn,
                List<TimesheetDetailUser> listTimesheetDetailOfUser
        )
        {

            int projectNumber = listTimesheetDetailOfUser.Select(x => x.ProjectNumber).FirstOrDefault();
            foreach (var timesheetDetailOfUser in listTimesheetDetailOfUser)
            {
                var nameSheetDetailTimesheet = timesheetDetailOfUser.FullName.Replace(" ", "") + "_" + timesheetDetailOfUser.ProjectName.Replace(" ", "");
                if (projectNumber == 1)
                {
                    nameSheetDetailTimesheet = timesheetDetailOfUser.FullName.Replace(" ", "");
                }
                var sheetDetailTimesheet = CopySheet(excelPackageIn.Workbook, "Detail", nameSheetDetailTimesheet);
                sheetDetailTimesheet.Cells["B2:E2"].Value = $"TIMESHEET OF {timesheetDetailOfUser.FullName.ToUpper()} IN {timesheetDetailOfUser.ProjectName.ToUpper()}"; ;

                var tsTable = sheetDetailTimesheet.Tables.First();
                var tsTableStart = tsTable.Address.Start;
                sheetDetailTimesheet.InsertRow(tsTableStart.Row + 1, timesheetDetailOfUser.TimesheetDetails.Count - 1, tsTableStart.Row + timesheetDetailOfUser.TimesheetDetails.Count);
                int rowIndex = tsTableStart.Row + 1;

                foreach (var timesheetDetail in timesheetDetailOfUser.TimesheetDetails)
                {
                    sheetDetailTimesheet.Cells[rowIndex, 2].Value = timesheetDetail.DateAt;
                    sheetDetailTimesheet.Cells[rowIndex, 3].Value = timesheetDetail.ManDay;
                    sheetDetailTimesheet.Cells[rowIndex, 4].Value = timesheetDetail.TaskName;
                    sheetDetailTimesheet.Cells[rowIndex, 5].Value = timesheetDetail.Note;
                    rowIndex++;
                }
            }

            return excelPackageIn;

        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Timesheets_TimesheetDetail_EditInvoiceInfo)]
        public async Task<float> UpdateTimesheetProject(UpdateTimesheetProjectDto input)
        {
            var timesheetProject = await WorkScope.GetAll<TimesheetProject>().FirstOrDefaultAsync(x => x.Id == input.Id);
            if (timesheetProject != null)
            {
                long timesheetId = timesheetProject.TimesheetId;
                var timesheet = await WorkScope.GetAll<Timesheet>().Where(x => x.Id == timesheetId).Where(x => x.IsActive).FirstOrDefaultAsync();
                if (timesheet == default)
                {
                    throw new UserFriendlyException($"Unable to update for closed timesheet Id {timesheet}");
                }

                timesheetProject.WorkingDay = input.WorkingDay;
                timesheetProject.InvoiceNumber = input.InvoiceNumber;
                timesheetProject.TransferFee = input.TransferFee;
                timesheetProject.Discount = input.Discount;
                var timesheetProjectUpdate = await WorkScope.UpdateAsync<TimesheetProject>(timesheetProject);
                return timesheetProjectUpdate.WorkingDay;
            }

            return default;
        }
    }

}

