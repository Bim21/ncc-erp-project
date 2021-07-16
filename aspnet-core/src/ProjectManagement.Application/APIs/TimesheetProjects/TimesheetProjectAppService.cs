using Abp.Authorization;
using Abp.UI;
using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.APIs.ProjectUserBills.Dto;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.APIs.Timesheets.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.Services.Finance;
using ProjectManagement.Services.Finance.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.TimesheetProjects
{
    public class TimesheetProjectAppService : ProjectManagementAppServiceBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly FinanceService _financeService;

        public TimesheetProjectAppService(IWebHostEnvironment environment, FinanceService financeService)
        {
            _hostingEnvironment = environment;
            _financeService = financeService;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_GetAllByproject)]
        public async Task<List<GetTimesheetProjectDto>> GetAllByProject(long projectId)
        {
            var query = from ts in WorkScope.GetAll<Timesheet>()
                        join tsp in WorkScope.GetAll<TimesheetProject>().Where(x => x.ProjectId == projectId)
                        on ts.Id equals tsp.ProjectId into pp
                        from p in pp.DefaultIfEmpty()
                        select new GetTimesheetProjectDto
                        {
                            Id = ts.Id,
                            TimeSheetName = $"T{ts.Month}/{ts.Year}",
                            ProjectId = p.ProjectId,
                            TimesheetFile = "/timesheets/" + p.FilePath,
                            Note = p.Note
                        };
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
        //public async Task<MergeInvoiceDto> CreateInvoice(MergeInvoiceDto input)
        //{
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
        //                FilePath = "timesheets/" + p.FilePath
        //            }).ToList()
        //        });

        //    var createInvoice = new List<CreateInvoiceDto>();
        //    foreach (var isMerge in input.MergeInvoice)
        //    {
        //        if(isMerge.isMergeInvoice)
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
        //                    Name = $"Invoice {item.Month}/{item.Year} - {item.ClientName} - Project:[{projectName}]",
        //                    TimeAt = $"{DateTime.Now}",
        //                    AccountCode = item.ClientCode,
        //                    TotalPrice = 0,
        //                    Status = InvoiceStatus.New,
        //                    Note = null,
        //                    Detail = item.TimesheetProject.Select(x => new InvoiceDetailDto
        //                    {
        //                        ProjectName = x.ProjectName,
        //                        LinkFile = x.FilePath
        //                    }).ToList()
        //                };
        //                createInvoice.Add(invoice);
        //            }
        //        } else
        //        {
        //            var client1 = query.Where(c => c.ClientId == isMerge.ClientId);

        //            foreach (var item in client1)
        //            {
        //                foreach (var p in item.TimesheetProject)
        //                {
        //                    var invoice = new CreateInvoiceDto
        //                    {
        //                        Name = $"Invoice {item.Month}/{item.Year} - {item.ClientName} - Project:[{p.ProjectName}]",
        //                        TimeAt = $"{DateTime.Now}",
        //                        AccountCode = item.ClientCode,
        //                        TotalPrice = 0,
        //                        Status = InvoiceStatus.New,
        //                        Note = null,
        //                        Detail = new List<InvoiceDetailDto>
        //                            {
        //                                new InvoiceDetailDto
        //                                {
        //                                    ProjectName = p.ProjectName,
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
        //    return input;
        //}

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

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_GetAllProjectTimesheetByTimesheet, PermissionNames.PmManager_TimesheetProject_ViewOnlyme, PermissionNames.PmManager_TimesheetProject_ViewOnlyActiveProject)]
        public async Task<List<GetTimesheetDetailDto>> GetAllProjectTimesheetByTimesheet(long timesheetId)
        {
            var viewAll = PermissionChecker.IsGranted(PermissionNames.PmManager_TimesheetProject_GetAllProjectTimesheetByTimesheet);
            var viewonlyme = PermissionChecker.IsGranted(PermissionNames.PmManager_TimesheetProject_ViewOnlyme);
            var viewActiveProject = PermissionChecker.IsGranted(PermissionNames.PmManager_TimesheetProject_ViewOnlyActiveProject);

            var query = from tsp in WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId)
                        join p in WorkScope.GetAll<Project>() on tsp.ProjectId equals p.Id
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
                            PmName = u.FullName,
                            ClientId = c.Id,
                            ClientName = c.Name,
                            File = "/timesheets/" + tsp.FilePath,
                            ProjectBillInfomation = tsp.ProjectBillInfomation,
                            Note = tsp.Note
                        };

            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_Create)]
        public async Task<TimesheetProjectDto> Create(TimesheetProjectDto input)
        {
            var isExist = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId);
            if (isExist)
                throw new UserFriendlyException($"TimesheetProject with ProjectId {input.ProjectId} already exist in Timesheet !");

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<TimesheetProject>(input));

            return input;
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_GetAllRemainProjectInTimesheet)]
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
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_Update)]
        public async Task<TimesheetProjectDto> Update(TimesheetProjectDto input)
        {
            var timeSheetProject = await WorkScope.GetAsync<TimesheetProject>(input.Id);

            var isExist = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.Id != input.Id && (x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId));
            if (isExist)
                throw new UserFriendlyException($"TimesheetProject with ProjectId {input.ProjectId} already exist in Timesheet !");

            ObjectMapper.Map<TimesheetProjectDto, TimesheetProject>(input, timeSheetProject);
            await WorkScope.GetRepo<TimesheetProject, long>().UpdateAsync(timeSheetProject);
            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_Delete)]
        public async Task Delete(long timesheetProjectId)
        {
            var timeSheetProject = await WorkScope.GetAsync<TimesheetProject>(timesheetProjectId);

            if (timeSheetProject.FilePath != null)
            {
                throw new UserFriendlyException("Timesheet already has attachments, cannot be deleted !");
            }

            await WorkScope.DeleteAsync(timeSheetProject);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_UploadFileTimesheetProject)]
        public async Task UpdateFileTimeSheetProject([FromForm] FileInputDto input)
        {
            String path = Path.Combine(_hostingEnvironment.WebRootPath, "timesheets");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var timesheetProject = await WorkScope.GetAll<TimesheetProject>().Include(x => x.Project).Include(x =>x.Timesheet)
                                        .Where(x => x.Id == input.TimesheetProjectId).FirstOrDefaultAsync();

            if (input != null && input.File != null && input.File.Length > 0)
            {
                string fileName = input.File.FileName;
                string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                if (FileExtension == "xlsx" || FileExtension == "xltx" || FileExtension == "docx")
                {
                    var filePath = timesheetProject.Timesheet.Year + "-" + timesheetProject.Timesheet.Month + "_" + timesheetProject.Project.Code + "_" + fileName;
                    if (timesheetProject.FilePath != null && timesheetProject.FilePath != fileName)
                    {
                        File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, "timesheets", timesheetProject.FilePath));

                        timesheetProject.FilePath = null;
                        await WorkScope.UpdateAsync(timesheetProject);
                    }

                    using (var stream = System.IO.File.Create(Path.Combine(_hostingEnvironment.WebRootPath, "timesheets", filePath)))
                    {
                        await input.File.CopyToAsync(stream);
                        timesheetProject.FilePath = filePath;
                        await WorkScope.UpdateAsync(timesheetProject);
                    }
                }
                else
                {
                    throw new UserFriendlyException(String.Format("Only accept files xlsx, xltx, docx !"));
                }
            }
            else
            {
                File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, "timesheets", timesheetProject.FilePath));
                
                timesheetProject.FilePath = null;
                await WorkScope.UpdateAsync(timesheetProject);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFileTimesheetProject([FromQuery] long timesheetProjectId)
        {
            var timesheetProject = await WorkScope.GetAsync<TimesheetProject>(timesheetProjectId);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "timesheets", timesheetProject.FilePath);

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return new FileContentResult(bytes, "application/octet-stream") {
                FileDownloadName = timesheetProject.FilePath
            };
        }
    }
}
