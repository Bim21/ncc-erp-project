using Abp.Runtime.Session;
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
        private const string pathUrl = "api/services/app/ProjectTool";
        public TalentService(
            IWorkScope workScope, 
            HttpClient httpClient, 
            IConfiguration configuration,
            ILogger<TalentService> logger,
            IAbpSession abpSession
        ) : base(httpClient,configuration,logger,abpSession,serviceName)
        {
            _workScope = workScope;
        }

        public async Task<BaseResponseDto> SendRecruitmentToTalent(SendRecruitmentTalentDto recruitment)
        {
            return await PostAsync<BaseResponseDto>($"{pathUrl}/CreateRequestFromProject", recruitment);
        }
        public async Task CancelRequest(CloseResourceRequestDto input)
        {
            await PostAsync<string>($"{pathUrl}/CancelRequest",input);
        }
        public async Task<List<SubPositionDto>> GetPositions()
        {
            return await GetAsync<List<SubPositionDto>>($"{pathUrl}/GetSubPositions");
        }
        public async Task<List<BranchDto>> GetBranches()
        {
            return await GetAsync<List<BranchDto>>($"{pathUrl}/GetBranches");
        }
    }
}
