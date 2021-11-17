import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-training-project-detail',
  templateUrl: './training-project-detail.component.html',
  styleUrls: ['./training-project-detail.component.css']
})
export class TrainingProjectDetailComponent implements OnInit {
  currentUrl: string = "";
  requestId: string = "";
  constructor(public router : Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.currentUrl =this.router.url
    this.router.events.subscribe(res => this.currentUrl = this.router.url)
    this.requestId = this.route.snapshot.queryParamMap.get("id");
 
  }
  public routingGeneralTab(){
    this.router.navigate(['training-project-general'],{
      relativeTo:this.route, queryParams:{
        id:this.requestId
      },
    })
  }

  public routingResourceTab() {
    this.router.navigate(['training-resource-management'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
    
  }

 
  public routingMilestoneTab() {
    this.router.navigate(['training-milestone'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  public routingWeeklyReportTab(){
    this.router.navigate(['training-weekly-report'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  public routingProjectChecklistTab(){
    this.router.navigate(['training-project-checklist'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  public routingTimesheetTab(){
    this.router.navigate(['training-timesheet-tab'], {
      relativeTo: this.route, queryParams: {
        id: this.requestId
      },
      // replaceUrl: true
    })
  }
  
  public routingDescriptionTab(){
    this.router.navigate(['training-description-tab'],{
      relativeTo: this.route, queryParams:{
        id:this.requestId
      }
    })
  }

}
