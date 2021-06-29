import { filter } from 'rxjs/operators';
import { ListProjectService } from '@app/service/api/list-project.service';
import { ProjectDto } from './../../../../service/model/list-project.dto';
import { BaseApiService } from '@app/service/api/base-api.service';
import { TimesheetDetailDto, ProjectTimesheetDto } from './../../../../service/model/timesheet.dto';
import { ImportFileTimesheetDetailComponent } from './../import-file-timesheet-detail/import-file-timesheet-detail.component';
import { TimesheetService } from '@app/service/api/timesheet.service';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import {TimesheetProjectService} from '@app/service/api/timesheet-project.service';
import * as XLSX from 'xlsx';
import { Binary } from '@angular/compiler';
import * as _ from 'lodash';
@Component({
  selector: 'app-create-edit-timesheet-detail',
  templateUrl: './create-edit-timesheet-detail.component.html',
  styleUrls: ['./create-edit-timesheet-detail.component.css']
})
export class CreateEditTimesheetDetailComponent implements OnInit {

  public isDisable = false;
  public timesheetDetail = {} as TimesheetDetailDto;
  public project= {} as ProjectDto;
  public projectList:any;
  public projectTimesheet= {} as ProjectTimesheetDto;
  selectedFiles: FileList;
  currentFileUpload: File;
  projectTimesheetId:any;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  public dialogRef: MatDialogRef<CreateEditTimesheetDetailComponent>,
  private timesheetService:TimesheetService,
  private router: Router, private dialog: MatDialog,
  private timesheetProjectService: TimesheetProjectService,
  private projectService: ListProjectService,
  private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.getAllProject();
    this.projectTimesheet.timesheetId = Number(this.route.snapshot.queryParamMap.get('id'));
    this.projectTimesheetId= this.data.projectTimesheetDetailId;
    console.log(this.data)

  }

  selectFile(event) {
    this.selectedFiles = event.target.files;
    this.currentFileUpload = this.selectedFiles.item(0);
    this.importExcel();

  }
  SaveAndClose(){
    this.isDisable = true;
    if (this.data.command == "create") {
      this.timesheetProjectService.create(this.projectTimesheet).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("created branch successfully");
        this.dialogRef.close();
      }, () => this.isDisable = false);
    }
    else {
      this.timesheetProjectService.update(this.projectTimesheet).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("edited branch successfully");
        this.dialogRef.close();
      }, () => this.isDisable = false);
    }

  }
 
  importExcel() {
    if (!this.selectedFiles) {
      abp.message.error("Choose a file!")
      return
    }
    const formData:FormData = new FormData();
    formData.append("file",this.currentFileUpload);
    // this.projectTimesheet.file = formData;
    console.log(formData)
  }

  getAllProject(){
    this.projectService.getAll().subscribe(data=>{
      this.projectList= data.result.filter(item => !this.projectTimesheetId.includes(item.id))
    })
  }
  

  

}
