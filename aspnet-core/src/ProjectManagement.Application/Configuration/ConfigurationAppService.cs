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

        [AbpAuthorize(PermissionNames.Admin_Configuration_ViewAll)]
        public async Task<AppSettingDto> Get()
        {
            return new AppSettingDto
            {
                ClientAppId = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ClientAppId),
                SecurityCode = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.SecurityCode),
                SecretCode = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.SecretCode),
                FinanceUri = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.FinaceUri),
                FinanceSecretKey = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.FinanceSecretKey),
                TimesheetUri = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetUri),
                TimesheetSecretKey = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetSecretKey),
                CanSendDay = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.CanSendDay),
                CanSendHour = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.CanSendHour),
                ExpiredDay = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ExpiredDay),
                ExpiredHour = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ExpiredHour),
                KomuUrl = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUrl),
                KomuUserNames = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUserNames),
                UserBot = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.UserBot),
                PasswordBot = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.PasswordBot),
                ProjectUri = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ProjectUri),
            };
        }

        [AbpAuthorize(PermissionNames.Admin_Configuration_Edit)]
        public async Task<AppSettingDto> Change(AppSettingDto input)
        {
            if (string.IsNullOrEmpty(input.ClientAppId) ||
                string.IsNullOrEmpty(input.SecurityCode) ||
                string.IsNullOrEmpty(input.SecretCode) ||
                string.IsNullOrEmpty(input.FinanceUri) ||
                string.IsNullOrEmpty(input.FinanceSecretKey) ||
                string.IsNullOrEmpty(input.TimesheetUri) ||
                string.IsNullOrEmpty(input.TimesheetSecretKey) ||
                string.IsNullOrEmpty(input.CanSendDay) ||
                string.IsNullOrEmpty(input.CanSendHour) ||
                string.IsNullOrEmpty(input.ExpiredDay) ||
                string.IsNullOrEmpty(input.ExpiredHour) ||
                string.IsNullOrEmpty(input.KomuUserNames) ||
                string.IsNullOrEmpty(input.KomuUrl) ||
                string.IsNullOrEmpty(input.UserBot) ||
                string.IsNullOrEmpty(input.PasswordBot) ||
                string.IsNullOrEmpty(input.ProjectUri))
            {
                throw new UserFriendlyException("All setting values need to be completed");

            }
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ClientAppId, input.ClientAppId);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SecurityCode, input.SecurityCode);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SecretCode, input.SecretCode);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.FinaceUri, input.FinanceUri);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.FinanceSecretKey, input.FinanceSecretKey);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.TimesheetUri, input.TimesheetUri);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.TimesheetSecretKey, input.TimesheetSecretKey);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.CanSendDay, input.CanSendDay);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.CanSendHour, input.CanSendHour);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ExpiredDay, input.ExpiredDay);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ExpiredHour, input.ExpiredHour);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.KomuUrl, input.KomuUrl);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.KomuUserNames, input.KomuUserNames);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserBot, input.UserBot);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.PasswordBot, input.PasswordBot);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ProjectUri, input.ProjectUri);
            return input;
        }

    }
}
