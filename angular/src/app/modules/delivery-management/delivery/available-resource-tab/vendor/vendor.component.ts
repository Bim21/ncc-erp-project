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

@Component({
  selector: 'app-vendor',
  templateUrl: './vendor.component.html',
  styleUrls: ['./vendor.component.css']
})
export class VendorComponent extends PagedListingComponentBase<PlanResourceComponent> implements OnInit {
  public listSkills: SkillDto[] = [];
  public skill = '';
  public skillsParam = [];
  subscription: Subscription[] = [];
  DeliveryManagement_ResourceRequest_CancelResource = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_CancelResource

  // count=0
  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
    skill?
  ): void {

    // this.pageSizeType = request.maxResultCount
    // this.pageSizeType = this.pageSize
    // request.maxResultCount = this.pageSize
    this.isLoading = true;
    let check = false;
    request.filterItems.forEach((item) => {
      if (item.filterType == 4) {
        request.filterItems = this.clearFilter(request, 'skill', 0);
        check = true;
        this.skill = item.value;
      }
    });

    this.subscription.push(
      this.availableRerourceService
        .GetVendorResource(request, this.skill)
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
          if (check == true) {
            request.filterItems.push({
              propertyName: 'skill',
              comparision: 0,
              value: this.skill,
              filterType: 4,
              dropdownData: this.skillsParam,
            });
            this.skill = '';
          }
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
      propertyName: 'projectName',
      comparisions: [0, 6, 7, 8],
      displayName: 'Project Name',
    },
    {
      propertyName: 'projectUserPlans',
      comparisions: [0, 6, 7, 8],
      displayName: 'Project User Plans',
    },
    {
      propertyName: 'used',
      comparisions: [0, 1, 2, 3, 4],
      displayName: 'Used',
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
    private skillService: SkillService,
    private userInfoService: UserService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.pageSizeType = 100;
    this.changePageSize();
    this.getAllSkills();
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
        this.FILTER_CONFIG.push({
          propertyName: 'skill',
          comparisions: [0],
          displayName: 'Skill',
          filterType: 4,
          dropdownData: this.skillsParam,
        });
      })
    )

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
        userSkills: user.listSkills.map(skill => { return { skillId: skill.id } }),
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
            this.availableRerourceService.CancelResourcePlan(projectUser.projectUserId).subscribe(rs => {
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
                  <span> - </span> <span>${moment(project.startTime).format("YYYY/MM/DD")}</span></p>
              </div>
              <div class="col-1">
                  <span class="badge ${project.allowcatePercentage > 0 ? 'bg-success' : 'bg-secondary'}">${project.allowcatePercentage > 0 ? 'Join' : 'Out'} </span>
              </div>
          </div>
         `

          }
        });
        if (count > 10) {
          user.conditionHistory = true
        } else {
          user.conditionHistory = false;
        }
        user.userProjectHistory = userHisTory
      })
    )
  }
  showMoreHistory(user) {
    user.showAllHistory = !user.showAllHistory;
  }
  ngOnDestroy(): void {
    this.subscription.forEach(sub => {
      sub.unsubscribe()
    })
  }
}
