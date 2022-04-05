import { ResourcePlanDto } from './../../../../../service/model/resource-plan.dto';
import { IDNameDto } from './../../../../../service/model/id-name.dto';
import { FormPlanUserComponent } from './../../../../delivery-management/delivery/request-resource-tab/form-plan-user/form-plan-user.component';
import { FormSetDoneComponent } from '@app/modules/delivery-management/delivery/request-resource-tab/form-set-done/form-set-done.component';
import { CreateUpdateResourceRequestComponent } from '@app/modules/delivery-management/delivery/request-resource-tab/create-update-resource-request/create-update-resource-request.component';
import { DeliveryResourceRequestService } from '@app/service/api/delivery-request-resource.service';
import { MatDialog } from '@angular/material/dialog';
import { UpdateUserSkillDialogComponent } from './../../../../../users/update-user-skill-dialog/update-user-skill-dialog.component';
import { ReleaseUserDialogComponent } from './../../../list-project/list-project-detail/resource-management/release-user-dialog/release-user-dialog.component';
import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { UserService } from './../../../../../service/api/user.service';
import { AppComponentBase } from '@shared/app-component-base';
import { UserDto } from './../../../../../../shared/service-proxies/service-proxies';
import { ProjectUserBillService } from './../../../../../service/api/project-user-bill.service';
import { InputFilterDto } from './../../../../../../shared/filter/filter.component';
import { ProjectResourceRequestService } from './../../../../../service/api/project-resource-request.service';
import { ProjectUserService } from './../../../../../service/api/project-user.service';
import { ActivatedRoute } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { projectUserBillDto, projectResourceRequestDto, projectUserDto } from './../../../../../service/model/project.dto';
import { Component, OnInit, Injector } from '@angular/core';
import * as moment from 'moment';
import { ConfirmPopupComponent } from '@app/modules/pm-management/list-project/list-project-detail/resource-management/confirm-popup/confirm-popup.component';

@Component({
  selector: 'app-product-resource-management',
  templateUrl: './product-resource-management.component.html',
  styleUrls: ['./product-resource-management.component.css']
})
export class ProductResourceManagementComponent extends AppComponentBase implements OnInit {
  PmManager_ProjectUser = PERMISSIONS_CONSTANT.PmManager_ProjectUser_ViewAllByProject;
  PmManager_ProjectUser_Create = PERMISSIONS_CONSTANT.PmManager_ProjectUser_Create;
  PmManager_ProjectUser_Delete = PERMISSIONS_CONSTANT.PmManager_ProjectUser_Delete;
  PmManager_ProjectUser_Update = PERMISSIONS_CONSTANT.PmManager_ProjectUser_Update;

  PmManager_ResourceRequest_Create = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_Create
  PmManager_ResourceRequest_Delete = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_Delete
  PmManager_ResourceRequest_Update = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_Update
  DeliveryManagement_ResourceRequest_Delete = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Delete;
  DeliveryManagement_ResourceRequest_Update = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Update;
  private projectId: number;
  public userBillCurrentPage = 1;
  public resourceRequestCurrentPage = 1;
  public userListCurrentPage = 1;
  public itemPerPage = 50;
  public isEditUserProject: boolean = false;
  public searchUser: string = "";
  public searchUserBill: string = "";
  public searchPlannedResource :string ='';


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

  public tomorrowDate = new Date();
  public selectStatus: any = 0;
  public listStatuses: any[] = [];
  public listSkills: any[] = []
  public listLevels: any[] = []
  public listProjectUserRoles: IDNameDto[] = []
  public isShowModal: string = 'none'
  public modal_title: string
  public strNotePM: string
  public typePM: string
  public resourceRequestId: number




