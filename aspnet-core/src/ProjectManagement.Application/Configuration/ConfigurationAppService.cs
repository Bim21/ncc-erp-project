using System;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectManagement.Authorization;
using ProjectManagement.Configuration.Dto;
using ProjectManagement.Services.CheckConnectDto;
using ProjectManagement.Services.Finance;
using ProjectManagement.Services.Finance.Dto;
using ProjectManagement.Services.HRM;
using ProjectManagement.Services.HRM.Dto;
using ProjectManagement.Services.ProjectTimesheet;
using ProjectManagement.Services.ProjectTimesheet.Dto;
using ProjectManagement.Services.ResourceRequestService.Dto;
using ProjectManagement.Services.Talent;
using ProjectManagement.Services.Talent.Dtos;
using ProjectManagement.Services.Timesheet;
using ProjectManagement.Services.Timesheet.Dto;
using System.Net.Http;
using System.Net.NetworkInformation;
using static ProjectManagement.Constants.Enum.ProjectEnum;
using System.Text.Json;

namespace ProjectManagement.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ProjectManagementAppServiceBase, IConfigurationAppService
    {
        private static IConfiguration _appConfiguration;
        private readonly TalentService _talentService;
        private readonly HRMService _hrmService;
        private readonly FinfastService _finfastService;
        private readonly TimesheetService _timesheetService;

        public ConfigurationAppService(IConfiguration appConfiguration,
            TalentService talentService,
            HRMService hrmService,
            FinfastService finfastService,
            TimesheetService timesheetService)
        {
            _appConfiguration = appConfiguration;
            _talentService = talentService;
            _hrmService = hrmService;
            _finfastService = finfastService;
            _timesheetService = timesheetService;
        }
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }

        public async Task<string> GetGoogleClientAppId()
        {
            return await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ClientAppId);
        }

        [AbpAuthorize(PermissionNames.Admin_Configuartions)]
        public async Task<AppSettingDto> Get()
        {
            return new AppSettingDto
            {
                ClientAppId = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ClientAppId),
                SecurityCode = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.SecurityCode),
                FinanceUri = _appConfiguration.GetValue<string>("FinfastService:BaseAddress"),
                FinanceSecretCode = _appConfiguration.GetValue<string>("FinfastService:SecurityCode"),
                TimesheetUri = _appConfiguration.GetValue<string>("TimesheetService:BaseAddress"),
                TimesheetSecretCode = _appConfiguration.GetValue<string>("TimesheetService:SecurityCode"),
                AutoUpdateProjectInfoToTimesheetTool = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.AutoUpdateProjectInfoToTimesheetTool),
                CanSendDay = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.CanSendDay),
                CanSendHour = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.CanSendHour),
                ExpiredDay = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ExpiredDay),
                ExpiredHour = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ExpiredHour),
                KomuUrl = _appConfiguration.GetValue<string>("KomuService:BaseAddress"),
                NoticeToKomu = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.NoticeToKomu),
                KomuSecretCode = _appConfiguration.GetValue<string>("KomuService:SecurityCode"),
                KomuUserNames = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUserNames),
                UserBot = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.UserBot),
                PasswordBot = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.PasswordBot),
                HRMUri = _appConfiguration.GetValue<string>("HRMService:BaseAddress"),
                HRMSecretCode = _appConfiguration.GetValue<string>("HRMService:SecurityCode"),
                KomuRoom = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuRoom),
                DefaultWorkingHours = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.DefaultWorkingHours),
                TrainingRequestChannel = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.TrainingRequestChannel),
                TalentUriBA = _appConfiguration.GetValue<string>("TalentService:BaseAddress"),
                TalentUriFE = _appConfiguration.GetValue<string>("TalentService:FEAddress"),
                TalentSecurityCode = _appConfiguration.GetValue<string>("TalentService:SecurityCode"),
                MaxCountHistory = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.MaxCountHistory),
                
            };
        }

        [AbpAuthorize(PermissionNames.Admin_Configuartions_Edit)]
        public async Task<AppSettingDto> Change(AppSettingDto input)
        {
            if (string.IsNullOrEmpty(input.ClientAppId) ||
                string.IsNullOrEmpty(input.SecurityCode) ||
                string.IsNullOrEmpty(input.FinanceUri) ||
                string.IsNullOrEmpty(input.FinanceSecretCode) ||
                string.IsNullOrEmpty(input.TimesheetUri) ||
                string.IsNullOrEmpty(input.TimesheetSecretCode) ||
                string.IsNullOrEmpty(input.AutoUpdateProjectInfoToTimesheetTool) ||
                string.IsNullOrEmpty(input.CanSendDay) ||
                string.IsNullOrEmpty(input.CanSendHour) ||
                string.IsNullOrEmpty(input.ExpiredDay) ||
                string.IsNullOrEmpty(input.ExpiredHour) ||
                string.IsNullOrEmpty(input.KomuUserNames) ||
                string.IsNullOrEmpty(input.KomuUrl) ||
                string.IsNullOrEmpty(input.NoticeToKomu) ||
                string.IsNullOrEmpty(input.KomuSecretCode) ||
                string.IsNullOrEmpty(input.UserBot) ||
                string.IsNullOrEmpty(input.PasswordBot) ||
                string.IsNullOrEmpty(input.HRMUri) ||
                string.IsNullOrEmpty(input.HRMSecretCode) ||
                string.IsNullOrEmpty(input.KomuRoom) ||
                string.IsNullOrEmpty(input.DefaultWorkingHours) ||
                string.IsNullOrEmpty(input.MaxCountHistory)
                )
            {
                throw new UserFriendlyException("All setting values need to be completed");

            }
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ClientAppId, input.ClientAppId);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.FinanceUri, input.FinanceUri);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.FinanceSecretCode, input.FinanceSecretCode);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.TimesheetUri, input.TimesheetUri);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.TimesheetSecretCode, input.TimesheetSecretCode);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.AutoUpdateProjectInfoToTimesheetTool, input.AutoUpdateProjectInfoToTimesheetTool);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.CanSendDay, input.CanSendDay);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.CanSendHour, input.CanSendHour);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ExpiredDay, input.ExpiredDay);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ExpiredHour, input.ExpiredHour);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.KomuUrl, input.KomuUrl);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.NoticeToKomu, input.NoticeToKomu);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.KomuSecretCode, input.KomuSecretCode);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.KomuUserNames, input.KomuUserNames);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserBot, input.UserBot);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.PasswordBot, input.PasswordBot);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.HRMUri, input.HRMUri);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.HRMSecretCode, input.HRMSecretCode);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.KomuRoom, input.KomuRoom);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.DefaultWorkingHours, input.DefaultWorkingHours);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.TrainingRequestChannel, input.TrainingRequestChannel);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.MaxCountHistory, input.MaxCountHistory);
            return input;
        }

        [AbpAuthorize(PermissionNames.Admin_Configuartions_Edit)]
        public async Task<ProjectSetting> ChangeProjectSetting(ProjectSetting input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SecurityCode, input.SecurityCode);
            return input;

        }

        [AbpAllowAnonymous]
        [HttpGet]
        public async Task<WeeklyReportSettingDto> GetTimeCountDown()
        {
            return new WeeklyReportSettingDto
            {
                TimeCountDown =  Convert.ToInt32(await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimeCountDown))
            };
        }
        [AbpAuthorize(PermissionNames.Admin_Configuartions_WeeklyReportTime_Edit)]
        [HttpPost]
        public async Task<WeeklyReportSettingDto> SetTimeCountDown(WeeklyReportSettingDto input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.TimeCountDown, input.TimeCountDown.ToString());
            return input;
        }

        [AbpAuthorize(PermissionNames.Admin_Configuartions_Edit)]
        [HttpPost]
        public async Task<AuditScoreDto> SetAuditScore(AuditScoreDto input)
        {
            var json = JsonSerializer.Serialize(input);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ScoreAudit, json);
            return input;
        }

        [AbpAuthorize(PermissionNames.Admin_Configuartions_ViewAuditScoreSetting)]
        [HttpGet]
        public async Task<AuditScoreDto> GetAuditScore()
        {
            var json = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ScoreAudit);

            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return JsonSerializer.Deserialize<AuditScoreDto>(json);
        }

        [AbpAuthorize(
            //PermissionNames.Admin_Configuartions_Edit,
            )]
        [HttpPost]
        public async Task<GuideLineDto> SetGuideLine(GuideLineDto input)
        {
            var allowUpdateGuideline = await PermissionChecker.IsGrantedAsync(PermissionNames.WeeklyReport_ReportDetail_GuideLine_Update);
            if (!allowUpdateGuideline)
            {
                throw new UserFriendlyException("You are not allow to update this guideline!");
            }
            var json = JsonSerializer.Serialize(input);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.GuideLine, json);
            return input;
        }


        [AbpAuthorize(
          //PermissionNames.Admin_Configurations_ViewGuideLineSetting
          )]
        [HttpGet]
         public async Task<GuideLineDto> GetGuideLine()
         {
            var allowViewGuideline = await PermissionChecker.IsGrantedAsync(PermissionNames.WeeklyReport_ReportDetail_GuideLine_View);
            if (!allowViewGuideline)
            {
                throw new UserFriendlyException("You are not allow to view this guideline!");
            }
            var json = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.GuideLine);

             if (string.IsNullOrEmpty(json))
             {
                 return null;
             }

             return JsonSerializer.Deserialize<GuideLineDto>(json);
         }

        [HttpGet]
        public async Task<GetResultConnectDto> CheckConnectToTimesheet()
        {
            return await _timesheetService.CheckConnectToTimesheet();
        }
        [HttpGet]
        public async Task<GetResultConnectDto> CheckConnectToTalent()
        {
            return await _talentService.CheckConnectToTalent();
        }
        [HttpGet]
        public async Task<GetResultConnectDto> CheckConnectToFinfast()
        {
            return await _finfastService.CheckConnectToFinance();
        }
    }
}
