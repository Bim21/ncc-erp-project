﻿using Abp.Authorization;
using Abp.UI;
using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Helper;
using NccCore.IoC;
using NccCore.Uitls;
using ProjectManagement.APIs.HRMv2.Dto;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants;
using ProjectManagement.Entities;
using ProjectManagement.NccCore.Helper;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using ProjectManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.HRMv2
{
    public class Hrmv2AppService : ProjectManagementAppServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private KomuService _komuService;
        public Hrmv2AppService(IHttpContextAccessor httpContextAccessor, KomuService komuService)
        {
            _httpContextAccessor = httpContextAccessor;
            _komuService = komuService;
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
        private async Task<ProjectManagement.Entities.Branch> GetBranchByCode(string code)
        {
            var branch = await WorkScope.GetAll<ProjectManagement.Entities.Branch>().Where(s => s.Code == code).FirstOrDefaultAsync();
            if (branch == default)
            {
                branch = await WorkScope.GetAll<ProjectManagement.Entities.Branch>().FirstOrDefaultAsync();
            }
            return branch;
        }
        private long GetPositionIdByCode(string code)
        {
            var positionDevId = WorkScope.GetAll<Position>()
                                         .Where(s => s.Code.ToLower().Trim() == "dev")
                                         .Select(s => s.Id)
                                        .FirstOrDefault();
            if (code == null)
                return positionDevId;

            var positionId = WorkScope.GetAll<Position>()
                                      .Where(s => s.Code.ToLower().Trim() == code.ToLower().Trim())
                                      .Select(s => s.Id)
                                      .FirstOrDefault();

            if (positionId == default && positionDevId != default)
                return positionDevId;
            return positionId;
        }

        [AbpAllowAnonymous]
        [HttpPost]
        public async Task<User> CreateUserByHRM(CreateUpdateUserFromHRMV2Dto model)
        {
            CheckSecurityCode();
            var existUser = WorkScope.GetAll<User>()
                .Where(x => x.EmailAddress.ToLower().Trim() == model.EmailAddress.ToLower().Trim());

            if (existUser.Any())
            {
                throw new UserFriendlyException($"failed to create user from HRM, user with email {model.EmailAddress} is already exist");
            }
            var branch = await GetBranchByCode(model.BranchCode);
            var positionId = GetPositionIdByCode(model.PositionCode);
            var user = new User
            {
                UserName = model.EmailAddress.ToLower(),
                Name = model.Name,
                Surname = model.Surname,
                EmailAddress = model.EmailAddress,
                NormalizedEmailAddress = model.EmailAddress.ToUpper(),
                NormalizedUserName = model.EmailAddress.Replace("@ncc.asia", "").ToLower(),
                UserType = model.Type,
                UserLevel = model.Level,
                IsActive = true,
                Password = RandomPasswordHelper.CreateRandomPassword(8),
                BranchId = branch.Id,
                PositionId = positionId,
            };
            await WorkScope.InsertAsync(user);
            var userName = UserHelper.GetUserName(user.EmailAddress);
            var messageToGeneral = $"Welcome **{userName}** [{branch.DisplayName}] to **NCC**";


            _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = userName,
                Message = messageToGeneral,
                CreateDate = DateTimeUtils.GetNow(),
            }, ChannelTypeConstant.GENERAL_CHANNEL);

            var messageToPM = $"HR has onboarded: **{userName}** [{branch.DisplayName}](**{CommonUtil.UserLevelName(user.UserLevel)}**)";

            _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = userName,
                Message = messageToPM,
                CreateDate = DateTimeUtils.GetNow(),
            }, ChannelTypeConstant.PM_CHANNEL);

            return user;
        }

        [AbpAllowAnonymous]
        [HttpPost]
        public async Task UpdateUserFromHRM(CreateUpdateUserFromHRMV2Dto input)
        {
            CheckSecurityCode();
            var user = await WorkScope.GetAll<User>()
                .Where(x => x.EmailAddress.ToLower().Trim() == input.EmailAddress.ToLower().Trim())
                .FirstOrDefaultAsync();
            var positionId = GetPositionIdByCode(input.PositionCode);
            if (user != null)
            {
                var branch = await GetBranchByCode(input.BranchCode);
                user.UserName = input.EmailAddress;
                user.Name = input.Name;
                user.Surname = input.Surname;
                user.EmailAddress = input.EmailAddress;
                user.UserType = input.Type;
                user.UserLevel = input.Level;
                user.BranchId = branch.Id;
                user.PositionId = positionId;
            }
            await WorkScope.UpdateAsync(user);
        }
    }
}
