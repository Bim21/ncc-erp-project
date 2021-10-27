import { ViewBillComponent } from './view-bill/view-bill.component';
import { PagedResultResultDto } from './../../../../shared/paged-listing-component-base';
import { result } from 'lodash-es';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { AppComponentBase } from '@shared/app-component-base';
import { BaseApiService } from '@app/service/api/base-api.service';
import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';
import { CreateEditTimesheetDetailComponent } from './create-edit-timesheet-detail/create-edit-timesheet-detail.component';
import { ActivatedRoute, Router } from '@angular/router';
import { TimesheetDetailDto, ProjectTimesheetDto, UploadFileDto } from './../../../service/model/timesheet.dto';
import { Component, OnInit, Injector } from '@angular/core';
import { InputFilterDto } from '@shared/filter/filter.component';
import { TimesheetService } from '@app/service/api/timesheet.service'
import { catchError } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { ImportFileTimesheetDetailComponent } from './import-file-timesheet-detail/import-file-timesheet-detail.component';
import * as FileSaver from 'file-saver';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { CreateInvoiceComponent } from './create-invoice/create-invoice.component';
@Component({
  selector: 'app-timesheet-detail',
  templateUrl: './timesheet-detail.component.html',
  styleUrls: ['./timesheet-detail.component.css']
})
export class TimesheetDetailComponent extends PagedListingComponentBase<TimesheetDetailDto> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.timesheetProjectService.GetTimesheetDetail(this.timesheetId, request).pipe(catchError(this.timesheetProjectService.handleError))
      .subscribe((data: PagedResultResultDto) => {
        this.TimesheetDetaiList = data.result.items;
        this.showPaging(data.result, pageNumber);
        this.projectTimesheetDetailId = data.result.items.map(el => { return el.projectId })
      })
  }
  protected delete(item: TimesheetDetailDto): void {
    abp.message.confirm(
      "Delete TimeSheet " + item.projectName + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.timesheetProjectService.delete(item.id).pipe(catchError(this.timesheetProjectService.handleError)).subscribe(() => {
            abp.notify.success("Deleted Project Timesheet " + item.projectName);
            this.refresh();
          });
        }
      }
    );
  }




  public TimesheetDetaiList: TimesheetDetailDto[] = [];
  public tempTimesheetDetaiList: TimesheetDetailDto[] = [];
  public requestId: any;
  public projectTimesheetDetailId: any;
  public searchText: string = "";
  public timesheetId: any;
  public isActive: boolean;
  public createdInvoice: boolean;
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'clientName', displayName: "Client Name", comparisions: [0, 6, 7, 8] },
    { propertyName: 'pmUserName', displayName: "PM Name", comparisions: [0, 6, 7, 8] },
    { propertyName: 'projectName', displayName: "Project Name", comparisions: [0, 6, 7, 8] },
    { propertyName: 'hasFile', displayName: "Has file", comparisions: [0], filterType:2 },

  ];


  Timesheet_TimesheetProject_Create = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_Create;
  Timesheet_TimesheetProject_Delete = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_Delete;
  Timesheet_TimesheetProject_DownloadFileTimesheetProject = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_DownloadFileTimesheetProject;
  Timesheet_TimesheetProject_Update = PERMISSIONS_CONSTANT.Timesheet_Timesheet_Update;
  Timesheet_TimesheetProject_UploadFileTimesheetProject = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_UploadFileTimesheetProject;
  Timesheet_TimesheetProject_CreateInvoice = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_CreateInvoice;
  Timesheet_TimesheetProject_ExportInvoice = PERMISSIONS_CONSTANT.Timesheet_TimesheetProject_ExportInvoice


  constructor(
    private timesheetService: TimesheetService,
    public timesheetProjectService: TimesheetProjectService,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    injector: Injector,


  ) {
    super(injector)


  }
  ngOnInit(): void {
    this.timesheetId = this.route.snapshot.queryParamMap.get('id');
    this.isActive = this.route.snapshot.queryParamMap.get('isActive') == 'true' ? true : false;
    this.createdInvoice = this.route.snapshot.queryParamMap.get('createdInvoice') == 'true' ? true : false;
    this.refresh();

  }
  showDialog(command: String, Timesheet: any): void {
    let timesheetDetail = {};
    if (command == "edit") {
      timesheetDetail = {
        projectId: Timesheet.projectId,
        timesheetId: Timesheet.timesheetId,
        clientName: Timesheet.clientName,
        projectName: Timesheet.projectName,
        note: Timesheet.note,
        id: Timesheet.id,
        projectBillInfomation: Timesheet.projectBillInfomation

      }

    }
    const show = this.dialog.open(CreateEditTimesheetDetailComponent, {
      data: {
        item: timesheetDetail,
        command: command,
        projectTimesheetDetailId: this.projectTimesheetDetailId,

      },
      width: "700px",
      disableClose: true,
    });
    show.afterClosed().subscribe(res => {
      if (res) {
        this.refresh();
      }
    })
  }
  createTimeSheet() {
    this.showDialog('create', {})
  }
  editTimesheet(timesheet: TimesheetDetailDto) {
    //ProjectDto
    this.showDialog("edit", timesheet);
  }

  showDialogUpdateFile(command: string) {

  }
  importExcel(id: any) {
    const dialogRef = this.dialog.open(ImportFileTimesheetDetailComponent, {
      data: { id: id, width: '500px' }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.refresh();
      }
    });
  }
  DeleteFile(item: any) {
    abp.message.confirm(
      "Delete File " + item.file + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.timesheetProjectService.UpdateFileTimeSheetProject(null, item.id).pipe(catchError(this.timesheetProjectService.handleError)).subscribe(() => {
            abp.notify.success("Deleted File  " + item.file);
            this.refresh();
          });
        }
      }
    );

  }
  search() {
    this.TimesheetDetaiList = this.tempTimesheetDetaiList.filter((item) => {
      return item.projectName.toLowerCase().includes(this.searchText.toLowerCase()) ||
        item.file?.toLowerCase().includes(this.searchText.toLowerCase());
    });


  }

  importFile(id: number) {
    this.timesheetProjectService.DownloadFileTimesheetProject(id).subscribe(data => {
    })
  }

  downloadFile(projectTimesheet: any) {
    this.timesheetProjectService.GetTimesheetFile(projectTimesheet.id).subscribe(data => {
      const file = new Blob([this.s2ab(atob(data.result.data))], {
        type: "application/vnd.ms-excel;charset=utf-8"
      });
      FileSaver.saveAs(file, data.result.fileName);
    })

  }
  s2ab(s) {
    var buf = new ArrayBuffer(s.length);
    var view = new Uint8Array(buf);
    for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
    return buf;
  }

  createInvoice() {
    const show = this.dialog.open(CreateInvoiceComponent, {
      data: {
        timeSheetId: this.timesheetId,
        title:"Create Invoice"
      },
      width: "700px",
      disableClose: true,
    });
    show.afterClosed().subscribe(res => {
      if (res) {
        this.reloadComponent();
        // this.refresh();
      }
    })
  }

  exportInvoice() {
    const show = this.dialog.open(CreateInvoiceComponent, {
      data: {
        timeSheetId: this.timesheetId,
        title:"Export Invoice"
      },
      width: "700px",
      disableClose: true,
    });
    show.afterClosed().subscribe(res => {
      if (res) {
        this.reloadComponent();
        // this.refresh();
      }
    })
  }
  
  public reloadComponent() {
    this.router.navigate(['app/timesheetDetail'], {
      queryParams: {
        id: this.timesheetId,
        createdInvoice: true,
        isActive: false
      }
    })
    this.isActive = false;
    this.createdInvoice = true;
  }
  public viewBillDetail(bill){
    const show= this.dialog.open(ViewBillComponent,{
      width: "95%",
      data: bill
    })
    show.afterClosed().subscribe((res)=>{
      this.refresh();
    })
  }
  mouseEnter(item){
    item.showIcon =true

  }
  mouseLeave(item){
    item.showIcon =false
  }

  exportInvocie(item:any){
    this.timesheetProjectService.exportInvoice(this.timesheetId,item.projectId).pipe(catchError(this.timesheetProjectService.handleError)).subscribe(data=>{
      const file = new Blob([this.s2ab(atob(data.result.base64))], {
        type: "application/vnd.ms-excel;charset=utf-8"
      });
      FileSaver.saveAs(file, data.result.fileName);
    })
  }


}