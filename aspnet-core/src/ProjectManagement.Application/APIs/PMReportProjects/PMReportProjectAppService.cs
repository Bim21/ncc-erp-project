using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.PMReportProjects.Dto;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.PMReportProjects
{
    public class PMReportProjectAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        public async Task<GridResult<GetPMReportProjectDto>> GetAllPaging(GridParam input)
        {
            var query = WorkScope.GetAll<PMReportProject>().Select(x => new GetPMReportProjectDto
            {
                Id = x.Id,
                PMReportId = x.PMReportId,
                PMReportName = x.PMReport.Name,
                ProjectId = x.ProjectId,
                ProjectName = x.Project.Name,
                Status = x.Status,
                ProjectHealth = x.ProjectHealth,
                PMId = x.PMId,
                PmName = x.PM.Name,
                Note = x.Note
            });
            return await query.GetGridResult(query, input);
        }

        [HttpPost]
        public async Task<PMReportProjectDto> Create(PMReportProjectDto input)
        {
            var isExist = await WorkScope.GetAll<PMReportProject>().AnyAsync(x => x.PMReportId == input.PMReportId && x.ProjectId == input.ProjectId);
            if (isExist)
                throw new UserFriendlyException("PMReportProject already exist !");

            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<PMReportProject>(input));
            return input;
        }

        [HttpPut]
        public async Task<PMReportProjectDto> Update(PMReportProjectDto input)
        {
            var pmReportProject = await WorkScope.GetAsync<PMReportProject>(input.Id);

            var isExist = await WorkScope.GetAll<PMReportProject>().AnyAsync(x => x.Id != input.Id && x.PMReportId == input.PMReportId && x.ProjectId == input.ProjectId);
            if (isExist)
                throw new UserFriendlyException("PMReportProject already exist !");

            await WorkScope.UpdateAsync(ObjectMapper.Map<PMReportProjectDto, PMReportProject>(input, pmReportProject));
            return input;
        }

        [HttpDelete]
        public async Task Delete(long pmPeportProjectId)
        {
            var pmReportProject = await WorkScope.GetAsync<PMReportProject>(pmPeportProjectId);

            await WorkScope.DeleteAsync(pmReportProject);
        }
    }
}
