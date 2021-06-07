using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Projects.Dto;
using ProjectManagement.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Projects
{
    public class ProjectAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        public async Task<GridResult<ProjectDto>> GetAllPaging(GridParam input)
        {
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
            var rs =  await WorkScope.GetAsync<Project>(id);
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
    }
}
