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
                new SettingDefinition(AppSettingNames.ClientAppId,"313933079512-lmpvf98bmvgidrv3m65624is4q700v17.apps.googleusercontent.com",scopes:SettingScopes.Application| SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.SecurityCode, "Uqhfwwg%fyef@HUSAA744fiegyeR", scopes:SettingScopes.Application| SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.FinanceUri,"http://finance-api.dev.nccsoft.vn/",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.FinanceSecretCode,"Uqhfwwg%fyef@HUSAA744fiegyeR",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.TimesheetUri,"http://uat.timesheetapi.nccsoft.vn:2021/",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.TimesheetSecretCode,"Xnsks4@llslhl%hjsksCCHHA145",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.AutoUpdateProjectInfoToTimesheetTool,"true",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.HRMUri,"http://hrm-api.dev.nccsoft.vn/",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.HRMSecretCode,"Xnsks4@llslhl%hjsksCCHHA145",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.CanSendDay,"2",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.CanSendHour,"15",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.ExpiredDay,"3",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.ExpiredHour,"0",scopes:SettingScopes.Application|SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.UserBot,"bot1@ncc.asia",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.PasswordBot,"12345678a@",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.KomuUrl,"http://172.16.11.90:3000/",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.NoticeToKomu,"true",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.KomuSecretCode,"6kkCZQja9Gn27kTiv",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.ProjectUri,"http://project.dev.nccsoft.vn/",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.KomuUserNames,"hang.buithidiem.ncc;trung.do.trong;lam.buihoang.ncc;",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.KomuRoom,"erp-team-ncc",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.DefaultWorkingHours,"8",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.TimeCountDown,"180",scopes:SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.TrainingRequestChannel, "1008574549283061820",scopes: SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.MaxCountHistory, "12",scopes: SettingScopes.Application |SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.ScoreAudit, "{\"FINAL_SCORE\":100,\"PROJECT_SCORE_WHEN_STATUS_GREEN\":85,\"PROJECT_SCORE_WHEN_STATUS_AMBER\":70,\"PROJECT_SCORE_WHEN_STATUS_RED\":70,\"PROJECT_PROCESS_CRITERIA_RESULT_STATUS_NC\":-20,\"PROJECT_PROCESS_CRITERIA_RESULT_STATUS_OBSERVATION\":-15,\"PROJECT_PROCESS_CRITERIA_RESULT_STATUS_RE\":15,\"PROJECT_PROCESS_CRITERIA_RESULT_STATUS_EX\":20}", scopes: SettingScopes.Application | SettingScopes.Tenant)
            };
        }
    }
}
