using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using OfficeOpenXml;
using ProjectManagement.APIs.ProjectProcessCriterias.Dto;
using ProjectManagement.APIs.TimesheetProjects.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.Helper;
using ProjectManagement.Net.MimeTypes;
using ProjectManagement.Services.Common;
using ProjectManagement.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static ProjectManagement.APIs.ProjectProcessResults.Dto.GetProjectProcessResultDto;

namespace ProjectManagement.APIs.ProjectProcessCriterias
{
    [AbpAuthorize]
    public class ProjectProcessCriteriaAppService : ProjectManagementAppServiceBase
    {
        private readonly CommonManager _commonManager;

        public ProjectProcessCriteriaAppService(CommonManager commonManager)
        {
            _commonManager = commonManager;
        }

        [HttpPost]
        public async Task<List<GetAllProjectProcessCriteriaDto>> GetAll(InputToGetAllDto input)
        {
            var listPPCs = WorkScope.GetAll<ProjectProcessCriteria>()
            .Select(x => new
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                Project = x.Project,
                PM = x.Project.PM,
                Client = x.Project.Client,
                ProcessCriteriaId = x.ProcessCriteriaId
            })
            .AsEnumerable()
            .GroupBy(x => new { x.ProjectId })
            .Select(x => new GetAllProjectProcessCriteriaDto
            {
                Id = x.FirstOrDefault().Id,
                ProjectId = x.Key.ProjectId,
                ProjectCode = x.FirstOrDefault().Project.Code,
                ProjectName = x.FirstOrDefault().Project.Name,
                ProjectType = CommonUtil.GetProjectTypeString(x.FirstOrDefault().Project.ProjectType),
                PMName = x.FirstOrDefault().Project.PM.FullName,
                ClientName = x.FirstOrDefault().Project.Client.Name,
                ListProcessCriteriaIds = x.Select(pc => pc.ProcessCriteriaId).ToList()
            }).ToList();

            if (input.GetAll())
            {
                return listPPCs;
            }
            if (input.ProjectId != null && input.ProjectId != 0)
            {
                listPPCs = listPPCs.Where(x => x.ProjectId == input.ProjectId).ToList();
            }
            if (input.ProcessCriteriaId != null && input.ProcessCriteriaId != 0)
            {
                listPPCs = listPPCs.Where(x => x.ListProcessCriteriaIds.Contains((long)input.ProcessCriteriaId)).ToList();
            }

            return listPPCs;
        }

        [AbpAuthorize(PermissionNames.Audits_Tailoring)]
        [HttpPost]
        public async Task<GridResult<GetAllPagingProjectProcessCriteriaDto>> GetAllPaging(GridParam input)
        {
            var listPPCs = WorkScope.GetAll<ProjectProcessCriteria>()
                .Select(x => new
                {
                    ProjectInfor = new
                    {
                        ProjectId = x.ProjectId,
                        ProjectName = x.Project.Name,
                        ProjectCode = x.Project.Code,
                        ProjectType = x.Project.ProjectType,
                        ProjectStatus = x.Project.Status,
                        ClientName = x.Project.Client.Name,
                        ClientCode = x.Project.Client.Code,
                        PMName = x.Project.PM.FullName
                    },
                    ProcessCriteriaId = x.ProcessCriteriaId,
                })
                .ToList()
                .GroupBy(x => x.ProjectInfor).Select(
                x => new GetAllPagingProjectProcessCriteriaDto
                {
                    ProjectId = x.Key.ProjectId,
                    ProjectName = x.Key.ProjectName,
                    ProjectCode = x.Key.ProjectCode,
                    ProjectType = x.Key.ProjectType,
                    ProjectStatus = x.Key.ProjectStatus,
                    ClientName = x.Key.PMName,
                    ClientCode = x.Key.ClientCode,
                    PMName = x.Key.ProjectName,
                    CountCriteria = x.Select(pc => pc.ProcessCriteriaId).Count()
                }).AsQueryable();
            return listPPCs.GetGridResultSync(listPPCs, input);
        }

