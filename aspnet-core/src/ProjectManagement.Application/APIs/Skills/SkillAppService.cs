﻿using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.APIs.Skills.Dto;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Skills
{
    public class SkillAppService : ProjectManagementAppServiceBase
    {
        [HttpGet]
        public async Task<List<SkillDto>> GetAll()
        {
            var query = WorkScope.GetAll<Skill>().Select(x => new SkillDto
            {
                Id = x.Id,
                Name = x.Name
            });
            return await query.ToListAsync();
        }

        [HttpPost]
        public async Task<SkillDto> Create(SkillDto input)
        {
            var isExist = await WorkScope.GetAll<Skill>().AnyAsync(x => x.Name == input.Name);
            if (isExist)
                throw new UserFriendlyException("Skill already exist !");

            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<Skill>(input));

            return input;
        }

        [HttpPut]
        public async Task<SkillDto> Update(SkillDto input)
        {
            var skill = await WorkScope.GetAsync<Skill>(input.Id);
            if (skill == null)
                throw new UserFriendlyException("Skill not exist !");

            var isExist = await WorkScope.GetAll<Skill>().AnyAsync(x => x.Id != input.Id && x.Name == input.Name);
            if (isExist)
                throw new UserFriendlyException("Skill already exist !");

            await WorkScope.UpdateAsync(ObjectMapper.Map<SkillDto, Skill>(input, skill));
            return input;
        }

        [HttpDelete]
        public async Task Delete(long skillId)
        {
            var skill = await WorkScope.GetAsync<Skill>(skillId);
            if (skill == null)
                throw new UserFriendlyException("Skill not exist !");

            var hasUserSkill = await WorkScope.GetAll<UserSkill>().AnyAsync(x => x.SkillId == skillId);
            if (hasUserSkill)
                throw new UserFriendlyException("Skill has Userskill  !");

            await WorkScope.DeleteAsync(skill);
        }
    }
}