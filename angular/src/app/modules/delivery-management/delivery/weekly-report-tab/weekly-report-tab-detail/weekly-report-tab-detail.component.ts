import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { ProjectResourceRequestService } from './../../../../../service/api/project-resource-request.service';
import { MatDialog } from '@angular/material/dialog';
import { ApproveDialogComponent } from './../../../../pm-management/list-project/list-project-detail/weekly-report/approve-dialog/approve-dialog.component';
import { UserService } from './../../../../../service/api/user.service';
import { ProjectUserService } from './../../../../../service/api/project-user.service';
import { projectUserDto } from './../../../../../service/model/project.dto';
import { PmReportService } from './../../../../../service/api/pm-report.service';
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
import * as moment from 'moment';

@Component({
  selector: 'app-weekly-report-tab-detail',
  templateUrl: './weekly-report-tab-detail.component.html',
  styleUrls: ['./weekly-report-tab-detail.component.css']
})
export class WeeklyReportTabDetailComponent extends PagedListingComponentBase<WeeklyReportTabDetailComponent> implements OnInit {
  DeliveryManagement_PMReportProject=PERMISSIONS_CONSTANT.DeliveryManagement_PMReport_CloseReport;
  DeliveryManagement_PMReportProject_Create=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_Create;
  DeliveryManagement_PMReportProject_Delete=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_Delete;
  DeliveryManagement_PMReportProject_GetAll=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_GetAll;
  DeliveryManagement_PMReportProject_Update=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_Update;
  DeliveryManagement_PMReportProject_GetAllByPmProject=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_GetAllByPmProject;
  DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek;
  DeliveryManagement_PMReportProject_ResourceChangesInTheFuture=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_ResourceChangesInTheFuture;
  DeliveryManagement_PMReportProject_SendReport=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_SendReport;
  DeliveryManagement_PMReportProjectIssue=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue;
  DeliveryManagement_PMReportProjectIssue_Create=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue_Create;
  DeliveryManagement_PMReportProjectIssue_Delete=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue_Delete;
  DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek;
  DeliveryManagement_PMReportProjectIssue_Update=PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue_Update;
  DeliveryManagement_ResourceRequest_ApproveUser = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_ApproveUser;
  DeliveryManagement_ResourceRequest_RejectUser = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_RejectUser
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
  public itemPerPage:number =5;
  public weeklyCurrentPage:number = 1;
  public futureCurrentPage:number=1;
  public problemCurrentPage:number=1;
  public searchText = "";
  public pmReportProjectList: pmReportProjectDto[] = [];
  public tempPmReportProjectList: pmReportProjectDto[] = [];
  public show: boolean = false;
  public pmReportProject = {} as pmReportProjectDto;
  public pmReportId: any;
  public isActive="";
  public weeklyPeportList: projectReportDto[] = [];
  public futureReportList: projectReportDto[] = [];
  public problemList: projectProblemDto[] = [];
  public problemIssueList: string[] = Object.keys(this.APP_ENUM.ProjectHealth);
  public projectRoleList: string[] = Object.keys(this.APP_ENUM.ProjectUserRole);
  public isssueStatusList: string[] = Object.keys(this.APP_ENUM.PMReportProjectIssueStatus)
  public activeReportId: number;
  public projectHealth;
  public pmReportProjectId;
  public isEditWeeklyReport:boolean=false;
  public isEditFutureReport:boolean=false;
  public isEditProblem:boolean=false;
  public minDate=new Date();




  constructor(private pmReportProjectService: PMReportProjectService,
    private reportIssueService: PmReportIssueService, private pmReportService: PmReportService,
    public route: ActivatedRoute,
    injector: Injector,
    private projectUserService: ProjectUserService,
    private userService: UserService,
    private dialog: MatDialog,
    private requestservice: ProjectResourceRequestService,
  ) {
    super(injector)
  }

