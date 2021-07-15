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
                new SettingDefinition(AppSettingNames.ClientAppId,"879411761479-0apou182r1lidkj45lf1ee1prji19ll3.apps.googleusercontent.com",scopes:SettingScopes.Application| SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.SecurityCode, "Xnsks4@llslhl%hjsksCCHHA145", scopes:SettingScopes.Application| SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.SecretCode, "Uqhfwwg%fyef@HUSAA744fiegyeR", scopes:SettingScopes.Application| SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.FinaceUri,"http://localhost:21021/",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.FinanceSecretKey,"Uqhfwwg%fyef@HUSAA744fiegyeR",scopes:SettingScopes.Application|SettingScopes.Tenant),
            };
        }
    }
}
