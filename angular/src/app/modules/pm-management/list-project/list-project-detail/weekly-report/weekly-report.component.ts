import { ListProjectService } from '@app/service/api/list-project.service';
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
import { UpdateUserSkillDialogComponent } from '@app/users/update-user-skill-dialog/update-user-skill-dialog.component';
import { ReleaseUserDialogComponent } from '../resource-management/release-user-dialog/release-user-dialog.component';
import { ConfirmPopupComponent } from '../resource-management/confirm-popup/confirm-popup.component';

@Component({
  selector: 'app-weekly-report',
  templateUrl: './weekly-report.component.html',
  styleUrls: ['./weekly-report.component.css']
})
export class WeeklyReportComponent extends AppComponentBase implements OnInit {
  PmManager_PMReportProject_Create = PERMISSIONS_CONSTANT.PmManager_PMReportProject_Create;
  PmManager_PMReportProject_Delete = PERMISSIONS_CONSTANT.PmManager_PMReportProject_Delete;
  PmManager_PMReportProject_Update = PERMISSIONS_CONSTANT.PmManager_PMReportProject_Update;
  PmManager_PMReportProject_GetAllByPmProject = PERMISSIONS_CONSTANT.PmManager_PMReportProject_GetAllByPmReport;
  PmManager_PMReportProject_ResourceChangesDuringTheWeek = PERMISSIONS_CONSTANT.PmManager_PMReportProject_ResourceChangesDuringTheWeek;
  PmManager_PMReportProject_ResourceChangesInTheFuture = PERMISSIONS_CONSTANT.PmManager_PMReportProject_ResourceChangesInTheFuture;
  PmManager_PMReportProject_SendReport = PERMISSIONS_CONSTANT.PmManager_PMReportProject_SendReport;
  PmManager_PMReportProjectIssue = PERMISSIONS_CONSTANT.PmManager_PMReportProjectIssue;
  PmManager_PMReportProjectIssue_Create = PERMISSIONS_CONSTANT.PmManager_PMReportProjectIssue_Create;
  PmManager_PMReportProjectIssue_Delete = PERMISSIONS_CONSTANT.PmManager_PMReportProjectIssue_Delete;
  PmManager_PMReportProjectIssue_ProblemsOfTheWeek = PERMISSIONS_CONSTANT.PmManager_PMReportProjectIssue_ProblemsOfTheWeek;
  PmManager_PMReportProjectIssue_Update = PERMISSIONS_CONSTANT.PmManager_PMReportProjectIssue_Update;
  PmManager_ResourceRequest_ApproveUser = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_ApproveUser;
  PmManager_ResourceRequest_RejectUser = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_RejectUser;
  PmManager_PMReportProject_UpdatePmReportProjectHealth = PERMISSIONS_CONSTANT.PmManager_PMReportProject_UpdatePmReportProjectHealth
  PmManager_PMReportProject_UpdateNote = PERMISSIONS_CONSTANT.PmManager_PMReportProject_UpdateNote

  // Paging
  public tomorrowDate = new Date();
  public itemPerPage: number = 50;
  public searchUser: string = "";
  public weeklyCurrentPage: number = 1;
  public futureCurrentPage: number = 1;
  public problemCurrentPage: number = 1;
  public processWeekly: boolean = false;
  public processFuture: boolean = false;
  public processProblem: boolean = false;
  public searchPmReport: string = "";
  public selectedReport = {} as pmReportDto;
  public isSentReport: boolean;
  public isEditingNote: boolean = false;

  public pmReportList: any = [];
  public weeklyPeportList: projectReportDto[] = [];
  public futureReportList: projectReportDto[] = [];
  public problemList: projectProblemDto[] = [];
  public isEditProblem: boolean = false;
  public isEditFutureReport: boolean = false;
  public minDate = new Date();
  public isEditWeeklyReport: boolean = false;
  public problemIssueList: string[] = Object.keys(this.APP_ENUM.ProjectHealth);
  public isssueStatusList: string[] = Object.keys(this.APP_ENUM.PMReportProjectIssueStatus)
  public userList: UserDto[] = [];
  public projectRoleList: string[] = Object.keys(this.APP_ENUM.ProjectUserRole);
  private projectId: number;
  generalNote: string = "";
  public projectName = "";
  allowSendReport: boolean = true;
  public projectHealth: any;


  public planResourceProcess = false
  public isEditPlannedResource = false


  constructor(injector: Injector, private reportService: PMReportProjectService, private route: ActivatedRoute, private requestservice: ProjectResourceRequestService,
    private projectUserService: ProjectUserService, private userService: UserService, private reportIssueService: PmReportIssueService, private dialog: MatDialog,
    private pmreportService: PmReportService, private projectService: ListProjectService) {
    super(injector);
    this.projectId = Number(route.snapshot.queryParamMap.get("id"));
  }

