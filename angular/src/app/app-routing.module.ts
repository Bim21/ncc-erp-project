import { ProductProjectsComponent } from './modules/pm-management/product-projects/product-projects.component';
import { ProjectDescriptionComponent } from './modules/pm-management/list-project/list-project-detail/project-description/project-description.component';
import { ReviewYourselfComponent } from './modules/checkpoint/review-yourself/review-yourself.component';
import { ReviewUserComponent } from './modules/checkpoint/review-user/review-user.component';
import { ResultReviewerComponent } from './modules/checkpoint/set-up-reviewer/result-reviewer/result-reviewer.component';
import { SetUpReviewerComponent } from './modules/checkpoint/set-up-reviewer/set-up-reviewer.component';
import { TagsComponent } from './modules/checkpoint/tags/tags.component';
import { PhaseComponent } from './modules/checkpoint/phase/phase.component';
import { CriteriaComponent } from './modules/checkpoint/category/criteria/criteria.component';
import { CategoryCriteriaComponent } from './modules/checkpoint/category/category-criteria/category-criteria.component';
import { ClientComponent } from './modules/admin/client/client.component';
import { FutureResourceComponent } from './modules/delivery-management/delivery/available-resource-tab/future-resource/future-resource.component';
import { PlanResourceComponent } from './modules/delivery-management/delivery/available-resource-tab/plan-resource/plan-resource.component';
import { ProjectTimesheetComponent } from './modules/pm-management/list-project/list-project-detail/project-timesheet/project-timesheet.component';
import { AvailableResourceTabComponent } from './modules/delivery-management/delivery/available-resource-tab/available-resource-tab.component';
import { ResourceRequestDetailComponent } from './modules/delivery-management/delivery/request-resource-tab/resource-request-detail/resource-request-detail.component';
import { RequestResourceTabComponent } from './modules/delivery-management/delivery/request-resource-tab/request-resource-tab.component';
import { WeeklyReportTabDetailComponent } from './modules/delivery-management/delivery/weekly-report-tab/weekly-report-tab-detail/weekly-report-tab-detail.component';
import { WeeklyReportTabComponent } from './modules/delivery-management/delivery/weekly-report-tab/weekly-report-tab.component';
import { SaoDoProjectDetailComponent } from './modules/saodo-management/sao-do/sao-do-detail/sao-do-project-detail/sao-do-project-detail.component';
import { InvoiceComponent } from './modules/timesheet/invoice/invoice.component';
import { SaoDoDetailComponent } from './modules/saodo-management/sao-do/sao-do-detail/sao-do-detail.component';
import { DeliveryComponent } from './modules/delivery-management/delivery/delivery.component';
import { ProjectChecklistComponent } from './modules/pm-management/list-project/list-project-detail/project-checklist/project-checklist.component';
import { WeeklyReportComponent } from './modules/pm-management/list-project/list-project-detail/weekly-report/weekly-report.component';
import { MilestoneComponent } from './modules/pm-management/list-project/list-project-detail/milestone/milestone.component';
import { ResourceManagementComponent } from './modules/pm-management/list-project/list-project-detail/resource-management/resource-management.component';
import { ListProjectDetailComponent } from './modules/pm-management/list-project/list-project-detail/list-project-detail.component';
import { TimesheetDetailComponent } from './modules/timesheet/timesheet-detail/timesheet-detail.component';
import { ListProjectComponent } from './modules/pm-management/list-project/list-project.component';
import { ChecklistComponent } from './modules/checklist-management/checklist/checklist.component';
import { ChecklistTitleComponent } from './modules/checklist-management/checklist-title/checklist-title.component';
import { TimesheetComponent } from './modules/timesheet/timesheet.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { SaoDoComponent } from './modules/saodo-management/sao-do/sao-do.component';
import { ListProjectGeneralComponent } from './modules/pm-management/list-project/list-project-detail/list-project-general/list-project-general.component';
import { SkillComponent } from './modules/admin/skill/skill.component';
import { ConfigurationComponent } from './modules/admin/configuration/configuration.component';
import { CurrencyComponent } from './modules/admin/currency/currency.component';
import { AllResourceComponent } from './modules/delivery-management/delivery/available-resource-tab/all-resource/all-resource.component';
import { TrainingProjectsComponent } from './modules/pm-management/training-projects/training-projects.component';


