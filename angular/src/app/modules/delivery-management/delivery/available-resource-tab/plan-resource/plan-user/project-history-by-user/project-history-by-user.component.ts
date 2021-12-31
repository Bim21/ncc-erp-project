import { Component, Inject, Injector, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

import { ProjectUserService } from "@app/service/api/project-user.service";
import { IProjectHistoryUser } from "@app/service/model/project.dto";
import { AppComponentBase } from "@shared/app-component-base";
import { catchError } from "rxjs/operators";
import { PlanUserComponent } from "../plan-user.component";

@Component({
  selector: "app-project-history-by-user",
  templateUrl: "./project-history-by-user.component.html",
  styleUrls: ["./project-history-by-user.component.css"],
})
export class ProjectHistoryByUserComponent
  extends AppComponentBase
  implements OnInit
{
  userId: number;
  emailAddress: string;
  projectsHistoryUser: IProjectHistoryUser[] = [];
  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: {
      item: {
        userId: number;
        emailAddress: string;
      };
    },
    public injector: Injector,
    public dialogRef: MatDialogRef<PlanUserComponent>,
    private projectUserService: ProjectUserService
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.userId = this.data.item.userId;
    this.emailAddress = this.data.item.emailAddress;
    this.getProjectHistoryByUser();
  }
  public getProjectHistoryByUser() {
    this.projectUserService
      .getProjectHistoryByUser(this.userId)
      .pipe(catchError(this.projectUserService.handleError))
      .subscribe((data) => {
        this.projectsHistoryUser = data.result;
      });
  }
}
