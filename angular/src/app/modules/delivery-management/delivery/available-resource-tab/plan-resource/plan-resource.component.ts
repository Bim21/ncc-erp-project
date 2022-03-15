import { AddNoteDialogComponent } from './add-note-dialog/add-note-dialog.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ProjectDetailComponent } from './plan-user/project-detail/project-detail.component';
import { SkillService } from './../../../../../service/api/skill.service';
import { InputFilterDto } from './../../../../../../shared/filter/filter.component';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { availableResourceDto } from './../../../../../service/model/delivery-management.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { PlanUserComponent } from './plan-user/plan-user.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { result } from 'lodash-es';
import { catchError, finalize, filter } from 'rxjs/operators';
// import { DeliveryResourceRequestService } from './../../../../service/api/delivery-request-resource.service';
// import { availableResourceDto } from './../../../../service/model/delivery-management.dto';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';
import { SkillDto } from '@app/service/model/list-project.dto';
import { ProjectResourceRequestService } from '@app/service/api/project-resource-request.service';
import { ProjectHistoryByUserComponent } from './plan-user/project-history-by-user/project-history-by-user.component';
import * as moment from 'moment';
import { UpdateUserSkillDialogComponent } from '@app/users/update-user-skill-dialog/update-user-skill-dialog.component';
import { Subscription } from 'rxjs';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';

@Component({
  selector: 'app-plan-resource',
  templateUrl: './plan-resource.component.html',
  styleUrls: ['./plan-resource.component.css'],
})
export class PlanResourceComponent
  extends PagedListingComponentBase<PlanResourceComponent>
  implements OnInit {
  public listSkills: SkillDto[] = [];
  public skill = '';
  public skillsParam = [];
  private subscription: Subscription[] = [];
  public selectedSkillId:number[]
  public isAndCondition:boolean = false
  DeliveryManagement_ResourceRequest_CancelAnyPlanResource = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_CancelAnyPlanResource
  DeliveryManagement_ResourceRequest_CancelMyPlanOnly = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_CancelMyPlanOnly
  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function, skill?

  ): void {
    this.isLoading = true;
    let requestBody:any = request 
    requestBody.skillIds = this.selectedSkillId
    requestBody.isAndCondition = this.isAndCondition
    this.subscription.push(
      this.availableRerourceService
        .GetAllPoolResource(requestBody, this.skill)
        .pipe(
          finalize(() => {
            finishedCallback();
          }),
          catchError(this.availableRerourceService.handleError)
        )
        .subscribe((data) => {
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
  protected delete(entity: PlanResourceComponent): void { }
  userTypeParam = Object.entries(this.APP_ENUM.UserType).map((item) => {
    return {
      displayName: item[0],
      value: item[1],
    };
  });
  branchParam = Object.entries(this.APP_ENUM.UserBranch).map((item) => {
    return {
      displayName: item[0],
      value: item[1],
    };
  });

  public readonly FILTER_CONFIG: InputFilterDto[] = [
    {
      propertyName: 'fullName',
      comparisions: [0, 6, 7, 8],
      displayName: 'User Name',
    },
    {
      propertyName: 'branch',
      comparisions: [0],
      displayName: 'Branch',
      filterType: 3,
      dropdownData: this.branchParam,
    },
    {
      propertyName: 'userType',
      comparisions: [0],
      displayName: 'User Type',
      filterType: 3,
      dropdownData: this.userTypeParam,
    },
  ];

  public availableResourceList: availableResourceDto[] = [];

  constructor(
    public injector: Injector,
    private _modalService: BsModalService,
    private availableRerourceService: DeliveryResourceRequestService,
    private dialog: MatDialog,
    private skillService: SkillService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.pageSizeType = 100;
    this.changePageSize();
    this.getAllSkills();


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
  showDialogPlanUser(command: string, user: any) {
    let item = {
      userId: user.userId,
      fullName: user.fullName,
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
  showDialogProjectHistoryUser(user: availableResourceDto) {
    let userInfo = {
      userId: user.userId,
      emailAddress: user.emailAddress,
    };

    const show = this.dialog.open(ProjectHistoryByUserComponent, {
      width: '700px',
      disableClose: true,
      data: {
        item: userInfo,
      },
    });
    show.afterClosed().subscribe((result) => {
      if (result) {
        this.refresh();
      }
    });
  }

  projectHistorUser(user: availableResourceDto) {
    this.showDialogProjectHistoryUser(user);
  }
  planUser(user: any) {
    this.showDialogPlanUser('plan', user);
  }
  showUserDetail(userId: any) { }

  getAllSkills() {
    this.subscription.push(
      this.skillService.getAll().subscribe((data) => {
        this.listSkills = data.result;
        this.skillsParam = data.result.map((item) => {
          return {
            displayName: item.name,
            value: item.id,
          };
        });
      }))
  }

  skillsCommas(arr) {
    arr = arr.map((item) => {
      return item.name;
    });
    return arr.join(', ');
  }

  projectsCommas(arr) {
    arr = arr.map((item) => {
      return item.projectName;
    });
    return arr.join(', ');
  }

  showProjectDetail(projectId, projectName) {
    const show = this.dialog.open(ProjectDetailComponent, {
      data: {
        projectId: projectId,
        projectName: projectName,
      },
      width: '95vw',
      height: '90vh',
    });
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

  formatDateStartPool(date: string) {
    return moment(date).format('DD/MM/YYYY');
  }

  updateNote(id, fullName) {
    let addOrEditNoteDialog: BsModalRef;
    addOrEditNoteDialog = this._modalService.show(AddNoteDialogComponent, {
      class: 'modal',
      initialState: {
        id: id,
        fullName: fullName,
      },
    });

    addOrEditNoteDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
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
  ngOnDestroy(): void {
    this.subscription.forEach(sub => {
      sub.unsubscribe()
    })
  }
}
