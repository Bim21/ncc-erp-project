using Abp.Authorization;
using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Mvc;
using NccCore.IoC;
using ProjectManagement.APIs.HRMv2.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.HRMv2
{
    public class Hrmv2AppService : ProjectManagementAppServiceBase
    {
        [AbpAllowAnonymous]
        [HttpPost]
        public async Task UpdateAvatarFromHrm(UpdateAvatarDto input)
        {
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
    }
}
