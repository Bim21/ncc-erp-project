import { catchError } from 'rxjs/operators';
import { result } from 'lodash-es';
import { UserDto } from './../../../../../../shared/service-proxies/service-proxies';
import { projectUserDto, projectResourceRequestDto, projectUserBillDto } from './../../../../../service/model/project.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { UserService } from './../../../../../service/api/user.service';
import { ProjectUserBillService } from './../../../../../service/api/project-user-bill.service';
import { InputFilterDto } from './../../../../../../shared/filter/filter.component';
import { ProjectResourceRequestService } from './../../../../../service/api/project-resource-request.service';
import { ActivatedRoute } from '@angular/router';
import { ProjectUserService } from './../../../../../service/api/project-user.service';
import { Component, OnInit, Injector } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-training-resource-management',
  templateUrl: './training-resource-management.component.html',
  styleUrls: ['./training-resource-management.component.css']
})
export class TrainingResourceManagementComponent extends AppComponentBase implements OnInit {

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
  // project user bill
  public userBillList: projectUserBillDto[] = [];
  public userForUserBill: UserDto[] = [];
  public isEditUserBill: boolean = false;
  public userBillProcess: boolean = false;
  public panelOpenState: boolean = false;
  public isShowUserBill: boolean = false;
  
  constructor(injector: Injector, private projectUserService: ProjectUserService, private projectUserBillService: ProjectUserBillService, private userService: UserService,
    private projectRequestService: ProjectResourceRequestService, private route: ActivatedRoute) { super(injector) }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: "Name", comparisions: [0, 6, 7, 8] },
  ];
  ngOnInit(): void {
    this.projectId = Number(this.route.snapshot.queryParamMap.get("id"));
    this.getProjectUser();
    this.getResourceRequestList();
    this.getUserBill();
    this.getAllUser();
    this.getAllFakeUser();
    
  }
  // get data
  private getProjectUser() {
      this.projectUserService.getAllProjectUser(this.projectId, this.viewHistory).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        this.projectUserList = data.result;
      })
  }
  private getResourceRequestList(): void {
      this.projectRequestService.getAllResourceRequest(this.projectId).pipe(catchError(this.projectRequestService.handleError)).subscribe(data => {
        this.resourceRequestList = data.result
      })
  }
  private getUserBill(): void {
      this.projectUserBillService.getAllUserBill(this.projectId).pipe(catchError(this.projectUserBillService.handleError)).subscribe(data => {
        this.userBillList = data.result
      })
  }
  private getAllUser() {
    this.userService.GetAllUserActive(false,false).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.userForProjectUser = data.result;
      // this.userForUserBill = data.result;
    })
  }
  private getAllFakeUser() {
    this.userService.GetAllUserActive(false,true).pipe(catchError(this.userService.handleError)).subscribe(data => {
      // this.userForProjectUser = data.result;
      this.userForUserBill = data.result;
    })
  }

  //  project user

  public addProjectUser() {
    let newUser = {} as projectUserDto
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
 
  saveProjectUser(user: projectUserDto) {
    if (!this.isEditUserProject) {
      let newUser: projectUserDto = this.projectUserList[0]
      // newUser.isFutureActive = false
      newUser.projectId = this.projectId
      newUser.isExpense = true;
      // newUser.status = "0";
      newUser.startTime = moment(newUser.startTime).format("YYYY-MM-DD");
      delete newUser["createMode"]
      this.projectUserService.create(newUser).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        this.getProjectUser();
        abp.notify.success(`Added user to project`);
        this.projectUserProcess = false
        this.searchUser = "";

      },
        () => {
          newUser.createMode = true
        })
    }
    else {
      user.startTime = moment(user.startTime).format("YYYY-MM-DD")
      this.projectUserService.update(user).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
        abp.notify.success(`updated user: ${user.userName}`);
        this.getProjectUser();
        this.isEditUserProject = false;
        this.projectUserProcess = false
        this.searchUser = "";

      })
    }
  }
  editProjectUser(user: projectUserDto) {
    this.isEditUserProject = true;
    user.createMode = true
    user.status = this.APP_ENUM.ProjectUserStatus[user.status]
    user.projectRole = this.APP_ENUM.ProjectUserRole[user.projectRole]
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
  cancelProjectUser() {
    this.getProjectUser();
    this.isEditUserProject = false;
    this.projectUserProcess = false
    this.searchUser = ""
  }
  private filterProjectUserDropDown() {

    let userProjectList = this.projectUserList.map(item => item.userId)
    this.userForProjectUser = this.userForUserBill.filter(user => userProjectList.indexOf(user.id) == -1)
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

  // user bill
  public addUserBill(): void {
    let newUserBill = {} as projectUserBillDto
    newUserBill.createMode = true;
    this.userBillProcess = true;
    this.userBillList.unshift(newUserBill)
  }
  public saveUserBill(userBill: projectUserBillDto): void {
    delete userBill["createMode"]
    // userBill.isActive = true;
    userBill.startTime = moment(userBill.startTime).format("YYYY-MM-DD");
    if (userBill.endTime) {
      userBill.endTime = moment(userBill.endTime).format("YYYY-MM-DD");

    }

    if (!this.isEditUserBill) {
      userBill.projectId = this.projectId
      this.projectUserBillService.create(userBill).pipe(catchError(this.projectUserBillService.handleError)).subscribe(res => {
        abp.notify.success(`Created new user bill`)
        this.getUserBill();
        this.userBillProcess = false;
        this.searchUser = ""
        this.searchUserBill = ""
      }, () => {
        userBill.createMode = true
      })
    }
    else {
      this.projectUserBillService.update(userBill).pipe(catchError(this.projectUserBillService.handleError)).subscribe(res => {
        abp.notify.success(`Updated request user bill`)
        this.getUserBill();
        this.userBillProcess = false;
        this.isEditUserBill = false;
        this.searchUser = ""
        this.searchUserBill = ""
      },
        () => {
          userBill.createMode = true;
        })
    }


  }
  public cancelUserBill(): void {
    this.getUserBill();
    this.userBillProcess = false;
    this.isEditUserBill = false;
    this.searchUser = ""
    this.searchUserBill = ""
  }
  public editUserBill(userBill: projectUserBillDto): void {
    userBill.createMode = true;
    this.userBillProcess = true;
    this.isEditUserBill = true;
    // userBill.billRole = this.APP_ENUM.ProjectUserRole[userBill.billRole];
  }
  public removeUserBill(userBill: projectUserBillDto): void {
    abp.message.confirm(
      "Delete user bill?",
      "",
      (result: boolean) => {
        if (result) {
          this.projectUserBillService.deleteUserBill(userBill.id).pipe(catchError(this.projectUserBillService.handleError)).subscribe(() => {
            abp.notify.success("Deleted user bill");
            this.getUserBill();
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



}

