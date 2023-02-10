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
import { pmReportProjectDto } from './../../../../../service/model/pmReport.dto';
import { PMReportProjectService } from './../../../../../service/api/pmreport-project.service';
import { Component, OnInit, Injector, ViewChild, Input, ElementRef } from '@angular/core';
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
import { Observable, forkJoin } from 'rxjs';
import { APP_ENUMS, EProjectReport } from '@shared/AppEnums';
import { TimeInterface } from 'angular-cd-timer';
import { AppConfigurationService } from '@app/service/api/app-configuration.service';
import { FormControl, Validators } from '@angular/forms';
import { CriteriaService } from '@app/service/api/criteria.service';
import { ProjectCriteriaResultService } from '@app/service/api/project-criteria-result.service';
import { ProjectCriteriaDto } from '@app/service/model/criteria-category.dto';
import { ProjectCriteriaResultDto } from '@app/service/model/project-criteria-result.dto';
import { log } from 'console';
@Component({
  selector: 'app-weekly-report-tab-detail',
  templateUrl: './weekly-report-tab-detail.component.html',
  styleUrls: ['./weekly-report-tab-detail.component.css'],
})
export class WeeklyReportTabDetailComponent extends PagedListingComponentBase<WeeklyReportTabDetailComponent> implements OnInit {
  WeeklyReport_ReportDetail_View = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_View;
  WeeklyReport_ReportDetail_UpdateNote = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_UpdateNote;
  WeeklyReport_ReportDetail_UpdateProjectHealth = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_UpdateProjectHealth;
  WeeklyReport_ReportDetail_Issue = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PMIssue;
  WeeklyReport_ReportDetail_Issue_View = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PMIssue_View;
  WeeklyReport_ReportDetail_Issue_AddMeetingNote = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PMIssue_AddMeetingNote;
  WeeklyReport_ReportDetail_Issue_SetDone = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PMIssue_SetDone;
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
  Admin_Configuartions_WeeklyReportTime_Edit = PERMISSIONS_CONSTANT.Admin_Configuartions_WeeklyReportTime_Edit;

  WeeklyReport_ReportDetail_ProjectHealthCriteria_View = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_ProjectHealthCriteria_View
  WeeklyReport_ReportDetail_ProjectHealthCriteria_ChangeStatus = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_ProjectHealthCriteria_ChangeStatus
  WeeklyReport_ReportDetail_ProjectHealthCriteria_Edit = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_ProjectHealthCriteria_Edit

  WeeklyReport_ReportDetail_PQAIssue_View = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PQAIssue_View
  WeeklyReport_ReportDetail_PQAIssue_AddMeetingNote = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PQAIssue_AddMeetingNote
  WeeklyReport_ReportDetail_PQAIssue_SetDone = PERMISSIONS_CONSTANT.WeeklyReport_ReportDetail_PQAIssue_SetDone

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

  readonly SEC_WARNING = 30;
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
  public projectStatus:string="ALL";
  public weeklyReportList: projectReportDto[] = [];
  public futureReportList: projectReportDto[] = [];
  public problemList: projectProblemDto[] = [];
  public PQAList: projectProblemDto[] = [];
  public problemIssueList: string[] = Object.keys(this.APP_ENUM.ProjectHealth);
  public projectRoleList: string[] = Object.keys(this.APP_ENUM.ProjectUserRole);
  public issueStatusList: string[] = Object.keys(this.APP_ENUM.PMReportProjectIssueStatus);
  public activeReportId: number;
  public typeSort: string = "No_Order";
  public weeklyReportStatus: string;

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
  public projectCurrentSupportUser: any = []
  public mondayOf5weeksAgo: any
  public lastWeekSunday: any
  public tempResourceList: any[] = []
  public officalResourceList: any[] = [];

  totalNormalWorkingTime: number = 0;
  totalOverTime: number = 0;
  overTimeNoCharge: number = 0;

