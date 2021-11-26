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
  tempUserList= []
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, public dialogRef: MatDialogRef<ViewBillComponent>, private userService: UserService,
    private timesheetProjectService: TimesheetProjectService,
    private projectBillService: TimeSheetProjectBillService, injector: Injector) {
    super(injector)
  }

  ngOnInit(): void {
    this.getProjectBill();
    this.getAllFakeUser();
    console.log(this.data)
  }
  public getProjectBill() {
    this.isLoading = true
    this.projectBillService.getProjectBill(this.data.projectId, this.data.timesheetId).subscribe(data => {
      this.billDetail = data.result
      this.isLoading = false
    },
      () => { this.isLoading = false })
  }


  public saveUserBill(userBill: projectUserBillDto): void {
    delete userBill["createMode"];


    userBill.startTime = moment(userBill.startTime).format("YYYY-MM-DD");
    if (userBill.endTime) {
      userBill.endTime = moment(userBill.endTime).format("YYYY-MM-DD");
    }
    userBill.timesheetId = this.data.timesheetId;
    userBill.projectId = this.data.projectId;
    let bill =
      [{
        "projectId": userBill.projectId,
        "timeSheetId": userBill.timesheetId,
        "userId": userBill.userId,
        "billRole": userBill.billRole,
        "billRate": userBill.billRate,
        "startTime": userBill.startTime,
        "endTime": userBill.endTime,
        "currency": userBill.currency,
        "note": userBill?.note,
        "shadowNote": userBill.shadowNote,
        "isActive": userBill.isActive,
        "workingTime": userBill.workingTime,
        "id": userBill.id
      }]
    if (this.isCreate) {
      userBill.projectId = this.data.projectId;
      this.projectBillService.createProjectBill(userBill).pipe(catchError(this.projectBillService.handleError)).subscribe(res => {
        abp.notify.success(`Create successfull`);
        this.getProjectBill();
        this.searchUserBill = "";
        this.getAllFakeUser()
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
      this.projectBillService.updateProjectBill(bill).pipe(catchError(this.projectBillService.handleError)).subscribe(res => {
        abp.notify.success(`Update successfull`)
        this.getProjectBill();
        this.searchUserBill = "";
        this.getAllFakeUser()

      },
        () => {
          userBill.createMode = true;
        })
      this.isEdit = false;
    }


  }

  isComplete(e) {
    if (e.checked == true) {
      this.data.isComplete = true;
    } else {
      this.data.isComplete = false;
    }
    let data = {
      projectId: this.data.projectId,
      timesheetId: this.data.timesheetId,
      note: this.data.note,
      projectBillInfomation: this.data.projectBillInfomation,
      isComplete: this.data.isComplete,
      id: this.data.id
    }
    this.timesheetProjectService.update(data).subscribe(res => {
      abp.notify.success(`Update successfull`);
      this.getProjectBill();
      this.searchUserBill = "";
    })
  }
  saveUserBills() {
    let arr = this.billDetail.map((userBill) => {
      return {
        projectId: userBill.projectId,
        timeSheetId: this.data.timesheetId,
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
    this.getAllFakeUser()

  }
  public editUserBill(userBill: projectUserBillDto): void {
    userBill.createMode = true;
    this.isEdit = true;
    this.getAllFakeUser(userBill.userId)




  }
  private getAllFakeUser(userId?) {
    this.userService.GetAllUserActive(false, true).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.userForUserBill = data.result;
      this.tempUserList = data.result
      this.projectBillService.getProjectBill(this.data.projectId, this.data.timesheetId).subscribe(rs => {
        let temp = rs.result.map(item=>item.userId)
        if(userId){
          temp.splice(temp.indexOf(userId),1)
          temp = new Set(temp)
        }
        this.userForUserBill = data.result.filter(item=> !temp.includes(item.id))
      })
    })
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

  }
  editMultiRows() {
    this.isEdittingRows = true;
    // this.userService.GetAllUserActive(false, true).pipe(catchError(this.userService.handleError)).subscribe(data => {
    //   this.userForUserBill = data.result;})

  }
  onUserSelect(user){
// console.log(user)
// this.getAllFakeUser()
  }

}
