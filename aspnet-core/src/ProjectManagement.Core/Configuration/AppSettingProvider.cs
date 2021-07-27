using System.Collections.Generic;
using Abp.Configuration;

namespace ProjectManagement.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.ClientAppId,"879411761479-734qv2e2efi9f68utvo8catolkcfbe47.apps.googleusercontent.com",scopes:SettingScopes.Application| SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.SecurityCode, "Xnsks4@llslhl%hjsksCCHHA145", scopes:SettingScopes.Application| SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.SecretCode, "Uqhfwwg%fyef@HUSAA744fiegyeR", scopes:SettingScopes.Application| SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.FinaceUri,"http://finance-api.dev.nccsoft.vn/",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.FinanceSecretKey,"Uqhfwwg%fyef@HUSAA744fiegyeR",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.CanSendDay,"2",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.CanSendHour,"15",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.ExpiredDay,"3",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.ExpiredHour,"0",scopes:SettingScopes.Application|SettingScopes.Tenant),
            };
        }
    }
}
