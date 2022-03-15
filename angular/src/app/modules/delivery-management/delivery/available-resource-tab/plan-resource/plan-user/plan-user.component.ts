import { ProjectUserService } from './../../../../../../service/api/project-user.service';
import { catchError } from 'rxjs/operators';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DeliveryResourceRequestService } from '../../../../../../service/api/delivery-request-resource.service';
import { AppComponentBase } from '@shared/app-component-base';
import { ProjectDto } from '../../../../../../service/model/project.dto';
import { ListProjectService } from '../../../../../../service/api/list-project.service';
import { planUserDto } from '../../../../../../service/model/delivery-management.dto';
import { Component, OnInit, Injector, Inject } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-plan-user',
  templateUrl: './plan-user.component.html',
  styleUrls: ['./plan-user.component.css']
})
export class PlanUserComponent extends AppComponentBase implements OnInit {
  public planUser = {} as planUserDto;
  public editUser = {} as planUserDto;
  public listProject: ProjectDto[] = [];
  public projectRoleList = Object.keys(this.APP_ENUM.ProjectUserRole);
  public searchProject: string = ""
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private listProjectService: ListProjectService,
    private availableResourceService: DeliveryResourceRequestService,
    public injector: Injector,
    public dialogRef: MatDialogRef<PlanUserComponent>,
    private projectUserService: ProjectUserService) { super(injector) }

  ngOnInit(): void {
    this.getAllProject();
    this.planUser.isPool = false
    this.planUser.allocatePercentage = 100
    this.planUser.userId = this.data.item.userId;
    this.planUser.fullName = this.data.item.fullName;
  }
  public SaveAndClose() {

    if (this.data.command == "plan") {
      this.planUser.percentUsage = 100
      this.planUser.startTime = moment(this.planUser.startTime).format("YYYY/MM/DD");
      this.availableResourceService.planUser(this.planUser).pipe(catchError(this.availableResourceService.handleError)).subscribe((res) => {
        abp.notify.success("Planed Successfully!");
        this.dialogRef.close(this.planUser);

      }, () => this.isLoading = false);
    }
  }

  public getAllProject() {
    this.listProjectService.getAll().subscribe(data => {
      this.listProject = data.result;

    })
  }
  getPercentage(user, data) {
    user.percentUsage = data
  }


}
