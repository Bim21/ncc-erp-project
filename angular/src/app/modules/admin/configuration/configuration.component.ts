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
  Admin_Configuration_Edit = PERMISSIONS_CONSTANT.Admin_Configuration_Edit;
  Admin_Configuration = PERMISSIONS_CONSTANT.Admin_Configuration;

  configuration = {} as ConfigurationDto;
  googleToken: string = '';
  public isEditClientId: boolean = false;
  public isEditProjectUri: boolean = false;
  public isEditSecretCode: boolean = false;
  public isEditUserBot: boolean = false;
  public isEditPassBot: boolean = false;
  public isEditKomuUrl: boolean = false;
  public isEditKomuUserNames: boolean = false;
  public isEditFinanceUri: boolean = false;
  public isEditFinanceSecretKey: boolean = false;
  public isEditTimesheetUri: boolean = false;
  public isEditTimesheetSecretKey: boolean = false;
  public isEditCanSendDay: boolean = false;
  public isEditCanSendHour: boolean = false;
  public isEditExpiredDay: boolean = false;
  public isEditExpiredHour: boolean = false;
  public isEditGoogleKey: boolean = false;
  public isEditHRM: boolean = false;
  public isEditHRMSecretKey: boolean = false;
  public isEditDefaultWorkingHours: boolean = false;
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
}
export class ConfigurationDto {
  clientAppId: string;
  projectUri: string;
  securityCode: string;
  userBot: string;
  passwordBot: string;
  komuUrl: string;
  komuUserNames: string;
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
}
