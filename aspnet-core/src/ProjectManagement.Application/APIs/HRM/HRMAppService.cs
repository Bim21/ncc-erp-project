using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.APIs.HRM.Dto;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants.Enum;
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

        public HRMAppService(IHttpContextAccessor httpContextAccessor, RoleManager roleManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
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
                Gender = input.Gender,
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
