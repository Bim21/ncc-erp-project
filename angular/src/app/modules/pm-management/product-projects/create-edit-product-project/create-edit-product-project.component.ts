import { ProjectDto } from './../../../../service/model/list-project.dto';
import { catchError } from 'rxjs/operators';
import { ListProjectService } from './../../../../service/api/list-project.service';
import { UserService } from './../../../../service/api/user.service';
import { DialogDataDto } from './../../../../service/model/common-DTO';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TrainingProjectDto } from './../../../../service/model/project.dto';
import { ClientDto } from './../../../../service/model/list-project.dto';
import { ClientService } from './../../../../service/api/client.service';
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
  public clientList: ClientDto[] = []
  public searchClient: string = ""
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogDataDto,
    private userService: UserService,
    private clientService: ClientService,
    public injector : Injector,
    public dialogRef : MatDialogRef<CreateEditProductProjectComponent>,
    public projectService : ListProjectService
  ) {
    super(injector)
  }

  ngOnInit(): void {
    this.getAllPM();
    this.getAllClient();
    if (this.data.command == "edit") {
      this.project = this.data.dialogData
      this.isEditStatus = true
    }
    else{
      this.project.status = this.APP_ENUM.ProjectStatus.InProgress;
      this.project.startTime = moment(new Date()).format("YYYY-MM-DD")
      this.getIdClientByCodeNcc();
    }
    this.title = this.project.name;
  }
  public getAllPM(): void {
    this.userService.GetAllUserActive(true).pipe(catchError(this.userService.handleError)).subscribe(data => { this.pmList = data.result })
  }
  
  private getAllClient(): void {
    this.clientService.getAll().pipe(catchError(this.clientService.handleError)).subscribe(data => {
      this.clientList = data.result
    })
  }
  private getIdClientByCodeNcc(): void {
    this.clientService.getIdClientByCodeNcc().pipe(catchError(this.clientService.handleError)).subscribe(data => {
      this.project.clientId = data.result
    })
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
      this.projectService.create(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("created new project");
        this.dialogRef.close(this.project);
        if(res.result == null || res.result == ""){
          abp.message.success(`<p>Create project name <b>${this.project.name}</b> in <b>PROJECT TOOL</b> successful!</p> 
          <p style='color:#28a745'>Create project name <b>${this.project.name}</b> in <b>TIMESHEET TOOL</b> successful!</p>`, 
         'Create project result',true);
        }
        else{
          abp.message.error(`<p>Create project name <b>${this.project.name}</b> in <b>PROJECT TOOL</b> successful!</p> 
          <p style='color:#dc3545'>${res.result}</p>`, 
          'Create project result',true);
        }
      }, () => this.isLoading = false);
    }
    else {
      this.project.projectType = 3;
      this.projectService.update(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("Edited: " + this.project.name);
        if(res.result == "update-only-project-tool"){
          abp.notify.success("Edited project: "+this.project.name);
        }
        else if(res.result == null || res.result == ""){
          abp.message.success(`<p>Edited project name <b>${this.project.name}</b> in <b>PROJECT TOOL</b> successful!</p> 
          <p style='color:#28a745'>Edited project name <b>${this.project.name}</b> in <b>TIMESHEET TOOL</b> successful!</p>`, 
         'Edit project result',true);
        }
        else{
          abp.message.error(`<p>Edited project <b>${this.project.name}</b> in <b>PROJECT TOOL</b> successful!</p> 
          <p style='color:#dc3545'>${res.result}</p>`, 
          'Edit project result',true);
        }
        this.dialogRef.close(this.project);
      }, () => this.isLoading = false);
    }
  }
  
  focusOut(){
    this.searchPM = '';
  }
}
