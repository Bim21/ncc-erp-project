import { TimesheetService } from './../../../../../service/api/timesheet.service';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { TimesheetProjectService } from './../../../../../service/api/timesheet-project.service';
import { ProjectTimesheetDto } from './../../../../../service/model/timesheet.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { catchError } from 'rxjs/operators';
import { ImportFileTimesheetDetailComponent } from './../../../../timesheet/timesheet-detail/import-file-timesheet-detail/import-file-timesheet-detail.component';
import { Component, OnInit, Injector } from '@angular/core';
import * as FileSaver from 'file-saver';

@Component({
  selector: 'app-training-project-timesheet',
  templateUrl: './training-project-timesheet.component.html',
  styleUrls: ['./training-project-timesheet.component.css']
})
export class TrainingProjectTimesheetComponent extends AppComponentBase implements OnInit {

  public listTimesheetByProject: ProjectTimesheetDto[] = [];
  private projectId: number;
  constructor(injector: Injector,
    public timesheetProjectService: TimesheetProjectService,
    private timesheetService: TimesheetService,
    private timesheetSerivce: TimesheetProjectService, private route: ActivatedRoute, private dialog: MatDialog) {
    super(injector);
    this.projectId = Number(route.snapshot.queryParamMap.get("id"));
  }

  ngOnInit(): void {
    this.getAllTimesheet();
  }
  private getAllTimesheet() {
    this.timesheetSerivce.getAllByProject(this.projectId).pipe(catchError(this.timesheetSerivce.handleError)).subscribe(data => {
      this.listTimesheetByProject = data.result;
    })
  }
  importExcel(id: any) {
    const dialog = this.dialog.open(ImportFileTimesheetDetailComponent, {
      data: { id: id, width: '500px' }
    });
    dialog.afterClosed().subscribe(result => {
      this.getAllTimesheet();
    });
  }
  importFile(id: number) {
    this.timesheetProjectService.DownloadFileTimesheetProject(id).subscribe(data => {
    })
  }
  DeleteFile(item: any) {
    abp.message.confirm(
      "Delete File " + item.timesheetFile + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.timesheetProjectService.UpdateFileTimeSheetProject(null, item.id).pipe(catchError(this.timesheetService.handleError)).subscribe(() => {
            abp.notify.success("Deleted File  " + item.timesheetFile);
            this.getAllTimesheet();
          });
        }
      }
    );

  }

  downloadFile(projectTimesheet: any) {
    this.timesheetProjectService.GetTimesheetFile(projectTimesheet.id).subscribe(data => {
      const file = new Blob([this.s2ab(atob(data.result.data))], {
        type: "application/vnd.ms-excel;charset=utf-8"
      });
      FileSaver.saveAs(file, data.result.fileName);
    })

  }
  s2ab(s) {
    var buf = new ArrayBuffer(s.length);
    var view = new Uint8Array(buf);
    for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
    return buf;
  }
}

