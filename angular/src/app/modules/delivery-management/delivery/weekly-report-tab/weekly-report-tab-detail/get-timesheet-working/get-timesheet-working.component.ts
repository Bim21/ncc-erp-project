import { DatePipe } from '@angular/common';
import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PMReportProjectService } from '@app/service/api/pmreport-project.service';
import { DialogDataDto } from '@app/service/model/common-DTO';
import { AppComponentBase } from '@shared/app-component-base';
import * as moment from 'moment';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-get-timesheet-working',
  templateUrl: './get-timesheet-working.component.html',
  styleUrls: ['./get-timesheet-working.component.css'],
  providers: [DatePipe]
})
export class GetTimesheetWorkingComponent extends AppComponentBase implements OnInit {
  timesheetWorking = {} as TimesheetWorkingDto
  constructor(injector: Injector, @Inject(MAT_DIALOG_DATA) public data: DialogDataDto, private datePipe: DatePipe,
    public dialogRef: MatDialogRef<GetTimesheetWorkingComponent>,
    private pmReportProjectService: PMReportProjectService) {
    super(injector)
  }

  ngOnInit(): void {
    this.timesheetWorking.pmReportProjectId = this.data.dialogData
    this.timesheetWorking.timeSheetStartTime = new Date()
    this.timesheetWorking.timeSheetStartTime = this.datePipe.transform(new Date(this.timesheetWorking.timeSheetStartTime.getTime() - 7 * 24 * 60 * 60 * 1000)
      .toLocaleString(), "yyyy-MM-dd")

  }
  SaveAndClose() {
    this.timesheetWorking.timeSheetStartTime = moment(this.timesheetWorking.timeSheetStartTime).format("YYYY-MM-DD")
    if (this.timesheetWorking.timeSheetEndTime) {
      this.timesheetWorking.timeSheetEndTime = moment(this.timesheetWorking.timeSheetEndTime).format("YYYY-MM-DD")
    }

    this.isLoading = true
    this.pmReportProjectService.GetTimesheetWorking(this.timesheetWorking.pmReportProjectId, this.timesheetWorking.timeSheetStartTime, this.timesheetWorking.timeSheetEndTime)
      .pipe(catchError(this.pmReportProjectService.handleError)).subscribe(rs => {
        this.dialogRef.close(this.timesheetWorking)
        this.isLoading = false
        abp.notify.success(`Get timesheet from ${this.timesheetWorking.timeSheetStartTime} to ${this.timesheetWorking.timeSheetEndTime}`)
      },
        () => {
          this.isLoading = false
        })
  }
}
export class TimesheetWorkingDto {
  pmReportProjectId: number
  timeSheetStartTime: any
  timeSheetEndTime: any
}
