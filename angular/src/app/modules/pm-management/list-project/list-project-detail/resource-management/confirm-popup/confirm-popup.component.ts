import { catchError } from 'rxjs/operators';
import { ProjectUserService } from '@app/service/api/project-user.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit, Inject } from '@angular/core';
import * as moment from 'moment';
import { PMReportProjectService } from '@app/service/api/pmreport-project.service';

@Component({
  selector: 'app-confirm-popup',
  templateUrl: './confirm-popup.component.html',
  styleUrls: ['./confirm-popup.component.css']
})
export class ConfirmPopupComponent implements OnInit {
  public startDate
  public user
  public title: string = ""
  public confirmMessage: string = ""
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private projectUserService: ProjectUserService, private pmReportProService:PMReportProjectService,
     private dialogRef: MatDialogRef<ConfirmPopupComponent>) { }

  ngOnInit(): void {
    this.user = this.data.user
    this.startDate = moment(this.data.user.startTime).format("YYYY-MM-DD")
    if (this.data.type === 'confirmJoin') {
      this.title = `Confirm user: <strong>${this.user.fullName}</strong> <strong class ="text-success">join project</strong>`
    }
    else if (this.data.type === 'confirmOut') {
      this.title = `Confirm user: <strong>${this.user.fullName}</strong> <strong class="text-success">Out project</strong>`
    }
  }
  confirm() {
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
            if (this.data.page == "weekly") {
              this.pmReportProService.ConfirmJoinProject(this.user.id, moment(this.startDate).format("YYYY-MM-DD")).pipe(catchError(this.pmReportProService.handleError)).subscribe(rs => {
                abp.notify.success(`Confirmed for user ${this.user.fullName} join project`)
                this.dialogRef.close(true)
              })
            }
            else {
              this.projectUserService.ConfirmJoinProject(this.user.id, moment(this.startDate).format("YYYY-MM-DD")).pipe(catchError(this.projectUserService.handleError)).subscribe(rs => {
                abp.notify.success(`Confirmed for user ${this.user.fullName} join project`)
                this.dialogRef.close(true)
              })
            }
          }
        },
        true
      );
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
}
