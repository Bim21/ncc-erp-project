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
  searchUserBill:string =""
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, public dialogRef: MatDialogRef<ViewBillComponent>, private userService: UserService,
    private projectBillService: TimeSheetProjectBillService, injector: Injector) {
    super(injector)
  }

  ngOnInit(): void {
    this.getProjectBill();
    this.getAllFakeUser()
  }
  public getProjectBill() {
    this.isLoading = true
    this.projectBillService.getProjectBill(this.data.projectId,this.data.timesheetId).subscribe(data => {
      this.billDetail = data.result
      this.isLoading = false
    },
      () => { this.isLoading = false })
  }
  public syncProjectBill() {
    abp.message.confirm(
      "Sync data from project Bill" + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.isLoading = true
          this.projectBillService.UpdateFromProjectUserBill(this.data.projectId).subscribe(rs => {
            this.isLoading = false
            abp.notify.success("Sync data sucessful")
            this.getProjectBill()
          },
            () => this.isLoading = false)
        }
      }
    );

  }

  public saveUserBill(userBill: projectUserBillDto): void {
    delete userBill["createMode"]
    userBill.startTime = moment(userBill.startTime).format("YYYY-MM-DD");
    if (userBill.endTime) {
      userBill.endTime = moment(userBill.endTime).format("YYYY-MM-DD");
    }
    this.projectBillService.updateProjectBill(userBill).pipe(catchError(this.projectBillService.handleError)).subscribe(res => {
      abp.notify.success(`Update successfull`)
      this.getProjectBill();
      this.searchUserBill = ""
    },
      () => {
        userBill.createMode = true;
      })
  }
  public cancelUserBill(): void {
    this.getProjectBill();
    this.searchUserBill = ""
  }
  public editUserBill(userBill: projectUserBillDto): void {
    userBill.createMode = true;
  }
  private getAllFakeUser() {
    this.userService.GetAllUserActive(false, true).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.userForUserBill = data.result;
    })
  }
  public onActiveChange(active,userBill){
    userBill.isActive = active.checked
  }
}
