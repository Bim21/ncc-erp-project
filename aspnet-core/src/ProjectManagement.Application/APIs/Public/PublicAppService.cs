using Abp;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectManagement.APIs.Public.Dto;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Entities;
using ProjectManagement.Services.ResourceManager;
using ProjectManagement.Services.ResourceManager.Dto;
using ProjectManagement.Services.ResourceService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Public
{
    public class PublicAppService : ProjectManagementAppServiceBase
    {
        ResourceManager resourceManager;
        private readonly IConfiguration _appConfiguration;
        public PublicAppService(ResourceManager resourceManager, IConfiguration appConfiguration)
        {
            this.resourceManager = resourceManager;
            this._appConfiguration = appConfiguration;
        }
        [HttpGet]
        public async Task<GetURIDto> GetConfigUri()
        {
            return new GetURIDto
            {
                TimesheetURI = _appConfiguration.GetValue<string>("TimesheetService:BaseAddress"),
                GoogleClientAppId = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ClientAppId)
            };
        }
        [HttpGet]
        public List<BaseUserInfo> GetAllUser()
        {
            return WorkScope.GetAll<User>().Select(x => new BaseUserInfo()
            {
                UserType = x.UserType,
                FullName = x.FullName,
                BranchName = x.Branch.Name,
                EmailAddress = x.EmailAddress,
                AvatarPath = x.AvatarPath
            }).ToList();
        }
        [HttpGet]
        public List<PMOfUserDto> GetPMOfUser(string email)
        {
            var userId = GetUserIdByEmail(email);

            if (userId == default)
            {
                Logger.Info($"No user by email: {email}");
                return null;
            }

            return resourceManager.QueryPMOfUser(userId);

        }
    }
}
