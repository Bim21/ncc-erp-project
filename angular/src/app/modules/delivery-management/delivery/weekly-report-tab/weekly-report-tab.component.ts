import { Permission } from './../../../../../shared/service-proxies/service-proxies';
import { projectReportDto, projectProblemDto } from './../../../../service/model/projectReport.dto';
import { result } from 'lodash-es';
import { finalize, catchError } from 'rxjs/operators';
import { PmReportService } from './../../../../service/api/pm-report.service';
import { pmReportDto } from './../../../../service/model/pmReport.dto';
import { Router } from '@angular/router';
import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { MatDialog } from '@angular/material/dialog';
import { EditReportComponent } from './edit-report/edit-report.component';
import { ReportInfoComponent } from './report-info/report-info.component';

@Component({
  selector: 'app-weekly-report-tab',
  templateUrl: './weekly-report-tab.component.html',
  styleUrls: ['./weekly-report-tab.component.css']
})
export class WeeklyReportTabComponent extends PagedListingComponentBase<WeeklyReportTabComponent> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.isLoading = true
    this.pmReportService.getAllPaging(request).pipe(finalize(()=>{
      finishedCallback();
    }),catchError(this.pmReportService.handleError)).subscribe((data)=>{
      this.pmReportList=data.result.items;
      this.showPaging(data.result,pageNumber);
      this.isLoading =false;
      this.pmReportList.forEach(item=>{
          item.note = JSON.parse(item.note)
      })
      console.log(this.pmReportList)
     
    })
  }
  protected delete(entity: WeeklyReportTabComponent): void {
    throw new Error('Method not implemented.');
  }

  public pmReportList:pmReportDto[]=[];
  public closeReportMessage;
  DeliveryManagement_PMReport_CloseReport=PERMISSIONS_CONSTANT.DeliveryManagement_PMReport_CloseReport;


  constructor(public router:Router,
    private pmReportService:PmReportService, private dialog:MatDialog,
    public injector:Injector) { super(injector)}
    

  ngOnInit(): void {
    this.refresh();
   
  }
  showDetail(item:any){
    this.router.navigate(['app/weeklyReportTabDetail'], {
      queryParams: {
        id:item.id,
        isActive:item.isActive
        
      }
    })
  }
  
  closeReport(report:any){
    abp.message.confirm(
      "Close this report : " + report.name + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.pmReportService.closeReport(report.id).subscribe((res)=>{
            if(res){
              abp.notify.success(res.result);
            }
            this.refresh();
          })
        }
      }
    );
  }
  editReport(pmReport:any){
   let dialogData = {} as any
     dialogData = {
       id: pmReport.id,
       name: pmReport.name,
       isActive: pmReport.isActive,
       year: pmReport.year,
       type: pmReport.type,
       pmReportStatus: pmReport.pmReportStatus,
       note: pmReport.note
     }
   
   const dialogRef = this.dialog.open(EditReportComponent, {
     width: '700px',
     disableClose: true,
     data: {
       dialogData: dialogData,
     },
   });
   dialogRef.afterClosed().subscribe(result => {
     if (result) {
       this.refresh()
     }
   });
  }
  viewReportInfo(report){
    let dialogRef = this.dialog.open(ReportInfoComponent,{
      width:'1000px',
      height: "98vh",
      disableClose: true,
      data: {
        report: report
      }
    })
  }
  // isJson(item) {
  //   item = typeof item !== "string"
  //     ? JSON.stringify(item)
  //     : item;

  //   try {
  //     item = JSON.parse(item);
  //   } catch (e) {
  //     return false;
  //   }

  //   if (typeof item === "object" && item !== null) {
  //     return true;
  //   }

  //   return false;
  // }
}