  PmManager_ResourceRequest_ViewAllByProject = PERMISSIONS_CONSTANT.PmManager_ResourceRequest_ViewAllByProject
  constructor(injector: Injector, private projectUserService: ProjectUserService, private projectUserBillService: ProjectUserBillService, private userService: UserService,
    private projectRequestService: ProjectResourceRequestService, private route: ActivatedRoute, private dialog: MatDialog,
    private resourceRequestService: DeliveryResourceRequestService,
    ) {
      super(injector)
      this.tomorrowDate.setDate(this.tomorrowDate.getDate() + 1)
  }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: "Name", comparisions: [0, 6, 7, 8] },
  ];
  ngOnInit(): void {
    this.projectId = Number(this.route.snapshot.queryParamMap.get("id"));
    this.getProjectUser();
    this.getResourceRequestList();
    this.getAllUser();
    this.getPlannedtUser();
    this.getAllSkills();
    this.getStatusesResourceRequest();
    this.getLevelsResourceRequest();
    this.getProjectUserRoles()

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
      this.resourceRequestService.getAllResourceRequestByProject(this.projectId, this.selectStatus).pipe(catchError(this.projectRequestService.handleError)).subscribe(data => {
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
        this.getAllUser()
        this.projectUserProcess = false;
        this.planResourceProcess = false;
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
  console.log("userrrr", user)
    if (this.isEditUserProject) {
      this.updateProjectCurrentResource(user)
    }
    else {
      user.userId = user.userInfo.id
      user.fullName = user.userInfo.fullName
      let workingProject = [];
      this.projectUserService.GetAllWorkingProjectByUserId(user.userId).subscribe(data => {
        workingProject = data.result
        if (workingProject.length > 0) {
          user.allocatePercentage = 100
          let ref = this.dialog.open(ConfirmPopupComponent,
            {
              width: "700px",
              data: {
                workingProject : workingProject,
                user: user
              }
            }
            )
            ref.afterClosed().subscribe(rs =>{
              if(rs){
                this.AddUserToProject(user)
              }
            })
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

  public createRequest() {
    this.showDialog("create", {});
  }
  public editRequest(item: any) {
    this.showDialog("edit", item);
  }

  showDialog(command: string, request: any) {
    let resourceRequest = {
      id: request.id ? request.id : null,
      projectId: this.projectId 
    }
    const show = this.dialog.open(CreateUpdateResourceRequestComponent, {
      data: {
        command: command,
        item: resourceRequest,
        skills: this.listSkills,
        levels: this.listLevels,
        typeControl: 'requestProject'
      },
      width: "700px",
      maxHeight: '90vh',
    })
    show.afterClosed().subscribe(rs => {
      if(!rs) return
      if(command == 'create')
        this.getResourceRequestList()
      else if(command == 'edit'){
        let index = this.resourceRequestList.findIndex(x => x.id == rs.id)
        if(index >= 0){
          this.resourceRequestList[index] = rs
        }
      }
    });
  }

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

  getStatusesResourceRequest(){
    this.resourceRequestService.getStatuses().subscribe(res => {
      this.listStatuses = res.result
    })
  }
  getAllSkills(){
    this.resourceRequestService.getSkills().subscribe((data) => {
      this.listSkills = data.result;
    })
  }
  getLevelsResourceRequest(){
    this.resourceRequestService.getLevels().subscribe(res => {
      this.listLevels = res.result
    })
  }

  getProjectUserRoles(){
    this.resourceRequestService.getProjectUserRoles().subscribe((rs: any) => {
      this.listProjectUserRoles = rs.result
    })
  }

  showActionViewRecruitment(status, isRecruitment){
    if(
      isRecruitment &&
      (status == 'INPROGRESS' || status == 'CANCELLED' || status == 'DONE')
    )
    {
      return true
    }
    return false
  }

  public setDoneRequest(item){
    let data = {
      ...item.planUserInfo, 
      requestName: item.name, 
      resourceRequestId: item.id
    }
    const showModal = this.dialog.open(FormSetDoneComponent, {
      data,
      width: "700px",
      maxHeight: "90vh"
    })
    showModal.afterClosed().subscribe((rs) => {
      if(rs)
        this.getResourceRequestList()
        this.getPlannedtUser()
        this.getProjectUser()
    })
  }

  cancelRequest(id){
    console.log("hllo",id)
    abp.message.confirm(
      'Are you sure cancel request?',
      '',
      (result) => {
        if(result){
          this.resourceRequestService.cancelResourceRequest(id).subscribe(res => {
            if(res.success){
              abp.notify.success('Cancel Request Success!')
              this.getResourceRequestList()
              this.getPlannedtUser()
            }
            else{
              abp.notify.error(res.result)
            }
          })
        }
      }
    )
  }

  deleteRequest(item: any){
    abp.message.confirm(
      "Delete this request ?",
      "",
      (result: boolean) => {
        if (result) {
          this.resourceRequestService.deleteMyRequest(item.id).pipe(catchError(this.resourceRequestService.handleError)).subscribe(() => {
            abp.notify.success("Delete request successfully");
            this.getResourceRequestList();
          });

        }
      }

    );
  }

  sendRecruitment(){
    abp.message.info('Chức năng này sẽ được cập nhật trong bản release sắp tới', 'Thông báo')
  }
  async getPlanResource(item){
    let data = new ResourcePlanDto(item.id, 0);
    if(!item.planUserInfo) return data;
    let res = await this.resourceRequestService.getPlanResource(item.planUserInfo.projectUserId, item.id)
    return res.result
  }

  async showModalPlanUser(item: any){
    let data = await this.getPlanResource(item);
    const show = this.dialog.open(FormPlanUserComponent, {
      data: {...data, projectUserRoles: this.listProjectUserRoles},
      width: "700px",
      maxHeight:"90vh"
    })
    show.afterClosed().subscribe(rs => {
      if(!rs) return
      if(rs.type == 'delete'){
        this.getResourceRequestList()
        this.getPlannedtUser()
      }
      else{
        this.getPlannedtUser()
        let index = this.resourceRequestList.findIndex(x => x.id == rs.data.resourceRequestId)
        if(index >= 0)
          this.resourceRequestList[index].planUserInfo = rs.data.result
      }
    });
  }
  public openModal(name, typePM, content, id){
    this.typePM = typePM
    this.modal_title = name
    this.strNotePM = content
    this.resourceRequestId = id
    this.isShowModal = 'block'
  }
  public closeModal(){
    this.isShowModal = 'none'
  }

  public updateNote(){
    let request = {
      resourceRequestId: this.resourceRequestId,
      note: this.strNotePM,
    }
    this.resourceRequestService.updateNote(request,this.typePM).subscribe(rs => {
      if(rs.success){
        abp.notify.success('Update Note Successfully!')
        let index = this.resourceRequestList.findIndex(x => x.id == request.resourceRequestId);
        if(index >= 0){
          if(this.typePM == 'PM')
            this.resourceRequestList[index].pmNote = request.note;
          else
            this.resourceRequestList[index].dmNote = request.note;
        }
      }
      else{
        abp.notify.error(rs.result)
      }
    })
    this.closeModal()
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
    this.searchPlannedResource = ""
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

  onUserSelect(user) {
    user.userSkills = user.userInfo.userSkills
    user.userId = user.userInfo.id
  }
  onPlanUserSelect(user, u) {
    user.userSkills = u.userSkills
    user.userId = u.id
  }
}