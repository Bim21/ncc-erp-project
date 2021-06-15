using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using NccCore.DataExport;
using NccCore.DataExport.Excel;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.Entities;
using ProjectManagement.Net.MimeTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Projects
{
    public class ProjectAppService : ProjectManagementAppServiceBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProjectAppService(IWebHostEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// Get All Paging
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GridResult<ProjectDto>> GetAllPaging(GridParam input)
        {
            var result = (from p in WorkScope.GetAll<Project>()
                          select new ProjectDto
                          {
                              Id = p.Id,
                              name = p.Name,
                              startTime = p.StartTime,
                              endTime = p.EndTime,
                              clientName = p.Client.Name,
                              projectStatus = p.Status,
                              projectType = p.Type,
                              stillCharge = p.StillCharge
                          });
            return await result.GetGridResult(result, input);
        }
        /// <summary>
        /// Get Project By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectDto> Get(long id)
        {
            return await WorkScope.GetAll<Project>().Where(x => x.Id == id).Select(p => new ProjectDto
            {
                Id = p.Id,
                name = p.Name,
                clientName = p.Client.Name,
                endTime = p.EndTime,
                startTime = p.StartTime,
                projectStatus = p.Status,
                projectType = p.Type,
                stillCharge = p.StillCharge
            }).FirstAsync();
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(long id)
        {
            var hasUser = await (from p in WorkScope.GetAll<Project>().Where(p => p.Id == id)
                                 join pu in WorkScope.GetAll<UserProjectResource>()
                                 on p.Id equals pu.ProjectId
                                 select new
                                 {
                                     pu.UserId
                                 }).AnyAsync();

            if (hasUser)
                throw new UserFriendlyException(string.Format("User already existed in Project"));
            await WorkScope.GetRepo<Project>().DeleteAsync(id);
        }

        /// <summary>
        /// Save when create and edit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ProjectDto> Update(ProjectDto input)
        {
            var isExist = await WorkScope.GetAll<Project>().AnyAsync(s => s.Name == input.name && s.Id != input.Id);
            if (isExist)
                throw new UserFriendlyException(string.Format("Project name {0} already existed", input.name));

            var item = await WorkScope.GetAsync<Project>(input.Id);
            ObjectMapper.Map<ProjectDto, Project>(input, item);
            await WorkScope.UpdateAsync(item);

            return input;
        }
        public async Task<ProjectDto> Create(ProjectDto input)
        {
            var checkP = WorkScope.GetAll<Project>().Any(x => x.Name == input.name);
            if (checkP)
            {
                throw new UserFriendlyException("Name project exists.");
            }
            var item = ObjectMapper.Map<Project>(input);
            input.Id = await WorkScope.InsertAndGetIdAsync(item);
            return input;
        }


        //public async Task<FileBase64Dto> TestExportProject(List<ProjectDto> input)
        //{
        //    //SecurityValidator.Validate(_ws, MangLuoiInfo.Id, input.AncestorId);
        //    //foreach (var item in input)
        //    //{
        //    //    item.SkipCount = 0;
        //    //    item.MaxResultCount = int.MaxValue;
        //    //}


        //    //List<ProjectDto> items = new List<ProjectDto>();
        //    //items.Add(input);
        //    var excelExporter = new EpPlusExcelExporter(new HostingEnvironment()
        //    {
        //        ApplicationName = _hostingEnvironment.ApplicationName,
        //        ContentRootFileProvider = _hostingEnvironment.ContentRootFileProvider,
        //        ContentRootPath = _hostingEnvironment.ContentRootPath,
        //        EnvironmentName = _hostingEnvironment.EnvironmentName
        //    });

        //    var fileName = $"THONG_KE_THONG_CHI_{DateTime.Now.Ticks.ToString()}.xlsx";
        //    var properties = new List<string>() {
        //        "stt",
        //        nameof(ProjectDto.name)
        //    };

        //    var templateFolderPath = Path.Combine(/*_hostingEnvironment.ContentRootPath*/ "C:\\Ncc\\ncc-erp-project\\aspnet-core\\src\\ProjectManagement.Core\\NccCore\\DataExport", "ExcelTemplates");
        //    var templateFilePath = Path.Combine(templateFolderPath, "test.xlsx");

        //    //var headervalues =
        //    //    (await getbaocaocommondonvi(input.ancestorid))
        //    //    .union(getbaocaocommonngaythang(input.from, input.to))
        //    //    .tolist();


        //    var fileBytes = excelExporter.Export(templateFilePath, input, properties, null, true);
        //    string fileBase64 = Convert.ToBase64String(fileBytes, 0, fileBytes.Length);

        //    return new FileBase64Dto
        //    {
        //        FileName = fileName,
        //        FileType = MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet,
        //        Base64 = fileBase64
        //    };
        //}
    }
}
