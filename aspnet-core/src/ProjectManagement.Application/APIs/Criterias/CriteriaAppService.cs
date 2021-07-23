using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.Criterias.Dto;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Criterias
{
    public class CriteriaAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        public async Task<GridResult<SelectCriteriaDto>> GetAllPaging(GridParam input)
        {
            var query = from c in WorkScope.GetAll<Criteria>()
                        join ct in WorkScope.GetAll<CriteriaCategory>() on c.CriteriaCategoryId equals ct.Id
                        select new SelectCriteriaDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Weight = c.Weight,
                            CriteriaCategoryId = ct.Id,
                            CriteriaCategoryName = ct.Name,
                            Note = c.Note
                        };
            return await query.GetGridResult(query, input);
        }
        [HttpPost]
        public async Task<CriteriaDto> Create(CriteriaDto input)
        {
            var isExist = await WorkScope.GetAll<Criteria>().AnyAsync(x => x.Name == input.Name);

            if (isExist)
                throw new UserFriendlyException(String.Format("Name already exist !"));

            await WorkScope.InsertAndGetIdAsync<Criteria>(ObjectMapper.Map<Criteria>(input));
            return input;
        }
        [HttpPut]
        public async Task<CriteriaDto> Update(CriteriaDto input)
        {
            var criteria = await WorkScope.GetAsync<Criteria>(input.Id);

            var isExist = await WorkScope.GetAll<Criteria>().AnyAsync(x => x.Id != input.Id && x.Name == input.Name);
            if (isExist)
                throw new UserFriendlyException(String.Format("Name already exist !"));

            await WorkScope.UpdateAsync(ObjectMapper.Map<CriteriaDto, Criteria>(input, criteria));
            return input;
        }
        [HttpDelete]
        public async Task Delete(long criteriaId)
        {
            var isExist = WorkScope.GetAll<CheckPointUserDetail>().Count(x => x.CriteriaId == criteriaId);
            if (isExist > 0)
                throw new UserFriendlyException(String.Format("Criteria này đang được dùng"));

            await WorkScope.DeleteAsync<Criteria>(criteriaId);
        }

    }
}
