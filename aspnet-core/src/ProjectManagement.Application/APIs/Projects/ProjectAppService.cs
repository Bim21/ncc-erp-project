using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using NccCore.DataExport;
using NccCore.DataExport.Excel;
using System.Collections.Generic;
using System;
using System.IO;
using ProjectManagement.Net.MimeTypes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace ProjectManagement.APIs.Projects
{
    public class ProjectAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        public async Task<GridResult<ProjectDto>> GetAllPaging(GridParam input)
        {

            //var result1 = WorkScope.GetAll<Project>().ProjectTo<ProjectDto>(null);


            var result = WorkScope.GetAll<Project>().Select(x => new ProjectDto
            {
                Name = x.Name,
                Code = x.Code,
                IsActive = x.IsActive,
                Number = x.Number,
            });
            return await result.GetGridResult(result, input);
        }
        public async Task<ProjectDto> Get(long id)
        {
            var rs = await WorkScope.GetAsync<Project>(id);
            return new ProjectDto
            {
                Id = rs.Id,
                Name = rs.Name,
                Code = rs.Code,
                IsActive = rs.IsActive,
                Number = rs.Number,
            };
        }
        //[HttpPost]
        public async Task<ProjectDto> Update(ProjectDto input)
        {
            await WorkScope.UpdateAsync(ObjectMapper.Map<Project>(input));
            return input;
        }
        public async Task<ProjectDto> Create(ProjectDto input)
        {
            var checkP = WorkScope.GetAll<Project>().Any(x => x.Code == input.Code);
            if (checkP)
            {
                throw new UserFriendlyException("Code project exists.");
            }
            input.Id = await WorkScope.InsertAndGetIdAsync(new Project
            {
                Name = input.Name,
                Code = input.Code,
                IsActive = input.IsActive,
                Number = input.Number,
            });
            return input;
        }
        public async Task Delete(long id)
        {
            await WorkScope.DeleteAsync<Project>(id);
        }

        private readonly IHostingEnvironment _hostingEnvironment;
        public async Task<FileBase64Dto> TestExportProject(List<ProjectDto> input)
        {
            //SecurityValidator.Validate(_ws, MangLuoiInfo.Id, input.AncestorId);
            foreach (var item in input)
            {
                item.SkipCount = 0;
                item.MaxResultCount = int.MaxValue;
            }


            //List<ProjectDto> items = new List<ProjectDto>();
            //items.Add(input);
            var excelExporter = new EpPlusExcelExporter(_hostingEnvironment);

            var fileName = $"THONG_KE_THONG_CHI_{DateTime.Now.Ticks.ToString()}.xlsx";
            var properties = new List<string>() {
                "stt",
                nameof(ProjectDto.Name),
                nameof(ProjectDto.Code),
                nameof(ProjectDto.Number),
                nameof(ProjectDto.IsActive),
            };

            var templateFolderPath = Path.Combine(/*_hostingEnvironment.ContentRootPath*/ "C:\\Ncc\\ncc-erp-project\\aspnet-core\\src\\ProjectManagement.Core\\NccCore\\DataExport", "ExcelTemplates");
            var templateFilePath = Path.Combine(templateFolderPath, "test.xlsx");

            //var headervalues =
            //    (await getbaocaocommondonvi(input.ancestorid))
            //    .union(getbaocaocommonngaythang(input.from, input.to))
            //    .tolist();


            var fileBytes = excelExporter.Export(templateFilePath, input, properties, null, true);
            string fileBase64 = Convert.ToBase64String(fileBytes, 0, fileBytes.Length);

            return new FileBase64Dto
            {
                FileName = fileName,
                FileType = MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet,
                Base64 = fileBase64
            };
        }

    }
}