  totalNormalWorkingTimeOfWeekly: number = 0;
  totalNormalWorkingTime1: number = 0;
  totalOverTime1: number = 0;
  totalNormalWorkingTimeOfWeekly1: number = 0;
  overTimeNoCharge1: number = 0;
  totalNormalWorkingTimeStandard: number = 0;
  totalNormalWorkingTimeStandard1: number = 0;

  sidebarExpanded: boolean;
  isShowCurrentResource: boolean = true;
  searchUser: string = ""
  isTimmerCounting: boolean = false
  isStopCounting: boolean = false
  isRefresh: boolean = false
  isStart: boolean = false
  isShowWarning = false;
  countdownInterval: FormControl = new FormControl(null, [Validators.min(30)]);
  isShowSettingCountDown = false;
  isShowWarningTimeOut = false;

  public listCriteria: ProjectCriteriaDto[] = []
  public listCriteriaResult: ProjectCriteriaResultDto[] = []
  public bgFlag: string = 'bg-success'
  public status: string = 'Green'
  public processCriteria: boolean = false

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
    private _configuration: AppConfigurationService,
    private pjCriteriaService: CriteriaService,
    private pjCriteriaResultService: ProjectCriteriaResultService,
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
      //this.projectStatus = ""
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
      let projectTypeFromUrl = this.route.snapshot.queryParamMap.get('projectType')
      if (projectTypeFromUrl) {
        this.projectType = projectTypeFromUrl
      }
      //this.getPmReportProject();
      this.getUser();
      this._layoutStore.sidebarExpanded.subscribe((value) => {
        this.sidebarExpanded = value;
      });

      this.pmReportService.filterProjectHealth.subscribe(projectStatus=>{
        this.projectStatus = projectStatus;
        this.getPmReportProject();
      });

