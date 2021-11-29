import { CurrencyService } from './../../../../service/api/currency.service';
import { CurrencyDto } from './../../../../service/model/currency.dto';
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
  public searchPM: string = "";
  public title ="";
  public currencyList: CurrencyDto[]=[];
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogDataDto,
    injector: Injector,
    public projectService: ListProjectService,
    public dialogRef: MatDialogRef<CreateEditListProjectComponent>,
    private clientService: ClientService,
    private userService: UserService,
    private currencyService: CurrencyService
  ) {
    super(injector);
    this.projectTypeList = Object.keys(this.APP_ENUM.ProjectType)
  }

  ngOnInit(): void {
    this.getAllPM();
    this.getAllCurrency();
    if (this.data.command == "edit") {
      this.project = this.data.dialogData
      console.log(this.project)
      // this.project.projectType = this.APP_ENUM.ProjectType[this.project.projectType]
      // this.project.status = this.APP_ENUM.ProjectStatus[this.project.status]
      this.isEditStatus = true
    }
    this.getAllClient()
    this.title = this.project.name;
    
  }
  public getAllPM(): void {
    this.userService.GetAllUserActive(true).pipe(catchError(this.userService.handleError)).subscribe(data => { this.pmList = data.result })
  }
  public getAllCurrency(){
    this.currencyService.getAll().pipe(catchError(this.currencyService.handleError)).subscribe(data => {
      this.currencyList = data.result;
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
      this.project.status = 0;
      this.projectService.create(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("created new project");
        this.dialogRef.close(this.project);
      }, () => this.isLoading = false);
    }
    else {
      this.projectService.update(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("edited project: "+this.project.name);
        this.dialogRef.close(this.project);
      }, () => this.isLoading = false);
    }
  }
  private getAllClient(): void {
    this.clientService.getAll().pipe(catchError(this.clientService.handleError)).subscribe(data => {
      this.clientList = data.result
    })
  }


}
