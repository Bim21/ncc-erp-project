import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { catchError } from 'rxjs/operators';
import { DeliveryResourceRequestService } from './../../../../../../../service/api/delivery-request-resource.service';
import { AppComponentBase } from '@shared/app-component-base';
import { APP_ENUMS } from './../../../../../../../../shared/AppEnums';
import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectUserService } from '@app/service/api/project-user.service';
import * as moment from 'moment';

@Component({
  selector: 'app-confirm-plan-dialog',
  templateUrl: './confirm-plan-dialog.component.html',
  styleUrls: ['./confirm-plan-dialog.component.css']
})
export class ConfirmPlanDialogComponent extends AppComponentBase implements OnInit {

  DeliveryManagement_ProjectUser_ConfirmMoveEmployeeToOtherProject = PERMISSIONS_CONSTANT.DeliveryManagement_ProjectUser_ConfirmMoveEmployeeToOtherProject
  DeliveryManagement_ProjectUser_ConfirmPickUserFromPoolToProject = PERMISSIONS_CONSTANT.DeliveryManagement_ProjectUser_ConfirmPickUserFromPoolToProject
  DeliveryManagement_ProjectUser_MoveEmployeeToOtherProject = PERMISSIONS_CONSTANT.DeliveryManagement_ProjectUser_MoveEmployeeToOtherProject
  DeliveryManagement_ProjectUser_PickUserFromPoolToProject = PERMISSIONS_CONSTANT.DeliveryManagement_ProjectUser_PickUserFromPoolToProject

  public allowConfirm:boolean  =true
  public startDate
  public user
  public title: string = ""
  public confirmMessage: string = ""
  public projectRoleList = Object.keys(APP_ENUMS.ProjectUserRole);
  public workingProject: any[] = []
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private puService: DeliveryResourceRequestService,
    injector: Injector,
    private dialogRef: MatDialogRef<ConfirmPlanDialogComponent>) {
    super(injector)
  }

  ngOnInit(): void {
    
    this.user = this.data.user
    this.workingProject = this.data.workingProject
    this.startDate = moment(this.data.user.startTime).format("YYYY-MM-DD")
    if (this.data.user.allocatePercentage > 0) {
      this.title = `Confirm <strong>${this.user.fullName}</strong> <strong class ="text-success">join</strong> <strong>${this.user.projectName}</strong>`
    }
    else if (this.data.user.allocatePercentage <= 0) {
      this.title = `Confirm <strong>${this.user.fullName}</strong> <strong class="text-danger">Out</strong> <strong>${this.user.projectName}</strong>`
    }
   this.checkConfirmPermission()

  }
  confirm() {
    if (this.user.allocatePercentage > 0) {
      this.puService.ConfirmJoinProject(this.user.id, moment(this.startDate).format("YYYY-MM-DD")).pipe(catchError(this.puService.handleError)).subscribe(rs => {
        abp.notify.success(`Confirmed for user ${this.user.fullName} join project`)
        this.dialogRef.close(true)
      })
    }
    else {
      let requestBody = {
        projectUserId: this.user.id,
        startTime: this.startDate
      }
      this.puService.ConfirmOutProject(requestBody).pipe(catchError(this.puService.handleError)).subscribe(rs => {
        abp.notify.success(`Confirmed for user ${this.user.fullName} out project`)
        this.dialogRef.close(true)
      })
    }
  }
  checkConfirmPermission() {
    console.log(this.permission.isGranted(this.DeliveryManagement_ProjectUser_ConfirmMoveEmployeeToOtherProject) == false)
    console.log(this.workingProject)
    if (this.permission.isGranted(this.DeliveryManagement_ProjectUser_ConfirmMoveEmployeeToOtherProject) == false) {
      this.workingProject.forEach(pu => {
        if (pu.isPool == false) {
          this.allowConfirm = false
          return 
        }
      }
      )
    }
  }
}