        public async Task<List<ProcessCriteriaOfProjectDto>> GetAllProjectCriteriaByProjectId(long projectId)
        {
            var listPCIds = WorkScope.GetAll<ProjectProcessCriteria>()
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.ProcessCriteriaId)
                .ToList();
            var listParentIds = new List<long>();
            var query = WorkScope.GetAll<ProcessCriteria>();
            var listPCs = query.ToList();
            foreach (var id in listPCIds)
            {
                listParentIds.AddRange(_commonManager.GetAllParentId(id, listPCs));
            }
            var listResults = query.Where(x => listParentIds.Contains(x.Id))
                .Select(x => new ProcessCriteriaOfProjectDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    IsApplicable = x.IsApplicable,
                    IsLeaf = x.IsLeaf,
                    Level = x.Level,
                    ParentId = x.ParentId
                }).ToList();
            return listResults;
        }

        public async Task<List<ProcessCriteriaOfProjectDto>> GetAllProcessCriteriaByProjectId(long projectId)
        {
            var listPCIds = await WorkScope.GetAll<ProjectProcessCriteria>()
              .Where(x => x.ProjectId == projectId)
              .Select(x => x.ProcessCriteriaId)
              .ToListAsync();

            var listPCs = await WorkScope.GetAll<ProcessCriteria>()
                .Where(x => listPCIds.Contains(x.Id))
                .Select(x => new ProcessCriteriaOfProjectDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    IsApplicable = x.IsApplicable,
                    IsLeaf = x.IsLeaf,
                    Level = x.Level,
                    ParentId = x.ParentId
                }).ToListAsync();

            return listPCs;
        }

        [AbpAuthorize(PermissionNames.Audits_Tailoring_Create)]
        public async Task<CreateProjectProcessCriteriaDto> AddMultiCriteriaToMultiProject(CreateProjectProcessCriteriaDto input)
        {
            ValidExistProject(null, input.ProjectIds);
            //GetAll PC is satify to add ( isLeaf and isApplicable)
            var listPCIds = WorkScope.GetAll<ProcessCriteria>()
                .Where(x => x.IsLeaf && x.IsApplicable && x.IsActive)
                .Select(x => x.Id)
                .ToList();
            if (listPCIds == null || listPCIds.Count < 1)
            {
                throw new UserFriendlyException("Can not found any process criteria (IsLeaf, IsActive)");
            }

            // Gán tất cả PC cho tất cả Project
            var listToAdd = input.ProjectIds.SelectMany(prId => listPCIds
            .Select(pcId => new ProjectProcessCriteria { ProjectId = prId, ProcessCriteriaId = pcId, Applicable = ProjectEnum.Applicable.Standard })).ToList();

            // Lấy ra tất cả PC có trong DB
            var listPPCIds = WorkScope.GetAll<ProjectProcessCriteria>()
                .Where(x => input.ProjectIds.Contains(x.ProjectId))
                .Select(x => new { ProjectId = x.ProjectId, ProcessCriteriaId = x.ProcessCriteriaId })
                .Distinct()
                .ToList();

            if (listPPCIds.Count() > 0)
            {
                // Thực thiện check trùng

                listToAdd.RemoveAll(x => listPPCIds.Any(y => y.ProjectId == x.ProjectId && y.ProcessCriteriaId == x.ProcessCriteriaId));
            }
            if (listToAdd == null || listToAdd.Count() < 1)
            {
                throw new UserFriendlyException("Project process criteria already exist");
            }
            await WorkScope.InsertRangeAsync(listToAdd);

            return input;
        }

        // public async Task<UpdateProjectProcessCriteriaDto> AddMultiCriteriaToOneProject(UpdateProjectProcessCriteriaDto input)
        // {
        //     ValidExistProject(input.ProjectId, null);
        //     var listPCIds = WorkScope.GetAll<ProcessCriteria>()
        //      .Where(x => input.ProcessCriteriaIds.Contains(x.Id))
        //      .Select(x => new { x.Id, x.IsLeaf, x.IsActive })
        //      .ToList();

        //     var invalidPCIds = listPCIds
        //         .Where(x => !x.IsLeaf || !x.IsActive)
        //         .Select(x => x.Id)
        //         .ToList();

        //     if (invalidPCIds.Any())
        //     {
        //         throw new UserFriendlyException($"The following ProcessCriteria are invalid (not IsLeaf or not IsActive): {string.Join(",", invalidPCIds)}");
        //     }

        //     var existingPPCs = WorkScope.GetAll<ProjectProcessCriteria>()
        //         .Where(x => x.ProjectId == input.ProjectId)
        //         .Where(x => listPCIds.Select(y => y.Id).Contains(x.ProcessCriteriaId))
        //         .ToList();

        //     var newPPCs = listPCIds
        //         .Where(x => !existingPPCs.Any(y => y.ProcessCriteriaId == x.Id))
        //         .Select(x => new ProjectProcessCriteria
        //         {
        //             ProjectId = input.ProjectId,
        //             ProcessCriteriaId = x.Id
        //         })
        //         .ToList();

        //     if (newPPCs == null || newPPCs.Count() < 1)
        //     {
        //         throw new UserFriendlyException("Project process criteria already exist");
        //     }

        //     await WorkScope.InsertRangeAsync(newPPCs);

        //     return new UpdateProjectProcessCriteriaDto
        //     {
        //         ProjectId = input.ProjectId,
        //         ProcessCriteriaIds = newPPCs.Select(x => x.ProcessCriteriaId).ToList()
        //     };
        // }
        [AbpAuthorize(PermissionNames.Audits_Tailoring_Update_Project_Tailoring)]
        public async Task<UpdateProjectProcessCriteriaDto> AddMultiCriteriaToOneProject(UpdateProjectProcessCriteriaDto input)
        {
            ValidExistProject(input.ProjectId, null);
            var listPCIds = WorkScope.GetAll<ProcessCriteria>()
                .Where(x => input.ProcessCriteriaIds.Contains(x.Id))
                .Where(x => x.IsLeaf && x.IsActive)
                .Select(x => x.Id)
                .ToList();
            if (listPCIds == null || listPCIds.Count() < 1)
            {
                throw new UserFriendlyException("Can not found any process criteria (IsLeaf, IsActive)");
            }

            var listPPCIds = WorkScope.GetAll<ProjectProcessCriteria>()
                .Where(x => x.ProjectId == input.ProjectId)
                .Where(x => listPCIds.Contains(x.ProcessCriteriaId))
                .Select(x => x.ProcessCriteriaId)
                .Distinct()
                .ToList();

            var listToAdd = listPCIds.Where(x => !listPPCIds.Contains(x)).ToList().Select(x => new ProjectProcessCriteria
            {
                ProjectId = input.ProjectId,
                ProcessCriteriaId = x,
                Applicable = ProjectEnum.Applicable.Standard
            }).ToList();

            if (listToAdd == null || listToAdd.Count() < 1)
            {
                throw new UserFriendlyException("Project process criteria already exist");
            }

            await WorkScope.InsertRangeAsync(listToAdd);
            return input;
        }

        public async Task<OneCriteriaToMutilProjectDto> AddSelectedCriteriaToMultiProject(OneCriteriaToMutilProjectDto input)
        {
            var listProjectIdInPPC = WorkScope.GetAll<ProjectProcessCriteria>().Select(x => x.ProjectId).ToList();
            ValidExistProject(null, listProjectIdInPPC);

            var processCriteria = WorkScope.GetAll<ProcessCriteria>()
                .Where(x => x.Id == input.ProcessCriteriaId && x.IsLeaf && x.IsApplicable && x.IsActive)
                .FirstOrDefault();

            if (processCriteria == null)
            {
                throw new UserFriendlyException("Can not found any process criteria (IsLeaf, IsApplicable, IsActive)");
            }

            var existingPPCs = WorkScope.GetAll<ProjectProcessCriteria>()
                .Where(x => x.ProcessCriteriaId == input.ProcessCriteriaId && listProjectIdInPPC.Contains(x.ProjectId))
                .ToList();

            var newPPCs = listProjectIdInPPC
                .Where(projectId => existingPPCs.All(ppc => ppc.ProjectId != projectId))
                .Select(projectId => new ProjectProcessCriteria
                {
                    ProjectId = projectId,
                    ProcessCriteriaId = input.ProcessCriteriaId,
                    Applicable = ProjectEnum.Applicable.Standard
                })
                .ToList();

            if (newPPCs.Count < 1)
            {
                throw new UserFriendlyException("Project process criteria already exist");
            }

            await WorkScope.InsertRangeAsync(newPPCs);

            return input;
        }

        [AbpAuthorize(PermissionNames.Audits_Tailoring_Update_Project_Tailoring)]
        [HttpPut]
        public async Task<DeleteCriteriaDto> DeleteCriteria(DeleteCriteriaDto input)
        {
            ValidExistProject(input.ProjectId, null);
            var listIds = WorkScope.GetAll<ProjectProcessCriteria>()
                .Where(x => x.ProjectId == input.ProjectId)
                .Where(x => input.ProcessCriteriaIds.Contains(x.ProcessCriteriaId))
                .Select(x => x.Id)
                .ToList();

            await DeleteProjectProcessCriteria(listIds);
            return input;
        }

        [AbpAuthorize(PermissionNames.Audits_Tailoring_Detail_Detele)]
        [HttpDelete]
        public async Task DeleteProjectProcessCriteria(long id)
        {
            await WorkScope.DeleteAsync<ProjectProcessCriteria>(id);
        }

        [AbpAuthorize(PermissionNames.Audits_Tailoring_Delete)]
        [HttpDelete]
        public async Task<long> DeleteProject(long projectId)
        {
            ValidExistProject(projectId, null);
            var listIds = WorkScope.GetAll<ProjectProcessCriteria>()
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.Id)
                .ToList();
            await DeleteProjectProcessCriteria(listIds);
            return projectId;
        }

        private async Task DeleteProjectProcessCriteria(List<long> listIds)
        {
            if (listIds == null || listIds.Count() < 1)
            {
                throw new UserFriendlyException($"Can not found any project process criteria");
            }
            if (listIds.Count() == 1)
            {
                await WorkScope.DeleteAsync<ProjectProcessCriteria>(listIds[0]);
            }
            else
            {
                foreach (var id in listIds)
                {
                    await WorkScope.DeleteAsync<ProjectProcessCriteria>(id);
                }
            }
        }

        private void ValidExistProject(long? projectId, List<long>? listProjectIds)
        {
            var project = WorkScope.GetAll<Project>()
                .Where(x => (projectId.HasValue && x.Id == projectId) || (listProjectIds != null && listProjectIds.Count() > 0 && listProjectIds.Contains(x.Id)))
                .FirstOrDefault();
            if (project == default)
            {
                throw new UserFriendlyException($"Can not found any project");
            }
        }

        public List<GetAllProjectToAddDto> GetAllProjectToAddPPC(InputToGetAllProjectToAddDto input)
        {
            var listPPCProjectIds = WorkScope.GetAll<ProjectProcessCriteria>()
                .Select(x => x.ProjectId)
                .ToList();
            var listProjects = WorkScope.GetAll<Project>()
                .Where(x => !listPPCProjectIds.Contains(x.Id))
                .Select(x => new GetAllProjectToAddDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Type = x.ProjectType,
                    PMName = x.PM.FullName,
                    ClientName = x.Client.Name
                }).ToList();

            return listProjects;
        }

        public async Task<List<GetAllPagingProjectProcessCriteriaDto>> GetProjectHaveNotBeenTailor()
        {
            var listExsit = WorkScope.GetAll<ProjectProcessCriteria>()
                .GroupBy(x => x.ProjectId).Select(x => x.Key).ToList();
            return await WorkScope.GetAll<Project>()
                .Where(x => !listExsit.Contains(x.Id)
                    && x.ProjectType != ProjectEnum.ProjectType.TRAINING
                    && x.Status != ProjectEnum.ProjectStatus.Closed)
                .Select(x => new GetAllPagingProjectProcessCriteriaDto
                {
                    ProjectId = x.Id,
                    ProjectName = x.Name,
                    ProjectCode = x.Code,
                    ProjectType = x.ProjectType,
                    ClientName = x.Client.Name,
                    ClientCode = x.Client.Code,
                    ProjectStatus = x.Status,
                    PMName = x.PM.FullName,
                }).ToListAsync();
        }

        [AbpAuthorize(PermissionNames.Audits_Tailoring_Import)]
        [HttpPost]
        public async Task<long> ImportProjectProcessCriteriaFromExcel([FromForm] ImportProjecProcessCriteriaDto input)
        {
            if (input.File == null || !Path.GetExtension(input.File.FileName).Equals(".xlsx"))
            {
                throw new UserFriendlyException("File null or is not .xlsx file");
            }

            var isExist = await WorkScope.GetAll<ProjectProcessCriteria>().AnyAsync(x => x.ProjectId == input.ProjectId);
            if (isExist)
            {
                throw new UserFriendlyException("Project had aready exist Tailoring!");
            }

            using (var stream = new MemoryStream())
            {
                input.File.CopyTo(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var mapCodeToId = await WorkScope.GetAll<ProcessCriteria>()
                        .Where(x => x.IsLeaf && x.IsActive)
                        .ToDictionaryAsync(x => x.Code.Trim(), y => y.Id);

                    var rowCount = worksheet.Dimension.End.Row;
                    if (rowCount < 2)
                    {
                        throw new UserFriendlyException("Can not found data on this file");
                    }

                    var listToAdd = new List<ProjectProcessCriteria>();
                    var listWarning = new List<ResponseFailDto>();
                    var listCriteriaIds = new List<long>();
                    for (int row = 2; row < rowCount; row++)
                    {
                        var code = worksheet.Cells[row, 1].Value.ToString().Trim();
                        var applicable = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString() : "";
                        var pmNote = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value.ToString() : "";

                        if (mapCodeToId.ContainsKey(code) && !string.IsNullOrEmpty(applicable) && (applicable == "Standard" || applicable == "Modify"))
                        {
                            var criteriaId = mapCodeToId[code];

                            listToAdd.Add(new ProjectProcessCriteria
                            {
                                Note = pmNote,
                                ProcessCriteriaId = criteriaId,
                                ProjectId = input.ProjectId,
                                Applicable = CommonUtil.GetPPCApplicable(applicable)
                            });
                        }
                    }
                    if (listToAdd.Count > 0)
                    {
                        await WorkScope.InsertRangeAsync(listToAdd);
                    }
                    return listToAdd.Count;
                }
            }
        }

        [AbpAuthorize(PermissionNames.Audits_Tailoring_Detail)]
        [HttpPost]
        public async Task<TreeProjectProcessCriteriaDto> GetDetail(InputToGetDetail input)
        {
            var listLCs = WorkScope.GetAll<ProcessCriteria>().ToList();
            var dicCriteriaId = WorkScope.GetAll<ProjectProcessCriteria>()
                .Where(x => x.ProjectId == input.ProjectId)
                .Select(x => new { x.ProcessCriteriaId, x.Note, x.Id, x.Applicable })
                .ToDictionary(x => x.ProcessCriteriaId, y => new { y.Note, y.Id, y.Applicable });
            var listCriteria = listLCs
                .Select(x => new GetProjectProcessCriteriaTreeDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    GuidLine = x.GuidLine,
                    IsActive = x.IsActive,
                    IsApplicable = x.IsApplicable,
                    IsLeaf = x.IsLeaf,
                    Level = x.Level,
                    Name = x.Name,
                    Note = dicCriteriaId.ContainsKey(x.Id) ? dicCriteriaId[x.Id].Note : "",
                    ParentId = x.ParentId,
                    ProjectProcessCriteriaId = dicCriteriaId.ContainsKey(x.Id) ? dicCriteriaId[x.Id].Id : 0,
                    Applicable = dicCriteriaId.ContainsKey(x.Id) ? dicCriteriaId[x.Id].Applicable : 0
                }).ToList();
            var resultAll = new List<long>();
            foreach (var id in dicCriteriaId.Select(x => x.Key))
            {
                resultAll.AddRange(_commonManager.GetAllParentId(id, listLCs));
            }
            resultAll = resultAll.Distinct().ToList();
            var listAllContain = listCriteria.Where(x => resultAll.Contains(x.Id)).ToList();
            var listLCsContain = listLCs.Where(x => resultAll.Contains(x.Id)).ToList();
            if (input.GetAll())
            {
                return new TreeProjectProcessCriteriaDto
                {
                    Childrens = listAllContain.GenerateTree(c => c.Id, c => c.ParentId)
                };
            }
            var listIdsFilter = listAllContain
                .Where(x => (x.Name.ToLower().Contains(input.SearchText.ToLower())) || (x.Code.ToLower().Contains(input.SearchText.ToLower())))
                .WhereIf(input.Applicable >= ProjectEnum.Applicable.Standard, x => x.Applicable == input.Applicable)
                .Select(x => x.Id)
                .ToList();
            var resultFilter = new List<long>();
            listIdsFilter.ForEach(x =>
            {
                resultFilter.AddRange(_commonManager.GetAllNodeAndLeafIdById(x, listLCsContain, true));
            });
            resultFilter = resultFilter.Distinct().ToList();
            return new TreeProjectProcessCriteriaDto
            {
                Childrens = listAllContain.Where(x => resultFilter.Contains(x.Id)).GenerateTree(c => c.Id, c => c.ParentId)
            };
        }

        [AbpAuthorize(PermissionNames.Audits_Tailoring_DownLoadTemplate)]
        [HttpPost]
        public async Task<FileBase64Dto> DownloadTemplate()
        {
            var data = WorkScope.GetAll<ProcessCriteria>()
                .Where(x => x.IsActive).AsEnumerable()
                .Select(y => new
                {
                    Code = y.Code,
                    Name = y.Name,
                    IsLeaf = y.IsLeaf,
                    QAExample = y.QAExample,
                    GuildLine = y.GuidLine,
                    Level = y.Level,
                    ParentId = y.ParentId
                }).OrderBy(x => x.Code).ThenBy(x => x.Level).ToList();
            using (var wb = new ExcelPackage())
            {
                var applicable = new List<string>() { "Standard", "Modify", "Not Yet" };
                var sheetAudit = wb.Workbook.Worksheets.Add("Audit");
                sheetAudit.Cells.Style.Font.Name = "Arial";
                sheetAudit.Cells.Style.Font.Size = 10;
                sheetAudit.Cells["A1:G1"].Style.Font.Bold = true;
                sheetAudit.Cells["A1:G1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheetAudit.Cells["A1:G1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheetAudit.Cells["A1:G1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                sheetAudit.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(112, 173, 71));
                sheetAudit.Cells["A1:G1"].Style.Font.Color.SetColor(Color.White);
                sheetAudit.Cells["A1"].Value = "No";
                sheetAudit.Cells["B1"].Value = "Criteria";
                sheetAudit.Cells["C1"].Value = "Applicable?";
                sheetAudit.Cells["D1"].Value = "Comment";
                sheetAudit.Cells["E1"].Value = "GuideLine";
                sheetAudit.Cells["F1"].Value = "Q&A Examples";
                var startAudit = sheetAudit.Cells["A2"].Start.Row;

                foreach (var i in data)
                {
                    sheetAudit.Cells[$"A{startAudit}"].Value = i.Code;
                    sheetAudit.Cells[$"B{startAudit}"].Value = i.Level > 1 ? new string('-', i.Level) + i.Name : i.Name;
                    sheetAudit.Cells[$"E{startAudit}"].Value = CommonUtil.ConvertHtmlToPlainText(i.GuildLine);
                    sheetAudit.Cells[$"F{startAudit}"].Value = CommonUtil.ConvertHtmlToPlainText(i.QAExample);

                    if (!i.IsLeaf)
                    {
                        sheetAudit.Cells[$"A{startAudit}:F{startAudit}"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        sheetAudit.Cells[$"A{startAudit}:F{startAudit}"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(218, 241, 243));
                    }
                    if (!i.ParentId.HasValue && !i.IsLeaf)
                    {
                        sheetAudit.Cells[$"A{startAudit}:F{startAudit}"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        sheetAudit.Cells[$"A{startAudit}:F{startAudit}"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(155, 194, 230));
                    }
                    if (i.IsLeaf)
                    {
                        var unitmeasure = sheetAudit.DataValidations.AddListValidation($"C{startAudit}");
                        foreach (var itemApplicable in applicable)
                        {
                            unitmeasure.Formula.Values.Add(itemApplicable);
                        }
                    }
                    if (!i.IsLeaf)
                    {
                        sheetAudit.Cells[$"A{startAudit}"].Style.Font.Bold = true;
                        sheetAudit.Cells[$"B{startAudit}"].Style.Font.Bold = true;
                    }
                    startAudit++;
                }

                sheetAudit.Cells[$"A2:B{startAudit}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheetAudit.Cells[$"A2:B{startAudit}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheetAudit.Cells[$"D2:F{startAudit}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheetAudit.Cells[$"D2:F{startAudit}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheetAudit.Cells.AutoFitColumns();
                sheetAudit.Column(4).Width = 60;
                sheetAudit.Cells.Style.WrapText = true;
                return new FileBase64Dto
                {
                    FileName = $"Import Tailoring Template.xlsx",
                    FileType = MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet,
                    Base64 = Convert.ToBase64String(wb.GetAsByteArray())
                };
            }
        }

        [AbpAuthorize(PermissionNames.Audits_Tailoring_Detail_Update)]
        [HttpPost]
        public async Task UpdateProjectProcessCriteria(UpdateNoteApplicableDto input)
        {
            var item = await WorkScope.GetAsync<ProjectProcessCriteria>(input.Id);
            item.Note = input.Note;
            item.Applicable = input.Applicable;
            await WorkScope.UpdateAsync(item);
        }
    }
}