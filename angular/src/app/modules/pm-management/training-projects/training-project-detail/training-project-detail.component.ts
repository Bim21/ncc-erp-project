import { AppComponentBase } from '@shared/app-component-base';
import { PERMISSIONS_CONSTANT } from './../../../../constant/permission.constant';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-training-project-detail',
  templateUrl: './training-project-detail.component.html',
  styleUrls: ['./training-project-detail.component.css']
})
export class TrainingProjectDetailComponent extends AppComponentBase implements OnInit {
  PmManager_CanViewMenu_Milestone=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_Milestone;
  PmManager_CanViewMenu_ProjectChecklist=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_ProjectChecklist;
  PmManager_CanViewMenu_ResourceManagement=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_ResourceManagement;
  PmManager_CanViewMenu_Timesheet=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_Timesheet;
  PmManager_CanViewMenu_WeeklyReport=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_WeeklyReport;
  PmManager_Project_ViewProjectInfor=PERMISSIONS_CONSTANT.PmManager_Project_ViewProjectInfor
  PmManager_CanViewMenu_ProjectFile= PERMISSIONS_CONSTANT.PmManager_CanViewMenu_ProjectFile
  currentUrl: string = "";
  requestId: string = "";
  projectName:string;
  projectCode:string;
  constructor(public router : Router,
    public injector : Injector,
    private route: ActivatedRoute) { super(injector)}

  ngOnInit(): void {
    this.currentUrl =this.router.url
    this.router.events.subscribe(res => this.currentUrl = this.router.url)
    this.requestId = this.route.snapshot.queryParamMap.get("id");
    this.projectName = this.route.snapshot.queryParamMap.get("projectName");
    this.projectCode = this.route.snapshot.queryParamMap.get("projectCode");
  }
  public routingGeneralTab(){
    this.router.navigate(['training-project-general'],{
      relativeTo:this.route, queryParams:{
        id:this.requestId,
        projectName: this.projectName,
        projectCode:this.projectCode
      },
    })
  }

  public routingResourceTab() {
    this.router.navigate(['training-resource-management'], {
      relativeTo: this.route, queryParams: {
        id:this.requestId,
        projectName: this.projectName,
        projectCode:this.projectCode
      },
      // replaceUrl: true
    })
    
  }

 
  public routingMilestoneTab() {
    this.router.navigate(['training-milestone'], {
      relativeTo: this.route, queryParams: {
        id:this.requestId,
        projectName: this.projectName,
        projectCode:this.projectCode
      },
      // replaceUrl: true
    })
  }
  public routingWeeklyReportTab(){
    this.router.navigate(['training-weekly-report'], {
      relativeTo: this.route, queryParams: {
        id:this.requestId,
        projectName: this.projectName,
        projectCode:this.projectCode
      },
      // replaceUrl: true
    })
  }
  public routingProjectChecklistTab(){
    this.router.navigate(['training-project-checklist'], {
      relativeTo: this.route, queryParams: {
        id:this.requestId,
        projectName: this.projectName,
        projectCode:this.projectCode
      },
      // replaceUrl: true
    })
  }
  public routingTimesheetTab(){
    this.router.navigate(['training-timesheet-tab'], {
      relativeTo: this.route, queryParams: {
        id:this.requestId,
        projectName: this.projectName,
        projectCode:this.projectCode
      },
      // replaceUrl: true
    })
  }
  
  public routingDescriptionTab(){
    this.router.navigate(['training-description-tab'],{
      relativeTo: this.route, queryParams:{
        id:this.requestId,
        projectName: this.projectName,
        projectCode:this.projectCode
      }
    })
  }
  public routingFileTab(){
    this.router.navigate(['project-file-tab'],{
      relativeTo: this.route, queryParams:{
        id:this.requestId,
        projectName: this.projectName,
        projectCode:this.projectCode
      }
    })
  }

}
