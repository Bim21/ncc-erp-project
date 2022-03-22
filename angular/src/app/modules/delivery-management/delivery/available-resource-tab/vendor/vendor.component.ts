import { PagedRequestDto } from './../../../../../../shared/paged-listing-component-base';
import { SkillDto } from './../../../../../service/model/list-project.dto';
import { PlanResourceComponent } from './../plan-resource/plan-resource.component';
import { InputFilterDto } from './../../../../../../shared/filter/filter.component';
import { ProjectHistoryByUserComponent } from './../plan-resource/plan-user/project-history-by-user/project-history-by-user.component';
import { availableResourceDto } from './../../../../../service/model/delivery-management.dto';
import { ProjectDetailComponent } from './../plan-resource/plan-user/project-detail/project-detail.component';
import { UpdateUserSkillDialogComponent } from './../../../../../users/update-user-skill-dialog/update-user-skill-dialog.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Component, Injector, OnInit } from '@angular/core';
import { PlanUserComponent } from '../plan-resource/plan-user/plan-user.component';
import { DeliveryResourceRequestService } from '../../../../../service/api/delivery-request-resource.service';
import { MatDialog } from '@angular/material/dialog';
import { catchError, finalize } from 'rxjs/operators';
import { PagedListingComponentBase } from '../../../../../../shared/paged-listing-component-base';
import { AddNoteDialogComponent } from '../plan-resource/add-note-dialog/add-note-dialog.component';
import { SkillService } from '../../../../../service/api/skill.service';
import * as moment from 'moment';
import { UserService } from '@app/service/api/user.service';
import { Subscription } from 'rxjs';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { ConfirmPlanDialogComponent } from '../plan-resource/plan-user/confirm-plan-dialog/confirm-plan-dialog.component';
import { ProjectUserService } from '@app/service/api/project-user.service';

@Component({
  selector: 'app-vendor',
  templateUrl: './vendor.component.html',
  styleUrls: ['./vendor.component.css']
})
export class VendorComponent extends PagedListingComponentBase<PlanResourceComponent> implements OnInit {
 
  subscription: Subscription[] = [];
  public listSkills: SkillDto[] = [];
  public skill = '';
  public skillsParam = [];
  public selectedSkillId:number[]
  public isAndCondition:boolean =false;
  DeliveryManagement_ResourceRequest_CancelAnyPlanResource = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_CancelAnyPlanResource
  DeliveryManagement_ResourceRequest_CancelMyPlanOnly = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_CancelMyPlanOnly

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function, skill?): void {
    this.isLoading = true;
    let requestBody:any = request
    requestBody.skillIds = this.selectedSkillId
    requestBody.isAndCondition = this.isAndCondition
    this.subscription.push(
      this.availableRerourceService.GetVendorResource(requestBody).pipe(finalize(() => {
        finishedCallback();
      }), catchError(this.availableRerourceService.handleError)).subscribe(data => {
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
  protected delete(entity: PlanResourceComponent): void {

  }
  userTypeParam = Object.entries(this.APP_ENUM.UserType).map((item) => {
    return {
      displayName: item[0],
      value: item[1]
    }

  })
  branchParam = Object.entries(this.APP_ENUM.UserBranch).map((item) => {
    return {
      displayName: item[0],
      value: item[1]
    }
  })

  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'fullName', comparisions: [0, 6, 7, 8], displayName: "User Name" },
    { propertyName: 'used', comparisions: [0, 1, 2, 3, 4], displayName: "Used" },
    { propertyName: 'branch', comparisions: [0], displayName: "Branch", filterType: 3, dropdownData: this.branchParam },
    { propertyName: 'userType', comparisions: [0], displayName: "User Type", filterType: 3, dropdownData: this.userTypeParam },


  ];

  public availableResourceList: availableResourceDto[] = [];

  constructor(public injector: Injector,
    private availableRerourceService: DeliveryResourceRequestService,
    private dialog: MatDialog,
    private skillService: SkillService,
    private _modalService: BsModalService,
    private userInfoService: UserService,
    private projectUserService: ProjectUserService


  ) { super(injector) }

  ngOnInit(): void {
    this.pageSizeType = 100
    this.changePageSize();
    this.getAllSkills();
    console.log("project status", this.APP_ENUM.ProjectStatus)
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
      if(this.permission.isGranted(this.DeliveryManagement_ResourceRequest_CancelAnyPlanResource)){
        return true
      }
      else if (creatorUserId === this.appSession.userId) {
        return true
      }
      else{
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
            this.availableRerourceService.CancelResourcePlan(projectUser.id).subscribe(rs => {
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
  editUserPlan(user: any, projectUser:any) {
    user.userId = projectUser.userId
    user.projectUserId = user.id 
    user.fullName = projectUser.fullName
    this.showDialogPlanUser('edit', user);
  }
}
