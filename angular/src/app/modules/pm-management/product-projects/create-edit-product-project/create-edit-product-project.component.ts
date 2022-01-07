import { ProjectDto } from './../../../../service/model/list-project.dto';
import { catchError } from 'rxjs/operators';
import { ListProjectService } from './../../../../service/api/list-project.service';
import { UserService } from './../../../../service/api/user.service';
import { DialogDataDto } from './../../../../service/model/common-DTO';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TrainingProjectDto } from './../../../../service/model/project.dto';
import { UserDto } from './../../../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, Inject, OnInit, Injector } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-create-edit-product-project',
  templateUrl: './create-edit-product-project.component.html',
  styleUrls: ['./create-edit-product-project.component.css']
})
export class CreateEditProductProjectComponent extends AppComponentBase implements OnInit {
  public pmList: UserDto[]=[];
  public searchPM: string = "";
  public project = {} as ProjectDto;
  public title ="";
  public projectStatusList: string[] = Object.keys(this.APP_ENUM.ProjectStatus)
  public isEditStatus = false;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogDataDto,
    private userService: UserService,
    public injector : Injector,
    public dialogRef : MatDialogRef<CreateEditProductProjectComponent>,
    public projectService : ListProjectService
  ) {
    super(injector)
  }

  ngOnInit(): void {
    this.getAllPM();
    this.title = this.project.name;
    if (this.data.command == "edit") {
      this.project = this.data.dialogData
      this.isEditStatus = true
    }    
  }
  public getAllPM(): void {
    this.userService.GetAllUserActive(true).pipe(catchError(this.userService.handleError)).subscribe(data => { this.pmList = data.result })
  }
  public saveAndClose(): void {
    this.isLoading = true
    this.project.startTime = moment(this.project.startTime).format("YYYY-MM-DD");
    if(this.project.endTime){
      this.project.endTime= moment(this.project.endTime).format("YYYY-MM-DD");
    }
    else{
      this.project.endTime =null
    }
    if (this.data.command == "create") {
      this.project.projectType = 3;
      this.project.status = 0;
      this.projectService.create(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("created new project");
        this.dialogRef.close(this.project);
      }, () => this.isLoading = false);
    }
    else {
      this.project.projectType = 3;
      this.projectService.update(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("edited project: "+this.project.name);
        this.dialogRef.close(this.project);
      }, () => this.isLoading = false);
    }
  }
}
