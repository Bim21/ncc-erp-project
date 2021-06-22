import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ListProjectService } from '@app/service/api/list-project.service';
import { ListProjectDto } from '@app/service/model/list-project.dto';
import { AppComponentBase } from '@shared/app-component-base';


import { APP_ENUMS } from '@shared/AppEnums';
import { catchError } from 'rxjs/operators';
@Component({
  selector: 'app-create-edit-list-project',
  templateUrl: './create-edit-list-project.component.html',
  styleUrls: ['./create-edit-list-project.component.css']
})
export class CreateEditListProjectComponent extends AppComponentBase implements OnInit {
  isDisable: boolean = false;
  dayProjectTypeList = Object.keys(APP_ENUMS.ProjectType) 
  listProject = {} as ListProjectDto;
  checklistItem = {}
  checked: boolean;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    injector: Injector,
    public listProjectService: ListProjectService,
    public dialogRef: MatDialogRef<CreateEditListProjectComponent>
  ) { 
    super(injector); 
  }

  ngOnInit(): void {
  }

  saveAndClose() {
    this.isDisable = true
    if (this.data.command == "create") {
      this.listProjectService.create(this.listProject).pipe(catchError(this.listProjectService.handleError)).subscribe((res) => {
        abp.notify.success("created branch successfully");
        this.dialogRef.close();
      }, () => this.isDisable = false);
    }
    else {
      this.listProjectService.update(this.listProject).pipe(catchError(this.listProjectService.handleError)).subscribe((res) => {
        abp.notify.success("edited branch successfully");
        this.dialogRef.close();
      }, () => this.isDisable = false);
    }
  }

  tableHeaderType = [
    {id: 0,value: 'ODC'},
    {id: 1,value: 'T&M'},
    {id: 2,value: 'FIXPRICE'},
    {id: 3,value: 'PRODUCT'}
  ]

}

