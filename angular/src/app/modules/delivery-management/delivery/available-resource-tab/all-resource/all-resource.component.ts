import { ResourceManagerService } from './../../../../../service/api/resource-manager.service';
import { UserService } from './../../../../../service/api/user.service';
import { MatDialog } from '@angular/material/dialog';
import { SkillService } from './../../../../../service/api/skill.service';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { availableResourceDto } from './../../../../../service/model/delivery-management.dto';
import { InputFilterDto } from './../../../../../../shared/filter/filter.component';
import { PlanResourceComponent } from './../plan-resource/plan-resource.component';
import { catchError, finalize } from 'rxjs/operators';
import { PagedRequestDto } from './../../../../../../shared/paged-listing-component-base';
import { BranchDto, SkillDto } from './../../../../../service/model/list-project.dto';
import { PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { PlanUserComponent } from './../plan-resource/plan-user/plan-user.component';
import { ProjectDetailComponent } from './../plan-resource/plan-user/project-detail/project-detail.component';
import { Component, OnInit, Injector, inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { UpdateUserSkillDialogComponent } from '@app/users/update-user-skill-dialog/update-user-skill-dialog.component';
import * as moment from 'moment';
import { Subscription } from 'rxjs';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { ProjectUserService } from '@app/service/api/project-user.service';
import { ConfirmPlanDialogComponent } from '../plan-resource/plan-user/confirm-plan-dialog/confirm-plan-dialog.component';
import { ConfirmFromPage } from '@app/modules/pm-management/list-project/list-project-detail/resource-management/confirm-popup/confirm-popup.component';
import { BranchService } from '@app/service/api/branch.service';

@Component({
  selector: 'app-all-resource',
  templateUrl: './all-resource.component.html',
  styleUrls: ['./all-resource.component.css']
})
export class AllResourceComponent extends PagedListingComponentBase<any> implements OnInit {

  subscription: Subscription[] = [];
  public listSkills: SkillDto[] = [];
  public listBranch: BranchDto[] = [];
  public skill = '';
  public skillsParam = [];
  public selectedSkillId: number[];
  public listBranchSelected: number[] = [];
  public listUserTypeSelected: number[] = [];
  public listPositionSelected: number[] = [];
  public isAndCondition: boolean = false;
  public requestBody: any;

  Resource_TabAllResource_View = PERMISSIONS_CONSTANT.Resource_TabAllResource_View
  Resource_TabAllResource_ViewHistory = PERMISSIONS_CONSTANT.Resource_TabAllResource_ViewHistory
  Resource_TabAllResource_CreatePlan = PERMISSIONS_CONSTANT.Resource_TabAllResource_CreatePlan
  Resource_TabAllResource_EditPlan = PERMISSIONS_CONSTANT.Resource_TabAllResource_EditPlan
  Resource_TabAllResource_ConfirmPickEmployeeFromPoolToProject = PERMISSIONS_CONSTANT.Resource_TabAllResource_ConfirmPickEmployeeFromPoolToProject
  Resource_TabAllResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = PERMISSIONS_CONSTANT.Resource_TabAllResource_ConfirmMoveEmployeeWorkingOnAProjectToOther
  Resource_TabAllResource_ConfirmOut = PERMISSIONS_CONSTANT.Resource_TabAllResource_ConfirmOut
  Resource_TabAllResource_CancelMyPlan = PERMISSIONS_CONSTANT.Resource_TabAllResource_CancelMyPlan
  Resource_TabAllResource_CancelAnyPlan = PERMISSIONS_CONSTANT.Resource_TabAllResource_CancelAnyPlan
  Resource_TabAllResource_UpdateSkill = PERMISSIONS_CONSTANT.Resource_TabAllResource_UpdateSkill

  protected delete(entity: PlanResourceComponent): void {
  }

  branchParam = Object.entries(this.APP_ENUM.UserBranch).map((item) => {
    return {
      displayName: item[0],
      value: item[1],
    };
  });

  userTypeParam = Object.entries(this.APP_ENUM.UserType).map((item) => {
    return {
      displayName: item[0],
      value: item[1],
    };
  });


  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'fullName', comparisions: [0, 6, 7, 8], displayName: "User Name" },
    { propertyName: 'used', comparisions: [0, 1, 2, 3, 4], displayName: "Used" },
    { propertyName: 'userType', comparisions: [0], displayName: "User Type", filterType: 3, dropdownData: this.userTypeParam },
  ];

  public availableResourceList: availableResourceDto[] = [];

  constructor(public injector: Injector,
    private availableRerourceService: ResourceManagerService,
    private dialog: MatDialog,
    private skillService: SkillService,
    private _modalService: BsModalService,
    private userInfoService: UserService,
    private projectUserService: ProjectUserService,
    private branchService: BranchService
  ) { super(injector) }

  ngOnInit(): void {
    this.pageSizeType = 100
    this.changePageSize();
    this.getAllSkills();
    this.getAllBranchs();
    this.userTypeParam.forEach(item => {
        this.listUserTypeSelected.push(item.value);
    })
  }
  showDialogPlanUser(command: string, user: any) {
    let item = {
      userId: user.userId,
      fullName: user.fullName,
      projectId: user.projectId,
      projectRole: user.projectRole,
      startTime: user.startTime,
      allocatePercentage: user.allocatePercentage,
      isPool: user.isPool,
      projectUserId: user.projectUserId
    };

    const show = this.dialog.open(PlanUserComponent, {
      width: '700px',
      disableClose: true,
      data: {
        item: item,
        command: command,
      },
    });
    show.afterClosed().subscribe((result) => {
      if (result) {
        this.refresh();
      }
    });
  }

  public isAllowCancelPlan(creatorUserId: number) {
    if (this.permission.isGranted(this.DeliveryManagement_ResourceRequest_CancelMyPlanOnly)) {
      if (this.permission.isGranted(this.DeliveryManagement_ResourceRequest_CancelAnyPlanResource)) {
        return true
      }
      else if (creatorUserId === this.appSession.userId) {
        return true
      }
      else {
        return false
      }
    }
  }
  planUser(user: any) {
    this.showDialogPlanUser("plan", user);
  }
  showUserDetail(userId: any) {

  }

  getAllSkills() {
    this.subscription.push(
      this.skillService.getAll().subscribe((data) => {
        this.listSkills = data.result;
        this.skillsParam = data.result.map(item => {
          return {
            displayName: item.name,
            value: item.id
          }
        })
      })
    )
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function, skill?): void {
    this.isLoading = true;
    this.requestBody = request
    this.requestBody.skillIds = this.selectedSkillId
    this.requestBody.branchIds = this.listBranchSelected
    this.requestBody.userTypes = this.listUserTypeSelected
    this.requestBody.UserLevels = this.listPositionSelected
    this.requestBody.isAndCondition = this.isAndCondition
    this.subscription.push(
      this.availableRerourceService.GetAllResource(this.requestBody).pipe(catchError(this.availableRerourceService.handleError)).subscribe(data => {
        this.availableResourceList = data.result.items.filter((item) => {
          if (item.userType !== 4) {
            return item;
          }
        });
        this.showPaging(data.result, pageNumber);
        this.isLoading = false;
      })
    )
  }

  onChangeBranchEvent(event?): void {
    this.listBranchSelected = event.value;
    this.subscription.push(
      this.availableRerourceService.GetAllResource(this.requestBody).pipe(catchError(this.availableRerourceService.handleError)).subscribe(data => {
        this.availableResourceList = data.result.items.filter((item) => {
          if (this.listBranchSelected.includes(item.branchId)) {
            return item;
          }
        });
        this.showPaging(data.result, this.pageNumber);
        this.isLoading = false;
      })
    )
  }

  onChangeUserTypeEvent(event?): void {
    this.listUserTypeSelected = event.value;
    this.requestBody.userTypes = this.listUserTypeSelected
    this.availableRerourceService.GetAllResource(this.requestBody).subscribe(data => {
      this.availableResourceList = data.result.items.filter((item) => {
        if (this.listUserTypeSelected.includes(item.userType)) {
          return item;
        }
      });
      this.showPaging(data.result, this.pageNumber);
      this.isLoading = false;
    })
  }

  getAllBranchs() {
    this.branchService.getAllNotPagging().subscribe((data) => {
      this.branchParam = data.result
      this.listBranchSelected = data.result.map(item => item.id)
      this.refresh();
    })
  }

  skillsCommas(arr) {
    arr = arr.map((item) => {
      return item.name;
    })
    return arr.join(', ')
  }
  projectsCommas(arr) {
    arr = arr.map((item) => {
      return item.projectName;
    })
    return arr.join(', ')
  }

  showProjectDetail(projectId, projectName) {
    const show = this.dialog.open(ProjectDetailComponent, {
      data: {
        projectId: projectId,
        projectName: projectName,
      },
      width: '95vw',
      height: '90vh',
    })
  }
  updateUserSkill(user) {
    let ref = this.dialog.open(UpdateUserSkillDialogComponent, {
      width: "700px",
      data: {
        userSkills: user.userSkills,
        id: user.userId,
        fullName: user.fullName
      }

    });
    ref.afterClosed().subscribe(rs => {
      if (rs) {
        this.refresh()
      }
    })
  }


  CancelResourcePlan(projectUser, userName: string) {
    abp.message.confirm(
      `Cancel plan to project [${projectUser.projectName}] for user [${userName}]?`,
      "",
      (result: boolean) => {
        if (result) {
          this.subscription.push(
            this.availableRerourceService.CancelAllResourcePlan(projectUser.id).pipe(catchError(this.availableRerourceService.handleError)).subscribe(rs => {
              this.refresh()
              abp.notify.success("Cancel plan for user")
            })
          )
        }
      }
    )
  }




  getHistoryProjectsByUserId(user) {
    this.subscription.push(
      this.userInfoService.getHistoryProjectsByUserId(user.userId).pipe(catchError(this.userInfoService.handleError)).subscribe(data => {
        user.isshowProjectHistory = true
        let userHisTory = '';
        let count = 0;
        let listHistory = data.result;
        listHistory.forEach(project => {
          count++;
          if (count <= 6 || user.showAllHistory) {
            userHisTory +=
              `<div class="mb-1 d-flex pointer ${project.allowcatePercentage > 0 ? 'join-project' : 'out-project'}">
                <div class="col-11 p-0">
                    <p class="mb-0" >
                    <strong>${project.projectName}</strong> 
                    <span class="badge ${this.APP_CONST.projectUserRole[project.projectRole]}">
                    ${this.getByEnum(project.projectRole, this.APP_ENUM.ProjectUserRole)}</span>
                    -  <span>${moment(project.startTime).format("DD/MM/YYYY")}</span></p>
                </div>
                <div class="col-1">
                    <span class="badge ${project.allowcatePercentage > 0 ? 'bg-success' : 'bg-secondary'}">${project.allowcatePercentage > 0 ? 'Join' : 'Out'} </span>
                </div>
            </div>
           `

          }
        });
        if (count > 6) {
          user.showMoreHistory = true
        } else {
          user.showMoreHistory = false;
        }
        user.userProjectHistory = userHisTory
      })
    )
  }
  showMoreHistory(user) {
    user.showAllHistory = !user.showAllHistory;
  }
  ngOnDestroy(): void {
    this.subscription.forEach(sub => sub.unsubscribe())
  }

  confirm(plan, userId, userName) {
    // if (user.allocatePercentage <= 0) {
    //   let ref = this.dialog.open(ReleaseUserDialogComponent, {
    //     width: "700px",
    //     data: {
    //       user: user,
    //       type: "confirmOut",
    //     },
    //   })
    //   ref.afterClosed().subscribe(rs => {
    //     if (rs) {
    //       this.refresh()
    //     }
    //   })
    // }
    // else if (user.allocatePercentage > 0) {

    plan.userId = userId
    plan.fullName = userName
    this.projectUserService.GetAllWorkingProjectByUserId(userId).subscribe(data => {
      let ref = this.dialog.open(ConfirmPlanDialogComponent, {
        width: '580px',
        data: {
          workingProject: data.result,
          user: plan,
          fromPage: ConfirmFromPage.allResource
        }
      })

      ref.afterClosed().subscribe(rs => {
        if (rs) {
          this.refresh()
        }
      })
    })
    // }
  }
  editUserPlan(user: any, projectUser: any) {
    user.userId = projectUser.userId
    user.projectUserId = user.id
    user.fullName = projectUser.fullName
    this.showDialogPlanUser('edit', user);
  }
}
