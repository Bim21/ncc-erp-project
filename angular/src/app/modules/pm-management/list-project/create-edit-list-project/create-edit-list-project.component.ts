import { UserDto } from './../../../../../shared/service-proxies/service-proxies';
import { UserService } from './../../../../service/api/user.service';
import { DialogDataDto } from './../../../../service/model/common-DTO';
import { ClientService } from './../../../../service/api/client.service';
import { ClientDto } from './../../../../service/model/list-project.dto';
import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ListProjectService } from '@app/service/api/list-project.service';
import { ProjectDto } from '@app/service/model/list-project.dto';
import { AppComponentBase } from '@shared/app-component-base';


import { catchError } from 'rxjs/operators';
import * as moment from 'moment';
@Component({
  selector: 'app-create-edit-list-project',
  templateUrl: './create-edit-list-project.component.html',
  styleUrls: ['./create-edit-list-project.component.css']
})
export class CreateEditListProjectComponent extends AppComponentBase implements OnInit {
  public project = {} as ProjectDto;
  public checked: boolean;
  public projectTypeList: string[] = Object.keys(this.APP_ENUM.ProjectType)
  public projectStatusList: string[] = Object.keys(this.APP_ENUM.ProjectStatus)
  public clientList: ClientDto[] = []
  public pmList: UserDto;
  public isEditStatus = false;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogDataDto,
    injector: Injector,
    public projectService: ListProjectService,
    public dialogRef: MatDialogRef<CreateEditListProjectComponent>,
    private clientService: ClientService,
    private userService: UserService
  ) {
    super(injector);
    this.projectTypeList = Object.keys(this.APP_ENUM.ProjectType)
  }

  ngOnInit(): void {
    this.getAllPM();
    if (this.data.command == "edit") {
      this.project = this.data.dialogData
      this.project.projectType = this.APP_ENUM.ProjectType[this.project.projectType]
      this.project.status = this.APP_ENUM.ProjectStatus[this.project.status]
      this.isEditStatus = true
    }
    this.getAllClient()
  }
  public getAllPM(): void {
    this.userService.getAll().pipe(catchError(this.userService.handleError)).subscribe(data => { this.pmList = data.result.items })
  }

  public saveAndClose(): void {
    if (this.project.startTime) {
      this.project.startTime = moment(this.project.startTime).format("YYYY-MM-DD");

    }
    if (this.project.endTime) {
      this.project.endTime = moment(this.project.endTime).format("YYYY-MM-DD");

    }
    if(new Date(this.project.startTime) < new Date(this.project.endTime)){
      this.isLoading = true
      if (this.data.command == "create") {
        this.project.status = 0;
        this.projectService.create(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
          abp.notify.success("created branch successfully");
          this.dialogRef.close(this.project);
        }, () => this.isLoading = false);
      }
      else {
        this.projectService.update(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
          abp.notify.success("edited branch successfully");
          this.dialogRef.close(this.project);
        }, () => this.isLoading = false);
      }
    }
    else{
      abp.notify.error("Project end time can't less than start time")
    }

 
  }
  private getAllClient(): void {
    this.clientService.getAll().pipe(catchError(this.clientService.handleError)).subscribe(data => {
      this.clientList = data.result
    })
  }


}

