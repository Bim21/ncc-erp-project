import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateEditCriteriaComponent } from '@app/modules/checkpoint/category/criteria/create-edit-criteria/create-edit-criteria.component';
import { TimesheetService } from '@app/service/api/timesheet.service';
import { AppComponentBase } from '@shared/app-component-base';
import * as moment from 'moment';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-close-timesheet',
  templateUrl: './close-timesheet.component.html',
  styleUrls: ['./close-timesheet.component.css']
})
export class CloseTimesheetComponent extends AppComponentBase implements OnInit {
  submitDate='';
  startDate: any;
  minDate: Date;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private timesheetService: TimesheetService,
    public injector: Injector,
    public dialogRef: MatDialogRef<CreateEditCriteriaComponent>,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    console.log(this.data);
    this.minDate= moment().add(1, 'days').toDate();

  }

  SaveAndClose() {
    this.timesheetService.ReverseActive(this.data.id,this.submitDate).pipe(catchError(this.timesheetService.handleError)).subscribe(rs => {
      if (rs.success) {
        abp.notify.success("Active timesheet: " + this.data.name)
        this.dialogRef.close()
      }

    })
  }
  public onDateChange(): void {
    this.submitDate = moment(this.startDate.toISOString()).format('YYYY-MM-DD');
  }
}
