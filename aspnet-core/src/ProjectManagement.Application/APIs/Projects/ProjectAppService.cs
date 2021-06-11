using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        /// <summary>
        /// Get All Paging
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GridResult<ProjectDto>> GetAllPaging(GridParam input)
        {
            var result =(from p in WorkScope.GetAll<Project>() 
                         select new ProjectDto { 
                             Id=p.Id,
                             name=p.Name,
                             startTime=p.StartTime,
                             endTime=p.EndTime,
                             clientName=p.Client.Name,
                             projectStatus=p.Status,
                             projectType=p.Type,
                             stillCharge=p.StillCharge
                         }
                         );
            return await result.GetGridResult(result, input);
        }
        /// <summary>
        /// Get Project By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectDto> Get(long id)
        {
            var rs = from p in WorkScope.GetAll<Project>()
                            where p.Id == id
                            select new ProjectDto
                            {
                                Id = p.Id,
                                name = p.Name,
                                clientName = p.Client.Name,
                                endTime = p.EndTime,
                                startTime = p.StartTime,
                                projectStatus = p.Status,
                                projectType = p.Type,
                                stillCharge = p.StillCharge
                            };

            return  rs.First();
                   
            
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete (long id)
        {
            var hasUser = await(from p in WorkScope.GetAll<Project>().Where(p => p.Id == id)
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
        public async Task<ProjectDto> Save(ProjectDto input)
        {
            var isExist = await WorkScope.GetAll<Project>().AnyAsync(s => s.Name == input.name && s.Id != input.Id);
            if (isExist)
                throw new UserFriendlyException(string.Format("Project name {0} already existed", input.name));

            if (input.Id <= 0) //insert
            {
                var item = ObjectMapper.Map<Project>(input);
                input.Id = await WorkScope.InsertAndGetIdAsync(item);
            }
            else //update
            {
                var item = await WorkScope.GetAsync<Project>(input.Id);
                ObjectMapper.Map<ProjectDto, Project>(input, item);
                await WorkScope.UpdateAsync(item);
            }
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
