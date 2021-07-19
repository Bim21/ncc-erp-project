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
    this.timesheetSerivce.getAllByProject(this.projectId).pipe(catchError(this.timesheetSerivce.handleError)).subscribe(data=>{
      this.listTimesheetByProject =data.result;
    })
  }
  
  // public addTimesheet(){
  //   let newTimesheet ={} as ProjectTimesheetDto
  //   newTimesheet.createMode = true;
  //    this.listTimesheetByProject.push(newTimesheet)
  // }

//  public importTimeSheet(id: any) {
//     const dialog = this.dialog.open(ImportFileTimesheetDetailComponent, {
//       data: { id: id, width: '500px' }
//     });
//     dialog.afterClosed().subscribe(result => {
//       this.getAllTimesheet();
//     });
//   }

}
