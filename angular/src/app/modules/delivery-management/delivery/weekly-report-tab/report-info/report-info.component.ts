import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PmReportService } from '@app/service/api/pm-report.service';
import { PmReportInfoDto } from '@app/service/model/pmReport.dto';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-report-info',
  templateUrl: './report-info.component.html',
  styleUrls: ['./report-info.component.css']
})
export class ReportInfoComponent implements OnInit {
  public reportInfo = {} as PmReportInfoDto
  constructor(private reportService:PmReportService, public dialogRef:MatDialogRef<ReportInfoComponent>,
     @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  }
  getReportInfo(){
    this.reportService.getStatisticsReport(this.data.reportId).pipe(catchError(this.reportService.handleError))
    .subscribe(data =>{
      this.reportInfo =data
    })
  }

}
