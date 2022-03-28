import { AddFutureResourceDialogComponent } from './../../../../delivery-management/delivery/weekly-report-tab/weekly-report-tab-detail/add-future-resource-dialog/add-future-resource-dialog.component';
import { GetTimesheetWorkingComponent, WorkingTimeDto } from './../../../../delivery-management/delivery/weekly-report-tab/weekly-report-tab-detail/get-timesheet-working/get-timesheet-working.component';
import { ListProjectService } from '@app/service/api/list-project.service';
import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { pmReportDto } from './../../../../../service/model/pmReport.dto';
import { ApproveDialogComponent } from './approve-dialog/approve-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ProjectResourceRequestService } from './../../../../../service/api/project-resource-request.service';
import { result } from 'lodash-es';
import { PmReportIssueService } from './../../../../../service/api/pm-report-issue.service';
import { projectUserDto, ProjectInfoDto } from './../../../../../service/model/project.dto';
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
import { UpdateUserSkillDialogComponent } from '@app/users/update-user-skill-dialog/update-user-skill-dialog.component';
import { ReleaseUserDialogComponent } from '../resource-management/release-user-dialog/release-user-dialog.component';
import { ConfirmPopupComponent } from '../resource-management/confirm-popup/confirm-popup.component';
import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';



// import { ApproveDialogComponent } from './../../../../pm-management/list-project/list-project-detail/weekly-report/approve-dialog/approve-dialog.component';


import { pmReportProjectDto } from './../../../../../service/model/pmReport.dto';

import {  ViewChild} from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import * as echarts from 'echarts';
import { RadioDropdownComponent } from '@shared/components/radio-dropdown/radio-dropdown.component';
import { LayoutStoreService } from '@shared/layout/layout-store.service';
import { MatMenuTrigger } from '@angular/material/menu';


