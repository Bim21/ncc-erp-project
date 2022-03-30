using Abp;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.APIs.Public.Dto;
using ProjectManagement.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.Public
{
    public class PublicAppService : ProjectManagementAppServiceBase
    {
        [HttpGet]
        public async Task<GetURIDto> GetConfigUri()
        {
            return new GetURIDto
            {
                TimesheetURI = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetUri)
            };
        }
    }
}
