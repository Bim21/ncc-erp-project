using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Services.Talent;
using ProjectManagement.Services.Talent.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Talent
{
    [AbpAuthorize]
    public class TalentAppService : ProjectManagementAppServiceBase
    {
        private readonly TalentService _talentService;
        public TalentAppService(TalentService talentService)
        {
            _talentService = talentService;
        }
        [HttpPost]
        public async Task<IActionResult> SendRecruitmentToTalent(RecruitmentTalentDto input)
        {
            await _talentService.SendRecruitmentToTalent(input);
            return new OkObjectResult(input);
        }
        [HttpGet]
        public async Task<List<PositionDto>> GetPositions()
        {
            return await _talentService.GetPositions();
        }
        [HttpGet]
        public async Task<List<BranchDto>> GetBranches()
        {
            return await _talentService.GetBranches();
        }
    }
}
