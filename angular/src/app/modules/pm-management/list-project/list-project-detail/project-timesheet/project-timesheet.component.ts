import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { ImportFileTimesheetDetailComponent } from './../../../../timesheet/timesheet-detail/import-file-timesheet-detail/import-file-timesheet-detail.component';
import { MatDialog } from '@angular/material/dialog';
import { catchError } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { ProjectTimesheetDto } from './../../../../../service/model/timesheet.dto';
import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, OnInit, Injector, inject } from '@angular/core';

@Component({
  selector: 'app-project-timesheet',
  templateUrl: './project-timesheet.component.html',
  styleUrls: ['./project-timesheet.component.css']
})
export class ProjectTimesheetComponent extends AppComponentBase implements OnInit {
  
  Timesheet_TimesheetProject = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject;
  Timesheet_TimesheetProject_Create= PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_Create;
  Timesheet_TimesheetProject_CreateInvoice = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_CreateInvoice;
  Timesheet_TimesheetProject_Delete = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_Delete;
  Timesheet_TimesheetProject_DownloadFileTimesheetProject =PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_DownloadFileTimesheetProject;
  Timesheet_TimesheetProject_GetAllByProject = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_GetAllByProject;
  Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet;
  Timesheet_TimesheetProject_Update = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_Update;
  Timesheet_TimesheetProject_UploadFileTimesheetProject = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_UploadFileTimesheetProject;
  Timesheet_TimesheetProject_ViewInvoice = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_ViewInvoice;
  public listTimesheetByProject: ProjectTimesheetDto[] = [];
  private projectId:number;
  constructor(injector:Injector, private timesheetSerivce:TimesheetProjectService, private route:ActivatedRoute, private dialog:MatDialog) {
    super(injector);
    this.projectId = Number(route.snapshot.queryParamMap.get("id"));
   }

  ngOnInit(): void {
    this.getAllTimesheet();
  }
  private getAllTimesheet(){
    if(this.permission.isGranted(this.Timesheet_TimesheetProject)){
      this.timesheetSerivce.getAllByProject(this.projectId).pipe(catchError(this.timesheetSerivce.handleError)).subscribe(data=>{
        this.listTimesheetByProject =data.result;
      })
    }
  }


}