@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "",
        component: AppComponent,
        children: [
          {
            path: "home",
            component: HomeComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "users",
            component: UsersComponent,
            data: { permission: "Pages.Users" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "roles",
            component: RolesComponent,
            data: { permission: "Pages.Roles" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "tenants",
            component: TenantsComponent,
            data: { permission: "Pages.Tenants" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "clients",
            component: ClientComponent,
            data: { permission: "" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "configurations",
            component: ConfigurationComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "currency",
            component: CurrencyComponent,
            data: { permission: "" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "skills",
            component: SkillComponent,
            data: { permission: "" },
            canActivate: [AppRouteGuard],
          },
          { path: "about", component: AboutComponent },
          { path: "update-password", component: ChangePasswordComponent },
          // timesheet
          {
            path: "timesheet",
            component: TimesheetComponent,
            data: { permission: "" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "invoice",
            component: InvoiceComponent,
            data: { permission: "" },
            canActivate: [AppRouteGuard],
          },

          {
            path: "checklist-title",
            component: ChecklistTitleComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "checklist",
            component: ChecklistComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "sao-do",
            component: SaoDoComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "list-project",
            component: ListProjectComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "training-projects",
            component: TrainingProjectsComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "product-projects",
            component: ProductProjectsComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "timesheetDetail",
            component: TimesheetDetailComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "list-project-detail",
            component: ListProjectDetailComponent,
            canActivate: [AppRouteGuard],
            children: [
                {
                    path: "list-project-general",
                    component: ListProjectGeneralComponent,
                    canActivate: [AppRouteGuard],
              },
              {
                path: "resourcemanagement",
                component: ResourceManagementComponent,
                canActivate: [AppRouteGuard],
              },
              {
                path: "milestone",
                component: MilestoneComponent,
                canActivate: [AppRouteGuard],
              },
              {
                path: "weeklyreport",
                component: WeeklyReportComponent,
                canActivate: [AppRouteGuard],
              },
              {
                path: "projectchecklist",
                component: ProjectChecklistComponent,
                canActivate: [AppRouteGuard],
              },
              {
                path: "timesheet-tab",
                component: ProjectTimesheetComponent,
                canActivate: [AppRouteGuard]
              },
              {
                path: "description-tab",
                component: ProjectDescriptionComponent,
                canActivate: [AppRouteGuard]
              }
            ],
          },
          {
            path: "weekly-report",
            component: WeeklyReportTabComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "resource-request",
            component: RequestResourceTabComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "available-resource",
            component: AvailableResourceTabComponent,
            canActivate: [AppRouteGuard],
            children:[
              {
                path:"pool",
                component:PlanResourceComponent,
                canActivate:[AppRouteGuard],
              },
              {
                path:"future-resource",
                component:FutureResourceComponent,
                canActivate:[AppRouteGuard],
              },
              {
                path:"all-resource",
                component:AllResourceComponent,
                canActivate:[AppRouteGuard],
              }
            ]
          },
          {
            path:"tags",
            component:TagsComponent,
            canActivate:[AppRouteGuard]
          },
          {
            path:"phase",
            component: PhaseComponent,
            canActivate:[AppRouteGuard]
          },
          {
            path:"setup-reviewer",
            component: SetUpReviewerComponent,
            canActivate:[AppRouteGuard]
          },
          {
            path:"result-reviewer",
            component: ResultReviewerComponent,
            canActivate:[AppRouteGuard]
          },
          {
            path:"review-user",
            component: ReviewUserComponent,
            canActivate:[AppRouteGuard]
          },
          {
            path:"review-yourself",
            component: ReviewYourselfComponent,
            canActivate:[AppRouteGuard]
          },
          {
            path:"category-criteria",
            component: CategoryCriteriaComponent,
            canActivate:[AppRouteGuard]
          },
          {
            path:"criteria",
            component: CriteriaComponent,
            canActivate:[AppRouteGuard]
          },
          {
            path: "resourceRequestDetail",
            component: ResourceRequestDetailComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "weeklyReportTabDetail",
            component: WeeklyReportTabDetailComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "saodoDetail",
                component: SaoDoDetailComponent,
                canActivate: [AppRouteGuard],
          },
          {
            path: "saodoProjectDetail",
                component: SaoDoProjectDetailComponent,
                canActivate: [AppRouteGuard],
          }
        ]
      }
    ])
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
