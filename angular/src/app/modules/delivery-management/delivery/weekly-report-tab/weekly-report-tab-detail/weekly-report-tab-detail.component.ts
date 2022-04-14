import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { ProjectResourceRequestService } from './../../../../../service/api/project-resource-request.service';
import { MatDialog } from '@angular/material/dialog';
import { ApproveDialogComponent } from './../../../../pm-management/list-project/list-project-detail/weekly-report/approve-dialog/approve-dialog.component';
import { UserService } from './../../../../../service/api/user.service';
import { ProjectUserService } from './../../../../../service/api/project-user.service';
import { ProjectInfoDto, projectUserDto } from './../../../../../service/model/project.dto';
import { PmReportService } from './../../../../../service/api/pm-report.service';
import { PmReportIssueService } from './../../../../../service/api/pm-report-issue.service';
import { projectProblemDto, projectReportDto } from './../../../../../service/model/projectReport.dto';
import { finalize, catchError } from 'rxjs/operators';

import { ActivatedRoute } from '@angular/router';
import { pmReportProjectDto} from './../../../../../service/model/pmReport.dto';
import { PMReportProjectService } from './../../../../../service/api/pmreport-project.service';
import { Component, OnInit, Injector, ViewChild, Input } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import * as moment from 'moment';
import * as echarts from 'echarts';
import { RadioDropdownComponent } from '@shared/components/radio-dropdown/radio-dropdown.component';
import { LayoutStoreService } from '@shared/layout/layout-store.service';
import { GetTimesheetWorkingComponent, WorkingTimeDto } from './get-timesheet-working/get-timesheet-working.component';
import { MatMenuTrigger } from '@angular/material/menu';
import { ReleaseUserDialogComponent } from '@app/modules/pm-management/list-project/list-project-detail/resource-management/release-user-dialog/release-user-dialog.component';
import { ConfirmFromPage, ConfirmPopupComponent } from '@app/modules/pm-management/list-project/list-project-detail/resource-management/confirm-popup/confirm-popup.component';
import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';
import { AddFutureResourceDialogComponent } from './add-future-resource-dialog/add-future-resource-dialog.component';
import { EditMeetingNoteDialogComponent } from './edit-meeting-note-dialog/edit-meeting-note-dialog.component';
@Component({
  selector: 'app-weekly-report-tab-detail',
  templateUrl: './weekly-report-tab-detail.component.html',
  styleUrls: ['./weekly-report-tab-detail.component.css']
})
export class WeeklyReportTabDetailComponent extends PagedListingComponentBase<WeeklyReportTabDetailComponent> implements OnInit {
  WeeklyReport_ReportDetail_View = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_View;
  WeeklyReport_ReportDetail_UpdateNote = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_UpdateNote;
  WeeklyReport_ReportDetail_UpdateProjectHealth = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_UpdateProjectHealth;
  WeeklyReport_ReportDetail_Issue = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_Issue;
  WeeklyReport_ReportDetail_Issue_View = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_Issue_View;
  WeeklyReport_ReportDetail_Issue_AddMeetingNote = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_Issue_AddMeetingNote;
  WeeklyReport_ReportDetail_Issue_SetDone = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_Issue_SetDone;
  WeeklyReport_ReportDetail_CurrentResource = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_CurrentResource;
  WeeklyReport_ReportDetail_CurrentResource_View = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_CurrentResource_View;
  WeeklyReport_ReportDetail_CurrentResource_Release = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_CurrentResource_Release;
  WeeklyReport_ReportDetail_PlannedResource = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PlannedResource;
  WeeklyReport_ReportDetail_PlannedResource_View = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PlannedResource_View;
  WeeklyReport_ReportDetail_PlannedResource_CreateNewPlan = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PlannedResource_CreateNewPlan;
  WeeklyReport_ReportDetail_PlannedResource_Edit = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PlannedResource_Edit;
  WeeklyReport_ReportDetail_PlannedResource_ConfirmPickEmployeeFromPoolToProject = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PlannedResource_ConfirmPickEmployeeFromPoolToProject;
  WeeklyReport_ReportDetail_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther;
  WeeklyReport_ReportDetail_PlannedResource_ConfirmOut = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PlannedResource_ConfirmOut;
  WeeklyReport_ReportDetail_PlannedResource_CancelPlan = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PlannedResource_CancelPlan;
  WeeklyReport_ReportDetail_ChangedResource = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_ChangedResource;

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
  public isActive: boolean;
  public projectType = "";
  public weeklyReportList: projectReportDto[] = [];
  public futureReportList: projectReportDto[] = [];
  public problemList: projectProblemDto[] = [];
  public problemIssueList: string[] = Object.keys(this.APP_ENUM.ProjectHealth);
  public projectRoleList: string[] = Object.keys(this.APP_ENUM.ProjectUserRole);
  public issueStatusList: string[] = Object.keys(this.APP_ENUM.PMReportProjectIssueStatus)
  public activeReportId: number;
  public projectHealth;
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
  public isEditingAutomationNote: boolean = false
  public generalNote: string = "";
  public automationNote: string = ""
  public isShowProblemList: boolean = false;
  public isShowWeeklyList: boolean = false;
  public isShowFutureList: boolean = false;
  public projectInfo = {} as ProjectInfoDto
  public projectCurrentResource: any = []
  public mondayOf5weeksAgo: any
  public lastWeekSunday: any
  public tempResourceList: any[] = []
  public officalResourceList: any[] = [];

