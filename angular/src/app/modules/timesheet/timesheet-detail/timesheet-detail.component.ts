import { async } from '@angular/core/testing';
import { result } from 'lodash-es';
import { MatMenuTrigger } from '@angular/material/menu';
import { ViewBillComponent } from './view-bill/view-bill.component';
import { PagedResultResultDto } from './../../../../shared/paged-listing-component-base';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { AppComponentBase } from '@shared/app-component-base';
import { BaseApiService } from '@app/service/api/base-api.service';
import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';
import { CreateEditTimesheetDetailComponent } from './create-edit-timesheet-detail/create-edit-timesheet-detail.component';
import { ActivatedRoute, Router } from '@angular/router';
import { TimesheetDetailDto, ProjectTimesheetDto, UploadFileDto } from './../../../service/model/timesheet.dto';
import { Component, OnInit, Injector, ViewChild, ViewChildren, QueryList, ElementRef, ChangeDetectorRef } from '@angular/core';
import { InputFilterDto } from '@shared/filter/filter.component';
import { TimesheetService } from '@app/service/api/timesheet.service'
import { catchError, filter } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { ImportFileTimesheetDetailComponent } from './import-file-timesheet-detail/import-file-timesheet-detail.component';
import * as FileSaver from 'file-saver';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { CreateInvoiceComponent } from './create-invoice/create-invoice.component';
import { UserService } from '@app/service/api/user.service';
import { ClientService } from '@app/service/api/client.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EditTimesheetProjectDialogComponent } from './edit-timesheet-project-dialog/edit-timesheet-project-dialog/edit-timesheet-project-dialog.component';
@Component({
  selector: 'app-timesheet-detail',
  templateUrl: './timesheet-detail.component.html',
  styleUrls: ['./timesheet-detail.component.css']
})
export class TimesheetDetailComponent extends PagedListingComponentBase<TimesheetDetailDto> implements OnInit {
  requestBody: PagedRequestDto
  pageNum: number
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.requestBody = request
    this.pageNum = pageNumber
    let objFilter = [
      {name: 'pmId', isTrue: false, value: this.pmId},
      {name: 'clientId', isTrue: false, value: this.clientId},
    ];

