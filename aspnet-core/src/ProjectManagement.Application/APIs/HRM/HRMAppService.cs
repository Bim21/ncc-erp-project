using Abp.Authorization;
using Abp.Configuration;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NccCore.Helper;
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
            var user = new User
            {
                UserName = model.EmailAddress.ToLower(),
                Name = model.Name,
                Surname = model.Surname,
                EmailAddress = model.EmailAddress,
                NormalizedEmailAddress = model.EmailAddress.ToUpper(),
                NormalizedUserName = model.UserName.ToUpper(),
                UserType = model.UserType,
                UserLevel = model.UserLevel,
                Branch = model.Branch,
                IsActive = true,
                Password = RandomPasswordHelper.CreateRandomPassword(8),
                UserCode = model.UserCode
            };
            model.Id = await WorkScope.InsertAndGetIdAsync(user);           
            var userName = UserHelper.GetUserName(user.EmailAddress);
            var message = $"Welcome các nhân viên mới vào làm việc tại công ty, đó là **{userName ?? user.UserName}**.\rCác PM hãy nhanh tay pick nhân viên vào dự án ngay nào.";
            await _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = userName ?? user.UserName,
                Message = message,
                CreateDate = DateTimeUtils.GetNow(),
            }, ChannelTypeConstant.GENERAL_CHANNEL);
            return model;
        }
        #region API HELPER
        private bool CheckSecurityCode()
        {
            var secretCode = SettingManager.GetSettingValue(AppSettingNames.SecurityCode);
            var header = _httpContextAccessor.HttpContext.Request.Headers;
            var securityCodeHeader = header["X-Secret-Key"].ToString();            
            if (secretCode == securityCodeHeader)
                return true;

            Logger.Error("secretCode: " + secretCode.Substring(0, secretCode.Length / 2));
            Logger.Error("securityCodeHeader: " + securityCodeHeader.Substring(0, securityCodeHeader.Length / 2));
            return false;
        }
        #endregion
    }
}
