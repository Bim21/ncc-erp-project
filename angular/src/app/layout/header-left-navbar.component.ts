import { Component, ChangeDetectionStrategy, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, ActivationEnd, NavigationEnd, NavigationStart, Router, Event as NavigationEvent } from '@angular/router';
import { AddReportNoteComponent } from '@app/modules/delivery-management/delivery/weekly-report-tab/weekly-report-tab-detail/add-report-note/add-report-note.component';
import { PmReportService } from '@app/service/api/pm-report.service';
import { pmReportDto } from '@app/service/model/pmReport.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { LayoutStoreService } from '@shared/layout/layout-store.service';
import { catchError, filter } from 'rxjs/operators';

@Component({
  selector: 'header-left-navbar',
  templateUrl: './header-left-navbar.component.html',
  styleUrls: ['./header-left-navbar.component.css']

  // changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderLeftNavbarComponent extends AppComponentBase implements OnInit {
  sidebarExpanded: boolean;
  isShowReportBar: boolean = false;
  currentUrl: string = "";
  pmReportList: pmReportDto[] = [];
  pmReport = {} as pmReportDto
  reportId:any

  constructor(public _layoutStore: LayoutStoreService, private router: Router, injector: Injector,
    private dialog: MatDialog, private route: ActivatedRoute, private reportService: PmReportService) {
    super(injector)
  }

  ngOnInit(): void {
    this._layoutStore.sidebarExpanded.subscribe((value) => {
      this.sidebarExpanded = value;
    });
    this.currentUrl = this.router.url
    if (this.currentUrl.includes("weeklyReportTabDetail")) {
      this.reportId = this.route.snapshot.queryParamMap.get("id")
      this.isShowReportBar = true
      this.getPmReportList();
    }
    else {
      this.isShowReportBar = false;
    }
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd)
      )
      .subscribe(
        (event: NavigationEvent) => {
          this.currentUrl = this.router.url
          this.reportId = this.route.snapshot.queryParamMap.get("id")
          if (this.currentUrl.includes("weeklyReportTabDetail")) {
            this.isShowReportBar = true
            this.getPmReportList();
          }
          else {
            this.isShowReportBar = false;
          }
        }
      )
  }
  getPmReportList() {
    this.reportService.getAll().pipe(catchError(this.reportService.handleError)).subscribe(data => {
      this.pmReportList = data.result
      this.pmReport = this.pmReportList.filter(report => report.id == Number(this.reportId))[0]
    })
  }

  toggleSidebar(): void {
    this._layoutStore.setSidebarExpanded(!this.sidebarExpanded);
  }
  showDialog() {
    let reportId = this.route.snapshot.queryParamMap.get("id")
    const show = this.dialog.open(AddReportNoteComponent, {
      data: {
        reportId: reportId
      },
      width: "700px",
    })
  }
  routingReportDetail(){
    // this.router.navigate(['weeklyReportTabDetail'], {
    //   relativeTo: this.route, queryParams: {
    //     id: this.pmReport.id,
    //     isActive: this.pmReport.isActive
    //   },
    // })



    this.router.navigateByUrl('', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/app/weeklyReportTabDetail'],{
        relativeTo: this.route, queryParams: {
          id: this.pmReport.id,
          isActive: this.pmReport.isActive
        },
      });
    });
    
  }
  
}
