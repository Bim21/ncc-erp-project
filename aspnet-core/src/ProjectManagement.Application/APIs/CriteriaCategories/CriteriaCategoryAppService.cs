﻿using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.CriteriaCategories.Dto;
using ProjectManagement.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.CriteriaCategories
{
    public class CriteriaCategoryAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        public async Task<GridResult<CriteriaCategoryDto>> GetAll(GridParam input)
        {
            var query = from cc in WorkScope.GetAll<CriteriaCategory>()
                        select new CriteriaCategoryDto
                        {
                            Id=cc.Id,
                            Name=cc.Name,
                        };
            return await query.GetGridResult(query, input);
        }
        [HttpPost]
        public async Task<CriteriaCategoryDto> Create(CriteriaCategoryDto input)
        {
            var isExist = await WorkScope.GetAll<CriteriaCategory>().AnyAsync(x => x.Name == input.Name);

            if (isExist)
                throw new UserFriendlyException(String.Format("Name already exists !"));

            await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<CriteriaCategory>(input));
            return input;
        }
        [HttpPut]
        public async Task<CriteriaCategoryDto> Update(CriteriaCategoryDto input)
        {
            var criteriacategory = await WorkScope.GetAsync<CriteriaCategory>(input.Id);

            var isExist = await WorkScope.GetAll<CriteriaCategory>().AnyAsync(x => x.Id != input.Id && x.Name == input.Name);
            if (isExist)
                throw new UserFriendlyException(String.Format("Name already exist !"));

            await WorkScope.UpdateAsync(ObjectMapper.Map<CriteriaCategoryDto, CriteriaCategory>(input, criteriacategory));
            return input;
        }
        [HttpDelete]
        public async System.Threading.Tasks.Task Delete(long id)
        {
            var isExist = WorkScope.GetAll<Criteria>().Where(x => x.CriteriaCategoryId == id).Count();

            if (isExist>0)
                throw new UserFriendlyException(String.Format("Category này đã có tiêu chí con nên không thể xóa"));

            await WorkScope.DeleteAsync<CriteriaCategory>(id);
        }
    }
}