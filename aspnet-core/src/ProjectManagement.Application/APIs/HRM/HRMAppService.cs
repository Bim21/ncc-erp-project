using Abp.Authorization;
using Abp.Configuration;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NccCore.Helper;
using NccCore.IoC;
using NccCore.Uitls;
using ProjectManagement.APIs.HRM.Dto;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants;
using ProjectManagement.Entities;
using ProjectManagement.NccCore.Helper;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;
using ProjectManagement.Services.ResourceManager;
using ProjectManagement.Utils;
using ProjectManagement.Services.ResourceService.Dto;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.APIs.HRM
{
    public class HRMAppService : ProjectManagementAppServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private KomuService _komuService;
        private readonly ResourceManager _resourceManager;

        public HRMAppService(IHttpContextAccessor httpContextAccessor, RoleManager roleManager, KomuService komuService,
            ResourceManager resourceManager,
            ISettingManager settingManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _komuService = komuService;
            _resourceManager = resourceManager;
        }

        [AbpAllowAnonymous]
        [HttpPost]
        public async Task<CreateUserHRMDto> CreateUserByHRM(CreateUserHRMDto model)
        {
            CheckSecurityCode();
            var existUser =  WorkScope.GetAll<User>()
                .Where(x => x.EmailAddress.ToLower().Trim() == model.EmailAddress.ToLower().Trim());

            if (existUser.Any())
            {
                throw new UserFriendlyException($"failed to create user from HRM, user with email {model.EmailAddress} is already exist");
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
            var messageToGeneral = $"Welcome **{userName}** [{CommonUtil.BranchName(user.Branch)}] to **NCC**"; 
            
           
            _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = userName,
                Message = messageToGeneral,
                CreateDate = DateTimeUtils.GetNow(),
            }, ChannelTypeConstant.GENERAL_CHANNEL);

            var messageToPM = $"@here HR has onboarded: **{userName}** [{CommonUtil.BranchName(user.Branch)}](**{CommonUtil.UserLevelName(user.UserLevel)}**)";

            _komuService.NotifyToChannel(new KomuMessage
            {
                UserName = userName ,
                Message = messageToPM,
                CreateDate = DateTimeUtils.GetNow(),
            }, ChannelTypeConstant.PM_CHANNEL);

            return model;
        }

        [AbpAllowAnonymous]
        [HttpPost]
        public async Task UpdateUserFromHRM(UpdateUserFromHRMDto input)
        {
            CheckSecurityCode();
            var user = await WorkScope.GetAll<User>()
                .Where(x => x.EmailAddress.ToLower().Trim() == input.EmailAddress.ToLower().Trim())
                .FirstOrDefaultAsync();
            if (user != null)
            {
                user.UserName = input.EmailAddress;
                user.Name = input.Name;
                user.Surname = input.Surname;
                user.EmailAddress = input.EmailAddress;
                user.UserType = input.UserType;
                user.UserLevel = input.UserLevel;
                user.Branch = input.Branch;
            }
            await WorkScope.UpdateAsync(user);
        }

        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<string> PlanUserQuitJob(PlanAndConfirmUserDto input)
        {
            CheckSecurityCode();
            await PlanUserFromHRMToProject(input, AppConsts.CHO_NGHI_PROJECT_CODE);
            return "Successful plan user to project CHO_NGHI";
        }

        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<string> ConfirmUserQuitJob(PlanAndConfirmUserDto input)
        {
            CheckSecurityCode();
            var user = WorkScope.GetAll<User>()
               .Where(x => x.EmailAddress.ToLower().Trim() == input.Email.ToLower().Trim())
               .FirstOrDefault();

            if (user == default)
            {
                return "PROJECT tool: Not found user with email " + input.Email;
            }

            var currentPlanForUser = WorkScope.GetAll<ProjectUser>()
              .Where(x => x.User.EmailAddress.ToLower().Trim() == input.Email.ToLower().Trim())
              .Where(x => x.Status == ProjectUserStatus.Future)
              .Where(x => x.Project.Code == AppConsts.CHO_NGHI_PROJECT_CODE)
              .FirstOrDefault();

            var ChoNghiproject = await WorkScope.GetAll<Project>()
                .Where(x => x.Code == AppConsts.CHO_NGHI_PROJECT_CODE)
                .FirstOrDefaultAsync();
            var activeReportId = await _resourceManager.GetActiveReportId();

            if (currentPlanForUser != default)
            {
                currentPlanForUser.Status = ProjectUserStatus.Present;
                await WorkScope.UpdateAsync(currentPlanForUser);
            }
            else
            {
                var newPU = new ProjectUser
                {
                    IsPool = false,
                    UserId = user.Id,
                    ProjectId = ChoNghiproject.Id,
                    Status = ProjectUserStatus.Present,
                    AllocatePercentage = 100,
                    StartTime = input.StartTime,
                    PMReportId = activeReportId
                };
                await WorkScope.InsertAsync(newPU);
            }

            var employee = new KomuUserInfoDto
            {
                FullName = user.FullName,
                UserId = user.Id,
                KomuUserId = user.KomuUserId,
                UserName = user.UserName
            };
            var sb = await _resourceManager.ReleaseUserFromAllWorkingProjectsByHRM(employee, activeReportId, "Nghỉ việc từ HRM Tool");

            user.IsActive = false;
            await WorkScope.UpdateAsync(user);

            sb.AppendLine($"{employee.KomuAccountInfo} quited job on {DateTimeUtils.ToString(input.StartTime)}");
            sb.AppendLine("");

            await SendKomu(sb);
            return sb.ToString();

        }

        [HttpPost]
        [AbpAllowAnonymous]
        public async Task PlanUserMaternityLeave(PlanAndConfirmUserDto input)
        {
            CheckSecurityCode();
            await PlanUserFromHRMToProject(input, AppConsts.NGHI_SINH_PROJECT_CODE);
        }

        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<string> ConfirmUserMaternityLeave(PlanAndConfirmUserDto input)
        {
            CheckSecurityCode();

            var employee = await _resourceManager.GetKomuUserInfo(input.Email.ToLower().Trim());

            if (employee == default)
            {
                return "PROJECT tool: Not found user with email " + input.Email.ToLower().Trim();
            }

            var activeReportId = await _resourceManager.GetActiveReportId();
            string note = "Nghỉ sinh từ HRM Tool";
            var sb = await _resourceManager.ReleaseUserFromAllWorkingProjectsByHRM(employee, activeReportId, note);

            var projectId = await GetProjectIdByCode(AppConsts.NGHI_SINH_PROJECT_CODE);

            var pu = await processMaternityLeavePU(employee.UserId, projectId, note, activeReportId, input.StartTime);

            sb.AppendLine($"{employee.KomuAccountInfo} was **maternity leave** from {DateTimeUtils.ToString(pu.StartTime)}");
            sb.AppendLine("");

            await SendKomu(sb);
            return sb.ToString();
        }


        [HttpPost]
        [AbpAllowAnonymous]
        public async Task ComfirmUserBackToWorkAfterQuitJob(ConfirmUserBackToWorkDto input)
        {
            CheckSecurityCode();
            long activeReportId = await _resourceManager.GetActiveReportId();

            var user = WorkScope.GetAll<User>()
              .Where(x => x.EmailAddress.ToLower().Trim() == input.EmailAddress.ToLower().Trim())
              .FirstOrDefault();

            var employee = new KomuUserInfoDto
            {
                FullName = user.FullName,
                UserId = user.Id,
                KomuUserId = user.KomuUserId,
                UserName = user.UserName
            };
            await _resourceManager.ReleaseUserFromAllWorkingProjectsByHRM(employee, activeReportId, "account recovery from HRM");

            user.IsActive = true;
            await WorkScope.UpdateAsync(user);
        }

        private async Task<ProjectUser> processMaternityLeavePU(long userId, long projectId, string note, long activeReportId, DateTime startTime)
        {
            var pu = await WorkScope.GetAll<ProjectUser>()
                .Where(s => s.Status == ProjectUserStatus.Future)
                .Where(s => s.AllocatePercentage > 0)
                .Where(s => s.UserId == userId)
                .Where(s => s.Project.Code == AppConsts.NGHI_SINH_PROJECT_CODE)
                .FirstOrDefaultAsync();
            if (pu != default)
            {
                pu.Status = ProjectUserStatus.Present;
                pu.StartTime = startTime;
                pu.IsPool = true;
                pu.PMReportId = activeReportId;
                await WorkScope.UpdateAsync(pu);
            }
            else
            {
                pu = new ProjectUser
                {
                    UserId = userId,
                    ProjectId = projectId,
                    StartTime = startTime,
                    Status = ProjectUserStatus.Present,
                    AllocatePercentage = 100,
                    IsPool = true,
                    PMReportId = activeReportId,
                    Note = note
                };
                await WorkScope.InsertAsync(pu);
            }
            return pu;
        }

        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<string> ConfirmMaternityUserComeBack(PlanAndConfirmUserDto input)
        {
            CheckSecurityCode();
            var NghiSinhPU = await WorkScope.GetAll<ProjectUser>()
                .Where(x => x.Project.Code == AppConsts.NGHI_SINH_PROJECT_CODE)
                .Where(x => x.User.EmailAddress.ToLower().Trim() == input.Email.ToLower().Trim())
                .Where(x => x.Status == ProjectUserStatus.Present)
                .Where(x => x.AllocatePercentage > 0)
                .FirstOrDefaultAsync();

            if (NghiSinhPU == default)
            {
                return $"Not found present working PU with email: {input.Email}, ProjectCode {AppConsts.NGHI_SINH_PROJECT_CODE}";
            }

            var activeReportId = await _resourceManager.GetActiveReportId();

            NghiSinhPU.Status = ProjectUserStatus.Past;
            await WorkScope.UpdateAsync(NghiSinhPU);

            NghiSinhPU.Status = ProjectUserStatus.Present;
            NghiSinhPU.StartTime = DateTimeUtils.GetNow();
            NghiSinhPU.PMReportId = activeReportId;
            NghiSinhPU.AllocatePercentage = 0;
            NghiSinhPU.Id = 0;

            await WorkScope.InsertAsync(NghiSinhPU);
           
            var sb = new StringBuilder();
            var employee = await _resourceManager.GetKomuUserInfo(input.Email);
            sb.AppendLine($"{employee.KomuAccountInfo} come back to work on {DateTimeUtils.ToString(NghiSinhPU.StartTime)} after finishing **maternity leave**");
            sb.AppendLine();
            await SendKomu(sb);

            return sb.ToString();
        }

        public IQueryable<ProjectUser> CheckExistPlanJoinPU(string email, string projectCode)
        {
            return WorkScope.GetAll<ProjectUser>()
                .Where(x => x.Project.Code == projectCode)
                .Where(x => x.User.EmailAddress.ToLower().Trim() == email.ToLower().Trim())
                .Where(x => x.Status == ProjectUserStatus.Future)
                .Where(x => x.AllocatePercentage > 0);
        }

        public async Task PlanUserFromHRMToProject(PlanAndConfirmUserDto input, string projectCode)
        {
            var activeReportId = await _resourceManager.GetActiveReportId();

            var existPlanPU = await CheckExistPlanJoinPU(input.Email, projectCode).FirstOrDefaultAsync();
            if (existPlanPU != default)
            {                
                existPlanPU.StartTime = input.StartTime;
                existPlanPU.PMReportId = activeReportId;
                await WorkScope.UpdateAsync(existPlanPU);
            }
            else
            {
                var projectId = await GetProjectIdByCode(projectCode);
                var userId = await WorkScope.GetAll<User>()
                    .Where(x => x.EmailAddress.ToLower().Trim() == input.Email.ToLower().Trim())
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                var newPU = new ProjectUser
                {
                    StartTime = input.StartTime,
                    ProjectId = projectId,
                    Status = ProjectUserStatus.Future,
                    AllocatePercentage = 100,
                    UserId = userId,
                    PMReportId = activeReportId,
                    IsPool = projectCode == AppConsts.NGHI_SINH_PROJECT_CODE,//nghi sinh - temp -> xuat hien o pool
                };
                await WorkScope.InsertAsync(newPU);
            }
        }

        private async Task<long> GetProjectIdByCode(string code)
        {
            return await WorkScope.GetAll<Project>()
                .Where(s => s.Code == code)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
        }

        private async Task SendKomu(string komuMessage)
        {
             _komuService.NotifyToChannel(new KomuMessage
            {
                CreateDate = DateTimeUtils.GetNow(),
                Message = komuMessage,
            },
            ChannelTypeConstant.PM_CHANNEL);
        }

        private async Task SendKomu(StringBuilder sb)
        {
            await SendKomu(sb.ToString());
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