  totalNormalWorkingTime: number = 0;
  totalOverTime: number = 0;
  overTimeNoCharge:number = 0;
  sidebarExpanded: boolean;
  isShowCurrentResource: boolean = true;
  searchUser: string = ""
  isTimmerCounting: boolean = false
  isStopCounting: boolean = false
  isRefresh: boolean = false
  isStart: boolean = false

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
  ) {
    super(injector)
  }
  ngOnInit(): void {
    let currentDate = new Date()
    currentDate.setDate(currentDate.getDate() - (currentDate.getDay() + 6) % 7);
    currentDate.setDate(currentDate.getDate() - 7);
    this.mondayOf5weeksAgo = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());
    this.mondayOf5weeksAgo = moment(this.mondayOf5weeksAgo.setDate(this.mondayOf5weeksAgo.getDate() - 28)).format("YYYY-MM-DD")
    this.lastWeekSunday = moment(new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 6)).format("YYYY-MM-DD");
    if (this.router.url.includes("weeklyReportTabDetail")) {
      this.pmReportService.currentProjectHealth.subscribe((data) => {
        if (data) {
          const pmReportPro = this.pmReportProjectList.find(e => e.id == data.pmReportProjectId);
          if (pmReportPro) {
            pmReportPro.projectHealth = this.problemIssueList[data.projectHealth];
          }
        }
      }
      );
      this.pmReportService.currentProjectType.subscribe(projectType => {
        this.projectType = projectType;
        this.getPmReportProject();
      }      
      );
      this.pmReportId = this.route.snapshot.queryParamMap.get('id');
      this.isActive = this.route.snapshot.queryParamMap.get('isActive') == "true";
      this.getPmReportProject();
      this.getUser();
      this._layoutStore.sidebarExpanded.subscribe((value) => {
        this.sidebarExpanded = value;
      });

    }
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
  public getPmReportProject(): void {
    if (this.router.url.includes("weeklyReportTabDetail") && this.pmReportId) {
      this.pmReportProjectService.GetAllByPmReport(this.pmReportId, this.projectType).subscribe((data => {
        this.pmReportProjectList = data.result;
        this.tempPmReportProjectList = data.result;
        this.projectId = this.pmReportProjectList[0]?.projectId
        this.generalNote = this.pmReportProjectList[0]?.note
        this.automationNote = this.pmReportProjectList[0]?.automationNote
        // this.totalNormalWorkingTime = this.pmReportProjectList[0]?.totalNormalWorkingTime
        this.totalOverTime = this.pmReportProjectList[0]?.totalOverTime
        this.projectHealth = this.APP_ENUM.ProjectHealth[this.pmReportProjectList[0]?.projectHealth]
        this.pmReportProjectService.projectHealth = this.projectHealth
        this.pmReportProjectId = this.pmReportProjectList[0]?.id
        if (this.pmReportProjectList[0]) {
          this.pmReportProjectList[0].setBackground = true
        }
        this.getProjectInfo();
        this.getChangedResource();
        this.getFuturereport();
        this.getProjectProblem();
        this.search()
      }))
    }
  }
  getProjectInfo() {
    this.isLoading = true;
    if (this.pmReportProjectId) {
      this.pmReportProjectService.GetInfoProject(this.pmReportProjectId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
        this.projectInfo = data.result

        this.isLoading = false;
        this.GetTimesheetWeeklyChartOfProject(this.projectInfo.projectCode, this.mondayOf5weeksAgo, this.lastWeekSunday)
        this.getCurrentResourceOfProject(this.projectInfo.projectCode)
        this.router.navigate(
          [],
          {
            relativeTo: this.route,
            queryParams: {
              name: this.projectInfo.projectName,
              client: this.projectInfo.clientName,
              pmName: this.projectInfo.pmName,
              pmReportProjectId: this.pmReportProjectId,
              projectHealth: this.projectHealth
            },
            queryParamsHandling: 'merge', // remove to replace all query params by provided
          });
      },
        () => { this.isLoading = false })
    }
  }
  public view(projectReport) {
    this.pmReportProjectId = projectReport.id
    this.projectId = projectReport.projectId;
    this.isEditingNote = false;
    this.isEditingAutomationNote = false
    this.projectHealth = this.APP_ENUM.ProjectHealth[projectReport.projectHealth]
    this.pmReportProjectService.projectHealth = this.projectHealth
    this.pmReportProjectList.forEach(element => {
      if (element.projectId == projectReport.projectId) {
        element.setBackground = true;
      } else {
        element.setBackground = false;
      }
    });
    // this.totalNormalWorkingTime = projectReport.totalNormalWorkingTime
    this.totalOverTime = projectReport.totalOverTime
    this.generalNote = projectReport.note
    this.automationNote = projectReport.automationNote


    this.getProjectInfo();
    this.getChangedResource();
    this.getFuturereport();
    this.getProjectProblem();
    this.isEditWeeklyReport = false;
    this.isEditFutureReport = false;
    this.isEditProblem = false;
    this.processFuture = false;
    this.processProblem = false
    this.processWeekly = false;
    this.searchUser = ""

  }


  public getChangedResource() {
    if (this.projectId) {
      this.pmReportProjectService.getChangesDuringWeek(this.projectId, this.pmReportId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
        this.weeklyReportList = data.result;
        this.isShowWeeklyList = this.weeklyReportList.length == 0 ? false : true;
      })
    }
  }
  public getFuturereport() {
    if (this.projectId) {
      this.pmReportProjectService.getChangesInFuture(this.projectId, this.pmReportId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
        this.futureReportList = data.result;
        this.isShowFutureList = this.futureReportList.length == 0 ? false : true;
      })
    }

  }
  public getProjectProblem() {
    if (this.projectId) {
      this.pmReportProjectService.problemsOfTheWeekForReport(this.projectId, this.pmReportId).pipe(catchError(this.reportIssueService.handleError)).subscribe(data => {
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
  public search() {
    this.pmReportProjectList = this.tempPmReportProjectList.filter((item) => {
      return item.projectName.toLowerCase().includes(this.searchText.toLowerCase()) ||
        item.pmEmailAddress?.toLowerCase().includes(this.searchText.toLowerCase());

    });
    // this.projectId = this.pmReportProjectList[0]?.projectId
    // this.generalNote = this.pmReportProjectList[0].note
    // this.automationNote = this.pmReportProjectList[0].automationNote
    // this.totalOverTime = this.pmReportProjectList[0].totalOverTime

    // this.pmReportProjectId = this.pmReportProjectList[0].id
    // this.pmReportProjectList.forEach(element => {
    //   if (element.projectId == this.pmReportProjectList[0].projectId) {
    //     element.setBackground = true;
    //   } else {
    //     element.setBackground = false;
    //   }
    // });

    this.searchUser = ""
  }

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

  setDone(issue){
    this.pmReportProjectService.SetDoneIssue(issue.id).subscribe((res)=>{
      if(res){
        abp.notify.success("Update Successfully!")
      }
      this.getProjectProblem();
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
  // public addFutureReport() {
  //   let newReport = {} as projectUserDto
  //   newReport.createMode = true;
  //   this.futureReportList.unshift(newReport)
  //   this.processFuture = true;
  // }
  // public saveFutureReport(report: projectReportDto) {
  //   delete report["createMode"]
  //   if (this.isEditFutureReport) {
  //     this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
  //       report.startTime = moment(report.startTime).format("YYYY-MM-DD")
  //       this.projectUserService.update(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
  //         abp.notify.success(`updated user: ${report.userName}`);
  //         this.getFuturereport();
  //         this.getCurrentResourceOfProject(this.projectInfo.projectCode);
  //         this.isEditFutureReport = false;
  //         this.processFuture = false
  //         this.searchUser = ""
  //       })
  //     },
  //       () => {
  //         report.createMode = true
  //       })
  //   }
  //   else {
  //     // report.isFutureActive = false
  //     report.projectId = this.projectId
  //     report.isExpense = true;
  //     report.status = "2";
  //     report.startTime = moment(report.startTime).format("YYYY-MM-DD");

  //     this.projectUserService.create(report).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
  //       abp.notify.success("created new future report");
  //       this.processFuture = false;
  //       report.createMode = false;
  //       this.getFuturereport();
  //       this.getCurrentResourceOfProject(this.projectInfo.projectCode);
  //       this.searchUser = ""
  //     },
  //       () => {
  //         report.createMode = true
  //       })
  //   }

  // }
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
    this.pmReportProjectService.updateNote(this.generalNote, this.pmReportProjectId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(rs => {
      abp.notify.success("Update successful!")
      this.isEditingNote = false;

      this.pmReportProjectList.forEach(item => {
        if (item.id == this.pmReportProjectId) {
          item.note = this.generalNote;
        }
      })
    })
  }
  cancelUpdateNote() {
    this.isEditingNote = false;
    this.pmReportProjectList.forEach(item => {
      if (item.id == this.pmReportProjectId) {
        this.generalNote = item.note
      }
    })
  }


  public updateAutoNote() {
    this.pmReportProjectService.updateAutomationNote(this.automationNote, this.pmReportProjectId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(rs => {
      abp.notify.success("Update successful!")
      this.isEditingAutomationNote = false;

      this.pmReportProjectList.forEach(item => {
        if (item.id == this.pmReportProjectId) {
          item.automationNote = this.automationNote
        }
      })
    })
  }
  cancelUpdateAutoNote() {
    this.isEditingAutomationNote = false;
    this.pmReportProjectList.forEach(item => {
      if (item.id == this.pmReportProjectId) {
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
        dialogData: this.pmReportProjectId,
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





  buildProjectTSChart(normalAndOTchartData, officalChartData, TempChartData) {
    setTimeout(() => {
      var chartDom = document.getElementById('timesheet-chart')!;
      var myChart = echarts.init(chartDom);
      var option: echarts.EChartsOption;

      let hasOtValue = normalAndOTchartData?.overTimeHours.some(item => item > 0)
      let hasOfficalDataNormal = officalChartData?.normalWoringHours.some(item => item > 0)
      let hasOfficalDataOT = officalChartData?.overTimeHours.some(item => item > 0)
      let hasTempDataNormal = TempChartData?.normalWoringHours.some(item => item > 0)
      let hasTempDataOT = TempChartData?.overTimeHours.some(item => item > 0)
      let hasOtNoCharge = normalAndOTchartData?.otNoChargeHours.some(item => item > 0)

      if (JSON.stringify(normalAndOTchartData?.normalWoringHours) == JSON.stringify(officalChartData?.normalWoringHours)) {
        hasOfficalDataNormal = false
      }
      if (JSON.stringify(normalAndOTchartData?.overTimeHours) == JSON.stringify(officalChartData?.overTimeHours)) {
        hasOfficalDataOT = false
      }

      option = {
        width: '90%',
        title: {
          text: 'Timesheet'
        },
        tooltip: {
          trigger: 'axis'
        },
        legend: {
          left:'25%',
          width:'80%',
          data: ['Total normal', `${hasOtValue ? 'Total OT' : ''}`, `${hasOfficalDataNormal ? 'Normal Offical' : ''}`
          , `${hasOfficalDataOT ? 'OT Offical' : ''}`, `${hasTempDataNormal ? 'Normal Temp' : ''}`,
          `${hasTempDataOT ? 'OT Temp' : ''}`,`${hasOtNoCharge ? 'OT NoCharge' : ''}`], 
        },
        color: ['#211f1f', 'red', 'blue', 'orange', '#787a7a', 'purple', 'green'],
        grid: {
          left: '3%',
          right: '4%',
          bottom: '1%',
          containLabel: true
        },

        xAxis: {
          axisLabel: {
            padding: [4, 0, 0, 0]
          },
          type: 'category',
          boundaryGap: false,
          data: normalAndOTchartData.labels
        },
        yAxis: {
          type: 'value'
        },
        series: [
          {
            lineStyle: { color: '#211f1f' },
            name: 'Total normal',
            type: 'line',
            data: normalAndOTchartData?.normalWoringHours
          },
          {
            lineStyle: { color: 'red' },
            name: 'Total OT',
            type: 'line',
            data: hasOtValue ? normalAndOTchartData?.overTimeHours : []
          },
          {
            lineStyle: { color: 'blue' },
            name: 'Normal Offical',
            type: 'line',
            data: hasOfficalDataNormal ? officalChartData?.normalWoringHours : []
          }, {
            lineStyle: { color: 'orange' },
            name: 'OT Offical',
            type: 'line',
            data: hasOfficalDataOT ? officalChartData?.overTimeHours : []
          }, {
            lineStyle: { color: '#787a7a' },
            name: 'Normal Temp',
            type: 'line',
            data: hasTempDataNormal ? TempChartData?.normalWoringHours : []
          }, {
            lineStyle: { color: 'purple' },
            name: 'OT Temp',
            type: 'line',
            data: hasTempDataOT ? TempChartData?.overTimeHours : []
          },{
            lineStyle: { color: 'green' },
            name: 'OT NoCharge',
            type: 'line',
            data: hasOtNoCharge ? TempChartData?.otNoChargeHours : []
          },

        ]
      };
      option && myChart.setOption(option);
    }, 1)

  }



  public genarateUserChart(user, chartData) {

    // var chartDom = document.getElementById(user.userId.toString());
    // var myChart = echarts.init(chartDom);
    let hasOtValue = chartData.overTimeHours.some(item => item > 0)
    let hasOtNocharge = chartData.otNoChargeHours.some(item => item > 0)
    setTimeout(() => {

      let chartDom = document.getElementById('user' + user.userId);

      let myChart = echarts.init(chartDom);
      let option: echarts.EChartsOption;
      option = {
        tooltip: {
          trigger: 'axis',
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
            name: 'Normal',
          },
          {
            // showSymbol: false,
            color:['#dc3545'],
            symbolSize: 2,
            data: hasOtValue ? chartData.overTimeHours : [],
            type: 'line',
            name: 'OT',
            lineStyle: { color: '#dc3545' }
          },
          {
            // showSymbol: false,
            color:['orange'],
            symbolSize: 2,
            data: hasOtNocharge ? chartData.otNoChargeHours : [],
            type: 'line',
            name: 'OT no charge',
            lineStyle: { color: 'orange' }
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
    var todayDate: any = new Date();
    var currentDate = this.formatDateYMD(todayDate)
    let fiveMonthAgo:Date =  new Date(todayDate.setMonth(todayDate.getMonth() - 5))

    fiveMonthAgo = this.getFirstDayOfMonth(fiveMonthAgo.getFullYear(), fiveMonthAgo.getMonth())
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
    let effortRequestBody = {
      projectCode: this.projectInfo.projectCode,
      emails: this.officalResourceList,
      startDate: this.formatDateYMD(fiveMonthAgo),
      endDate: currentDate 
    }
   await this.pmReportProjectService.GetEffortMonthlyChartOfUserGroupInProject(effortRequestBody).toPromise().then(rs=>{
      chartData.effort = rs.result
    })
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

    
    await this.tsProjectService.GetBillInfoChart(this.projectId, this.formatDateYMD(fiveMonthAgo), currentDate).toPromise().then(data => {
      chartData.billChart = data.result
    })
   
    this.buildProjectTSChart(chartData.normalAndOT, chartData.offical, chartData.temp)
    this.buildBillChart(chartData.billChart, chartData.effort)


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
      user.overTimeNoCharge = rs.result ? rs.result.overTimeNoCharge : 0
      this.totalNormalWorkingTime += user.normalWorkingTime
      this.totalOverTime += user.overTime
      this.overTimeNoCharge += user.overTimeNoCharge
    })
  }
   getFirstDayOfMonth(year, month) {
    return new Date(year, month, 1);
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
            type: "confirmJoin",
            page: ConfirmFromPage.weeklyReport
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





  public buildBillChart(billData, EffortData) {

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
          data: ['Bill.ManMonth', 'Bill.ManDay', 'Effort.ManDay']
        },
        xAxis: [
          {
            axisLabel: {
              padding: [4, 0, 0, 0]
            },
            type: 'category',
            data: billData.labels,
            boundaryGap: false,
          }
        ],
        yAxis: [
          {
            axisLabel: {
              padding: [0, 13, 0, 13]
            },
            type: 'value',
            name: 'ManMonth',

          },
          {
            axisLabel: {
              padding: [0, 13, 0, 13]
            },
            type: 'value',
            name: 'ManDay',

          }
        ],
        series: [

          {
            barWidth: 30,
            name: 'Bill.ManMonth',
            type: 'bar',
            data: billData.manMonths
          },
          {
            name: 'Bill.ManDay',
            type: 'line',
            yAxisIndex: 1,
            data: billData.manDays
          },
          {
            name: 'Effort.ManDay',
            type: 'line',
            yAxisIndex: 1,
            data: EffortData.manDays
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
      projectUserId: resource.id,
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
  public editMeetingNote(projectIssue){
    let item = {
      id: projectIssue.pmReportProjectId,
      note: projectIssue.meetingSolution
    }
    let ref = this.dialog.open(EditMeetingNoteDialogComponent,{
      width: "600px",
      data: item
      
    })
    ref.afterClosed().subscribe(rs=>{
      if(rs){
        this.getProjectProblem()
      }
    })
 
  }

}
