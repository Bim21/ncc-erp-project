import { Component, Inject, OnInit } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';

import { Router } from '@angular/router';
import { TimesheetDto } from '@app/service/model/timesheet.dto';
import { TimesheetService } from '@app/service/api/timesheet.service';
import { catchError } from 'rxjs/operators';

// tslint:disable-next-line:no-duplicate-imports






@Component({
  selector: 'app-create-edit-timesheet',
  templateUrl: './create-edit-timesheet.component.html',
  styleUrls: ['./create-edit-timesheet.component.css']
})

export class CreateEditTimesheetComponent implements OnInit {
  public timesheet = {} as TimesheetDto;
  public isDisable = false;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  public dialogRef: MatDialogRef<CreateEditTimesheetComponent>,
  private timesheetService :TimesheetService,
  private router: Router) { }

  ngOnInit(): void {
    if (this.data.command == "edit") {
      this.timesheet = this.data.item;
  

    }
  }
  SaveAndClose(){
    this.isDisable = true
    if (this.data.command == "create") {
      // this.timesheet.value = 0
      this.timesheetService.create(this.timesheet).pipe(catchError(this.timesheetService.handleError)).subscribe((res) => {
        abp.notify.success("created outcomeRequest ");
        this.reloadComponent()
        this.dialogRef.close();
      }, () => this.isDisable = false);
      // 
    }
    else {
      this.timesheetService.update(this.timesheet).pipe(catchError(this.timesheetService.handleError)).subscribe((res) => {
        abp.notify.success("edited outcomeRequest ");
        this.reloadComponent()
        this.dialogRef.close();
      }, () => this.isDisable = false);
    }
  }
  reloadComponent() {
    this.router.navigateByUrl('', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/app/timesheet']);
    });
  }
  public Months =
  [
   1,2,3,4,5,6,7,8,9,10,11,12
  ]
  public Years =
  [
    2016, 2017, 2018 , 2019 , 2020 , 2021 , 2022
  ]

}

