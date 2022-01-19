import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { AppComponentBase } from 'shared/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-list-project-detail',
  templateUrl: './list-project-detail.component.html',
  styleUrls: ['./list-project-detail.component.css']
})
export class ListProjectDetailComponent extends AppComponentBase implements OnInit {
  requestId: any;
  currentUrl: string = "";
  PmManager_CanViewMenu_Milestone=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_Milestone;
  PmManager_CanViewMenu_ProjectChecklist=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_ProjectChecklist;
  PmManager_CanViewMenu_ResourceManagement=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_ResourceManagement;
  PmManager_CanViewMenu_Timesheet=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_Timesheet;
  PmManager_CanViewMenu_WeeklyReport=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_WeeklyReport;
  PmManager_Project_ViewProjectInfor=PERMISSIONS_CONSTANT.PmManager_Project_ViewProjectInfor
  PmManager_CanViewMenu_ProjectFile =PERMISSIONS_CONSTANT.PmManager_CanViewMenu_ProjectFile

  constructor(private route: ActivatedRoute, private router: Router, injector:Injector) {
    super(injector)
  }

  ngOnInit(): void {
    this.currentUrl =this.router.url
    this.router.events.subscribe(res => this.currentUrl = this.router.url)
    this.requestId = this.route.snapshot.queryParamMap.get("id");
 
  }
  public routingGeneralTab(){
    this.router.navigate(['list-project-general'],{
      relativeTo:this.route, queryParams:{
        id:this.requestId
      },
    })
  }

  public routingResourceTab() {
    this.router.navigate(['resourcemanagement'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
    
  }

 
  public routingMilestoneTab() {
    this.router.navigate(['milestone'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  public routingWeeklyReportTab(){
    this.router.navigate(['weeklyreport'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  public routingProjectChecklistTab(){
    this.router.navigate(['projectchecklist'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  public routingTimesheetTab(){
    this.router.navigate(['timesheet-tab'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  
  public routingDescriptionTab(){
    this.router.navigate(['description-tab'],{
      relativeTo: this.route, queryParams:{
        id:this.requestId
      }
    })
  }
  public routingFileTab(){
    this.router.navigate(['project-file-tab'],{
      relativeTo: this.route, queryParams:{
        id:this.requestId
      }
    })
  }

}