    objFilter.forEach((item) => {
      if(!item.isTrue){
        request.filterItems = this.AddFilterItem(request, item.name, item.value)
      }
      if(item.value == -1){
        request.filterItems = this.clearFilter(request, item.name, "")
        item.isTrue = true
      }
    })
    this.timesheetProjectService.GetTimesheetDetail(this.timesheetId, request).pipe(catchError(this.timesheetProjectService.handleError))
      .subscribe((data: PagedResultResultDto) => {
        this.TimesheetDetaiList = data.result.items.map(el => {
          el.projectBillInfomation.map(item => {
            return {...item, isShow: false}
          })
          return el;
        });
        this.showPaging(data.result, pageNumber);
        this.projectTimesheetDetailId = data.result.items.map(el => { return el.projectId })
        objFilter.forEach((item) => {
          if(!item.isTrue){
            request.filterItems = this.clearFilter(request, item.name, '')
          }
        })
      })
  }
  protected delete(item: TimesheetDetailDto): void {
    this.menu.closeMenu();
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

  @ViewChildren('checkboxExportInvoice') private elementRefCheckbox : QueryList<any>;
  public TimesheetDetaiList: TimesheetDetailDto[] = [];
  public tempTimesheetDetaiList: TimesheetDetailDto[] = [];
  public requestId: any;
  public projectTimesheetDetailId: any;
  public searchText: string = "";
  public timesheetId: any;
  public isActive: boolean;
  public createdInvoice: boolean;
  public listExportInvoice: any[] = [];
  public listExportInvoiceChargeType: any[] = [];
  public isMonthlyToDaily = false;
  public clientId: number = -1;
  public isShowButtonAction: boolean;
  public pmId = -1;
  public pmList: any[] = [];
  public searchPM: string = "";
  public clientList: any[]= []
  public searchClient: string = ""
  public chargeType = ['d','m','h']
  public titleTimesheet: string = ''
  public meId: number;
  public updateAction = UpdateAction;
  public currency: string = "";

  @ViewChild(MatMenuTrigger)
  menu: MatMenuTrigger
  contextMenuPosition = { x: '0', y: '0' }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'pmFullName', displayName: "PM Name", comparisions: [0, 6, 7, 8] },
    { propertyName: 'projectName', displayName: "Project Name", comparisions: [0, 6, 7, 8] },
    { propertyName: 'hasFile', displayName: "Has file", comparisions: [0], filterType: 2 },
    { propertyName: 'isComplete', displayName: "Status", comparisions: [0], filterType: 5 },
    { propertyName: 'clientName', displayName: "Client Name", comparisions: [0, 6, 7, 8] },

  ];

  Timesheets_TimesheetDetail_View = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_View;
  Timesheets_TimesheetDetail_ViewBillRate = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_ViewBillRate;
  Timesheets_TimesheetDetail_AddProjectToTimesheet = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_AddProjectToTimesheet;
  Timesheets_TimesheetDetail_UploadTimesheetFile = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_UploadTimesheetFile;
  Timesheets_TimesheetDetail_ExportInvoice = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_ExportInvoice;
  Timesheets_TimesheetDetail_UpdateNote = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_UpdateNote;
  Timesheets_TimesheetDetail_Delete = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_Delete;
  Timesheets_TimesheetDetail_ViewAll = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_ViewAll;
  Timesheets_TimesheetDetail_ViewMyProjectOnly = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_ViewMyProjectOnly;
  Timesheets_TimesheetDetail_UpdateTimsheet = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_UpdateTimsheet;
  Timesheets_TimesheetDetail_UpdateBill = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_UpdateBill;
  Timesheets_TimesheetDetail_ExportInvoiceForTax = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_ExportInvoiceForTax;
  Timesheets_TimesheetDetail_EditInvoiceNumberWorkingDay = PERMISSIONS_CONSTANT.Timesheets_TimesheetDetail_EditInvoiceNumberWorkingDay;
  constructor(
    private timesheetService: TimesheetService,
    public timesheetProjectService: TimesheetProjectService,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    injector: Injector,
    private ref: ChangeDetectorRef,
    private userService: UserService,
    private clientService: ClientService,
    private _modalService: BsModalService,
  ) {
    super(injector)


  }
  ngOnInit(): void {
    this.meId = Number(this.appSession.userId);
    if(!this.permission.isGranted(this.Timesheets_TimesheetDetail_ViewAll)){
      this.pmId = this.meId
    }
    this.timesheetId = this.route.snapshot.queryParamMap.get('id');
    this.titleTimesheet = this.route.snapshot.queryParamMap.get('name')
    this.isActive = this.route.snapshot.queryParamMap.get('isActive') == 'true' ? true : false;
    this.createdInvoice = this.route.snapshot.queryParamMap.get('createdInvoice') == 'true' ? true : false;
    this.getAllPM();
    this.getAllClient()
    this.refresh();
    //this.showButtonAction();
  }
  ngAfterContentChecked() {
    this.ref.detectChanges();
}
  ngAfterViewInit(){
    this.elementRefCheckbox.changes.subscribe(c => {
      c.toArray().forEach(element => {
        if(this.listExportInvoice.includes(element.value.projectId)){
          element._checked = true
        }
        else{
          element._checked = false
        }
      });
    })
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
        projectBillInfomation: Timesheet.projectBillInfomation,
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
        this.refresh()
      }
    })
  }
 

  public isEnablePMFilter(){
    return this.permission.isGranted(this.Timesheets_TimesheetDetail_ViewAll)
  }
  public searchProjectTS(){
    if (this.isEnablePMFilter() && this.searchText != ""){
      this.pmId = -1;
    }
    this.refresh();    
  }


  // reloadTimesheetFile(id) {
  //   this.timesheetProjectService.GetTimesheetDetail(this.timesheetId, this.requestBody).pipe(catchError(this.timesheetProjectService.handleError))
  //     .subscribe((data: PagedResultResultDto) => {
  //       this.TimesheetDetaiList = data.result.items;
  //       if (!this.TimesheetDetaiList.filter(timesheet => timesheet.id == id)[0].file) {
  //         setTimeout(() => {
  //           this.reloadTimesheetFile(id)
  //         }, 1000)
  //       }
  //       else {
  //         this.showPaging(data.result, this.pageNum);
  //         this.projectTimesheetDetailId = data.result.items.map(el => { return el.projectId })
  //         abp.notify.success("import file successfull")
  //       }
  //     })
  // }

  createTimeSheet() {
    this.showDialog('create', {})
  }
  editTimesheet(timesheet: TimesheetDetailDto) {
    this.menu.closeMenu();
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
        // this.reloadTimesheetFile(result)
        this.refresh()
      }
    });
  }
  DeleteFile(item: any) {
    abp.message.confirm(
      "Delete File " + item.file + "?",
      "",
      async (result: boolean) => {
        if (result) {
          this.timesheetProjectService.UpdateFileTimeSheetProject(null, item.id).subscribe(
            (res) => {
              abp.notify.success("Deleted File Success");
              item.file = null
              item.hasFile = false
            },
            (err) => {
              abp.notify.error(err)
            },
          );
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
        title: "Create Invoice"
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
        title: "Export Invoice"
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
  showActions(e) {
    e.preventDefault();
    this.contextMenuPosition.x = e.clientX + 'px';
    this.contextMenuPosition.y = e.clientY + 'px';
    this.menu.openMenu();
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
  public viewBillDetail(bill,action) {
    this.menu.closeMenu()
    const show = this.dialog.open(ViewBillComponent, {
      width: "95%",
      data: {
        billInfo:bill,
        action: action
      }, 
     
    })
    show.afterClosed().subscribe((res) => {
      this.refresh();
    })
  }
  mouseEnter(item) {
    item.showIcon = true

  }
  mouseLeave(item) {
    item.showIcon = false
  }

  exportInvocie(item: any,exportInvoiceMode) {
    this.timesheetProjectService.exportInvoice(this.timesheetId, item.projectId,exportInvoiceMode).pipe(catchError(this.timesheetProjectService.handleError)).subscribe(data => {
      const file = new Blob([this.s2ab(atob(data.result.base64))], {
        type: "application/vnd.ms-excel;charset=utf-8"
      });
      FileSaver.saveAs(file, data.result.fileName);
    })
  }
  exportQuickInvoiceForTax(item: any) {
    let invoiceExcelDto = {
      timesheetId: this.timesheetId,
      projectIds: [item.projectId]
    }
    this.timesheetProjectService.exportInvoiceForTax(invoiceExcelDto).pipe(catchError(this.timesheetProjectService.handleError)).subscribe(data => {
      const file = new Blob([this.s2ab(atob(data.result.base64))], {
        type: "application/vnd.ms-excel;charset=utf-8"
      });
      FileSaver.saveAs(file, data.result.fileName);
    })
  }

  addProjectToExport(event) {
    if (!event.checked) {
      let index = this.listExportInvoice.indexOf(event.source.value.projectId);
      let indexChargeType = this.listExportInvoiceChargeType.indexOf(event.source.value.chargeType);
      if (index > -1){
        this.listExportInvoice.splice(index, 1);
      }
      if (indexChargeType > -1){
        this.listExportInvoiceChargeType.splice(indexChargeType, 1);
      }
    }
    else {
      let checkClientId = event.source.value.clientId;
      let checkCurrency = event.source.value.currency;
      if (this.listExportInvoice.length > 0 && this.clientId != checkClientId) {
        abp.notify.warn("Cannot export invoices for different clients!")
        event.checked = false;
        event.source._checked = false
        return;
      }
      if (this.listExportInvoice.length > 0 && this.currency != checkCurrency) {
        abp.notify.warn("Cannot export invoices for different currencies!")
        event.checked = false;
        event.source._checked = false
        return;
      }
      this.clientId = checkClientId;
      this.currency = checkCurrency;
      this.listExportInvoice.push(event.source.value.projectId);
      this.listExportInvoiceChargeType.push(event.source.value.chargeType);
    }
    if(this.listExportInvoiceChargeType.indexOf(this.APP_ENUM.ChargeType.Monthly) !== -1){
      this.isMonthlyToDaily = true;
    }
    else{
      this.isMonthlyToDaily = false;
    }
  }
  exportInvoiceClient(exportInvoiceMode) {
    let invoiceExcelDto = {
      timesheetId: this.timesheetId,
      projectIds: this.listExportInvoice,
      mode: exportInvoiceMode
    }
    this.timesheetProjectService.exportInvoiceClient(invoiceExcelDto).subscribe((res) => {
      const file = new Blob([this.s2ab(atob(res.result.base64))], {
        type: "application/vnd.ms-excel;charset=utf-8"
      });
      this.refresh();
      this.listExportInvoice=[];
      FileSaver.saveAs(file, res.result.fileName);
    })
  }

  exportInvoiceForTax() {
    let invoiceExcelDto = {
      timesheetId: this.timesheetId,
      projectIds: this.listExportInvoice
    }
    this.timesheetProjectService.exportInvoiceForTax(invoiceExcelDto).subscribe((res) => {
      const file = new Blob([this.s2ab(atob(res.result.base64))], {
        type: "application/vnd.ms-excel;charset=utf-8"
      });
      this.refresh();
      this.listExportInvoice=[];
      FileSaver.saveAs(file, res.result.fileName);
    })
  }

  public getAllPM(): void {
    this.timesheetProjectService.getAllPM().pipe(catchError(this.userService.handleError))
      .subscribe(data => {
        this.pmList = data.result;
      })
  }
  public getAllClient(): void{
    this.clientService.getAllClient()
    .subscribe((res) =>{
      this.clientList = res.result
    })
  }
  requiredFile(item){
    if(item.requireTimesheetFile && !item.hasFile)
      return 'bd-red';
    return ''
  }
  getChargeType(index){
    if(index != null && index >= 0)
      return '/'+this.chargeType[index]
    return ''
  }
  covertMoneyType(money){
    return (
      money.toLocaleString("vi", {
        currency: "VND",
      })
    );
  }

  updateTimesheetProject(id, invoiceNumber,workingDay,projectName) {
    let editTimesheetProjectDialog: BsModalRef;
    editTimesheetProjectDialog = this._modalService.show(EditTimesheetProjectDialogComponent, {
      class: 'modal',
      initialState: {
        id: id,
        invoiceNumber: invoiceNumber,
        workingDay: workingDay,
        projectName: projectName
      },
     
    });

    editTimesheetProjectDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

}
export const UpdateAction = {
  UpdateBillInfo: 1,
  UpdateTimesheet: 2
}