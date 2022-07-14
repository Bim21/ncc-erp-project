import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';
import { UserDto } from './../../../../../shared/service-proxies/service-proxies';
import { UserService } from './../../../../service/api/user.service';
import { catchError } from 'rxjs/operators';
import { projectUserBillDto } from './../../../../service/model/project.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { TimesheetProjectBill } from './../../../../service/model/timesheet.dto';
import { TimeSheetProjectBillService } from './../../../../service/api/time-sheet-project-bill.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, OnInit, Inject, Injector } from '@angular/core';
import * as moment from 'moment';
import { UpdateAction } from '../timesheet-detail.component';

@Component({
  selector: 'app-view-bill',
  templateUrl: './view-bill.component.html',
  styleUrls: ['./view-bill.component.css']
})
export class ViewBillComponent extends AppComponentBase implements OnInit {
  billDetail: TimesheetProjectBill[] = []
  userForUserBill: UserDto[] = []
  searchUserBill: string = "";
  public isCreate: boolean = false;
  public isEdit: boolean = false;
  public isEdittingRows: boolean = false;
  tempUserList = []
  public chargeType = ['d','m','h']
  public updateAction = UpdateAction
  Timesheets_TimesheetDetail_UpdateBill_Edit = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_UpdateBill_Edit
  Timesheets_TimesheetDetail_UpdateBill_SetDone = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_UpdateBill_SetDone
  Timesheets_TimesheetDetail_ViewBillRate = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_ViewBillRate
  Timesheets_TimesheetDetail_UpdateBill = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_UpdateBill
  Timesheets_TimesheetDetail_UpdateTimsheet = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_UpdateTimsheet
  Timesheets_TimesheetDetail_RemoveAccount = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_RemoveAccount

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, public dialogRef: MatDialogRef<ViewBillComponent>, private userService: UserService,
    private timesheetProjectService: TimesheetProjectService,
    private projectBillService: TimeSheetProjectBillService, injector: Injector) {
    super(injector)
  }

  ngOnInit(): void {
    this.getProjectBill();
    // this.getAllFakeUser(false);
  }
  public getProjectBill() {
    this.isLoading = true
    this.projectBillService.getProjectBill(this.data.billInfo.projectId, this.data.billInfo.timesheetId).subscribe(data => {
      this.billDetail = data.result
      this.isLoading = false
      //this.getAllFakeUser(false)
    },
      () => { this.isLoading = false })
  }


  public saveUserBill(userBill: projectUserBillDto): void {
    delete userBill["createMode"];


    userBill.startTime = moment(userBill.startTime).format("YYYY-MM-DD");
    if (userBill.endTime) {
      userBill.endTime = moment(userBill.endTime).format("YYYY-MM-DD");
    }
    userBill.timesheetId = this.data.billInfo.timesheetId;
    userBill.projectId = this.data.billInfo.projectId;
    
    if (this.isCreate) {
      userBill.projectId = this.data.billInfo.projectId;
      delete userBill['userList'];
      this.projectBillService.createProjectBill(userBill).pipe(catchError(this.projectBillService.handleError)).subscribe(res => {
        abp.notify.success(`Create successfull`);
        this.getProjectBill();
        this.searchUserBill = "";
        // this.getAllFakeUser(false)
      },
        () => {
          userBill.createMode = true;
        })
      this.isCreate = false;
      this.isEdit = false;
      // this.userForUserBill.forEach((element,index) => {
      //   if(element.id==userBill.userId)
      //   this.userForUserBill.splice(index, 1);
      // });


    } else {
      let bill =
      [{
        "userId": userBill.userId,
        "billRole": userBill.billRole,
        "billRate": userBill.billRate,
        "note": userBill?.note,
        "isActive": userBill.isActive,
        "workingTime": userBill.workingTime,
        "id": userBill.id
      }]
      this.projectBillService.updateProjectBill(bill).pipe(catchError(this.projectBillService.handleError)).subscribe(res => {
        abp.notify.success(`Update successfull`)
        this.getProjectBill();
        this.searchUserBill = "";
        // this.getAllFakeUser(true)

      },
        () => {
          userBill.createMode = true;
        })
      this.isEdit = false;
    }


  }

  isComplete(e) {
    this.data.billInfo.isComplete = e.checked;
    let data = {      
      isComplete: this.data.billInfo.isComplete,
      id: this.data.billInfo.id
    }
    this.timesheetProjectService.setComplete(data).subscribe(res => {
      abp.notify.success(`Update successfull`);
      // this.getProjectBill();
      // this.searchUserBill = "";
    })
  }
  saveUserBills() {
    let arr = this.billDetail.map((userBill) => {
      return {
        projectId: userBill.projectId,
        timeSheetId: this.data.billInfo.timesheetId,
        userId: userBill.userId,
        billRole: userBill.billRole,
        billRate: userBill.billRate,
        startTime: userBill.startTime,
        endTime: userBill.endTime,
        currency: userBill.currency,
        note: userBill?.note,
        shadowNote: userBill.shadowNote,
        isActive: userBill.isActive,
        workingTime: userBill.workingTime,
        id: userBill.id
      }
    })

    this.projectBillService.updateProjectBill(arr).pipe(catchError(this.projectBillService.handleError)).subscribe(res => {
      abp.notify.success(`Update successfull`)
      this.getProjectBill();
      this.searchUserBill = "";
      this.isEdittingRows = false;
    })

  }
  public cancelUserBill(): void {
    this.getProjectBill();
    this.searchUserBill = "";
    this.isEdit = false;
    this.isCreate = false
    this.isEdittingRows = false

  }
  public editUserBill(userBill: projectUserBillDto): void {
    userBill.createMode = true;
    this.isEdit = true;
    // this.getAllFakeUser(true)
  }
  // private getAllFakeUser(isEdited) {
  //   this.isLoading = true
  //   this.projectBillService.getAllUserUnused(this.data.billInfo.projectId, this.data.billInfo.timesheetId, isEdited).pipe(catchError(this.userService.handleError)).subscribe(data => {
  //     // this.userForUserBill = data.result;
  //     this.billDetail.forEach(item => {
  //       item.userList = data.result
  //       item.searchText = ""
  //     })
  //     this.tempUserList = data.result

  //   })
  // }
  searchUser(bill) {
    bill.userList = this.tempUserList.filter(item =>
      (this.removeAccents(item?.fullName.toLowerCase().replace(/\s/g, "")).includes(this.removeAccents(bill.searchText.toLowerCase().replace(/\s/g, ""))) || this.removeAccents(item.email?.toLowerCase().replace(/\s/g, "")).includes(this.removeAccents(bill.searchText.toLowerCase().replace(/\s/g, "")))))
  }
  removeAccents(str) {
    var AccentsMap = [
      "aàảãáạăằẳẵắặâầẩẫấậ",
      "AÀẢÃÁẠĂẰẲẴẮẶÂẦẨẪẤẬ",
      "dđ", "DĐ",
      "eèẻẽéẹêềểễếệ",
      "EÈẺẼÉẸÊỀỂỄẾỆ",
      "iìỉĩíị",
      "IÌỈĨÍỊ",
      "oòỏõóọôồổỗốộơờởỡớợ",
      "OÒỎÕÓỌÔỒỔỖỐỘƠỜỞỠỚỢ",
      "uùủũúụưừửữứự",
      "UÙỦŨÚỤƯỪỬỮỨỰ",
      "yỳỷỹýỵ",
      "YỲỶỸÝỴ"
    ];
    for (var i = 0; i < AccentsMap.length; i++) {
      var re = new RegExp('[' + AccentsMap[i].substr(1) + ']', 'g');
      var char = AccentsMap[i][0];
      str = str.replace(re, char);
    }
    return str;
  }
  public onActiveChange(active, userBill) {
    userBill.isActive = active.checked
  }
  public create() {
    let bill = {} as TimesheetProjectBill;
    this.billDetail.unshift(bill)
    bill.createMode = true;
    this.isEdit = true;
    this.isCreate = true;
    // this.getAllFakeUser(false)

  }
  editMultiRows() {
    this.isEdittingRows = true;
    // this.getAllFakeUser(true)
    // this.userService.GetAllUserActive(false, true).pipe(catchError(this.userService.handleError)).subscribe(data => {
    //   this.userForUserBill = data.result;})

  }
  onUserSelect(bill) {
    bill.searchText = ""
  }
  saveUpdateTS(data){
    let request = [{
      Id: data.id,
      workingTime: data.workingTime,
      isActive: data.isActive,
      note: data.note
    }]
    this.projectBillService.updateTS(request).subscribe((response) =>{
      if(response.success){
        abp.notify.success(response.result)
        this.isEdit = false;
        this.isCreate = false
        this.isEdittingRows = false
        this.getProjectBill();
      }
      else{
        abp.notify.error(response.message)
      }
    })
  }


  protected removeAccountTS(data:TimesheetProjectBill): void {
    abp.message.confirm(
      "Remove account " + data.fullName + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.projectBillService.removeAccountTS(data.id).pipe(catchError(this.projectBillService.handleError)).subscribe((response) => {
            if(response.success){
              abp.notify.success("Remove successfull")
              this.isEdit = false;
              this.isCreate = false
              this.isEdittingRows = false
              this.getProjectBill();
            }
            else{
              abp.notify.error(response.message)
            }
          });
        }
      }
    );
  }
  saveAllUpdateTS(){
    let arr = this.billDetail.map((userBill) => {
      return {
        note: userBill?.note,
        isActive: userBill.isActive,
        workingTime: userBill.workingTime,
        id: userBill.id
      }
    })

    this.projectBillService.updateTS(arr).pipe(catchError(this.projectBillService.handleError)).subscribe(res => {
      abp.notify.success(`Update successfull`)
      this.getProjectBill();
      this.searchUserBill = "";
      this.isEdittingRows = false;
    })
  }
  public getTitleRate(){
    if(this.billDetail.length > 0){
      return '(' + this.billDetail[0].currency + '/' + this.chargeType[this.billDetail[0].chargeType] + ')'
    }
    else
      return ''
  }
}
