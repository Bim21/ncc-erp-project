import { Component, Inject, Injector, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { DeliveryResourceRequestService } from '@app/service/api/delivery-request-resource.service';
import { ProjectUserService } from '@app/service/api/project-user.service';
import { AppComponentBase } from '@shared/app-component-base';
import * as moment from 'moment';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-form-set-done',
  templateUrl: './form-set-done.component.html',
  styleUrls: ['./form-set-done.component.css']
})
export class FormSetDoneComponent extends AppComponentBase implements OnInit {
  public planUserInfo: any;
  public plannedUserList: any = []
  public historyUser: string;
  public allowConfirm:boolean  =true
  public timeDone: any = new Date();
  PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject = PERMISSIONS_CONSTANT.PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject
  PmManager_ProjectUser_ConfirmPickUserFromPoolToProject = PERMISSIONS_CONSTANT.PmManager_ProjectUser_ConfirmPickUserFromPoolToProject
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _resourceRequestService: DeliveryResourceRequestService, 
    public injector: Injector,
    public dialogRef: MatDialogRef<FormSetDoneComponent>,
    private projectUserService: ProjectUserService, 
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.planUserInfo = this.data
    console.log(this.planUserInfo)
    this.getPlannedUser()
  }

  save(){

  }
  private getPlannedUser() {
    this.projectUserService.GetAllWorkingProjectByUserId(this.planUserInfo.userId).pipe(catchError(this.projectUserService.handleError)).subscribe(data => {
      this.plannedUserList = data.result;
    })
  }

  confirm() {
    const request = {
      resourceRequestId: this.planUserInfo.resourceRequestId,
      projectUserId: this.planUserInfo.projectUserId,
      startTime: moment(this.planUserInfo.plannedDate).format("YYYY-MM-DD"),
      timeDone:  moment(this.timeDone).format("YYYY-MM-DD")
    }
    console.log(request)
    if (this.plannedUserList.length > 0) {
        this._resourceRequestService.setDoneRequest(request).pipe(catchError(this.projectUserService.handleError)).subscribe(rs => {
          abp.notify.success(`Confirmed for user ${this.planUserInfo.plannedEmployee} join project`)
          this.dialogRef.close(true)
        })
    }
    else {
      abp.message.confirm(`Confirm user <strong>${this.planUserInfo.plannedEmployee}</strong> <strong class="text-success">join</strong> Project`, "", rs => {
        if (rs) {
            this._resourceRequestService.setDoneRequest(request).pipe(catchError(this.projectUserService.handleError)).subscribe(rs => {
              this.dialogRef.close(true)
              abp.notify.success(`Confirmed for user ${this.planUserInfo.plannedEmployee} join project`)
            })
        }
      }, true)
    }
  }

  checkConfirmPermission() {
    if (!this.permission.isGranted(this.PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject)) {
      this.plannedUserList.forEach(pu => {
        if (pu.isPool == false) {
          this.allowConfirm = false
          return 
        }
      }
      )
    }
  }

  cancel(){
    this.dialogRef.close(false);
  }

}
