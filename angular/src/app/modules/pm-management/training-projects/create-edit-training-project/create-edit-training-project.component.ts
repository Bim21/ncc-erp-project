import { TrainingProjectDto } from './../../../../service/model/project.dto';
import { DialogDataDto } from './../../../../service/model/common-DTO';
import { ListProjectService } from '@app/service/api/list-project.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AppComponentBase } from 'shared/app-component-base';
import { UserDto } from './../../../../../shared/service-proxies/service-proxies';
import { catchError } from 'rxjs/operators';
import { UserService } from './../../../../service/api/user.service';
import { Component, OnInit, Injector, Inject } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-create-edit-training-project',
  templateUrl: './create-edit-training-project.component.html',
  styleUrls: ['./create-edit-training-project.component.css']
})
export class CreateEditTrainingProjectComponent extends AppComponentBase implements OnInit {
  public pmList: UserDto[]=[];
  public searchPM: string = "";
  public project = {} as TrainingProjectDto;
  public title ="";
  public projectStatusList: string[] = Object.keys(this.APP_ENUM.ProjectStatus)
  public isEditStatus = false;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogDataDto,
    private userService: UserService,
    public injector : Injector,
    public dialogRef : MatDialogRef<CreateEditTrainingProjectComponent>,
    public projectService : ListProjectService
  ) {
    super(injector)
   }

   ngOnInit(): void {
    this.getAllPM();
    if (this.data.command == "edit") {
      this.project = this.data.dialogData
      this.isEditStatus = true
    }
    this.title = this.project.name;
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
      this.project.projectType = 5;
      this.project.status = 0;
      this.projectService.CreateTrainingProject(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("created new project");
        this.dialogRef.close(this.project);
      }, () => this.isLoading = false);
    }
    else {
      this.project.projectType = 5;
      this.projectService.UpdateTrainingProject(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("edited project: "+this.project.name);
        this.dialogRef.close(this.project);
      }, () => this.isLoading = false);
    }
  }

}