@Component({
  selector: 'app-weekly-report',
  templateUrl: './weekly-report.component.html',
  styleUrls: ['./weekly-report.component.css']
})
export class WeeklyReportComponent extends PagedListingComponentBase<WeeklyReportComponent> implements OnInit {
  DeliveryManagement_PMReportProject = PERMISSIONS_CONSTANT.DeliveryManagement_PMReport_CloseReport;
  DeliveryManagement_PMReportProject_Create = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_Create;
  DeliveryManagement_PMReportProject_Delete = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_Delete;
  DeliveryManagement_PMReportProject_GetAll = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_GetAll;
  DeliveryManagement_PMReportProject_Update = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_Update;
  DeliveryManagement_PMReportProject_GetAllByPmProject = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_GetAllByPmProject;
  DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek;
  DeliveryManagement_PMReportProject_ResourceChangesInTheFuture = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_ResourceChangesInTheFuture;
  DeliveryManagement_PMReportProject_SendReport = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProject_SendReport;
  DeliveryManagement_PMReportProjectIssue = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue;
  DeliveryManagement_PMReportProjectIssue_Create = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue_Create;
  DeliveryManagement_PMReportProjectIssue_Delete = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue_Delete;
  DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek;
  DeliveryManagement_PMReportProjectIssue_Update = PERMISSIONS_CONSTANT.DeliveryManagement_PMReportProjectIssue_Update;
  DeliveryManagement_ResourceRequest_ApproveUser = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_ApproveUser;
  DeliveryManagement_ResourceRequest_RejectUser = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_RejectUser;
  PmManager_PMReportProject_UpdatePmReportProjectHealth = PERMISSIONS_CONSTANT.PmManager_PMReportProject_UpdatePmReportProjectHealth
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    // this.pmReportProjectService.GetAllByPmReport(this.pmReportId, request).pipe(finalize(()=>{
    //   finishedCallback();
    // }),catchError(this.pmReportProjectService.handleError)).subscribe((data)=>{
    //   this.pmReportProjectList=data.result.items;
    //   this.showPaging(data.result,pageNumber);
    // })
  }
  protected delete(entity: WeeklyReportComponent): void {
    throw new Error('Method not implemented.');
  }
  @ViewChild(RadioDropdownComponent) child: RadioDropdownComponent;
  @ViewChild("timmer") timmerCount;
  @ViewChild(MatMenuTrigger)
  menu: MatMenuTrigger;
  contextMenuPosition = { x: '0px', y: '0px' };

  public itemPerPage: number = 20;
  public weeklyCurrentPage: number = 1;
  public futureCurrentPage: number = 1;
  public problemCurrentPage: number = 1;
  public searchText = "";
  public pmReportProjectList: pmReportProjectDto[] = [];
  public tempPmReportProjectList: pmReportProjectDto[] = [];
  public show: boolean = false;
  public pmReportProject = {} as pmReportProjectDto;
  public pmReportId: any;
  public isActive: boolean = true;
  public projectType = "";
  public weeklyReportList: projectReportDto[] = [];
  public futureReportList: projectReportDto[] = [];
  public problemList: projectProblemDto[] = [];
  public problemIssueList: string[] = Object.keys(this.APP_ENUM.ProjectHealth);
  public projectRoleList: string[] = Object.keys(this.APP_ENUM.ProjectUserRole);
  public issueStatusList: string[] = Object.keys(this.APP_ENUM.PMReportProjectIssueStatus)
  public projectHealthList: string[] =  Object.keys(this.APP_ENUM.ProjectHealth);
  pmReportList: pmReportDto[] = [];
  public activeReportId: number;
  public pmReportProjectId: number;
  public isEditWeeklyReport: boolean = false;
  public isEditFutureReport: boolean = false;
  public isEditProblem: boolean = false;
  public processFuture: boolean = false;
  public processProblem: boolean = false
  public processWeekly: boolean = false;
  public createdDate = new Date();
  public projectId: number;
  public projectIdReport: number;
  public isEditingNote: boolean = false;
  public isEditingAutomationNote:boolean = false
  public generalNote: string = "";
  public automationNote:string =""
  public isShowProblemList: boolean = false;
  public isShowWeeklyList: boolean = false;
  public isShowFutureList: boolean = false;
  public projectInfo = {} as ProjectInfoDto
  public projectCurrentResource: any = []
  public mondayOf5weeksAgo: any
  public lastWeekSunday: any
  public tempResourceList: any[] = []
  public officalResourceList: any[] = []
  public selectedReport = {} as pmReportDto;
  totalNormalWorkingTime: number = 0;
  totalOverTime: number = 0;
  sidebarExpanded: boolean;
  isShowCurrentResource: boolean = true;
  searchUser: string = ""
  isTimmerCounting: boolean = false
  isStopCounting: boolean = false
  isRefresh: boolean = false
  isStart: boolean = false
  public isSentReport: boolean;
  public searchPmReport: string = "";
  public projectHealth: any;
  allowSendReport: boolean = true;
  PmManager_PMReportProject_SendReport = PERMISSIONS_CONSTANT.PmManager_PMReportProject_SendReport;


  constructor(public pmReportProjectService: PMReportProjectService,
    private tsProjectService: TimesheetProjectService,
    private reportIssueService: PmReportIssueService, private pmReportService: PmReportService,
    public route: ActivatedRoute,
    injector: Injector,
    private projectUserService: ProjectUserService,
    private userService: UserService,
    private dialog: MatDialog,
    private requestservice: ProjectResourceRequestService,
    private _layoutStore: LayoutStoreService,
    private reportService: PMReportProjectService
  ) {
    super(injector);
    this.projectId = Number(route.snapshot.queryParamMap.get("id"));
    this.projectType = route.snapshot.queryParamMap.get("type")
  }
  ngOnInit(): void {
    this.getAllPmReport();
    let currentDate = new Date()
    currentDate.setDate(currentDate.getDate() - (currentDate.getDay() + 6) % 7);
    currentDate.setDate(currentDate.getDate() - 7);


    this.mondayOf5weeksAgo = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());
    this.mondayOf5weeksAgo = moment(this.mondayOf5weeksAgo.setDate(this.mondayOf5weeksAgo.getDate() - 28)).format("YYYY-MM-DD")
    this.lastWeekSunday = moment(new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 6)).format("YYYY-MM-DD");
      this.getUser();
      this._layoutStore.sidebarExpanded.subscribe((value) => {
        this.sidebarExpanded = value;
      });

  }
  public startTimmer() {
    if ((!this.isTimmerCounting && !this.isStopCounting) || this.isRefresh) {
      this.timmerCount.start()
      this.isTimmerCounting = true
      this.isStopCounting = false
      this.isRefresh = false
      this.isStart = true
    }
  }
  public stopTimmer() {
    this.timmerCount.stop()
    this.isTimmerCounting = false
    this.isStopCounting = true
    this.isRefresh = false

  }
  public refreshTimmer() {
    this.timmerCount.reset()
    this.isTimmerCounting = false
    // this.isStopCounting =true
    this.isRefresh = true
    this.isStart = false

  }
  public resumeTimmer() {
    this.timmerCount.resume()
    this.isTimmerCounting = true
    this.isStopCounting = false
    this.isRefresh = false



  }

  public getWeeklyReport() {
    this.pmReportProjectService.getChangesDuringWeek(this.projectId, this.selectedReport.reportId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
      this.weeklyPeportList = data.result;
    })
  }

  public getAllPmReport() {
  
    this.pmReportProjectService.GetAllByProject(this.projectId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
      this.pmReportList = data.result;
      this.selectedReport = this.pmReportList.filter(item => item.isActive == true)[0];
      this.isSentReport = this.selectedReport.status == 'Draft' ? true : false
      this.generalNote = this.selectedReport.note
      this.allowSendReport = this.selectedReport.note == null || this.selectedReport.note == '' ? false : true;
      this.projectHealth = this.APP_ENUM.ProjectHealth[this.selectedReport.projectHealth]
      this.getProjectInfo();
      this.getWeeklyReport();
      this.getFuturereport();
      this.getProjectProblem();
      this.getPmReportProject();
    })
  }

  public getPmReportProject(): void {
      this.pmReportProjectService.GetAllByPmReport(this.selectedReport.reportId, this.projectType).subscribe((data => {
        this.pmReportProjectList = data.result;
        this.automationNote = this.pmReportProjectList.find((item)=>item.projectId==this.projectId).automationNote;
      }))
  }
  
  public sendWeeklyreport() {
    abp.message.confirm(
      `send report ${this.selectedReport.pmReportName}? `,
      "",
      (result: boolean) => {
        if (result) {
          this.reportService.sendReport(this.projectId, this.selectedReport.reportId).pipe(catchError(this.reportService.handleError)).subscribe(data => {
            abp.notify.success("Send report successful");
            this.getAllPmReport();
          })
        }
      }
    );
    }

  getProjectInfo() {
    this.isLoading = true;
    if (this.selectedReport.pmReportProjectId) {
      this.pmReportProjectService.GetInfoProject(this.selectedReport.pmReportProjectId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
        this.projectInfo = data.result

        this.isLoading = false;
        this.GetTimesheetWeeklyChartOfProject(this.projectInfo.projectCode, this.mondayOf5weeksAgo, this.lastWeekSunday);
        this.getCurrentResourceOfProject(this.projectInfo.projectCode);
        this.getDataForBillChart();
        this.router.navigate(
          [], 
          {
            relativeTo: this.route,
            queryParams: {
              name: this.projectInfo.projectName,
              client: this.projectInfo.clientName,
              pmName: this.projectInfo.pmName,
              pmReportProjectId: this.selectedReport.pmReportProjectId,
              projectHealth: this.projectHealth
            }, 
            queryParamsHandling: 'merge', // remove to replace all query params by provided
          });
      },
        () => { this.isLoading = false })
    }
  }

  public getChangedResource() {
    if (this.projectId) {
      this.pmReportProjectService.getChangesDuringWeek(this.projectId, this.selectedReport.reportId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
        this.weeklyReportList = data.result;
        this.isShowWeeklyList = this.weeklyReportList.length == 0 ? false : true;
      })
    }
  }
  public getFuturereport() {
    if (this.projectId) {
      this.pmReportProjectService.getChangesInFuture(this.projectId, this.selectedReport.reportId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
        this.futureReportList = data.result;
        this.isShowFutureList = this.futureReportList.length == 0 ? false : true;
      })
    }

  }
  public getProjectProblem() {
    if (this.projectId) {
      this.pmReportProjectService.problemsOfTheWeekForReport(this.projectId, this.selectedReport.reportId).pipe(catchError(this.reportIssueService.handleError)).subscribe(data => {
        if (data.result) {
          this.problemList = data.result.result;

          this.projectHealth = data.result.projectHealth;
          this.pmReportProjectService.projectHealth = this.projectHealth

        } else {
          this.problemList = [];

        }
        this.isShowProblemList = this.problemList.length == 0 ? false : true;
      })
    }
  }
  // public search() {
  //   this.pmReportProjectList = this.tempPmReportProjectList.filter((item) => {
  //     return item.projectName.toLowerCase().includes(this.searchText.toLowerCase()) ||
  //       item.pmEmailAddress?.toLowerCase().includes(this.searchText.toLowerCase());

  //   });


  //   this.projectId = this.pmReportProjectList[0]?.projectId
  //   this.generalNote = this.pmReportProjectList[0].note
  //   this.automationNote = this.pmReportProjectList[0].automationNote
  //   // this.totalNormalWorkingTime = this.pmReportProjectList[0].totalNormalWorkingTime
  //   this.totalOverTime = this.pmReportProjectList[0].totalOverTime

  //   this.pmReportProjectId = this.pmReportProjectList[0].id
  //   // this.pmReportProjectList[0].setBackground = true
  //   this.pmReportProjectList.forEach(element => {
  //     if (element.projectId == this.pmReportProjectList[0].projectId) {
  //       element.setBackground = true;
  //     } else {
  //       element.setBackground = false;
  //     }
  //   });
  //   this.getProjectInfo();
  //   this.getChangedResource();
  //   this.getFuturereport();
  //   this.getProjectProblem()
  //   this.searchUser = ""
  // }

  public markRead(project) {
    this.pmReportProjectService.reverseDelete(project.id, {}).subscribe((res) => {

      if (project.seen == false) {
        abp.notify.success("Mark Read!");
      } else {
        abp.notify.success("Mark Unread!");
      }
      project.seen = !project.seen

    })

  }

  //weekly
  public addWeekReport() {
    let newReport = {} as projectUserDto
    newReport.createMode = true;
    this.weeklyReportList.unshift(newReport)
    this.processWeekly = true;
  }
  public saveWeekReport(report: projectReportDto) {
    report.projectId = this.projectId
    report.isExpense = true;
    report.status = "0";
    report.startTime = moment(report.startTime).format("YYYY-MM-DD");
    delete report["createMode"]
    if (this.isEditWeeklyReport == true) {
      this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        report.startTime = moment(report.startTime).format("YYYY-MM-DD")
        this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
          abp.notify.success(`updated user: ${report.userName}`);
          this.getChangedResource();
          this.getCurrentResourceOfProject(this.projectInfo.projectCode);
          this.isEditWeeklyReport = false;
          this.processWeekly = false;
          this.searchUser = ""
        })
      },
        () => {
          report.createMode = true
        })
    }
    else {
      this.projectUserService.create(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        abp.notify.success("created new weekly report");
        this.processWeekly = false;
        report.createMode = false;
        this.getChangedResource();
        this.getCurrentResourceOfProject(this.projectInfo.projectCode);
        this.searchUser = ""

      },
        () => {
          report.createMode = true
        })
    }

  }
  public cancelWeekReport() {
    this.processWeekly = false;
    this.isEditWeeklyReport = false;
    this.getChangedResource();
    this.searchUser = ""
  }
  updateWeekReport(report) {
    this.processWeekly = true
    this.isEditWeeklyReport = true;
    report.createMode = true;
    report.projectRole = this.APP_ENUM.ProjectUserRole[report.projectRole]
  }
  deleteWeekReport(report) {

    abp.message.confirm(
      "Delete Issue? ",
      "",
      (result: boolean) => {
        if (result) {
          this.projectUserService.removeProjectUser(report.id).pipe(catchError(this.projectUserService.handleError)).subscribe(() => {
            abp.notify.success("Deleted Report");
            this.getChangedResource();
          });
        }
      }
    );
  }


  //Future
  public getUser(): void {
    this.userService.GetAllUserActive(false).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.userList = data.result;
    })
  }

  public saveFutureReport(report: projectReportDto) {
    delete report["createMode"]
    if (this.isEditFutureReport) {
      this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        report.startTime = moment(report.startTime).format("YYYY-MM-DD")
        this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
          abp.notify.success(`updated user: ${report.userName}`);
          this.getFuturereport();
          this.getCurrentResourceOfProject(this.projectInfo.projectCode);
          this.isEditFutureReport = false;
          this.processFuture = false
          this.searchUser = ""
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
        this.getFuturereport();
        this.getCurrentResourceOfProject(this.projectInfo.projectCode);
        this.searchUser = ""
      },
        () => {
          report.createMode = true
        })
    }

  }
  public cancelFutureReport() {
    this.processFuture = false;
    this.isEditFutureReport = false;
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
      allocatePercentage: resource.allocatePercentage,
      fullName: resource.fullName
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
        this.getChangedResource();
        this.getCurrentResourceOfProject(this.projectInfo.projectCode);
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
    problem.createdAt = moment(this.createdDate).format("YYYY-MM-DD");
    delete problem["createMode"]
    if (!this.isEditProblem) {
      this.reportIssueService.createReportIssue(this.projectId, problem).pipe(catchError(this.reportIssueService.handleError)).subscribe(data => {
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
    this.isEditProblem = false;
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
    Issue.status = this.APP_ENUM.PMReportProjectIssueStatus[Issue.status]

  }

  public updateNote() {
    if (this.generalNote) {
      this.generalNote == ""
    }
    this.reportService.updateNote(this.generalNote, this.selectedReport.pmReportProjectId).pipe(catchError(this.reportService.handleError)).subscribe(rs => {
      abp.notify.success("Update successful!")
      this.isEditingNote = false;
      this.selectedReport.note = this.generalNote
      this.allowSendReport = this.generalNote ? true : false
    })
  }
  public cancelUpdateNote() {
    this.isEditingNote = false;
    this.generalNote = this.selectedReport.note
    this.allowSendReport = this.generalNote ? true : false
  }


  public updateAutoNote() {
    this.pmReportProjectService.updateAutomationNote(this.automationNote, this.selectedReport.pmReportProjectId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(rs => {
      abp.notify.success("Update successful!")
      this.isEditingAutomationNote = false;
  
      this.pmReportProjectList.forEach(item => {
        if (item.id == this.selectedReport.pmReportProjectId) {
          item.automationNote = this.automationNote
        }
      })
    })
  }
  cancelUpdateAutoNote() {
    this.isEditingAutomationNote = false;
    this.pmReportProjectList.forEach(item => {
      if (item.id == this.selectedReport.pmReportProjectId) {
        this.automationNote = item.automationNote
      }
    })
  }



  getPercentage(report, data) {
    report.allocatePercentage = data
  }
  charts = []
  getCurrentResourceOfProject(projectCode) {
    if (this.projectId) {
      this.tempResourceList = []
      this.officalResourceList = []
      var d = new Date();
      d.setDate(d.getDate() - (d.getDay() + 6) % 7);
      d.setDate(d.getDate() - 7);
      let lastWeekMonday = moment(new Date(d.getFullYear(), d.getMonth(), d.getDate())).format("YYYY-MM-DD")
      this.pmReportProjectService.GetCurrentResourceOfProject(this.projectId)
        .pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
          this.totalNormalWorkingTime = 0
          this.totalOverTime = 0
          this.projectCurrentResource = data.result
          this.projectCurrentResource.forEach(user => {
            if (user.isPool) {
              this.tempResourceList.push(user.emailAddress)
            }
            else {
              this.officalResourceList.push(user.emailAddress)
            }
            this.GetTimesheetWeeklyChartOfUserInProject(projectCode, user, this.mondayOf5weeksAgo, this.lastWeekSunday)
            this.GetTimesheetOfUserInProject(projectCode, user, lastWeekMonday, this.lastWeekSunday)
          })
        })
    }
  }

  getTimesheetWorking() {
    const dialogRef = this.dialog.open(GetTimesheetWorkingComponent, {
      data: {
        dialogData: this.selectedReport.pmReportProjectId,
      },
      width: "500px",
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((result: WorkingTimeDto) => {
      if (result) {
        // this.totalNormalWorkingTime = result.normalWorkingTime
        // this.totalOverTime = result.overTime
      }
    });
  }

  public onReportchange() {
    this.getWeeklyReport();
    this.getFuturereport();
    this.getProjectProblem();
    this.generalNote = this.selectedReport.note
    this.isEditingNote = false;
    this.projectHealth = this.APP_ENUM.ProjectHealth[this.selectedReport.projectHealth]
  }





  buildProjectTSChart(normalAndOTchartData, officalChartData, TempChartData) {
    setTimeout(() => {
      var chartDom = document.getElementById('timesheet-chart')!;
      var myChart = echarts.init(chartDom);
      var option: echarts.EChartsOption;

      let hasOtValue = normalAndOTchartData.overTimeHours.some(item => item > 0)
      let hasOfficalDataNormal = officalChartData?.normalWoringHours.some(item => item > 0)
      let hasOfficalDataOT = officalChartData?.overTimeHours.some(item => item > 0)
      let hasTempDataNormal = TempChartData?.normalWoringHours.some(item => item > 0)
      let hasTempDataOT = TempChartData?.overTimeHours.some(item => item > 0)

      if (JSON.stringify(normalAndOTchartData?.normalWoringHours) == JSON.stringify(officalChartData?.normalWoringHours)) {
        hasOfficalDataNormal = false
      }
      if (JSON.stringify(normalAndOTchartData?.overTimeHours) == JSON.stringify(officalChartData?.overTimeHours)) {
        hasOfficalDataOT = false
      }

      option = {
        title: {
          text: 'Timesheet'
        },
        tooltip: {
          trigger: 'axis'
        },
        legend: {
          data: ['Normal', `${hasOtValue ? 'OT' : ''}`, `${hasOfficalDataNormal ? 'Normal Offical' : ''}`
            , `${hasOfficalDataOT ? 'OT Offical' : ''}`, `${hasTempDataNormal ? 'Normal Temp' : ''}`,
            `${hasTempDataOT ? 'OT Temp' : ''}`],
        },
        color: ['green', 'red', 'blue', 'orange', 'yellow', 'purple'],
        grid: {
          left: '3%',
          right: '4%',
          bottom: '3%',
          containLabel: true
        },

        xAxis: {
          type: 'category',
          boundaryGap: false,
          data: normalAndOTchartData.labels
        },
        yAxis: {
          type: 'value'
        },
        series: [
          {
            lineStyle: { color: 'green' },
            name: 'Normal',
            type: 'line',
            data: normalAndOTchartData?.normalWoringHours
          },
          {
            lineStyle: { color: 'red' },
            name: 'OT',
            type: 'line',
            data: hasOtValue ? normalAndOTchartData?.overTimeHours : []
          },
          {
            lineStyle: { color: 'blue' },
            name: 'Normal Offical',
            type: 'line',
            data: hasOtValue ? officalChartData?.normalWoringHours : []
          }, {
            lineStyle: { color: 'orange' },
            name: 'OT Offical',
            type: 'line',
            data: hasOtValue ? officalChartData?.overTimeHours : []
          }, {
            lineStyle: { color: 'yellow' },
            name: 'Normal Temp',
            type: 'line',
            data: hasOtValue ? TempChartData?.normalWoringHours : []
          }, {
            lineStyle: { color: 'purple' },
            name: 'OT Temp',
            type: 'line',
            data: hasOtValue ? TempChartData?.overTimeHours : []
          },

        ]
      };
      option && myChart.setOption(option);
    }, 1)

  }


  public genarateUserChart(user, chartData) {

    // var chartDom = document.getElementById(user.userId.toString());
    // var myChart = echarts.init(chartDom);

    setTimeout(() => {

      let chartDom = document.getElementById('user' + user.userId);

      let myChart = echarts.init(chartDom);
      let option: echarts.EChartsOption;
      option = {
        tooltip: {
          trigger: 'axis'
        },
        grid: {
          top: "6%",
          left: '3%',
          right: '4%',
          bottom: '2%',
          containLabel: true
        },
        xAxis: {
          data: chartData.labels,
          show: false
        },
        yAxis: {
          type: 'value',
          min: 0,
          max: 60,
          show: false
        },
        series: [
          {
            // showSymbol: false,
            symbolSize: 2,
            data: chartData.normalWoringHours,
            type: 'line',
            name: 'timesheet',
          }
        ]
      };
      option && myChart.setOption(option);


    }, 1)






    // option && myChart.setOption(option);

  }



  // TimesheetWeeklyChart
  async GetTimesheetWeeklyChartOfProject(projectCode, startTime, endTime) {
    let chartData = {} as any
    await this.pmReportProjectService.GetTimesheetWeeklyChartOfProject(projectCode, startTime, endTime).toPromise().then(rs => {
      chartData.normalAndOT = rs.result
    })
    if (this.officalResourceList.length > 0) {
      let officalRequestBody = {
        projectCode: this.projectInfo.projectCode,
        emails: this.officalResourceList,
        startDate: startTime,
        endDate: endTime
      }
      await this.pmReportProjectService.GetTimesheetWeeklyChartOfUserGroupInProject(officalRequestBody).toPromise().then(rs => {
        chartData.offical = rs.result
      })

    }

    if (this.tempResourceList.length > 0) {
      let tempRequestBody = {
        projectCode: this.projectInfo.projectCode,
        emails: this.tempResourceList,
        startDate: startTime,
        endDate: endTime
      }
      await this.pmReportProjectService.GetTimesheetWeeklyChartOfUserGroupInProject(tempRequestBody).toPromise().then(rs => {
        chartData.temp = rs.result
      })
    }

    this.buildProjectTSChart(chartData.normalAndOT, chartData.offical, chartData.temp)
  }

  GetTimesheetWeeklyChartOfUserInProject(projectCode, user, startTime, endTime) {
    this.pmReportProjectService.GetTimesheetWeeklyChartOfUserInProject(projectCode, user.emailAddress, startTime, endTime).subscribe(rs => {
      this.genarateUserChart(user, rs.result)
    })
  }
  GetTimesheetOfUserInProject(projectCode, user, startTime, endTime) {
    this.pmReportProjectService.GetTimesheetOfUserInProject(projectCode, user.emailAddress, startTime, endTime).subscribe(rs => {
      user.normalWorkingTime = rs.result ? rs.result.normalWorkingTime : 0
      user.overTime = rs.result ? rs.result.overTime : 0
      this.totalNormalWorkingTime += user.normalWorkingTime
      this.totalOverTime += user.overTime
    })
  }

  GetTimesheetWeeklyChartOfUserGroupInProject(emailList) {
    // monday at 5 weeks ago =  last week mondy - 5 week (35 days)

    let requestBody = {
      projectCode: this.projectInfo.projectCode,
      emails: emailList,
      startDate: this.mondayOf5weeksAgo,
      endDate: this.lastWeekSunday
    }
    this.pmReportProjectService.GetTimesheetWeeklyChartOfUserGroupInProject(requestBody).subscribe(r => {

    })
  }
  showActions(e) {
    e.preventDefault();
    this.contextMenuPosition.x = e.clientX + 'px';
    this.contextMenuPosition.y = e.clientY + 'px';
    this.menu.openMenu()


  }
  showActionsPlan(e) {
    e.preventDefault();
    this.contextMenuPosition.x = e.clientX + 'px';
    this.contextMenuPosition.y = e.clientY + 'px';
    this.menu.openMenu()


  }

  // Current project action

  releaseUser(user) {
    let ref = this.dialog.open(ReleaseUserDialogComponent, {
      width: "700px",
      data: {
        user: user
      }
    })
    ref.afterClosed().subscribe(rs => {
      if (rs) {
       this.getCurrentResourceOfProject(this.projectInfo.projectCode)
       this.getChangedResource()
      }
    })
  }


  //  planned resource action

  confirm(user) {
    if (user.allocatePercentage <= 0) {
      let ref = this.dialog.open(ReleaseUserDialogComponent, {
        width: "700px",
        data: {
          user: user,
          type: "confirmOut"
        },
      })
      ref.afterClosed().subscribe(rs => {
        if (rs) {
          this.getChangedResource()
          this.getFuturereport()
          this.getCurrentResourceOfProject(this.projectInfo.projectCode)
        }
      })
    }
    else if (user.allocatePercentage > 0) {
      let workingProject = [];
      this.projectUserService.GetAllWorkingProjectByUserId(user.userId).subscribe(data => {
        workingProject = data.result
        let ref = this.dialog.open(ConfirmPopupComponent, {
          width: '700px',
          data: {
            workingProject: workingProject,
            user: user,
            type: "confirmJoin"
          }
        })

        ref.afterClosed().subscribe(rs => {
          if (rs) {
            this.getChangedResource()
            this.getFuturereport()
            this.getCurrentResourceOfProject(this.projectInfo.projectCode)
          }
        })
      })
    }
  }

  cancelResourcePlan(user) {
    abp.message.confirm(
      `Cancel plan for user <strong>${user.fullName}</strong> <strong class = "${user.allocatePercentage > 0 ? 'text-success' : 'text-danger'}">
      ${user.allocatePercentage > 0 ? 'Join project' : 'Out project'}</strong>?`,
      "",
      (result: boolean) => {
        if (result) {
          this.projectUserService.CancelResourcePlan(user.id).subscribe(rs => {
            abp.notify.success(`Cancel plan for user ${user.fullName}`)
            this.getFuturereport()
          })
        }
      },
      true
    )
  }

  updateHealth(projectHealth) {
    this.pmReportProjectService.updateHealth(this.selectedReport.pmReportProjectId, projectHealth).subscribe((data) => {
      abp.notify.success("Update project health to " + this.getByEnum(projectHealth, this.APP_ENUM.ProjectHealth))
    })
 
  }

    //
    getDataForBillChart() {
      var todayDate: any = new Date();
      var toDate = this.formatDateYMD(todayDate)
      let fromDate = this.formatDateYMD(todayDate.setMonth(todayDate.getMonth() - 5));
      this.tsProjectService.GetBillInfoChart(this.projectId, fromDate, toDate).subscribe(data => {
        this.buildBillChart(data.result)
      })
    }
  
  
  
    public buildBillChart(chartData) {
  
      // var chartDom = document.getElementById(user.userId.toString());
      // var myChart = echarts.init(chartDom);
  
      setTimeout(() => {
  
        let chartDom = document.getElementById('bill-chart');
  
        let myChart = echarts.init(chartDom);
        let option: echarts.EChartsOption;
        option = {
          tooltip: {
            trigger: 'axis',
          },
          title: {
            text: 'Bill info'
          },
          grid: {
            left: '3%',
            right: '4%',
            bottom: '1%',
            containLabel: true
          },
          legend: {
            data: ['manMonths', 'manDays']
          },
          xAxis: [
            {
              type: 'category',
              data: chartData.labels,
              boundaryGap: false,
            }
          ],
          yAxis: [
            {
              type: 'value',
              name: 'manMonths',
  
            },
            {
              type: 'value',
              name: 'manDays',
  
            }
          ],
          series: [
  
            {
              name: 'manMonths',
              type: 'bar',
              data: chartData.manMonths
            },
            {
              name: 'manDays',
              type: 'line',
              yAxisIndex: 1,
              data: chartData.manDays
            }
          ]
        };
        option && myChart.setOption(option);
      }, 1)
    }
  
  
    addPlanResource() {
      let ref = this.dialog.open(AddFutureResourceDialogComponent, {
        width: "700px",
        data: {
          projectId: this.projectId,
          projectName: this.projectInfo.projectName
        }
      })
      ref.afterClosed().subscribe(rs => {
        if (rs) {
          this.getFuturereport()
        }
      })
    }
    editResourcePlan(resource) {
      let item = {
        projectUserId:resource.id,
        fullName: resource.fullName,
        projectId: this.projectId,
        projectRole: resource.projectRole,
        userId: resource.userId,
        startDate: resource.startDate,
        isPool: resource.isPool,
        allocatePercentage: resource.allocatePercentage,
        startTime: resource.startTime
      }
      let ref = this.dialog.open(AddFutureResourceDialogComponent, {
        width: "700px",
        data: {
          command: "edit",
          item: item
        }
      })
      ref.afterClosed().subscribe(rs => {
        if (rs) {
          this.getFuturereport()
        }
      })
    }
  

}
