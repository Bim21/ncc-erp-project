using Abp.Authorization;
using Abp.UI;
using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NccCore.IoC;
using ProjectManagement.APIs.HRMv2.Dto;
using ProjectManagement.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.HRMv2
{
    public class Hrmv2AppService : ProjectManagementAppServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Hrmv2AppService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [AbpAllowAnonymous]
        [HttpPost]
        public async Task UpdateAvatarFromHrm(UpdateAvatarDto input)
        {
            CheckSecurityCode();
            if (string.IsNullOrEmpty(input.AvatarPath))
            {
                Logger.Error($"user with {input.AvatarPath} is null or empty");
                return;
            }
            var user = await GetUserByEmailAsync(input.EmailAddress);

            if (user == null)
            {
                Logger.Error($"not found user with email {input.EmailAddress}");
                return;
            }

            user.AvatarPath = input.AvatarPath;
            await WorkScope.UpdateAsync(user);
        }
        private void CheckSecurityCode()
        {
            var secretCode = SettingManager.GetSettingValue(AppSettingNames.SecurityCode);
            var header = _httpContextAccessor.HttpContext.Request.Headers;
            var securityCodeHeader = header["X-Secret-Key"].ToString();
            if (secretCode == securityCodeHeader)
                return;

            throw new UserFriendlyException($"SecretCode does not match! {secretCode.Substring(0, secretCode.Length / 2)} != {securityCodeHeader.Substring(0, securityCodeHeader.Length / 2)}");
        }
    }
}
