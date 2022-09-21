import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { AppConfigurationService } from '../../../service/api/app-configuration.service';
@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css'],
})
export class ConfigurationComponent extends AppComponentBase implements OnInit {
  Admin_Configuartions_Edit = PERMISSIONS_CONSTANT.Admin_Configuartions_Edit;
  Admin_Configuartions_ViewKomuSetting = PERMISSIONS_CONSTANT.Admin_Configuartions_ViewKomuSetting;
  Admin_Configuartions_ViewProjectSetting = PERMISSIONS_CONSTANT.Admin_Configuartions_ViewProjectSetting;
  Admin_Configuartions_ViewHrmSetting = PERMISSIONS_CONSTANT.Admin_Configuartions_ViewHrmSetting;
  Admin_Configuartions_ViewTimesheetSetting = PERMISSIONS_CONSTANT.Admin_Configuartions_ViewTimesheetSetting;
  Admin_Configuartions_ViewFinanceSetting = PERMISSIONS_CONSTANT.Admin_Configuartions_ViewFinanceSetting;
  Admin_Configuartions_ViewGoogleClientAppSetting = PERMISSIONS_CONSTANT.Admin_Configuartions_ViewGoogleClientAppSetting;
  Admin_Configuartions_ViewDefaultWorkingHourPerDaySetting = PERMISSIONS_CONSTANT.Admin_Configuartions_ViewDefaultWorkingHourPerDaySetting;

  configuration = {} as ConfigurationDto;
  googleToken: string = '';
  public isEditClientId: boolean = false;
  public isEditProjectUri: boolean = false;
  public isEditSecretCode: boolean = false;
  public isEditUserBot: boolean = false;
  public isEditPassBot: boolean = false;
  public isEditKomuUrl: boolean = false;
  public isEditDefaultWorkingHours: boolean = false;
  public isShowKomuSetting: boolean = false;
  public isExpandingProjectSetting: boolean = false;
  public isShowHRMSetting: boolean = false;
  public isShowTimesheetSetting: boolean = false;
  public isShowFinanceSetting: boolean = false;
  public isShowGoogleClientApp: boolean = false;
  public isShowDefaultWorking: boolean = false;
  public isShowDiscordChannel: boolean = false;
  public isEditDiscordChannel: boolean = false;
 
  public listDays: any[] = [
    { value: '2', text: 'Monday' },
    { value: '3', text: 'Tuesday' },
    { value: '4', text: 'Wednesday' },
    { value: '5', text: 'Thursday' },
    { value: '6', text: 'Friday' },
    { value: '7', text: 'Saturday' },
    { value: '8', text: 'Sunday' },
  ];
  public listHours = [];
  public isEditingKomu: boolean = false;
  public isEditingTimesheet: boolean = false;
  constructor(
    private settingService: AppConfigurationService,
    injector: Injector
  ) {
    super(injector);
  }

  ngOnInit(): void {
    for (let index = 0; index < 24; index++) {
      let hour = { value: index.toString(), text: index.toString() + 'h' };
      this.listHours.push(hour);
    }
    this.getSetting();
  }
  getSetting() {
    this.settingService.getConfiguration().subscribe((data) => {
      this.configuration = data.result;
    });
  }
  saveConfiguration() {
    this.settingService
      .editConfiguration(this.configuration)
      .subscribe((data) => {
        abp.notify.success('Edited');
      });
  }
  enableNoticeToKomu(value){
    this.configuration.noticeToKomu = value.toString();
  }
  enableAutoUpdateProjectInfoToTimesheetTool(value){
    this.configuration.autoUpdateProjectInfoToTimesheetTool = value.toString();
  }
  checkNoticeToKomu() {
    if (this.configuration.noticeToKomu == 'true') return true;
    return false;
  }

  public updateProjectSettingConfig(){
    let projectConfig = {} as ProjectSetting;
    projectConfig.securityCode = this.configuration.securityCode;
    this.settingService
    .updateProjectSettingConfig(projectConfig)
    .subscribe((rs)=>{
      abp.notify.success('Update project setting successful!');
      
    })

  }

}
export class ConfigurationDto {
  clientAppId: string;
  projectUri: string;
  securityCode: string;
  userBot: string;
  passwordBot: string;
  komuUrl: string;
  komuUserNames: string;
  komuSecretCode: string;
  financeUri: string;
  financeSecretCode: string;
  timesheetUri: string;
  timesheetSecretCode: string;
  canSendDay: string;
  canSendHour: string;
  expiredDay: string;
  expiredHour: string;
  hrmUri: string;
  hrmSecretCode: string;
  defaultWorkingHours: string;
  noticeToKomu: string;
  autoUpdateProjectInfoToTimesheetTool: string;
  trainingRequestChannel: string;
}

export class ProjectSetting{
  securityCode: string;
}
