

import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-edit-timesheet-project-dialog',
  templateUrl: './edit-timesheet-project-dialog.component.html',
  styleUrls: ['./edit-timesheet-project-dialog.component.css']
})
export class EditTimesheetProjectDialogComponent implements OnInit {

  id: number;
  invoiceNumber: number;
  workingDay: number;
  transferFee: number;
  discount: number;
  projectName: string;
  saving = false;
  @Output() onSave = new EventEmitter<null>();

  subscription: Subscription[] = [];
  constructor(public bsModalRef: BsModalRef, private timesheetProjectService:TimesheetProjectService) {}

  ngOnInit(): void {
   
  }

  SaveAndClose() {
    if(+this.invoiceNumber <= 0) {
      abp.message.error("Invoice Number must be bigger than 0!");
      return;
    }
    if(+this.workingDay <= 0) {
      abp.message.error("Working Day must be bigger than 0!");
      return;
    }
    if(+this.transferFee < 0) {
      abp.message.error("Transfer Fee must be bigger than or equal to 0!");
      return;
    }
    if(+this.discount < 0) {
      abp.message.error("Discount must be bigger than or equal to 0!");
      return;
    }
    let requestBody = {
      id : this.id,
      invoiceNumber: this.invoiceNumber,
      workingDay: this.workingDay,
      transferFee: this.transferFee,
      discount: this.discount,
    }
    this.saving = true;
    this.subscription.push(
      this.timesheetProjectService
        .updateTimesheetProject(requestBody)
        .pipe(
          finalize(() => {
            this.saving = false;
          })
        )
        .subscribe(() => {
          this.bsModalRef.hide();
          this.onSave.emit();
          abp.notify.success("Updated Invoice Info")
        })
    );
  }

  ngOnDestroy() {
    this.subscription.forEach((sub) => {
      sub.unsubscribe();
    });
  }

}
