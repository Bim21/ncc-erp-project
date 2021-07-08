import { projectUserDto } from './../../../../../service/model/project.dto';
import { UserService } from './../../../../../service/api/user.service';
import { UserDto } from './../../../../../../shared/service-proxies/service-proxies';
import { catchError } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { PMReportProjectService } from './../../../../../service/api/pmreport-project.service';
import { projectReportDto } from './../../../../../service/model/projectReport.dto';
import { Component, OnInit, Injector } from '@angular/core';
import { ProjectUserService } from '@app/service/api/project-user.service';
import * as moment from 'moment';

@Component({
  selector: 'app-weekly-report',
  templateUrl: './weekly-report.component.html',
  styleUrls: ['./weekly-report.component.css']
})
export class WeeklyReportComponent extends AppComponentBase implements OnInit {
  // Paging
  public itemPerPage:number =5;
  public searchUser:string ="";
  public weeklyCurrentPage:number=1;
  public futureCurrentPage:number=1;
  
  public weeklyPeportList: projectReportDto[] = [];
  public futureReportList: projectReportDto[] = [];
  public userList:UserDto[]=[];
  public inProcess:boolean=false;
  public projectRoleList:string[] = Object.keys( this.APP_ENUM.ProjectUserRole);
  public projectHeathList:string[]= Object.keys( this.APP_ENUM.ProjectHealth);
  private projectId: number;
  constructor(injector: Injector, private reportService: PMReportProjectService, private route: ActivatedRoute,
     private projectUserService:ProjectUserService, private userService:UserService) { 
    super(injector);
    this.projectId = Number(route.snapshot.queryParamMap.get("id"));
  }

  ngOnInit(): void {
    this.getWeeklyReport();
    this.getFuturereport();
    this.getUser();
  }
  public getWeeklyReport() {
    this.reportService.getChangesDuringWeek(this.projectId).pipe(catchError(this.reportService.handleError)).subscribe(data => {
      this.weeklyPeportList = data.result;
    })
  }
  public getFuturereport(): void {
    this.reportService.getChangesInFuture(this.projectId).pipe(catchError(this.reportService.handleError)).subscribe(data => {
      this.futureReportList = data.result
    })
  }
  public getUser():void{
      this.userService.getAll().pipe(catchError(this.userService.handleError)).subscribe(data=>{
        this.userList =data.result.items;
      })
  }
  // Weekly report
  public addWeekReport() {
    let newReport = {} as projectUserDto
    newReport.createMode = true;
    this.weeklyPeportList.unshift(newReport)
    this.inProcess = true;
  }
  public saveWeekReport(report:projectReportDto){
    report.isFutureActive = false
    report.projectId = this.projectId
    report.isExpense = true;
    report.status = "0";
    report.startTime = moment(report.startTime).format("YYYY-MM-DD");
    delete report["createMode"]
    this.projectUserService.create(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data=>{
      abp.notify.success("created new weekly report");
      this.inProcess=false;
      report.createMode =false;
      this.getWeeklyReport();
    },
    () => {
      report.createMode = true
    })

  }
  public cancelWeekReport(){
    this.inProcess =false;
    this.getWeeklyReport();
  }

  // Future Report
  public addFutureReport() {
    let newReport = {} as projectUserDto
    newReport.createMode = true;
    this.futureReportList.unshift(newReport)
    this.inProcess = true;
  }
  public saveFutureReport(report:projectReportDto){
    report.isFutureActive = false
    report.projectId = this.projectId
    report.isExpense = true;
    report.status = "2";
    report.startTime = moment(report.startTime).format("YYYY-MM-DD");
    delete report["createMode"]
    this.projectUserService.create(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data=>{
      abp.notify.success("created new future report");
      this.inProcess=false;
      report.createMode =false;
      this.getFuturereport();
    },
    () => {
      report.createMode = true
    })

  }
  public cancelFutureReport(){
    this.inProcess =false;
    this.getFuturereport();
  }

}
