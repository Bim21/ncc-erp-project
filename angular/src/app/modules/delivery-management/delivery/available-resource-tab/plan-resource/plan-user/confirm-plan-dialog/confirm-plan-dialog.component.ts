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

  public startDate
  public user
  public title: string = ""
  public confirmMessage: string = ""
  public projectRoleList = Object.keys(APP_ENUMS.ProjectUserRole);

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private puService: DeliveryResourceRequestService,
    injector: Injector,
    private dialogRef: MatDialogRef<ConfirmPlanDialogComponent>) {
    super(injector)
  }

  ngOnInit(): void {
    this.user = this.data.user
    console.log(this.data)
    this.startDate = moment(this.data.user.startTime).format("YYYY-MM-DD")
    if (this.data.user.allocatePercentage > 0) {
      this.title = `Confirm <strong>${this.user.fullName}</strong> <strong class ="text-success">join</strong> <strong>${this.user.projectName}</strong>`
    }
    else if (this.data.user.allocatePercentage <= 0) {
      this.title = `Confirm <strong>${this.user.fullName}</strong> <strong class="text-danger">Out</strong> <strong>${this.user.projectName}</strong>`
    }
  }
  confirm() {

    if (this.user.allocatePercentage > 0) {
      if (this.data.workingProject.length > 0) {
        let message: string = ""
        this.data.workingProject.forEach(project => {
          message += `<p>- <strong>${project.projectName} (${project.pmName})</strong> from ${moment(project.startTime).format("DD/MM/YYYY")}</p>`
        })
        abp.message.confirm(
          `<div class='text-left'><div style= "font-size: 22px;" ><strong>${this.user.fullName} </strong> is working on: </div> <br/>
            ${message}
           <div >
           Are you sure to confirm <strong>${this.user.fullName}</strong> join project and release from other projects?
           </div>
              </div>`
          , `   `, (rs) => {
            if (rs) {
              this.puService.ConfirmJoinProject(this.user.id, moment(this.startDate).format("YYYY-MM-DD")).pipe(catchError(this.puService.handleError)).subscribe(rs => {
                abp.notify.success(`Confirmed for user ${this.user.fullName} join project`)
                this.dialogRef.close(true)
              })
            }
          },
          true
        );
      }
      else {
        abp.message.confirm(`Confirm user <strong>${this.user.fullName}</strong> <strong class="text-success">join</strong> Project`, "", rs => {
          if (rs) {
            this.puService.ConfirmJoinProject(this.user.id, moment(this.startDate).format("YYYY-MM-DD")).pipe(catchError(this.puService.handleError)).subscribe(rs => {
              abp.notify.success(`Confirmed for user ${this.user.fullName} join project`)
              this.dialogRef.close(true)
            })

          }
        }, true)
      }
    }
    else {
      let requestBody ={
        projectUserId: this.user.id,
        startTime: this.startDate
      }
      this.puService.ConfirmOutProject(requestBody).pipe(catchError(this.puService.handleError)).subscribe(rs => {
        abp.notify.success(`Confirmed for user ${this.user.fullName} out project`)
        this.dialogRef.close(true)
      })
    }
  }
}
