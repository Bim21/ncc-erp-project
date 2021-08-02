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
  searchWeekResource: string = "";
  searchFutureResource: string = "";
  problemCurrentPage: number = 1;
  weeklyCurrentPage: number = 1;
  futureCurrentPage: number = 1;
  itemPerPage: number = 20;
  tempReportInfo = {} as PmReportInfoDto
  public reportInfo = {} as PmReportInfoDto
  constructor(private reportService: PmReportService, public dialogRef: MatDialogRef<ReportInfoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, injector: Injector) {
    super(injector)
  }

  ngOnInit(): void {
    this.getReportInfo();
  }
  getReportInfo() {
    this.isLoading = true;
    this.reportService.getStatisticsReport(this.data.report.id).pipe(catchError(this.reportService.handleError))
      .subscribe(data => {
        this.reportInfo = data.result
        this.tempReportInfo.resourceInTheWeek = data.result.resourceInTheWeek
        this.tempReportInfo.resourceInTheFuture =data.result.resourceInTheFuture
        this.isLoading = false;
      })
  }
  searchWeeklyUser() {
    this.reportInfo.resourceInTheWeek = this.tempReportInfo.resourceInTheWeek.filter(user => {
      return user.email.toLowerCase().includes(this.searchWeekResource.toLowerCase())
        || user.fullName.toLowerCase().includes(this.searchWeekResource.toLowerCase())
    })
  }
  searchFutureUser() {
    this.reportInfo.resourceInTheFuture = this.tempReportInfo.resourceInTheFuture.filter(user => {
      return user.email.toLowerCase().includes(this.searchFutureResource.toLowerCase())
        || user.fullName.toLowerCase().includes(this.searchFutureResource.toLowerCase())
    })
  }

}
