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
                throw new UserFriendlyException("SecretKey does not match!");
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

            var userRole = new UserRole
            {
                UserId = input.Id,
                RoleId = roleEmployee.Id
            };
            await WorkScope.InsertAndGetIdAsync(userRole);
                //var login = new LoginDto
                //{
                //    password = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.PasswordBot),
                //    user = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.UserBot)
                //};
                //var response = await _komuService.Login(login);
                //if (response.IsSuccessStatusCode)
                //{
                //    var responseContent = await response.Content.ReadAsStringAsync();
                //    var DecryptContent = JsonConvert.DeserializeObject<LoginJsonPrase>(responseContent);
                //    var projectUri = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri);
                //    var room = await _settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuRoom);
                //    var message = $"Welcome các nhân viên mới vào làm việc tại công ty, đó là {input.Surname+" "+ input.Name}. Các PM hãy nhanh tay pick nhân viên vào dự án ngay nào. ";
                //    var alias = "Nhắc việc NCC";
                //    var postMessage = new PostMessage
                //    {
                //        channel = room,
                //        text = message.ToString(),
                //        alias = alias
                //    };
                //    await _komuService.PostMessage(postMessage, DecryptContent.data);

                //    await _komuService.Logout(DecryptContent.data);
                //}
            return input;
        }

        private bool CheckSecurityCode()
        {
            var secretCode = SettingManager.GetSettingValue(AppSettingNames.SecretCode);
            var header = _httpContextAccessor.HttpContext.Request.Headers;
            var securityCodeHeader = header["X-Secret-Key"];
            if (secretCode == securityCodeHeader)
                return true;
            return false;
        }
    }
}
