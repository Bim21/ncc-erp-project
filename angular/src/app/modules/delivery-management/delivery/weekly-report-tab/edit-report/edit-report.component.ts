import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PmReportService } from '@app/service/api/pm-report.service';
import { DialogDataDto } from '@app/service/model/common-DTO';
import { pmReportDto } from '@app/service/model/pmReport.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-edit-report',
  templateUrl: './edit-report.component.html',
  styleUrls: ['./edit-report.component.css']
})
export class EditReportComponent extends AppComponentBase implements OnInit {
  report = {} as pmReportDto
  reportName: string = ""
  constructor(injector: Injector, public dialogRef: MatDialogRef<EditReportComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogDataDto, private reportService: PmReportService) {
    super(injector)
  }

  ngOnInit(): void {
    this.report = this.data.dialogData
    this.reportName = this.report.name
  }
  saveAndClose() {
    this.isLoading = true
    this.reportService.update(this.report).pipe(catchError(this.reportService.handleError))
      .subscribe(rs => {
        abp.notify.success("Updated report: " + this.report.name)
        this.dialogRef.close(this.report)
      },
        () => this.isLoading = false)

  }
}
