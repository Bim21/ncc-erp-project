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

@Component({
  selector: 'app-weekly-report-tab',
  templateUrl: './weekly-report-tab.component.html',
  styleUrls: ['./weekly-report-tab.component.css']
})
export class WeeklyReportTabComponent extends PagedListingComponentBase<WeeklyReportTabComponent> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.pmReportService.getAllPaging(request).pipe(finalize(()=>{
      finishedCallback();
    }),catchError(this.pmReportService.handleError)).subscribe((data)=>{
      this.pmReportList=data.result.items;
      this.showPaging(data.result,pageNumber);
    })
  }
  protected delete(entity: WeeklyReportTabComponent): void {
    throw new Error('Method not implemented.');
  }

  public pmReportList:pmReportDto[]=[];
  public closeReportMessage;
  DeliveryManagement_PMReport_CloseReport=PERMISSIONS_CONSTANT.DeliveryManagement_PMReport_CloseReport;


  constructor(public router:Router,
    private pmReportService:PmReportService,
    public injetor:Injector) { super(injetor)}
    

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
    this.pmReportService.closeReport(report.id).subscribe((res)=>{
      if(res){
        abp.notify.success(res.result);
      }
      this.refresh();
    })
  }


}
