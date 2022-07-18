using Abp;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.APIs.Public.Dto;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Entities;
using ProjectManagement.Services.ResourceManager;
using ProjectManagement.Services.ResourceManager.Dto;
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
        public PublicAppService(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }
        [HttpGet]
        public async Task<GetURIDto> GetConfigUri()
        {
            return new GetURIDto
            {
                TimesheetURI = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetUri),
                GoogleClientAppId = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ClientAppId)
            };
        }
        [HttpGet]
        public List<UserInfo> GetAllUser()
        {
            return WorkScope.GetAll<User>().Select(x => new UserInfo()
            {
                UserType = x.UserType,
                FullName = x.FullName,
                BranchName = x.Branch.Name,
                BranchDisplayName = x.Branch.DisplayName,
                Email = x.EmailAddress,
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
