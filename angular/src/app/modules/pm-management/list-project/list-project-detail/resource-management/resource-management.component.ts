import { MatDialog } from '@angular/material/dialog';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';

import { UserDto } from './../../../../../../shared/service-proxies/service-proxies';
import { UserService } from './../../../../../service/api/user.service';
import { ActivatedRoute } from '@angular/router';
import { projectUserDto, projectResourceRequestDto, projectUserBillDto } from './../../../../../service/model/project.dto';
import { ProjectResourceRequestService } from './../../../../../service/api/project-resource-request.service';
import { ProjectUserBillService } from './../../../../../service/api/project-user-bill.service';
import { ProjectUserService } from './../../../../../service/api/project-user.service';
import { AppComponentBase } from 'shared/app-component-base';
import { Component, Injector, OnInit, Input } from '@angular/core';
import { ClientDto } from '@app/service/model/list-project.dto';
import { InputFilterDto } from '@shared/filter/filter.component';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize, catchError } from 'rxjs/operators';
import * as moment from 'moment';
import { UpdateUserSkillDialogComponent } from '@app/users/update-user-skill-dialog/update-user-skill-dialog.component';
import { ReleaseUserDialogComponent } from './release-user-dialog/release-user-dialog.component';
import { ConfirmPopupComponent } from './confirm-popup/confirm-popup.component';

@Component({
  selector: 'app-resource-management',
  templateUrl: './resource-management.component.html',
  styleUrls: ['./resource-management.component.css']
})
export class ResourceManagementComponent extends AppComponentBase implements OnInit {
  PmManager_ProjectUser = PERMISSIONS_CONSTANT.PmManager_ProjectUser_ViewAllByProject;
  PmManager_ProjectUser_Create = PERMISSIONS_CONSTANT.PmManager_ProjectUser_Create;
  PmManager_ProjectUser_Delete = PERMISSIONS_CONSTANT.PmManager_ProjectUser_Delete;
  PmManager_ProjectUser_Update = PERMISSIONS_CONSTANT.PmManager_ProjectUser_Update;

  PmManager_ResourceRequest_Create = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_Create
  PmManager_ResourceRequest_Delete = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_Delete
  PmManager_ResourceRequest_Update = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_Update
  private projectId: number;
  public userBillCurrentPage = 1;
  public resourceRequestCurrentPage = 1;
  public userListCurrentPage = 1;
  public itemPerPage = 50;
  public isEditUserProject: boolean = false;
  public searchUser: string = "";
  public searchUserBill: string = "";


  // project user
  public projectUserList: projectUserDto[] = [];
  public projectRoleList: string[] = Object.keys(this.APP_ENUM.ProjectUserRole)
  public userStatusList: string[] = Object.keys(this.APP_ENUM.ProjectUserStatus)
  public userForProjectUser: UserDto[] = [];
  public viewHistory: boolean = false;
  public projectUserProcess: boolean = false;
  public isShowProjectUser: boolean = true;
  // resource request
  public resourceRequestList: projectResourceRequestDto[] = [];
  public requestStatusList: string[] = Object.keys(this.APP_ENUM.ResourceRequestStatus);
  public isEditRequest: boolean = false;
  public requestProcess: boolean = false;
  public isShowRequest: boolean = false;
  // plan resource
  public planResourceProcess: boolean = false;
  public plannedUserList: any = []
  public resourceListCurrentPage = 1
  public isShowCurrentResouce: boolean = true;
  public isEditPlannedResource: boolean = false
  public searchPlanResource: string = ""




