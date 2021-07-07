import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { ProjectChecklistComponent } from './modules/pm-management/list-project/list-project-detail/project-checklist/project-checklist.component';
import { MilestoneComponent } from './modules/pm-management/list-project/list-project-detail/milestone/milestone.component';
import { WeeklyReportComponent } from './modules/pm-management/list-project/list-project-detail/weekly-report/weekly-report.component';
import { ResourceManagementComponent } from './modules/pm-management/list-project/list-project-detail/resource-management/resource-management.component';
import { ListProjectDetailComponent } from './modules/pm-management/list-project/list-project-detail/list-project-detail.component';
import { TimesheetDetailComponent } from './modules/timesheet/timesheet-detail/timesheet-detail.component';
import { ListProjectComponent } from './modules/pm-management/list-project/list-project.component';
import { CreateEditListProjectComponent } from './modules/pm-management/list-project/create-edit-list-project/create-edit-list-project.component';
import { ChecklistComponent } from './modules/checklist-management/checklist/checklist.component';
import { CreateChecklistItemComponent } from './modules/checklist-management/checklist/create-checklist-item/create-checklist-item.component';
import { CreateEditChecklistTitleComponent } from './modules/checklist-management/checklist-title/create-edit-checklist-title/create-edit-checklist-title.component';
import { ChecklistTitleComponent } from './modules/checklist-management/checklist-title/checklist-title.component';
import { FilterComponent } from './../shared/filter/filter.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from '@app/home/home.component';
import { AboutComponent } from '@app/about/about.component';
import { BrowserModule } from '@angular/platform-browser';
// tenants
import { TenantsComponent } from '@app/tenants/tenants.component';
import { CreateTenantDialogComponent } from './tenants/create-tenant/create-tenant-dialog.component';
import { EditTenantDialogComponent } from './tenants/edit-tenant/edit-tenant-dialog.component';
// roles
import { RolesComponent } from '@app/roles/roles.component';
import { CreateRoleDialogComponent } from './roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './roles/edit-role/edit-role-dialog.component';
// users
import { UsersComponent } from '@app/users/users.component';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from './users/reset-password/reset-password.component';
// layout
import { HeaderComponent } from './layout/header.component';
import { HeaderLeftNavbarComponent } from './layout/header-left-navbar.component';
import { HeaderLanguageMenuComponent } from './layout/header-language-menu.component';
import { HeaderUserMenuComponent } from './layout/header-user-menu.component';
import { FooterComponent } from './layout/footer.component';
import { SidebarComponent } from './layout/sidebar.component';
import { SidebarLogoComponent } from './layout/sidebar-logo.component';
import { SidebarUserPanelComponent } from './layout/sidebar-user-panel.component';
import { SidebarMenuComponent } from './layout/sidebar-menu.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TimesheetComponent } from './modules/timesheet/timesheet.component';
import { SaoDoComponent } from './modules/saodo-management/sao-do/sao-do.component';
import { CreateEditTimesheetComponent } from './modules/timesheet/create-edit-timesheet/create-edit-timesheet.component';
import { ImportFileTimesheetDetailComponent } from './modules/timesheet/timesheet-detail/import-file-timesheet-detail/import-file-timesheet-detail.component';
import { CreateEditTimesheetDetailComponent } from './modules/timesheet/timesheet-detail/create-edit-timesheet-detail/create-edit-timesheet-detail.component';
import { DeliveryComponent } from './modules/delivery-management/delivery/delivery.component';
import { ProjectStatusTabComponent } from './modules/delivery-management/delivery/project-status-tab/project-status-tab.component';
import { ProjectMilestoneTabComponent } from './modules/delivery-management/delivery/project-milestone-tab/project-milestone-tab.component';
import { ListProjectGeneralComponent } from './modules/pm-management/list-project/list-project-detail/list-project-general/list-project-general.component';
import { CreateEditSaodoComponent } from './modules/saodo-management/sao-do/create-edit-saodo/create-edit-saodo.component';
import { SaoDoDetailComponent } from './modules/saodo-management/sao-do/sao-do-detail/sao-do-detail.component';
import { InvoiceComponent } from './modules/timesheet/invoice/invoice.component';
import { CreateEditInvoiceComponent } from './modules/timesheet/invoice/create-edit-invoice/create-edit-invoice.component';
import { CreateEditSaoDoProjectComponent } from './modules/saodo-management/sao-do/sao-do-detail/create-edit-sao-do-project/create-edit-sao-do-project.component';
import { SaoDoProjectDetailComponent } from './modules/saodo-management/sao-do/sao-do-detail/sao-do-project-detail/sao-do-project-detail.component';
import { WeeklyReportTabComponent } from './modules/delivery-management/delivery/weekly-report-tab/weekly-report-tab.component';
import { WeeklyReportTabDetailComponent } from './modules/delivery-management/delivery/weekly-report-tab/weekly-report-tab-detail/weekly-report-tab-detail.component';




export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    // tenants
    TenantsComponent,
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    RolesComponent,
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    UsersComponent,
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ChangePasswordComponent,
    ResetPasswordDialogComponent,
    // layout
    HeaderComponent,
    HeaderLeftNavbarComponent,
    HeaderLanguageMenuComponent,
    HeaderUserMenuComponent,
    FooterComponent,
    SidebarComponent,
    SidebarLogoComponent,
    SidebarUserPanelComponent,
    SidebarMenuComponent,

    FilterComponent,
    
    TimesheetComponent,

    ChecklistTitleComponent,
    CreateEditChecklistTitleComponent,
    ChecklistComponent,
    CreateChecklistItemComponent,
    SaoDoComponent,
    ListProjectComponent,
    CreateEditListProjectComponent,
    CreateEditTimesheetComponent,
    TimesheetDetailComponent,
    ListProjectDetailComponent,
    ResourceManagementComponent,
    MilestoneComponent,
    WeeklyReportComponent,
    ProjectChecklistComponent,
    ImportFileTimesheetDetailComponent,
    CreateEditTimesheetDetailComponent,
    DeliveryComponent,
    ProjectStatusTabComponent,
    ProjectMilestoneTabComponent,
    ListProjectGeneralComponent,
    CreateEditSaodoComponent,
    SaoDoDetailComponent,
    InvoiceComponent,
    CreateEditInvoiceComponent,
    CreateEditSaoDoProjectComponent,
    SaoDoProjectDetailComponent,
    WeeklyReportTabComponent,
    WeeklyReportTabDetailComponent,
    
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ModalModule.forChild(),
    BsDropdownModule,
    CollapseModule,
    TabsModule,
    AppRoutingModule,
    ServiceProxyModule,
    SharedModule,
    NgxPaginationModule,
    SocialLoginModule,
    NgxMaskModule.forRoot(),
    Ng2SearchPipeModule,
   
  


    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      }
    })
  ],
  exports: [
    TranslateModule,
  ],
  providers: [
  {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('879411761479-bgjd9vk52gsrs937ve5vumnmcd7v64oe.apps.googleusercontent.com'
            ),
          },
        ],
      } as SocialAuthServiceConfig,
    },
    {
      provide: MAT_DATE_LOCALE, useValue: 'en-GB'
    }

  ],
  entryComponents: [
    // tenants
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ResetPasswordDialogComponent,
  ],
})
export class AppModule {}
