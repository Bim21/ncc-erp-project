import { ActivatedRoute } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { UserService } from '@app/service/api/user.service';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { AppComponentBase } from '@shared/app-component-base';
import { UserDto } from '@shared/service-proxies/service-proxies';
import { projectUserBillDto, ProjectRateDto } from './../../../../../service/model/project.dto';
import { ProjectUserBillService } from './../../../../../service/api/project-user-bill.service';
import { Component, OnInit, Injector } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-project-bill',
  templateUrl: './project-bill.component.html',
  styleUrls: ['./project-bill.component.css']
})
export class ProjectBillComponent extends AppComponentBase implements OnInit {
  public userBillList: projectUserBillDto[] = [];
  public userForUserBill: UserDto[] = [];
  public isEditUserBill: boolean = false;
  public userBillProcess: boolean = false;
  public panelOpenState: boolean = false;
  public isShowUserBill: boolean = false;
  public searchUserBill: string = ""
  private projectId: number
  public userBillCurrentPage: number = 1
  public rateInfo = {} as ProjectRateDto;
  public chargeType = ['d', 'm', 'h']

  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_View = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_View;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Create = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Create;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Edit = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Edit;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete;
  constructor(private projectUserBillService: ProjectUserBillService, private route: ActivatedRoute,
    injector: Injector, private userService: UserService) {
    super(injector)
    this.projectId = Number(this.route.snapshot.queryParamMap.get("id"));

  }

  ngOnInit(): void {
    this.getUserBill();
    this.getAllFakeUser();
    this.getRate();

  }
  getRate() {
    this.projectUserBillService.getRate(this.projectId).subscribe(data => {
      this.rateInfo = data.result;
      console.log('rate', this.rateInfo)
    })
  }
  public getTitleRate() {
    return '(' + this.rateInfo.currencyName + '/' + this.chargeType[this.rateInfo.chargeType] + ')'
  }

  private getAllFakeUser() {
    this.userService.GetAllUserActive(false, true).pipe(catchError(this.userService.handleError)).subscribe(data => {
      // this.userForProjectUser = data.result;
      this.userForUserBill = data.result;
    })
  }
  public addUserBill(): void {
    let newUserBill = {} as projectUserBillDto
    newUserBill.createMode = true;
    this.userBillProcess = true;
    this.userBillList.unshift(newUserBill)
  }
  public saveUserBill(userBill: projectUserBillDto): void {
    delete userBill["createMode"]
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
        this.searchUserBill = ""
      },
        () => {
          userBill.createMode = true;
        })
    }


  }
  // private filterProjectUserDropDown() {

  //   let userProjectList = this.projectUserList.map(item => item.userId)
  //   this.userForProjectUser = this.userForUserBill.filter(user => userProjectList.indexOf(user.id) == -1)
  // }
  public cancelUserBill(): void {
    this.getUserBill();
    this.userBillProcess = false;
    this.isEditUserBill = false;
    this.searchUserBill = ""
  }
  public editUserBill(userBill: projectUserBillDto): void {
    userBill.createMode = true;
    this.userBillProcess = true;
    this.isEditUserBill = true;
    // userBill.billRole = this.APP_ENUM.ProjectUserRole[userBill.billRole];
  }
  private getUserBill(): void {
    this.projectUserBillService.getAllUserBill(this.projectId).pipe(catchError(this.projectUserBillService.handleError)).subscribe(data => {
      this.userBillList = data.result
    })
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
  public focusOut() {
    this.searchUserBill = '';
  }


}
