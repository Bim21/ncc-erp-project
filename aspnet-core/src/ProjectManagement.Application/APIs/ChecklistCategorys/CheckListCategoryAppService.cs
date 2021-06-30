using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.CheckListItems;
using ProjectManagement.APIs.ChecklistTitles.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.ChecklistTitles
{
    public class CheckListCategoryAppService : ProjectManagementAppServiceBase
    {
        private readonly CheckListItemAppService _checkListItem;

        public CheckListCategoryAppService(CheckListItemAppService checkListItem)
        {
            _checkListItem = checkListItem;
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.SaoDo_CheckListCategory_ViewAll)]
        public async Task<GridResult<CheckListCategoryDto>> GetAllPaging(GridParam input)
        {
            var query = WorkScope.GetAll<CheckListCategory>()
                        .Select(x => new CheckListCategoryDto
                        {
                            Id = x.Id,
                            Name = x.Name
                        });
            return await query.GetGridResult(query, input);
        }

        public async Task<List<CheckListCategoryDto>> GetAll()
        {
            return await WorkScope.GetAll<CheckListCategory>()
                        .Select(x => new CheckListCategoryDto
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).ToListAsync();
        }
        [AbpAuthorize(PermissionNames.SaoDo_CheckListCategory_Create)]
        public async Task<CheckListCategoryDto> Create(CheckListCategoryDto input)
        {
            var isExist = await WorkScope.GetAll<CheckListCategory>().AnyAsync(x => x.Name.ToLower().Contains(input.Name));
            if (isExist)
            {
                throw new UserFriendlyException("Name '" + input.Name + "' of Checklist Title already existed.");
            }
            var item = ObjectMapper.Map<CheckListCategory>(input);
            input.Id = await WorkScope.InsertAndGetIdAsync(item);
            return input;
        }
        [AbpAuthorize(PermissionNames.SaoDo_CheckListCategory_Update)]
        public async Task<CheckListCategoryDto> Update(CheckListCategoryDto input)
        {
            var isExist = await WorkScope.GetAll<CheckListCategory>()
                                .AnyAsync(x => x.Name.ToLower().Contains(input.Name) && x.Id != input.Id);
            if (isExist)
            {
                throw new UserFriendlyException("Name '" + input.Name + "' of Checklist Title already existed.");
            }
            var item = await WorkScope.GetAsync<CheckListCategory>(input.Id);
            ObjectMapper.Map(input, item);
            await WorkScope.UpdateAsync(item);
            return input;
        }
        [AbpAuthorize(PermissionNames.SaoDo_CheckListCategory_Delete)]
        public async Task Delete(long id)
        {
            var delItem = await WorkScope.GetAll<CheckListItem>().Where(x => x.CategoryId == id).Select(x => x.Id).ToListAsync();
            foreach (var i in delItem)
            {
                await _checkListItem.Delete(i);
            }
            await WorkScope.DeleteAsync<CheckListCategory>(id);
        }

    }
}
