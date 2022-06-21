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
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EditNoteDialogComponent } from './add-note-dialog/edit-note-dialog.component';


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
  public lastInvoiceNumber;
  public discount;
  public chargeType = ['d', 'm', 'h'];
  public isEditLastInvoiceNumber: boolean = false;
  public isEditDiscount: boolean = false;
  
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_View = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_View;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Create = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Create;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Edit = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Edit;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_LastInvoiceNumber_View = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_LastInvoiceNumber_View;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_LastInvoiceNumber_Edit = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_LastInvoiceNumber_Edit;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Discount_View = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Discount_View;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Discount_Edit = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Discount_Edit;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Rate_View = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Rate_View;
  Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Note_Edit = PERMISSIONS_CONSTANT.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Note_Edit;
  constructor(
    private projectUserBillService: ProjectUserBillService,
    private route: ActivatedRoute,
    injector: Injector, 
    private userService: UserService,
    private _modalService: BsModalService) {
    super(injector)
    this.projectId = Number(this.route.snapshot.queryParamMap.get("id"));

  }

  ngOnInit(): void {
    this.getUserBill();
    this.getAllFakeUser();
    this.getRate();
    this.getLastInvoiceNumber();
    this.getDiscount();
  }
  getRate() {
    this.projectUserBillService.getRate(this.projectId).subscribe(data => {
      this.rateInfo = data.result;
      console.log('rate', this.rateInfo)
    })
  }
  getLastInvoiceNumber() {
    this.projectUserBillService.getLastInvoiceNumber(this.projectId).subscribe(data => {
      this.lastInvoiceNumber = data.result;
    })
  }
  updateLastInvoiceNumber(){
    let data = {
      projectId : this.projectId,
      lastInvoiceNumber : this.lastInvoiceNumber,
    }
    if(+this.lastInvoiceNumber <= 0) {
      abp.message.error(this.l("Last Invoice Number must be bigger than 0!"));
      this.getLastInvoiceNumber();
      return;
    }
    this.projectUserBillService.updateLastInvoiceNumber(data).subscribe(data => {
      this.lastInvoiceNumber = data.result;
      abp.notify.success(`Updated Last Invoice Number`);
      this.isEditLastInvoiceNumber = false;
    })
  }

  getDiscount() {
    this.projectUserBillService.getDiscount(this.projectId).subscribe(data => {
      this.discount = data.result;
    })
  }
  updateDiscount(){
    let data = {
      projectId : this.projectId,
      discount : this.discount,
    }
    if(+this.discount < 0) {
      abp.message.error(this.l("Discount must be bigger than or equal to 0!"));
      this.getLastInvoiceNumber();
      return;
    }
    this.projectUserBillService.updateDiscount(data).subscribe(data => {
      this.discount = data.result;
      this.isEditDiscount = false;
      abp.notify.success(`Updated Discount`)
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
    newUserBill.isActive = true;
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
    console.log(userBill)
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

  cancelLastInvoiceNumber() {
    this.isEditLastInvoiceNumber = false;
  }
  cancelDiscount() {
    this.isEditDiscount = false;
  }
  
  updateNote(id, fullName,projectName,note) {
    let editNoteDialog: BsModalRef;
    editNoteDialog = this._modalService.show(EditNoteDialogComponent, {
      class: 'modal',
      initialState: {
        id: id,
        fullName: fullName,
        projectName: projectName,
        note: note,
      },
    });

    editNoteDialog.content.onSave.subscribe(() => {
      this.getUserBill();
    });
  }


}
