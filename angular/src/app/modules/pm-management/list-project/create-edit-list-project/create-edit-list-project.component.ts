import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ListProjectService } from '@app/service/api/list-project.service';
import { ProjectDto } from '@app/service/model/list-project.dto';
import { AppComponentBase } from '@shared/app-component-base';


import { catchError } from 'rxjs/operators';
@Component({
  selector: 'app-create-edit-list-project',
  templateUrl: './create-edit-list-project.component.html',
  styleUrls: ['./create-edit-list-project.component.css']
})
export class CreateEditListProjectComponent extends AppComponentBase implements OnInit {
  isDisable: boolean = false;
  projectTypeList:string[]=[]
  project = {} as ProjectDto;
  checklistItem = {}
  checked: boolean;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    injector: Injector,
    public projectService: ListProjectService,
    public dialogRef: MatDialogRef<CreateEditListProjectComponent>
  ) { 
    super(injector); 
    this.projectTypeList = Object.keys(this.APP_ENUM.ProjectType) 
  }

  ngOnInit(): void {
    console.log(this.projectTypeList);
  }

  saveAndClose() {
    this.isDisable = true
    if (this.data.command == "create") {
      this.projectService.create(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("created branch successfully");
        this.dialogRef.close();
      }, () => this.isDisable = false);
    }
    else {
      this.projectService.update(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("edited branch successfully");
        this.dialogRef.close();
      }, () => this.isDisable = false);
    }
  }



}