  ngOnInit(): void {
    this.pmReportId = this.route.snapshot.queryParamMap.get('id');
    this.isActive=this.route.snapshot.queryParamMap.get('isActive');
    this.minDate.setDate(this.minDate.getDate()+1)
    this.getPmReportProject();
    this.getActiveReport();
    this.getUser();
   
  }
  public getPmReportProject(): void {
    this.pmReportProjectService.GetAllByPmReport(this.pmReportId, {}).subscribe((data => {
      this.pmReportProjectList = data.result;
      this.tempPmReportProjectList = data.result;
      this.getActiveReport()
      if (localStorage.getItem('read') && JSON.parse(localStorage.getItem('read')).reportId == this.pmReportId) {
        this.pmReportProjectList = JSON.parse(localStorage.getItem('read')).pmProjectList;
      }

    }))
  }
  private getActiveReport() {
    this.pmReportService.getAll().subscribe(data => {
      this.activeReportId = data.result.filter(item=>item.isActive==true)[0].id;
      this.view(this.pmReportProjectList[0].projectId);
    })
    
  }

  
 public projectId;
  view(item?) {
    this.projectId=item;
    this.pmReportProjectList.forEach(element => {
      if(element.projectId==item){
        element.setBackground=true;
      }else{
        element.setBackground=false;
      }
    });

    
    this.getWeeklyReport();
    this.getFuturereport();
    this.getProjectProblem()
    
    
  }
  getWeeklyReport(){
    this.pmReportProjectService.getChangesDuringWeek(this.projectId,this.pmReportId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
      this.weeklyPeportList = data.result;
    })
  }
  getFuturereport(){
    this.pmReportProjectService.getChangesInFuture(this.projectId,this.pmReportId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
      this.futureReportList = data.result;
    })
  }
  getProjectProblem(){
    this.pmReportProjectService.problemsOfTheWeekForReport(this.projectId,this.pmReportId).pipe(catchError(this.reportIssueService.handleError)).subscribe(data => {
      if(data.result){
        this.problemList = data.result.result;
      }else{
        this.problemList =[];
      }
      this.pmReportProjectId=data.result.pmReportProjectId;
      this.projectHealth= data.result.projectHealth;

    })
  }
  search() {
    this.pmReportProjectList = this.tempPmReportProjectList.filter((item) => {
      return item.projectName.toLowerCase().includes(this.searchText.toLowerCase()) ||
        item.projectName?.toLowerCase().includes(this.searchText.toLowerCase());
    });

  }
  tick(project){
    project.createMode=true;
    let item= {pmProjectList: this.pmReportProjectList,
    reportId: this.pmReportId}
    localStorage.setItem("read",JSON.stringify(item))
  }
  updateHealth(projectHealth){
    this.pmReportProjectService.updateHealth(this.pmReportProjectId,projectHealth).subscribe((data)=>{
      this.view(this.projectId);
    })

  }
  //weekly
  public addWeekReport() {
    let newReport = {} as projectUserDto
    newReport.createMode = true;
    this.weeklyPeportList.unshift(newReport)
    this.processWeekly = true;
  }
  public saveWeekReport(report: projectReportDto) {
    // report.isFutureActive = false
    report.projectId = this.projectId
    report.isExpense = true;
    report.status = "0";
    report.startTime = moment(report.startTime).format("YYYY-MM-DD");
    delete report["createMode"]
    if(this.isEditWeeklyReport==true){
      this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        report.startTime = moment(report.startTime).format("YYYY-MM-DD")
        this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
          abp.notify.success(`updated user: ${report.userName}`);
          this.getWeeklyReport();
          this.isEditFutureReport = false;
          this.processWeekly = false;
        })
      },
        () => {
          report.createMode = true
        })
    }
      else{
        this.projectUserService.create(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
          abp.notify.success("created new weekly report");
          this.processWeekly = false;
          report.createMode = false;
          this.getWeeklyReport();
        },
          () => {
            report.createMode = true
          })
      }
    

  }
  public cancelWeekReport() {
    this.processWeekly = false;
    this.getWeeklyReport();
  }
  updateWeekReport(report){
    this.processWeekly = true
    this.isEditWeeklyReport = true;
    report.createMode = true;
    report.projectRole = this.APP_ENUM.ProjectUserRole[report.projectRole]
  }
  deleteWeekReport(report){
    
    abp.message.confirm(
      "Delete Issue? ",
      "",
      (result: boolean) => {
        if (result) {
          this.projectUserService.removeProjectUser(report.id).pipe(catchError(this.projectUserService.handleError)).subscribe(() => {
            abp.notify.success("Deleted Report");
            this.getWeeklyReport();
          });
        }
      }
    );
  }
  

  //Future
  public getUser(): void {
    this.userService.GetAllUserActive(true).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.userList = data.result;
    })
  }
  public addFutureReport() {
    let newReport = {} as projectUserDto
    newReport.createMode = true;
    this.futureReportList.unshift(newReport)
    this.processFuture = true;
  }
  public saveFutureReport(report: projectReportDto) {
    delete report["createMode"]
    if (this.isEditFutureReport) {
      this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        report.startTime = moment(report.startTime).format("YYYY-MM-DD")
        this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
          abp.notify.success(`updated user: ${report.userName}`);
          this.getFuturereport();
          this.isEditFutureReport = false;
          this.processFuture = false
        })
      },
        () => {
          report.createMode = true
        })
    }
    else {
      // report.isFutureActive = false
      report.projectId = this.projectId
      report.isExpense = true;
      report.status = "2";
      report.startTime = moment(report.startTime).format("YYYY-MM-DD");

      this.projectUserService.create(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        abp.notify.success("created new future report");
        this.processFuture = false;
        report.createMode = false;
        this.isEditFutureReport = false
        this.getFuturereport();
      },
        () => {
          report.createMode = true
        })
    }
  }
  public cancelFutureReport() {
    this.processFuture = false;
    this.getFuturereport();
  }
  public approveRequest(resource: projectUserDto): void {
    this.showDialog(resource);

  }
  showDialog(resource: any): void {
    let dialogData = {}
    dialogData = {
      id: resource.id,
      userId: resource.userId,
      projectRole: resource.projectRole,
      startTime: resource.startTime,
      allocatePercentage: resource.allocatePercentage
    }
    const dialogRef = this.dialog.open(ApproveDialogComponent, {
      data: {
        dialogData: dialogData,
      },
      width: "700px",
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getFuturereport();
        this.getWeeklyReport();
      }
    });

  }
  
  public rejectRequest(report): void {
    this.requestservice.rejectRequest(report.id).pipe(catchError(this.requestservice.handleError)).subscribe(data => {
      abp.notify.success("Rejected request")
      this.getFuturereport();
    })
  }
  public updateRequest(report): void {
    this.processFuture = true
    this.isEditFutureReport = true;
    report.createMode = true;
    report.projectRole = this.APP_ENUM.ProjectUserRole[report.projectRole]
  }
  //Problem
  public addIssueReport() {
    let newIssue = {} as projectProblemDto
    newIssue.createMode = true;
    this.problemList.unshift(newIssue)
    this.processProblem = true;
  }
  public saveProblemReport(problem: projectProblemDto) {
    problem.createdAt = moment(problem.createdAt).format("YYYY-MM-DD");
    delete problem["createMode"]
    if (!this.isEditProblem) {
      this.reportIssueService.createReportIssue(this.projectId,problem).pipe(catchError(this.reportIssueService.handleError)).subscribe(data => {
        abp.notify.success("created new Issue");
        this.processProblem = false;
        problem.createMode = false;
        this.getProjectProblem();
      },
        () => {
          problem.createMode = true
        })
    }
    else {
      this.reportIssueService.update(problem).pipe(catchError(this.reportIssueService.handleError)).subscribe(data => {
        abp.notify.success("edited Issue");
        this.processProblem = false;
        problem.createMode = false;
        this.getProjectProblem();
        this.isEditProblem = false;
      },
        () => {
          problem.createMode = true
        })
    }

  }
  public cancelProblemReport() {
    this.processProblem = false;
    this.getProjectProblem();
  }
  public editProblemReport(user: projectUserDto) {
    this.isEditProblem = true;
    user.createMode = true
    user.status = this.APP_ENUM.ProjectUserStatus[user.status]
    user.projectRole = this.APP_ENUM.ProjectUserRole[user.projectRole]
    // this.projectUserProcess = true
  }

  public deleteProblem(problem) {
    abp.message.confirm(
      "Delete Issue? ",
      "",
      (result: boolean) => {
        if (result) {
          this.reportIssueService.deleteReportIssue(problem.id).pipe(catchError(this.reportIssueService.handleError)).subscribe(() => {
            abp.notify.success("Deleted Issue ");
            this.getProjectProblem();
          });
        }
      }
    );
  }
  public updateReportIssue(Issue): void {
    this.processProblem = true
    this.isEditProblem = true;
    Issue.createMode = true
    Issue.status= this.APP_ENUM.PMReportProjectIssueStatus[Issue.status]
    
  }
  


}
