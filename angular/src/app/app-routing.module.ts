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


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent, canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent },
                    { path: 'update-password', component: ChangePasswordComponent },
                    // timesheet
                    { path: 'timesheet', component: TimesheetComponent, data: { permission: '' }, canActivate: [AppRouteGuard] },

                    { path: 'checklist-title', component: ChecklistTitleComponent, canActivate: [AppRouteGuard]  },
                    { path: 'checklist', component: ChecklistComponent, canActivate: [AppRouteGuard]  },
                    { path: 'sao-do', component: SaoDoComponent, canActivate: [AppRouteGuard]  },
                    { path: 'list-project', component: ListProjectComponent, canActivate: [AppRouteGuard]  },
                    { path: 'timesheetDetail', component: TimesheetDetailComponent, canActivate: [AppRouteGuard]  },
                    { path: 'list-project-detail', component: ListProjectDetailComponent, canActivate: [AppRouteGuard] ,
                    children: [{
                        path: "resourcemanagement",
                        component: ResourceManagementComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "milestone",
                        component: MilestoneComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "weeklyreport",
                        component: WeeklyReportComponent,
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: "projectchecklist",
                        component: ProjectChecklistComponent,
                        canActivate: [AppRouteGuard]
                    }],
                    
                    
                }

                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
