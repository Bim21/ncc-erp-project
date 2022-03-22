import { AppComponentBase } from '@shared/app-component-base';
import { catchError } from 'rxjs/operators';
import { ProjectUserService } from '@app/service/api/project-user.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit, Inject, Injector } from '@angular/core';
import * as moment from 'moment';
import { PMReportProjectService } from '@app/service/api/pmreport-project.service';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';

@Component({
  selector: 'app-confirm-popup',
  templateUrl: './confirm-popup.component.html',
  styleUrls: ['./confirm-popup.component.css']
})
export class ConfirmPopupComponent extends AppComponentBase implements OnInit {
  PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject = PERMISSIONS_CONSTANT.PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject
  PmManager_ProjectUser_ConfirmPickUserFromPoolToProject = PERMISSIONS_CONSTANT.PmManager_ProjectUser_ConfirmPickUserFromPoolToProject
  
  public startDate
  public user
  public title: string = ""
  public confirmMessage: string = ""
  public allowConfirm:boolean  =true
  public workingProject: any[] = []
 
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, injector:Injector,
    private projectUserService: ProjectUserService, private pmReportProService:PMReportProjectService,
     private dialogRef: MatDialogRef<ConfirmPopupComponent>) {
       super(injector)
      }

  ngOnInit(): void {
    this.user = this.data.user
    console.log("user",this.user)
    this.startDate = moment(this.data.user.startTime).format("YYYY-MM-DD")
    if (this.data.type === 'confirmJoin') {
      this.title = `Confirm user: <strong>${this.user.fullName}</strong> <strong class ="text-success">join project</strong>`
    }
    else if (this.data.type === 'confirmOut') {
      this.title = `Confirm user: <strong>${this.user.fullName}</strong> <strong class="text-success">Out project</strong>`
    }
    else{
      this.title = `Add user: <strong>${this.user.fullName}</strong> to project`
    }
    this.workingProject = this.data.workingProject
    this.checkConfirmPermission()
  }
  confirm() {
    if (this.data.workingProject.length > 0) {
      if (this.data.page == "weekly") {
        this.pmReportProService.ConfirmJoinProject(this.user.id, moment(this.startDate).format("YYYY-MM-DD")).pipe(catchError(this.pmReportProService.handleError)).subscribe(rs => {
          abp.notify.success(`Confirmed for user ${this.user.fullName} join project`)
          this.dialogRef.close(true)
        })
      }
      else if(this.data.type == 'confirmJoin'){
        this.projectUserService.ConfirmJoinProject(this.user.id, moment(this.startDate).format("YYYY-MM-DD")).pipe(catchError(this.projectUserService.handleError)).subscribe(rs => {
          abp.notify.success(`Confirmed for user ${this.user.fullName} join project`)
          this.dialogRef.close(true)
        })
      }
      else{
        this.dialogRef.close(true)
      }
    }
    else {
      abp.message.confirm(`Confirm user <strong>${this.user.fullName}</strong> <strong class="text-success">join</strong> Project`, "", rs => {
        if (rs) {

          if (this.data.page == "weekly") {
            this.pmReportProService.ConfirmJoinProject(this.user.id, moment(this.startDate).format("YYYY-MM-DD")).pipe(catchError(this.pmReportProService.handleError)).subscribe(rs => {
              this.dialogRef.close(true)
              abp.notify.success(`Confirmed for user ${this.user.fullName} join project`)
            })
          }
          else {
            this.projectUserService.ConfirmJoinProject(this.user.id, moment(this.startDate).format("YYYY-MM-DD")).pipe(catchError(this.projectUserService.handleError)).subscribe(rs => {
              this.dialogRef.close(true)
              abp.notify.success(`Confirmed for user ${this.user.fullName} join project`)
            })

          }


        }
      }, true)
    }
  }

  checkConfirmPermission() {
    if (this.permission.isGranted(this.PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject) == false) {
      this.data.workingProject.forEach(pu => {
        if (pu.isPool == false) {
          this.allowConfirm = false
          return 
        }
      }
      )
    }
  }
}
