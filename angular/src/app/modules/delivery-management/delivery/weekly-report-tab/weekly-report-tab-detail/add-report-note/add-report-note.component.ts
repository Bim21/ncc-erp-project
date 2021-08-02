import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PmReportService } from '@app/service/api/pm-report.service';
import { AppComponentBase } from '@shared/app-component-base';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-add-report-note',
  templateUrl: './add-report-note.component.html',
  styleUrls: ['./add-report-note.component.css']
})
export class AddReportNoteComponent extends AppComponentBase implements OnInit {
  PMReportNote: string = ""
  reportId:any
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, injector: Injector,
    public dialogRef: MatDialogRef<AddReportNoteComponent>, private reportService: PmReportService) {
    super(injector)
  }

  ngOnInit(): void {
    console.log(this.data.reportId)
    this.reportId = this.data.reportId
  }
  saveAndClose() {
    this.isLoading = true
    this.reportService.updateReportNote(this.reportId, this.PMReportNote).pipe(catchError(this.reportService.handleError))
      .subscribe(data => { abp.notify.success("Updated note"), this.isLoading = false, this.dialogRef.close() },
      () => { this.isLoading = false })
  }
}
