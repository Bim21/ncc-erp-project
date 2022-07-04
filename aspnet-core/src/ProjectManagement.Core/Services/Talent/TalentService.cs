using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NccCore.IoC;
using Newtonsoft.Json;
using ProjectManagement.Entities;
using ProjectManagement.Services.Talent.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services.Talent
{
    public class TalentService : BaseWebService
    {
        private readonly IWorkScope _workScope;
        private const string serviceName = "TalentService";
        private const string baseUrl = "api/services/app/ProjectTool";
        public TalentService(
            IWorkScope workScope, 
            HttpClient httpClient, 
            IConfiguration configuration,
            ILogger<TalentService> logger
        ) : base(httpClient,configuration,logger,serviceName)
        {
            _workScope = workScope;
        }

        public async Task SendRecruitmentToTalent(RecruitmentTalentDto input)
        {
            var resourceRequest = await _workScope.GetAll<ResourceRequest>()
                .Where(q => q.Id == input.ResourceRequestId)
                .Select(s => new
                {
                    Id = s.Id,
                    Level = s.Level,
                    SkillNames = s.ResourceRequestSkills.Select(s => s.Skill.Name).ToList(),
                    TimeNeed = s.TimeNeed,
                    Priority = s.Priority,
                    Note = s.PMNote, 
                    Url = s.RecruitmentUrl,
                    IsRecruitmentSend = s.IsRecruitmentSend
                }).FirstOrDefaultAsync();

            if (resourceRequest == null) 
                throw new UserFriendlyException("Not Found Resource Request!");

            var recruitment = new SendRecruitmentTalentDto
            {
                BranchId = input.BranchId,
                Level = resourceRequest.Level,
                ResourceRequestId = input.ResourceRequestId,
                PositionId = input.PositionId,
                Quantity = 1,
                Priority = resourceRequest.Priority,
                SkillNames = resourceRequest.SkillNames,
                TimeNeed = resourceRequest.TimeNeed,
                Note = resourceRequest.Note
            };

            var response = await PostAsync<ResponseResult<string>>($"{baseUrl}/CreateRequestFromProject", recruitment);

            var model = await _workScope.GetAsync<ResourceRequest>(input.ResourceRequestId);
            model.IsRecruitmentSend = true;
            model.RecruitmentUrl = response.Result;
            await _workScope.UpdateAsync(model);
        }
        public async Task CancelRequest(long resourceRequestId)
        {
            await GetAsync<ResponseResult<string>>($"{baseUrl}/CancelRequest?resourceRequestId={resourceRequestId}");
        }
        public async Task<List<PositionDto>> GetPositions()
        {
            var response = await GetAsync<ResponseResult<List<PositionDto>>>($"{baseUrl}/GetPositions");
            return response.Result;
        }
        public async Task<List<BranchDto>> GetBranches()
        {
            var response = await GetAsync<ResponseResult<List<BranchDto>>>($"{baseUrl}/GetBranches");
            return response.Result;
        }
    }
}