      this.pmReportService.filterSort.subscribe(typeSort => {
        this.typeSort = typeSort;
        this.getPmReportProject();
      });
      this.getTimeCountDown();
    }
  }

  public startTimmer() {
    this.refreshTimmer();
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
    this.timmerCount?.reset()
    this.isTimmerCounting = false
    this.isRefresh = true
    this.isStart = false
    this.isShowWarning = false;
    this.isShowWarningTimeOut = false;
  }
  public resumeTimmer() {
    this.timmerCount.resume()
    this.isTimmerCounting = true
    this.isStopCounting = false
    this.isRefresh = false
  }
  public getPmReportProject(): void {
    if (this.router.url.includes("weeklyReportTabDetail") && this.pmReportId) {
      this.pmReportProjectService.GetAllByPmReport(this.pmReportId, this.projectType, this.projectStatus, this.typeSort)
      .subscribe((data => {
        this.pmReportProjectList = data.result;
        this.tempPmReportProjectList = data.result;
        this.projectId = this.pmReportProjectList[0]?.projectId
        this.generalNote = this.pmReportProjectList[0]?.note
        this.automationNote = this.pmReportProjectList[0]?.automationNote
        this.totalOverTime = this.pmReportProjectList[0]?.totalOverTime
        this.projectHealth = this.APP_ENUM.ProjectHealth[this.pmReportProjectList[0]?.projectHealth]
        //this.pmReportProjectService.projectHealth = this.projectHealth
        this.pmReportProjectId = this.pmReportProjectList[0]?.id
        if (this.pmReportProjectList[0]) {
          this.pmReportProjectList[0].setBackground = true
        }
        this.getAllCriteria();
        this.getProjectInfo();
        this.getChangedResource();
        this.getFuturereport();
        this.getProjectProblem();
        this.search()
      }))
    }
  }


  public getAllCriteria() {
    forkJoin([this.pjCriteriaService.getAll(), this.pjCriteriaResultService.getAllCriteriaResult(this.projectId, this.pmReportId)])
      .subscribe(([resCriteria, resCriteriaResult]) => {
        this.bgFlag = 'bg-success';
        this.status = 'Green'
        this.listCriteriaResult = []
        const listTmpCriteria = resCriteria.result as ProjectCriteriaDto[];
        const listTmpCriteriaResult = resCriteriaResult.result as ProjectCriteriaResultDto[];
        for (let i = 0; i < listTmpCriteria.length; i++) {
          const criteria = listTmpCriteria[i];
          const check = listTmpCriteriaResult.find(item => item.projectCriteriaId === listTmpCriteria[i].id);
          const itemCriteriaResult = {
            criteriaName: criteria.name,
            note: check?.note || '',
            status: check?.status,
            editMode: false,
            projectCriteriaId: criteria.id,
            projectId: Number(this.projectId),
            pmReportId: Number(this?.pmReportId),
            id: check?.id || undefined,
            isActive: criteria.isActive
          } as ProjectCriteriaResultDto
          if (this.isActive == true) {
            if (itemCriteriaResult.id) {
              this.listCriteriaResult.push(itemCriteriaResult);
            }
            else if (itemCriteriaResult.isActive) {
              this.listCriteriaResult.push(itemCriteriaResult);
            }
          }
          else {
            if (itemCriteriaResult.id) {
              this.listCriteriaResult.push(itemCriteriaResult);
            }
          }
        }
        for (let i = 0; i < this.listCriteriaResult.length; i++) {
          if (this.listCriteriaResult[i].status === APP_ENUMS.ProjectHealth.Red) {
            this.bgFlag = 'bg-danger';
            this.status = 'Red';
            break;
          }
          else if (this.listCriteriaResult[i].status === APP_ENUMS.ProjectHealth.Yellow) {
            this.bgFlag = 'bg-warning';
            this.status = 'Yellow'
          }
        }
    })
  }

  public handleEditCriteriaResult(index: number) {
    this.listCriteriaResult[index].editMode = true;
    this.processCriteria = true;
  }

  public cancelEditCriteriaResult(index: number) {
    this.listCriteriaResult[index].editMode = false;
    this.processCriteria = false;
    this.getAllCriteria();
  }

  public handleChangeStatus(item: ProjectCriteriaResultDto) {
    if (!item.editMode) {
      if (item.id) {
        this.pjCriteriaResultService.update(item).subscribe(res => {
          abp.notify.success(`Change status ${item.criteriaName} successfully`);
          this.getAllCriteria();
          this.getProjectProblem();
        });
      }
      else {
        this.pjCriteriaResultService.create(item).subscribe(res => {
          abp.notify.success(`Change status ${item.criteriaName} successfully`);
          this.getAllCriteria();
          this.getProjectProblem();
        })
      }
    }
  }

  public saveCriteriaResult(item: ProjectCriteriaResultDto) {
    item.pmReportId = this.pmReportId;
    if (item.id) {
      this.pjCriteriaResultService.update(item).subscribe(res => {
        abp.notify.success(`Update ${item.criteriaName} successfully`);
        item.editMode = false;
        this.processCriteria = false;
        this.getProjectProblem();
      });
    }
    else {
      this.pjCriteriaResultService.create(item).subscribe(res => {
        abp.notify.success(`Update ${item.criteriaName} successfully`);
        item.editMode = false;
        this.processCriteria = false;
        this.getProjectProblem();
      })
    }
  }




  getProjectInfo() {
    this.isLoading = true;
    if (this.pmReportProjectId) {
      this.pmReportProjectService.GetInfoProject(this.pmReportProjectId).pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
        this.projectInfo = data.result
        this.generalNote = data.result.pmNote
        this.automationNote= data.result.automationNote
        this.isLoading = false;
        this.getDataForBillChart(this.projectInfo.projectCode)
        this.getCurrentResourceOfProject(this.projectInfo.projectCode)
        if (this.weeklyReportStatus == 'Sent') {
          this.router.navigate(
            [],
            {
              relativeTo: this.route,
              queryParams: {
                name: this.projectInfo.projectName,
                client: this.projectInfo.clientName,
                clientCode: this.projectInfo.clientCode,
                pmName: this.projectInfo.pmName,
                pmReportProjectId: this.pmReportProjectId,
                projectHealth: this.projectHealth,
                projectType: this.projectType
              },
              queryParamsHandling: 'merge',
            });
        }
        else {
          this.router.navigate(
            [],
            {
              relativeTo: this.route,
              queryParams: {
                name: this.projectInfo.projectName,
                client: this.projectInfo.clientName,
                clientCode: this.projectInfo.clientCode,
                pmName: this.projectInfo.pmName,
                pmReportProjectId: this.pmReportProjectId,
                projectHealth: 0,
                projectType: this.projectType
              },
              queryParamsHandling: 'merge',
            });
        }
      },
        () => { this.isLoading = false })
    }
  }
  public view(projectReport) {
    this.pmReportProjectId = projectReport.id
    this.projectId = projectReport.projectId;
    this.isEditingNote = false;
    this.isEditingAutomationNote = false
    this.projectHealth = this.APP_ENUM.ProjectHealth[projectReport.projectHealth];
    this.pmReportProjectService.projectHealth = this.projectHealth
    this.pmReportProjectList.forEach(element => {
      if (element.projectId == projectReport.projectId) {
        element.setBackground = true;
      } else {
        element.setBackground = false;
      }
    });
    this.totalOverTime = projectReport.totalOverTime
    this.generalNote = projectReport.note
    this.automationNote = projectReport.automationNote

    this.getAllCriteria();
    this.getProjectProblem();
    this.getProjectInfo();
    this.getChangedResource();
    this.getFuturereport();
    this.isEditWeeklyReport = false;
    this.isEditFutureReport = false;
    this.isEditProblem = false;
    this.processFuture = false;
    this.processProblem = false
    this.processWeekly = false;
    this.searchUser = ""
    this.getTimeCountDown(true);
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
          this.problemList = [];
          this.PQAList = []
          for (let i = 0; i < data.result.result.length; i++) {
            if (data.result.result[i].reportType === EProjectReport.PM) {
              this.problemList.push(data.result.result[i]);
            }
            else {
              this.PQAList.push(data.result.result[i])
            }
          }

          this.projectHealth = data.result.projectHealth;
          this.weeklyReportStatus = data.result.status;
          if (data.result.status === 'Sent') {
            this.pmReportProjectService.projectHealth = this.projectHealth
            this.router.navigate(
              [],
              {
                relativeTo: this.route,
                queryParams: {
                  name: this.projectInfo.projectName,
                  client: this.projectInfo.clientName,
                  clientCode: this.projectInfo.clientCode,
                  pmName: this.projectInfo.pmName,
                  pmReportProjectId: this.pmReportProjectId,
                  projectHealth: this.projectHealth,
                  projectType: this.projectType
                },
                queryParamsHandling: 'merge',
              });
          }
          else {
            this.pmReportProjectService.projectHealth = ""
            this.router.navigate(
              [],
              {
                relativeTo: this.route,
                queryParams: {
                  name: this.projectInfo.projectName,
                  client: this.projectInfo.clientName,
                  clientCode: this.projectInfo.clientCode,
                  pmName: this.projectInfo.pmName,
                  pmReportProjectId: this.pmReportProjectId,
                  projectHealth: 0,
                  projectType: this.projectType
                },
                queryParamsHandling: 'merge',
              });
          }
          this.pmReportProjectList.find(x => x.id == data.result.pmReportProjectId).status = data.result.status
          this.pmReportProjectList.find(x => x.id == data.result.pmReportProjectId).projectHealth = data.result.projectHealthString

        } else {
          this.problemList = [];
          this.PQAList = [];

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

  setDone(issue) {
    this.pmReportProjectService.SetDoneIssue(issue.id).subscribe((res) => {
      if (res) {
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


  public getUser(): void {
    this.userService.GetAllUserActive(false).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.userList = data.result;
    })
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
    newIssue.reportType = EProjectReport.PM
  }

  public addPQAIssueReport() {
    let newIssue = {} as projectProblemDto
    newIssue.createMode = true;
    newIssue.reportType = EProjectReport.PQA;
    this.PQAList.unshift(newIssue)
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
      let usersEmail = []
      this.pmReportProjectService.GetCurrentResourceOfProject(this.projectId)
        .pipe(catchError(this.pmReportProjectService.handleError)).subscribe(data => {
          this.totalNormalWorkingTime = 0
          this.totalOverTime = 0
          this.overTimeNoCharge = 0
          this.totalNormalWorkingTimeOfWeekly = 0
          this.totalNormalWorkingTime1 = 0
          this.totalNormalWorkingTimeOfWeekly1 = 0
          this.totalOverTime1 = 0
          this.overTimeNoCharge1 = 0
          this.totalNormalWorkingTimeStandard = 0
          this.totalNormalWorkingTimeStandard1 = 0
          this.projectCurrentResource = data.result
          this.projectCurrentResource.forEach(user => {
            if (user.isPool) {
              this.tempResourceList.push(user.emailAddress)
            }
            else {
              this.officalResourceList.push(user.emailAddress)
            }
            usersEmail.push(user.emailAddress)
            this.GetTimesheetWeeklyChartOfUserInProject(projectCode, user, this.mondayOf5weeksAgo, this.lastWeekSunday)
            this.GetTimesheetOfUserInProjectNew(projectCode, user, lastWeekMonday, this.lastWeekSunday)
          })
          this.getUserFromTimesheet(projectCode, usersEmail, lastWeekMonday)
          this.getDataForWeeklyChart(projectCode, this.mondayOf5weeksAgo, this.lastWeekSunday)
        })
    }
  }
  getUserFromTimesheet(projectCode, usersEmail, lastWeekMonday)
  {
    this.projectCurrentSupportUser = []
    this.pmReportProjectService.GetUserInProjectFromTimesheet(projectCode, usersEmail, this.mondayOf5weeksAgo, this.lastWeekSunday).subscribe(rs => {
      this.projectCurrentSupportUser = rs.result
      this.projectCurrentSupportUser.forEach(user => {
        this.GetTimesheetOfSupportUserInProject(projectCode, user, lastWeekMonday, this.lastWeekSunday)
        this.GetTimesheetWeeklyChartOfUserInProject(projectCode, user, this.mondayOf5weeksAgo, this.lastWeekSunday)
      })
    })
  }

  getTimesheetWorking() {
    const dialogRef = this.dialog.open(GetTimesheetWorkingComponent, {
      data: {
        dialogData: this.pmReportProjectId,
      },
      width: "500px",
      disableClose: true,
    });
  }

  getDataForWeeklyChart(projectCode, startTime, endTime) {
    let totalNormalAndOT = this.pmReportProjectService.GetTimesheetWeeklyChartOfProject(projectCode, startTime, endTime)
    let temp = null
    let offical = null
    let forkJoinRequest = [totalNormalAndOT]
    let apiPayload = {
      projectCode: projectCode,
      emails: this.officalResourceList,
      startDate: startTime,
      endDate: endTime
    }
    if (this.officalResourceList.length > 0) {
      apiPayload.emails = this.officalResourceList
      offical = this.pmReportProjectService.GetTimesheetWeeklyChartOfUserGroupInProject(apiPayload)
      forkJoinRequest.push(offical)
    }

    if (this.tempResourceList.length > 0) {
      apiPayload.emails = this.tempResourceList
      temp = this.pmReportProjectService.GetTimesheetWeeklyChartOfUserGroupInProject(apiPayload)
      forkJoinRequest.push(temp)
    }

    forkJoin(forkJoinRequest).subscribe((data: any) => {
      totalNormalAndOT = data[0].result
      offical = data[1]?.result
      temp = data[2]?.result
      this.buildProjectTSChart(totalNormalAndOT, offical, temp)
    });
  }


  getDataForBillChart(projectCode: string) {
    var now = new Date();
    var currentDate = this.formatDateYMD(new Date())
    let fiveMonthAgo: any =  new Date(now.setMonth(now.getMonth() - 5))
    fiveMonthAgo =  this.formatDateYMD(this.getFirstDayOfMonth(fiveMonthAgo.getFullYear(), fiveMonthAgo.getMonth()))

    let apiPayload = {
      projectCode: projectCode,
      startDate: fiveMonthAgo,
      endDate: currentDate
    }

    let effort = this.pmReportProjectService.GetEffortMonthlyChartProject(apiPayload)
    let bill = this.tsProjectService.GetBillInfoChart(this.projectId, fiveMonthAgo, currentDate)

    forkJoin([effort, bill]).subscribe(data => {
      effort = data[0].result
      bill = data[1].result
      this.buildBillChart(bill, effort)
    });

  }

  buildProjectTSChart(normalAndOTchartData, officalChartData, TempChartData) {
    setTimeout(() => {
      var chartDom = document.getElementById('timesheet-chart')!;
      var myChart = echarts.init(chartDom);
      var option: echarts.EChartsOption;

      let hasOtValue = normalAndOTchartData?.overTimeHours?.some(item => item > 0)
      let hasOfficalDataNormal = officalChartData?.normalWoringHours?.some(item => item > 0)
      let hasOfficalDataOT = officalChartData?.overTimeHours?.some(item => item > 0)
      let hasTempDataNormal = TempChartData?.normalWoringHours?.some(item => item > 0)
      let hasTempDataOT = TempChartData?.overTimeHours?.some(item => item > 0)
      let hasOtNoCharge = normalAndOTchartData?.otNoChargeHours?.some(item => item > 0)

      if (JSON.stringify(normalAndOTchartData?.normalWoringHours) == JSON.stringify(officalChartData?.normalWoringHours)) {
        hasOfficalDataNormal = false
      }
      if (JSON.stringify(normalAndOTchartData?.overTimeHours) == JSON.stringify(officalChartData?.overTimeHours)) {
        hasOfficalDataOT = false
      }

      option = {
        width: '90%',
        title: {
          text: 'Timesheet',
          show: false
        },
        tooltip: {
          trigger: 'axis'
        },
        legend: {
          itemGap: 4,
          left: '10%',
          right: '0',
          padding: [0, 0, 0, 0],
          data: ['Total normal', `${hasOtValue ? 'Total OT' : ''}`, `${hasOfficalDataNormal ? 'Normal Offical' : ''}`
            , `${hasOfficalDataOT ? 'OT Offical' : ''}`, `${hasTempDataNormal ? 'Normal Temp' : ''}`,
            `${hasTempDataOT ? 'OT Temp' : ''}`, `${hasOtNoCharge ? 'OT NoCharge' : ''}`],
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
          }, {
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
            symbolSize: 2,
            data: chartData.normalWoringHours,
            type: 'line',
            name: 'Normal',
          },
          {
            color: ['#dc3545'],
            symbolSize: 2,
            data: hasOtValue ? chartData.overTimeHours : [],
            type: 'line',
            name: 'OT',
            lineStyle: { color: '#dc3545' }
          },
          {
            color: ['orange'],
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
  GetTimesheetOfUserInProjectNew(projectCode, user, startTime, endTime) {
    this.pmReportProjectService.GetTimesheetOfUserInProjectNew(projectCode, user.emailAddress, startTime, endTime).subscribe(rs => {
      user.normalWorkingTime = rs.result ? rs.result.normalWorkingTime : 0
      user.overTime = rs.result ? rs.result.overTime : 0
      user.overTimeNoCharge = rs.result ? rs.result.overTimeNoCharge : 0
      user.normalWorkingTimeAll = rs.result ? rs.result.normalWorkingTimeAll : 0
      user.normalWorkingTimeStandard = rs.result ? rs.result.normalWorkingTimeStandard : 0
      this.totalNormalWorkingTime += user.normalWorkingTime
      this.totalOverTime += user.overTime
      this.overTimeNoCharge += user.overTimeNoCharge
      this.totalNormalWorkingTimeOfWeekly += user.normalWorkingTimeAll
      this.totalNormalWorkingTimeStandard += user.normalWorkingTimeStandard
    })
  }
  GetTimesheetOfSupportUserInProject(projectCode, user, startTime, endTime) {
    this.pmReportProjectService.GetTimesheetOfUserInProjectNew(projectCode, user.emailAddress, startTime, endTime).subscribe(rs => {
      user.normalWorkingTime = rs.result ? rs.result.normalWorkingTime : 0
      user.overTime = rs.result ? rs.result.overTime : 0
      user.overTimeNoCharge = rs.result ? rs.result.overTimeNoCharge : 0
      user.normalWorkingTimeAll = rs.result ? rs.result.normalWorkingTimeAll : 0
      user.normalWorkingTimeStandard = rs.result ? rs.result.normalWorkingTimeStandard : 0
      this.totalNormalWorkingTime1 += user.normalWorkingTime
      this.totalOverTime1 += user.overTime
      this.overTimeNoCharge1 += user.overTimeNoCharge
      this.totalNormalWorkingTimeOfWeekly1 += user.normalWorkingTimeAll
      this.totalNormalWorkingTimeStandard1 += user.normalWorkingTimeStandard
    })
  }

  getFirstDayOfMonth(year, month) {
    return new Date(year, month, 1);
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
    setTimeout(() => {
      let chartDom = document.getElementById('bill-chart');

      let myChart = echarts.init(chartDom);
      let option: echarts.EChartsOption;
      option = {
        tooltip: {
          trigger: 'axis',
        },
        title: {
          text: 'Bill info',
          show: false
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
  public editMeetingNote(projectIssue) {
    let item = {
      id: projectIssue.id,
      note: projectIssue.meetingSolution
    }
    let ref = this.dialog.open(EditMeetingNoteDialogComponent, {
      width: "600px",
      data: item
    })
    ref.afterClosed().subscribe(rs => {
      if (rs) {
        this.getProjectProblem()
      }
    })

  }
  isShowChangeDoneText(issue){
    return this.APP_ENUM.PMReportProjectIssueStatus[issue.status] == this.APP_ENUM.PMReportProjectIssueStatus.InProgress
  }
  isShowChangeInProgressText(issue){
    return this.APP_ENUM.PMReportProjectIssueStatus[issue.status] == this.APP_ENUM.PMReportProjectIssueStatus.Done
  }

  onTick(data: TimeInterface) {
    if (!this.isShowWarning && data.minutes === 0 && data.seconds <= 30) {
      this.isShowSettingCountDown = false;
      this.isShowWarning = true;
    }
    if (this.isShowWarning && data.minutes === 0 && data.seconds === 0) {
      this.isShowWarningTimeOut = true;
    }

  }

  setTimeCountDown() {
    if(this.countdownInterval.invalid) return
    this._configuration.setTimeCountDown(this.countdownInterval.value).subscribe((data) => {
      abp.notify.success('Update time was successfully!');
      this.closeSettingCountDown();
      this.refreshTimmer();
    });
  }

  getTimeCountDown(autoStart: boolean = false) {
    this._configuration.getTimeCountDown().subscribe((rs) => {
      this.countdownInterval.setValue(rs.result.timeCountDown);
      if(autoStart) setTimeout(() => this.startTimmer());
    });
  }

  openSettingCountDown() {
    this.isShowSettingCountDown = true;
  }

  closeSettingCountDown() {
    this.isShowSettingCountDown = !this.isShowSettingCountDown;
    this.getTimeCountDown();
  }
}
