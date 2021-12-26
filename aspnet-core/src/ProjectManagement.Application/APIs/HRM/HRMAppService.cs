using Abp.Authorization;
using Abp.Configuration;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NccCore.Uitls;
using ProjectManagement.APIs.HRM.Dto;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants;
using ProjectManagement.NccCore.Helper;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using System;
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
        public async Task<CreateUserHRMDto> CreateUserByHRM(CreateUserHRMDto model)
        {
            if (!CheckSecurityCode())
            {
                throw new UserFriendlyException("SecretCode does not match!");
            }
            var userDto = new User
            {
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname,
                EmailAddress = model.EmailAddress,
                NormalizedEmailAddress = model.EmailAddress.ToUpper(),
                NormalizedUserName = model.UserName.ToUpper(),
                UserType = model.UserType,
                UserLevel = model.UserLevel,
                Branch = model.Branch,
                IsActive = true,
                Password = "123qwe",
                UserCode = model.UserCode
            };
            model.Id = await WorkScope.InsertAndGetIdAsync(userDto);
            var user = await WorkScope.GetAsync<User>(model.Id);
            var userName = UserHelper.GetUserName(user.EmailAddress);
            var message = new StringBuilder();
            message.AppendLine($"Welcome các nhân viên mới vào làm việc tại công ty, đó là **{userName ?? user.UserName}**.\rCác PM hãy nhanh tay pick nhân viên vào dự án ngay nào.");
            await _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = userName ?? user.UserName,
                Message = message.ToString(),
                CreateDate = DateTimeUtils.GetNow(),
            }, ChannelTypeConstant.GENERAL_CHANNEL);
            return model;
        }
        #region API HELPER
        private bool CheckSecurityCode()
        {
            var secretCode = SettingManager.GetSettingValue(AppSettingNames.SecretCode);
            var header = _httpContextAccessor.HttpContext.Request.Headers;
            var securityCodeHeader = header["X-Secret-Key"];
            if (secretCode == securityCodeHeader)
                return true;
            return false;
        }
        #endregion
    }
}