  PmManager_ResourceRequest_ViewAllByProject = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_ViewAllByProject
  constructor(injector: Injector, private projectUserService: ProjectUserService, private projectUserBillService: ProjectUserBillService, private userService: UserService,
    private projectRequestService: ProjectResourceRequestService, private route: ActivatedRoute, private dialog: MatDialog) { super(injector) }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: "Name", comparisions: [0, 6, 7, 8] },
  ];
  ngOnInit(): void {
    this.projectId = Number(this.route.snapshot.queryParamMap.get("id"));
    this.getProjectUser();
    this.getResourceRequestList();
    this.getAllUser();
    this.getPlannedtUser();

  }
  // get data
  private getProjectUser() {
    if (this.permission.isGranted(this.PmManager_ProjectUser)) {
      this.projectUserService.getAllProjectUser(this.projectId, this.viewHistory).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        this.projectUserList = data.result;
      })
    }

  }

  private getPlannedtUser() {
    this.projectUserService.GetAllPlannedUserByProject(this.projectId).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
      this.plannedUserList = data.result;
    })

  }


  private getResourceRequestList(): void {
    if (this.permission.isGranted(this.PmManager_ResourceRequest_ViewAllByProject)) {
      this.projectRequestService.getAllResourceRequest(this.projectId).pipe(catchError(this.projectRequestService.handleError)).subscribe(data => {
        this.resourceRequestList = data.result
      })
    }
  }

  private getAllUser() {
    this.userService.GetAllUserActive(false, false).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.userForProjectUser = data.result;
      // this.userForUserBill = data.result;
    })
  }



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
        this.getProjectUser()
        this.getPlannedtUser()
      }
    })
  }

  releaseUser(user) {
    let ref = this.dialog.open(ReleaseUserDialogComponent, {
      width: "700px",
      data: {
        user: user
      }
    })
    ref.afterClosed().subscribe(rs => {
      if (rs) {
        this.getProjectUser()
        this.getPlannedtUser()
      }
    })
  }

  //  project user

  public addProjectUser() {
    let newUser = {} as projectUserDto
    newUser.isPool = false;
    newUser.startTime = moment(new Date()).format("YYYY-MM-DD")
    newUser.createMode = true;
    this.projectUserList.unshift(newUser)
    this.projectUserProcess = true;
  }


  public getValueByEnum(enumValue: number, enumObject) {
    for (const key in enumObject) {
      if (enumObject[key] == enumValue) {
        return key;
      }
    }
  }

  saveProjectUser(user: any) {

    if (this.isEditUserProject) {
      this.updateProjectCurrentResource(user)
    }
    else {
      user.userId = user.userInfo.id
      let workingProject = [];
      this.projectUserService.GetAllWorkingProjectByUserId(user.userId).subscribe(data => {
        workingProject = data.result
        if (workingProject.length > 0) {
          let message: string = ""
          workingProject.forEach(project => {
            message += `<p>- <strong>${project.projectName} (${project.pmName}) </strong>from ${moment(project.startTime).format("DD/MM/YYYY")}</p>`
          })
          abp.message.confirm(
            `<div class='text-left'><div style= "font-size: 22px;" ><strong>${user.userInfo.fullName} </strong> is working on: </div> <br/>
            ${message}
           <div >
           Are you sure to add <strong>${user.userInfo.fullName}</strong> join project and release from other projects?
           </div>
              </div>`
            , `   `, (rs) => {
              if (rs) {
                this.AddUserToProject(user)
              }
            },
            true
          );
        }
        else {
          abp.message.confirm(`Add user <strong>${user.userInfo.fullName}</strong> to Project`, "", rs => {
            if (rs) {
              this.AddUserToProject(user)
            }
          }, true)
        }
      })
    }

  }

  AddUserToProject(user) {
    user.startTime = moment(user.startTime).format("YYYY-MM-DD")
    user.projectId = this.projectId
    delete user["createMode"]
    this.projectUserService.AddUserToProject(user).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
      this.getProjectUser();
      abp.notify.success(`Added new employee to project`);
      this.projectUserProcess = false
      this.searchUser = "";
    },
      () => {
        user.createMode = true
      })
  }
  updateProjectCurrentResource(user) {
    user.startTime = moment(user.startTime).format("YYYY-MM-DD")
    this.projectUserService.UpdateCurrentResourceDetail(user).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
      abp.notify.success(`updated user: ${user.userName}`);
      this.getProjectUser();
      this.isEditUserProject = false;
      user.editMode = false;
      this.projectUserProcess = false
      this.searchUser = "";
    })
  }

  editProjectUser(user) {
    this.isEditUserProject = true;
    user.editMode = true
    this.projectUserProcess = true
  }
  removeUser(user: projectUserDto) {
    abp.message.confirm(
      "Remove user: " + user.fullName + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.projectUserService.removeProjectUser(user.id).pipe(catchError(this.projectUserService.handleError)).subscribe(() => {
            abp.notify.success("Removed user " + user.fullName + " from project " + user.projectName);
            this.getProjectUser()
          });
        }
      }
    );
  }
  filterProjectUser(event) {
    this.viewHistory = event.checked;
    this.projectUserService.getAllProjectUser(this.projectId, this.viewHistory).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
      this.projectUserList = data.result;
    })
  }
  cancelProjectUser(user) {
    this.getProjectUser();
    this.isEditUserProject = false;
    user.editMode = false;
    this.projectUserProcess = false
    this.searchUser = ""
  }
  private filterProjectUserDropDown() {

    let userProjectList = this.projectUserList.map(item => item.userId)
    // this.userForProjectUser = this.userForUserBill.filter(user => userProjectList.indexOf(user.id) == -1)
  }
  // resource request

  public addResourcceRequest(): void {
    let newResource = {} as projectResourceRequestDto
    newResource.createMode = true;
    this.requestProcess = true;
    this.resourceRequestList.unshift(newResource)
  }


  public saveProjectRerequest(request: projectResourceRequestDto): void {
    delete request["createMode"]
    request.timeNeed = moment(request.timeNeed).format("YYYY-MM-DD");
    if (!this.isEditRequest) {
      request.projectId = this.projectId
      this.projectRequestService.create(request).pipe(catchError(this.projectRequestService.handleError)).subscribe(res => {
        abp.notify.success(`Created request: ${request.name}`)
        this.getResourceRequestList();
        this.requestProcess = false;
      },
        () => { request.createMode = true })
    }
    else {
      this.projectRequestService.update(request).pipe(catchError(this.projectRequestService.handleError)).subscribe(res => {
        abp.notify.success(`Updated request: ${request.name}`)
        this.getResourceRequestList();
        this.requestProcess = false;
        this.isEditRequest = false;

      },
        () => { request.createMode = true })
    }
  }

  public cancelProjectRerequest(): void {
    this.getResourceRequestList();
    this.requestProcess = false
    this.isEditRequest = false;
    this.searchUser = "";

  }
  public editProjectRerequest(request: projectResourceRequestDto): void {
    request.createMode = true
    this.requestProcess = true
    this.isEditRequest = true
  }
  public removeProjectRerequest(request: projectResourceRequestDto): void {
    abp.message.confirm(
      `Delete request: ${request.name}`,
      "",
      (result: boolean) => {
        if (result) {
          this.projectRequestService.deleteProjectRequest(request.id).pipe(catchError(this.projectRequestService.handleError)).subscribe(() => {
            abp.notify.success("Deleted request: " + request.name);
            this.getResourceRequestList();
          });

        }
      }
    );
  }


  public filterUser(userId: number) {
    return this.userForProjectUser.filter(item => item.id == userId)[0];
  }

  getPercentage(user, data) {
    user.allocatePercentage = data
  }

  //  Planned resource
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
          this.getPlannedtUser()
          this.getProjectUser()
        }
      })
    }
    else if (user.allocatePercentage > 0) {
      let workingProject = [];
      this.projectUserService.GetAllWorkingProjectByUserId(user.userId).subscribe(data => {
        workingProject = data.result
      let ref =  this.dialog.open(ConfirmPopupComponent,{
          width: '700px',
          data: {
            workingProject: workingProject,
            user: user,
            type: "confirmJoin"
          }
        })

        ref.afterClosed().subscribe(rs=>{
          if(rs){
            this.getProjectUser()
            this.getPlannedtUser()
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
            this.getPlannedtUser()
          })
        }
      },
      true
    )
  }

  saveResourcePlan(projectUser) {
    projectUser.projectId = this.projectId
    this.projectUserService.EditProjectUserPlan(projectUser).subscribe(rs => {
      abp.notify.success(`Edited plan for user ${projectUser.fullName}`)
      this.getPlannedtUser()
    })
  }

  public addPlanResource() {
    let newPlan = {} as any
    newPlan.isPool = false
    newPlan.allocatePercentage = 100
    newPlan.createMode = true;
    this.plannedUserList.unshift(newPlan)
    this.planResourceProcess = true;
  }
  cancelPlanResourceProcess(user) {
    this.getPlannedtUser();
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
      this.projectUserService.EditProjectUserPlan(requestBody).pipe(catchError(this.projectUserService.handleError)).subscribe(rs => {
        abp.notify.success(`Edited plan for user ${projectUser.fullName}`)
        this.getPlannedtUser()
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
      this.projectUserService.PlanNewResourceToProject(requestBody).pipe(catchError(this.projectUserService.handleError)).subscribe(rs => {
        abp.notify.success("added new plan to project")
        this.getPlannedtUser()
        this.planResourceProcess = false;
      })
    }

  }
  editResourcePlan(projectUser) {
    projectUser.editMode = true
    this.isEditPlannedResource = true;
    this.planResourceProcess = true
  }
}