  ngOnInit(): void {
    this.tomorrowDate.setDate(this.tomorrowDate.getDate() + 1)

    this.getAllPmReport();
    this.getUser();
    this.minDate.setDate(this.minDate.getDate() + 1)
    this.getProjectById()
  }
  public getWeeklyReport() {
    this.reportService.getChangesDuringWeek(this.projectId, this.selectedReport.reportId).pipe(catchError(this.reportService.handleError)).subscribe(data => {
      this.weeklyPeportList = data.result;
    })
  }

  public getFuturereport(): void {
    if (this.permission.isGranted(this.PmManager_PMReportProject_ResourceChangesInTheFuture)) {
      this.reportService.getChangesInFuture(this.projectId, this.selectedReport.reportId).pipe(catchError(this.reportService.handleError)).subscribe(data => {
        this.futureReportList = data.result
      })
    }


  }
  getProjectById() {
    this.projectService.getProjectById(this.projectId).pipe(catchError(this.projectService.handleError)).subscribe(rs => {
      this.projectName = rs.result.name
    })
  }
  public getUser(): void {
    this.userService.GetAllUserActive(false).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.userList = data.result;
    })
  }
  private getProjectProblem(): void {
    if (this.permission.isGranted(this.PmManager_PMReportProjectIssue_ProblemsOfTheWeek)) {
      this.reportIssueService.getProblemsOfTheWeek(this.projectId, this.selectedReport.reportId).pipe(catchError(this.reportIssueService.handleError)).subscribe(data => {
        this.problemList = data.result;
        // this.projectHealth = data.result.projectHealth;
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
    // report.isFutureActive = false
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
          this.getWeeklyReport();
          this.isEditFutureReport = false;
          this.processWeekly = false;
        })
      },
        () => {
          report.createMode = true
        })
    } else {
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
    this.searchUser = ""


  }
  public cancelWeekReport() {
    this.processWeekly = false;
    this.isEditWeeklyReport = false;
    this.getWeeklyReport();
    this.searchUser = ""
  }
  updateWeekReport(report) {
    this.processWeekly = true
    this.isEditWeeklyReport = true;
    report.createMode = true;
    report.projectRole = this.APP_ENUM.ProjectUserRole[report.projectRole]
    console.log("aaaaaaaaaaaa", report);
  }

  deleteWeekReport(report) {

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

  // Future Report
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
  //         this.isEditFutureReport = false;
  //         this.processFuture = false
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
  //       this.isEditFutureReport = false
  //       this.getFuturereport();
  //     },
  //       () => {
  //         report.createMode = true
  //       })
  //   }
  //   this.searchUser = ""
  // }
  // public cancelFutureReport() {
  //   this.isEditFutureReport = false;
  //   this.processFuture = false;
  //   this.getFuturereport();
  //   this.searchUser = ""
  // }
  // public approveRequest(resource: projectUserDto): void {
  //   this.showDialog(resource);

  // }
  // showDialog(resource: any): void {
  //   let dialogData = {}
  //   dialogData = {
  //     id: resource.id,
  //     userId: resource.userId,
  //     fullName: resource.fullName,
  //     projectRole: resource.projectRole,
  //     startTime: resource.startTime,
  //     allocatePercentage: resource.allocatePercentage
  //   }
  //   const dialogRef = this.dialog.open(ApproveDialogComponent, {
  //     data: {
  //       dialogData: dialogData,
  //     },
  //     width: "700px",
  //     disableClose: true,
  //   });
  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result) {
  //       this.getFuturereport();
  //       this.getWeeklyReport();
  //     }
  //   });

  // }
  // public rejectRequest(report): void {
  //   this.requestservice.rejectRequest(report.id).pipe(catchError(this.requestservice.handleError)).subscribe(data => {
  //     abp.notify.success("Rejected request")
  //     this.getFuturereport();
  //   })
  // }
  // public updateRequest(report): void {
  //   this.processFuture = true
  //   this.isEditFutureReport = true;
  //   report.createMode = true;
  //   report.projectRole = this.APP_ENUM.ProjectUserRole[report.projectRole]
  // }

  // Project Issue
  public getAllPmReport() {
    this.reportService.GetAllByProject(this.projectId).pipe(catchError(this.reportService.handleError)).subscribe(data => {
      this.pmReportList = data.result;
      this.selectedReport = this.pmReportList.filter(item => item.isActive == true)[0];
      this.isSentReport = this.selectedReport.status == 'Draft' ? true : false
      this.generalNote = this.selectedReport.note
      this.allowSendReport = this.selectedReport.note == null || this.selectedReport.note == '' ? false : true;
      this.projectHealth = this.APP_ENUM.ProjectHealth[this.selectedReport.projectHealth]
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
    this.getProjectProblem();
    this.isEditProblem = false;
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
  public onReportchange() {
    this.getWeeklyReport();
    this.getFuturereport();
    this.getProjectProblem();
    this.generalNote = this.selectedReport.note
    this.isEditingNote = false;
    this.projectHealth = this.APP_ENUM.ProjectHealth[this.selectedReport.projectHealth]
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
  public canCelUpdateNote() {
    this.isEditingNote = false;
    this.generalNote = this.selectedReport.note

    this.allowSendReport = this.generalNote ? true : false
  }

  updateHealth(projectHealth) {
    this.reportService.updateHealth(this.selectedReport.pmReportProjectId, projectHealth).subscribe((data) => {
      abp.notify.success("Update project health to " + this.getByEnum(projectHealth, this.APP_ENUM.ProjectHealth))
    })

  }
  getProjectHealthByEnum(enumValue: string, enumObject: any) {
    for (const value in enumObject) {
      if (enumObject[enumValue] == value) {
        return value;
      }
    }
  }
  public getByEnum(enumValue: number, enumObject: any) {
    for (const key in enumObject) {
      if (enumObject[key] == enumValue) {
        return key;
      }
    }
  }

  // getWeekPercentage(report, data) {
  //   report.allocatePercentage = data
  // }
  // getFuturePercentage(report, data) {
  //   report.allocatePercentage = data
  // }
  // updateHealth(projectHealth) {
  //   this.reportService.updateHealth(this.selectedReport.pmReportProjectId, projectHealth).pipe(catchError(this.reportService.handleError))
  //     .subscribe((data) => {
  //       this.pmReportProjectList.forEach(item => {
  //         if (item.id == this.pmReportProjectId) {
  //           item.projectHealth = this.getByEnum(projectHealth, this.APP_ENUM.ProjectHealth)
  //         }
  //         abp.notify.success("Update successfull")
  //       })
  //       this.getWeeklyReport();
  //       this.getFuturereport();
  //       this.getProjectProblem()
  //     })
  // }
  updateUserSkill(user) {
    let ref = this.dialog.open(UpdateUserSkillDialogComponent, {
      width: "700px",
      data: {
        userSkills: user?.userSkills,
        id: user.userId,
        fullName: user.fullName
      }

    });
    ref.afterClosed().subscribe(rs => {
      if (rs) {
        this.getWeeklyReport();
        this.getFuturereport();
        this.planResourceProcess;
      }
    })
  }


  // Planned resource
  confirm(user) {
    if (user.allocatePercentage <= 0) {
      let ref = this.dialog.open(ReleaseUserDialogComponent, {
        width: "700px",
        data: {
          user: user,
          type: "confirmOut",
          page: "weekly"
        },
      })
      ref.afterClosed().subscribe(rs => {
        if (rs) {
          this.getFuturereport();
          this.getWeeklyReport();
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
            page: "weekly"

          }
        })

        ref.afterClosed().subscribe(rs => {
          if (rs) {
            this.getWeeklyReport();
            this.getFuturereport();
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
          this.reportService.CancelResourcePlan(user.id).subscribe(rs => {
            abp.notify.success(`Cancel plan for user ${user.fullName}`)
            this.getFuturereport();
          })
        }
      },
      true
    )
  }

  saveResourcePlan(projectUser) {
    projectUser.projectId = this.projectId
    this.reportService.EditProjectUserPlan(projectUser).subscribe(rs => {
      abp.notify.success(`Edited plan for user ${projectUser.fullName}`)
      this.getFuturereport();
    })
  }

  public addPlanResource() {
    let newPlan = {} as any
    newPlan.isPool = false
    newPlan.allocatePercentage = 100
    newPlan.createMode = true;
    this.futureReportList.unshift(newPlan)
    this.planResourceProcess = true;
  }
  cancelPlanResourceProcess(user) {
    this.getFuturereport();;
    this.planResourceProcess = false
    this.searchUser = ""
  }


  savePlanResource(projectUser) {
    if (this.isEditPlannedResource) {
      let requestBody = {
        projectUserId: projectUser.id,
        projectId: projectUser.projectId,
        startTime: moment(projectUser.startTime).format("YYYY-MM-DD"),
        allocatePercentage: projectUser.allocatePercentage,
        note: projectUser.note,
        isPool: projectUser.isPool,
        projectRole: projectUser.projectRole
      }
      this.reportService.EditProjectUserPlan(requestBody).pipe(catchError(this.reportService.handleError)).subscribe(rs => {
        abp.notify.success(`Edited plan for user ${projectUser.fullName}`)
        this.getFuturereport();
        this.planResourceProcess = false
        this.isEditPlannedResource = false
      })
    }
    else {
      let requestBody = {
        userId: projectUser.userId,
        projectId: this.projectId,
        isPool: projectUser.isPool,
        allocatePercentage: projectUser.allocatePercentage,
        startTime: moment(projectUser.startTime).format("YYYY-MM-DD"),
        note: projectUser.note,
        projectRole: projectUser.projectRole
      }
      this.reportService.PlanNewResourceToProject(requestBody).pipe(catchError(this.reportService.handleError)).subscribe(rs => {
        abp.notify.success("added new plan to project")
        this.getFuturereport();
        this.planResourceProcess = false;
      })
    }

  }
  editResourcePlan(projectUser) {
    projectUser.editMode = true
    this.isEditPlannedResource = true;
    this.planResourceProcess = true
  }

  onPlanUserSelect(user, u){
    user.userSkills = u.userSkills
    user.userId = u.id
  }
}

