using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Checklists.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Checklists
{
    public class CheckListItemAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        [AbpAuthorize(PermissionNames.SaoDo_CheckListItem_ViewAll)]
        public async Task<GridResult<CheckListItemDto>> GetAllPaging(GridParam input)
        {
            var listMan = WorkScope.GetAll<CheckListItemMandatory>();
            var query = from i in WorkScope.GetAll<CheckListItem>()
                        select new CheckListItemDto
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Code = i.Code,
                            CategoryId = i.CategoryId,
                            Title = i.CheckListCategory.Name,
                            Description = i.Description,
                            Mandatorys = listMan.Where(x => x.CheckListItemId == i.Id).Select(x => x.ProjectType).ToList(),
                            AuditTarget = i.AuditTarget,
                            PersonInCharge = i.PersonInCharge,
                            Note = i.Note,
                        };
            return await query.GetGridResult(query, input);
        }
        [AbpAuthorize(PermissionNames.SaoDo_CheckListItem_Create)]
        public async Task<CheckListItemDto> Create(CheckListItemDto input)
        {
            var isExist = await WorkScope.GetAll<CheckListItem>().AnyAsync(x => x.Code.Contains(input.Code));
            if (isExist)
            {
                throw new UserFriendlyException("Code '" + input.Code + "' of Checklist Item already existed.");
            }
            var item = ObjectMapper.Map<CheckListItem>(input);
            input.Id = await WorkScope.InsertAndGetIdAsync(item);

            foreach (var i in input.Mandatorys)
            {// insert mandatory
                var itemMan = new CheckListItemMandatory
                {
                    CheckListItemId = input.Id,
                    ProjectType = i,
                };
                await WorkScope.InsertAndGetIdAsync(itemMan);
            }
            return input;
        }
        [AbpAuthorize(PermissionNames.SaoDo_CheckListItem_Update)]
        public async Task<CheckListItemDto> Update(CheckListItemDto input)
        {
            var checkExist = await WorkScope.GetAll<CheckListItem>()
                                .AnyAsync(x => x.Code.ToLower().Contains(input.Code.ToLower()) && x.Id != input.Id);
            if (checkExist)
            {
                throw new UserFriendlyException(string.Format("Code '{0}' of Checklist Item already existed.", input.Code));
            }

            var item = await WorkScope.GetAsync<CheckListItem>(input.Id);
            ObjectMapper.Map(input, item);
            await WorkScope.UpdateAsync(item);

            // delete mandatory not exist in input
            var deleteMan = await WorkScope.GetAll<CheckListItemMandatory>()
                              .Where(x => x.CheckListItemId == item.Id && !input.Mandatorys.Contains(x.ProjectType)).ToListAsync();
            foreach (var i in deleteMan)
            {
                await WorkScope.DeleteAsync(i);
            }
            // insert if not exist mandatory
            foreach (var i in input.Mandatorys)
            {
                var isExist = await WorkScope.GetAll<CheckListItemMandatory>()
                                .AnyAsync(x => x.CheckListItemId == input.Id && x.ProjectType == i);
                if (isExist) continue;
                var itemMan = new CheckListItemMandatory
                {
                    CheckListItemId = input.Id,
                    ProjectType = i,
                };
                itemMan.Id = await WorkScope.InsertAndGetIdAsync(itemMan);
            }
            return input;
        }
        [AbpAuthorize(PermissionNames.SaoDo_CheckListItem_Delete)]
        public async Task Delete(long id)
        {
            var item = await WorkScope.GetAsync<CheckListItem>(id);
            // delete mandatory
            var mandatorys = await WorkScope.GetAll<CheckListItemMandatory>().Where(x => x.CheckListItemId == id).ToListAsync();
            foreach (var i in mandatorys)
            {
                await WorkScope.DeleteAsync(i);
            }
            await WorkScope.DeleteAsync(item);
        }
    }
}
