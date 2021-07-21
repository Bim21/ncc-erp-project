import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { pmReportDto } from './../../../../../service/model/pmReport.dto';
import { ApproveDialogComponent } from './approve-dialog/approve-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ProjectResourceRequestService } from './../../../../../service/api/project-resource-request.service';
import { result } from 'lodash-es';
import { PmReportIssueService } from './../../../../../service/api/pm-report-issue.service';
import { projectUserDto } from './../../../../../service/model/project.dto';
import { UserService } from './../../../../../service/api/user.service';
import { UserDto } from './../../../../../../shared/service-proxies/service-proxies';
import { catchError } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { PMReportProjectService } from './../../../../../service/api/pmreport-project.service';
import { projectReportDto, projectProblemDto } from './../../../../../service/model/projectReport.dto';
import { Component, OnInit, Injector } from '@angular/core';
import { ProjectUserService } from '@app/service/api/project-user.service';
import * as moment from 'moment';
import { PmReportService } from '@app/service/api/pm-report.service';

@Component({
  selector: 'app-weekly-report',
  templateUrl: './weekly-report.component.html',
  styleUrls: ['./weekly-report.component.css']
})
export class WeeklyReportComponent extends AppComponentBase implements OnInit {
  // Paging
  public itemPerPage: number = 5;
  public searchUser: string = "";
  public weeklyCurrentPage: number = 1;
  public futureCurrentPage: number = 1;
  public problemCurrentPage: number = 1;
  public processWeekly: boolean = false;
  public processFuture: boolean = false;
  public processProblem: boolean = false;
  public searchPmReport:string="";
  public activeReportId= {} as pmReportDto;

  public pmReportList: any = [];
  public weeklyPeportList: projectReportDto[] = [];
  public futureReportList: projectReportDto[] = [];
  public problemList: projectProblemDto[] = [];
  public isEditProblem: boolean = false;
  public isEditFutureReport: boolean = false;
  public minDate = new Date();
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

  public isssueStatusList: string[] = Object.keys(this.APP_ENUM.PMReportProjectIssueStatus)
  public userList: UserDto[] = [];
  public projectRoleList: string[] = Object.keys(this.APP_ENUM.ProjectUserRole);
  public projectHeathList: string[] = Object.keys(this.APP_ENUM.ProjectHealth);
  private projectId: number;
  constructor(injector: Injector, private reportService: PMReportProjectService, private route: ActivatedRoute, private requestservice: ProjectResourceRequestService,
    private projectUserService: ProjectUserService, private userService: UserService, private reportIssueService: PmReportIssueService, private dialog: MatDialog,
    private pmreportService:PmReportService) {
    super(injector);
    this.projectId = Number(route.snapshot.queryParamMap.get("id"));
  }

  ngOnInit(): void {
   
    this.getAllPmReport();
    this.getUser();
    this.minDate.setDate(this.minDate.getDate()+1)
  }
  public getWeeklyReport() {
    this.reportService.getChangesDuringWeek(this.projectId,this.activeReportId.id).pipe(catchError(this.reportService.handleError)).subscribe(data => {
      this.weeklyPeportList = data.result;
    })
  }
  
  public getFuturereport(): void {
    if(this.permission.isGranted(this.DeliveryManagement_PMReportProject_ResourceChangesInTheFuture)){
      this.reportService.getChangesInFuture(this.projectId,this.activeReportId.id).pipe(catchError(this.reportService.handleError)).subscribe(data => {
        this.futureReportList = data.result
      })
    }

    
  }
  public getUser(): void {
    this.userService.GetAllUserActive(true).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.userList = data.result;
    })
  }
  private getProjectProblem(): void {
    if(this.permission.isGranted(this.DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek)){
      this.reportIssueService.getProblemsOfTheWeek(this.projectId,this.activeReportId.id).pipe(catchError(this.reportIssueService.handleError)).subscribe(data => {
        this.problemList = data.result;
      })
    }
    
  }
  // Weekly report
  public addWeekReport() {
    let newReport = {} as projectUserDto
    newReport.createMode = true;
    this.weeklyPeportList.unshift(newReport)
    this.processWeekly = true;
  }
  public saveWeekReport(report: projectReportDto) {
    report.isFutureActive = false
    report.projectId = this.projectId
    report.isExpense = true;
    report.status = "0";
    report.startTime = moment(report.startTime).format("YYYY-MM-DD");
    delete report["createMode"]
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
  public cancelWeekReport() {
    this.processWeekly = false;
    this.getWeeklyReport();
  }

  // Future Report
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
      report.isFutureActive = false
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
  // Project Issue
  public getAllPmReport() {
    this.pmreportService.getAll().pipe(catchError(this.reportService.handleError)).subscribe(data => {
      this.pmReportList = data.result;
      this.activeReportId= this.pmReportList.filter(item=>item.isActive==true)[0];
      this.getWeeklyReport();
      this.getFuturereport();
      this.getProjectProblem();
    })
  }

  public addIssueReport() {
    let newIssue = {} as projectProblemDto
    newIssue.createMode = true;
    this.problemList.unshift(newIssue)
    this.processProblem = true;
  }
  public saveProblemReport(problem: projectProblemDto) {
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
  public onReportchange(){
    this.getWeeklyReport();
    this.getFuturereport();
    this.getProjectProblem();
  }
  public sendWeeklyreport(){
    abp.message.confirm(
      `send report ${this.activeReportId.name}? `,
      "",
      (result: boolean) => {
        if (result) {
          this.reportService.sendReport(this.projectId,this.activeReportId.id).pipe(catchError(this.reportService.handleError)).subscribe(data=>{
              abp.notify.success("Send report successful");
          })
        }
      }
    );



    
  }
}
