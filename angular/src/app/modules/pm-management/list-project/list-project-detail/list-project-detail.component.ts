import { AppComponentBase } from 'shared/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-list-project-detail',
  templateUrl: './list-project-detail.component.html',
  styleUrls: ['./list-project-detail.component.css']
})
export class ListProjectDetailComponent extends AppComponentBase implements OnInit {
  requestId: any
  currentUrl: string = ""
  constructor(private route: ActivatedRoute, private router: Router, injector:Injector) {
    super(injector)
  }

  ngOnInit(): void {
    this.router.events.subscribe(res => this.currentUrl = this.router.url)
    this.requestId = this.route.snapshot.queryParamMap.get("id");
    this.router.navigate(['list-project-general'],{
      relativeTo:this.route, queryParams:{
        id:this.requestId
      },
      replaceUrl:true
    })
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

}
