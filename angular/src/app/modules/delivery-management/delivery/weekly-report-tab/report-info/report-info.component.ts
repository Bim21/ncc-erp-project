import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PmReportService } from '@app/service/api/pm-report.service';
import { PmReportInfoDto } from '@app/service/model/pmReport.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-report-info',
  templateUrl: './report-info.component.html',
  styleUrls: ['./report-info.component.css']
})
export class ReportInfoComponent extends AppComponentBase implements OnInit {
  searchWeekResource:string="";
  searchFutureResource:string="";
  public reportInfo = {} as PmReportInfoDto
  constructor(private reportService: PmReportService, public dialogRef: MatDialogRef<ReportInfoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, injector: Injector) {
    super(injector)
  }

  ngOnInit(): void {
    this.getReportInfo();
  }
  getReportInfo() {
    this.isLoading=true 
    this.reportService.getStatisticsReport(this.data.report.id).pipe(catchError(this.reportService.handleError))
      .subscribe(data => {
        this.reportInfo = data.result
        this.isLoading=false
      })
  }

}
