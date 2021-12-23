using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagement.APIs.HRM.Dto;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.HRM
{
    public class HRMAppService : ProjectManagementAppServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager _roleManager;
        private ISettingManager _settingManager;
        private KomuService _komuService;
        public HRMAppService(IHttpContextAccessor httpContextAccessor, RoleManager roleManager, KomuService komuService,
            ISettingManager settingManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _komuService = komuService;
            _settingManager = settingManager;
        }

        [AbpAllowAnonymous]
        [HttpPost]
        public async Task<CreateUserHRMDto> CreateUserByHRM(CreateUserHRMDto input)
        {
            if (!CheckSecurityCode())
            {
                throw new UserFriendlyException("SecretCode does not match!");
            }

            var roleEmployee = _roleManager.GetRoleByName(RoleConstants.ROLE_EMPLOYEE);

            var user = new User
            {
                UserName = input.UserName,
                Name = input.Name,
                Surname = input.Surname,
                EmailAddress = input.EmailAddress,
                NormalizedEmailAddress = input.EmailAddress.ToUpper(),
                NormalizedUserName = input.UserName.ToUpper(),
                UserType = input.UserType,
                UserLevel = input.UserLevel,
                Branch = input.Branch,
                IsActive = true,
                Password = "123qwe",
                UserCode = input.UserCode
            };
            input.Id = await WorkScope.InsertAndGetIdAsync(user);
            var alias = "Nhắc việc NCC";
            var message = new StringBuilder();
            message.AppendLine(alias);
            message.AppendLine($"Welcome các nhân viên mới vào làm việc tại công ty, đó là {input.Surname + " " + input.Name}. Các PM hãy nhanh tay pick nhân viên vào dự án ngay nào.");
            await _komuService.NotifyPMChannel(new KomuMessage
            {
                Message = message.ToString(),
                CreateDate = DateTime.Now,
            });
            return input;
        }

        private bool CheckSecurityCode()
        {
            var secretCode = SettingManager.GetSettingValue(AppSettingNames.SecurityCode);
            var header = _httpContextAccessor.HttpContext.Request.Headers;
            var securityCodeHeader = header["X-Secret-Key"];
            if (secretCode == securityCodeHeader)
                return true;
            return false;
        }
    }
}
