import { AppComponentBase } from '@shared/app-component-base';
import { PERMISSIONS_CONSTANT } from './../../../../constant/permission.constant';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-product-project-detail',
  templateUrl: './product-project-detail.component.html',
  styleUrls: ['./product-project-detail.component.css']
})
export class ProductProjectDetailComponent extends AppComponentBase implements OnInit {
  PmManager_CanViewMenu_Milestone=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_Milestone;
  PmManager_CanViewMenu_ProjectChecklist=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_ProjectChecklist;
  PmManager_CanViewMenu_ResourceManagement=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_ResourceManagement;
  PmManager_CanViewMenu_Timesheet=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_Timesheet;
  PmManager_CanViewMenu_WeeklyReport=PERMISSIONS_CONSTANT.PmManager_CanViewMenu_WeeklyReport;
  PmManager_Project_ViewProjectInfor=PERMISSIONS_CONSTANT.PmManager_Project_ViewProjectInfor
  public currentUrl: string= '';
  requestId: string = "";
  constructor(public router : Router,
    public injector : Injector,
    private route: ActivatedRoute) {super(injector) }

  ngOnInit(): void {
    this.currentUrl =this.router.url
    this.router.events.subscribe(res => this.currentUrl = this.router.url)
    this.requestId = this.route.snapshot.queryParamMap.get("id");
 
  }
  public routingGeneralTab(){
    this.router.navigate(['product-project-general'],{
      relativeTo:this.route, queryParams:{
        id:this.requestId
      },
    })
  }

  public routingResourceTab() {
    this.router.navigate(['product-resource-management'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
    
  }

 
  public routingMilestoneTab() {
    this.router.navigate(['product-milestone'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  public routingWeeklyReportTab(){
    this.router.navigate(['product-weekly-report'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  public routingProjectChecklistTab(){
    this.router.navigate(['product-project-checklist'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  public routingTimesheetTab(){
    this.router.navigate(['product-timesheet-tab'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  
  public routingDescriptionTab(){
    this.router.navigate(['product-description-tab'],{
      relativeTo: this.route, queryParams:{
        id:this.requestId
      }
    })
  }


}
