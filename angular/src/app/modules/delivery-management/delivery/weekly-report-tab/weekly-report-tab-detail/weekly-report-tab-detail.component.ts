import { APP_ENUMS } from './../../../../../../shared/AppEnums';
import { isNgTemplate } from '@angular/compiler';
import { PmReportIssueService } from './../../../../../service/api/pm-report-issue.service';
import { projectProblemDto, projectReportDto } from './../../../../../service/model/projectReport.dto';
import { result } from 'lodash-es';
import { finalize, catchError } from 'rxjs/operators';

import { ActivatedRoute } from '@angular/router';
import { pmReportProjectDto } from './../../../../../service/model/pmReport.dto';
import { PMReportProjectService } from './../../../../../service/api/pmreport-project.service';
import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { JsonHubProtocol } from '@aspnet/signalr';

@Component({
  selector: 'app-weekly-report-tab-detail',
  templateUrl: './weekly-report-tab-detail.component.html',
  styleUrls: ['./weekly-report-tab-detail.component.css']
})
export class WeeklyReportTabDetailComponent extends PagedListingComponentBase<WeeklyReportTabDetailComponent> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    // this.pmReportProjectService.GetAllByPmReport(this.pmReportId, request).pipe(finalize(()=>{
    //   finishedCallback();
    // }),catchError(this.pmReportProjectService.handleError)).subscribe((data)=>{
    //   this.pmReportProjectList=data.result.items;
    //   this.showPaging(data.result,pageNumber);
    // })
  }
  protected delete(entity: WeeklyReportTabDetailComponent): void {
    throw new Error('Method not implemented.');
  }
  public searchText="";
  public pmReportProjectList:pmReportProjectDto[]=[];
  public tempPmReportProjectList:pmReportProjectDto[]=[];
  public show:boolean=false;
  public pmReportProject={} as pmReportProjectDto;
  public pmReportId:any;
  public weeklyPeportList: projectReportDto[] = [];
  public futureReportList: projectReportDto[] = [];
  public problemList: projectProblemDto[] = [];
  public flagList: string[] = Object.keys(this.APP_ENUM.MilestoneFlag);



  constructor(private pmReportProjectService:PMReportProjectService,
    private reportIssueService:PmReportIssueService,
    
    public route: ActivatedRoute,
    injector:Injector,
    ) {super(injector) 
  }

  ngOnInit(): void {
    this.pmReportId=this.route.snapshot.queryParamMap.get('id');
    this.getPmReportProject();
    
    
    
  }

  public getPmReportProject():void{
    this.pmReportProjectService.GetAllByPmReport(this.pmReportId,{}).subscribe((data=>{
      this.pmReportProjectList=data.result;
      this.tempPmReportProjectList=data.result;
      if(localStorage.getItem('read') && JSON.parse(localStorage.getItem('read')).reportId==this.pmReportId){
        this.pmReportProjectList=JSON.parse(localStorage.getItem('read')).pmProjectList;
       }
      
    }))
  }
  public getWeeklyReport(item) {
    this.pmReportProjectService.getChangesDuringWeek(item).pipe(catchError(this.reportService.handleError)).subscribe(data => {
      this.weeklyPeportList = data.result;
    })
  }
 
  view(item){ 
    
    this.pmReportProjectService.getChangesDuringWeek(item).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
      this.weeklyPeportList = data.result;
    })
    
    this.pmReportProjectService.getChangesInFuture(item).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
          this.futureReportList = data.result;
    })
    this.reportIssueService.getProblemsOfTheWeek(item).pipe(catchError(this.reportIssueService.handleError)).subscribe(data => {
        this.problemList = data.result;
    })
  }
  search() {
    this.pmReportProjectList = this.tempPmReportProjectList.filter((item) => {
      return item.projectName.toLowerCase().includes(this.searchText.toLowerCase()) ||
        item.projectName?.toLowerCase().includes(this.searchText.toLowerCase());
    });

  }
  tick(){
    let item= {pmProjectList: this.pmReportProjectList,
    reportId: this.pmReportId}
    localStorage.setItem("read",JSON.stringify(item))
     
    
  }
  

}
