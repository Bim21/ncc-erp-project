using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using ProjectManagement.Authorization;
using ProjectManagement.Configuration.Dto;

namespace ProjectManagement.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ProjectManagementAppServiceBase, IConfigurationAppService
    {
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
                FinanceUri = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.FinanceUri),
                FinanceSecretCode = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.FinanceSecretCode),
                TimesheetUri = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetUri),
                TimesheetSecretCode = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetSecretCode),
                AutoUpdateProjectInfoToTimesheetTool = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.AutoUpdateProjectInfoToTimesheetTool),
                CanSendDay = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.CanSendDay),
                CanSendHour = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.CanSendHour),
                ExpiredDay = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ExpiredDay),
                ExpiredHour = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ExpiredHour),
                KomuUrl = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUrl),
                NoticeToKomu = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.NoticeToKomu),
                KomuSecretCode = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuSecretCode),
                KomuUserNames = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUserNames),
                UserBot = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.UserBot),
                PasswordBot = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.PasswordBot),
                ProjectUri = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri),
                HRMUri = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.HRMUri),
                HRMSecretCode = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.HRMSecretCode),
                KomuRoom = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuRoom),
                DefaultWorkingHours = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.DefaultWorkingHours),
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
                string.IsNullOrEmpty(input.ProjectUri) ||
                string.IsNullOrEmpty(input.HRMUri) ||
                string.IsNullOrEmpty(input.HRMSecretCode) ||
                string.IsNullOrEmpty(input.KomuRoom) ||
                string.IsNullOrEmpty(input.DefaultWorkingHours)
                )
            {
                throw new UserFriendlyException("All setting values need to be completed");

            }
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ClientAppId, input.ClientAppId);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SecurityCode, input.SecurityCode);
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
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ProjectUri, input.ProjectUri);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.HRMUri, input.HRMUri);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.HRMSecretCode, input.HRMSecretCode);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.KomuRoom, input.KomuRoom);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.DefaultWorkingHours, input.DefaultWorkingHours);
            return input;
        }

    }
}
