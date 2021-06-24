using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.APIs.Timesheets.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.TimesheetProjects
{
    public class TimesheetProjectAppService : ProjectManagementAppServiceBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TimesheetProjectAppService(IWebHostEnvironment environment)
        {
             _hostingEnvironment = environment;
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
                            Id = p.Id,
                            TimeSheet = $"T{ts.Month}/{ts.Year}",
                            TimesheetId = p.TimesheetId,
                            ProjectId = p.ProjectId,
                            TimesheetFile = p.TimesheetFile,
                            Note = p.Note
                        };
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_Create)]
        public async Task<TimesheetProjectDto> Create(TimesheetProjectDto input)
        {
            var isExist = await WorkScope.GetAll<TimesheetProject>().AnyAsync(x => x.ProjectId == input.ProjectId && x.TimesheetId == input.TimesheetId);
            if(isExist)
                throw new UserFriendlyException($"TimesheetProject with ProjectId {input.ProjectId} already exist in Timesheet !");

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<TimesheetProject>(input));

            if(input.TimesheetFile != null)
            {
                await UpdateFileTimeSheetProject(new FileInputDto
                {
                    File = input.File,
                    TimesheetProjectId = input.Id
                });
            }

            return input;
        }
        [HttpGet]
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_GetAllRemainProjectInTimesheet)]
        public async Task<List<ProjectDto>> GetAllRemainProjectInTimesheet(long timesheetId)
        {
            var timesheetProjects = WorkScope.GetAll<TimesheetProject>().Where(x => x.TimesheetId == timesheetId).Select(x => x.ProjectId);
            var query = WorkScope.GetAll<Project>().Where(x => x.IsCharge == true && x.Status != ProjectStatus.Closed && !timesheetProjects.Contains(x.Id))
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

            if(input.File != null)
            {
                await UpdateFileTimeSheetProject(new FileInputDto
                {
                    File = input.File,
                    TimesheetProjectId = input.Id
                });
            } else
            {
                if(timeSheetProject.TimesheetFile != null)
                {
                    File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, "timesheets", timeSheetProject.TimesheetFile));
                }
                input.TimesheetFile = null;
            }

            ObjectMapper.Map<TimesheetProjectDto, TimesheetProject>(input, timeSheetProject);
            await WorkScope.GetRepo<TimesheetProject, long>().UpdateAsync(timeSheetProject);

            return input;
        }

        [HttpDelete]
        [AbpAuthorize(PermissionNames.PmManager_TimesheetProject_Delete)]
        public async Task Delete(long timesheetProjectId)
        {
            var timeSheetProject = await WorkScope.GetAsync<TimesheetProject>(timesheetProjectId);

            if(timeSheetProject.TimesheetFile != null)
            {
                throw new UserFriendlyException("Timesheet already has attachments, cannot be deleted !");
            }

            await WorkScope.DeleteAsync(timeSheetProject);
        }

        [HttpPost]
        private async Task UpdateFileTimeSheetProject([FromForm] FileInputDto input)
        {
            String path = Path.Combine(_hostingEnvironment.WebRootPath, "timesheets");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (input != null && input.File != null && input.File.Length > 0)
            {
                string fileName = input.File.FileName;
                string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                if (FileExtension == "xlsx" || FileExtension == "xltx")
                {
                    var timesheetProject = await WorkScope.GetAsync<TimesheetProject>(input.TimesheetProjectId);
                    var filePath = DateTimeOffset.Now.ToUnixTimeMilliseconds() + "_" + fileName;

                    if(timesheetProject.TimesheetFile != null && timesheetProject.TimesheetFile != fileName)
                    {
                        File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, "timesheets", timesheetProject.TimesheetFile));

                        timesheetProject.TimesheetFile = null;
                        await WorkScope.UpdateAsync(timesheetProject);
                    }

                    using (var stream = System.IO.File.Create(Path.Combine(_hostingEnvironment.WebRootPath, "timesheets", filePath)))
                    {
                        await input.File.CopyToAsync(stream);
                        timesheetProject.TimesheetFile = filePath;
                        await WorkScope.UpdateAsync(timesheetProject);
                    }
                }
                else
                {
                    throw new UserFriendlyException(String.Format("Only accept files xlsx,xltx"));
                }
            }
            else
            {
                throw new UserFriendlyException(String.Format("No file upload!"));
            }
        }

    }
}
