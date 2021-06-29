import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';
import { CreateEditTimesheetDetailComponent } from './create-edit-timesheet-detail/create-edit-timesheet-detail.component';
import { ActivatedRoute } from '@angular/router';
import { TimesheetDetailDto, ProjectTimesheetDto,UploadFileDto } from './../../../service/model/timesheet.dto';
import { CreateEditTimesheetComponent } from './../create-edit-timesheet/create-edit-timesheet.component';
import { ProjectDto } from '@app/service/model/list-project.dto';
import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { InputFilterDto } from '@shared/filter/filter.component';
import {TimesheetService} from '@app/service/api/timesheet.service'
import { catchError, finalize } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { ImportFileTimesheetDetailComponent } from './import-file-timesheet-detail/import-file-timesheet-detail.component';
@Component({
  selector: 'app-timesheet-detail',
  templateUrl: './timesheet-detail.component.html',
  styleUrls: ['./timesheet-detail.component.css']
})
export class TimesheetDetailComponent extends PagedListingComponentBase<ProjectDto> implements OnInit {
 
  public TimesheetDetaiList:TimesheetDetailDto[] = []
  public requestId:any;
  public projectTimesheetDetailId:any;

  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: "Name", comparisions: [0, 6, 7, 8] },
  ];
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    // throw new Error('Method not implemented.');
    request.sort = this.transDate;
    request.sortDirection = this.sortDrirect;
    this.timesheetService.GetTimesheetDetail(this.timesheetId).subscribe(data => {
      this.TimesheetDetaiList= data.result;
      this.projectTimesheetDetailId=data.result.map(el=>{return el.projectId})
      console.log(this.projectTimesheetDetailId)
      this.showPaging(data.result, pageNumber);
    })
  }
  protected delete (item: ProjectDto): void {
    // throw new Error('Method not implemented.');
    abp.message.confirm(
      "Delete TimeSheet " + item.name + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.timesheetProjectService.delete(item.id).pipe(catchError(this.timesheetService.handleError)).subscribe(() => {
            abp.notify.success("Deleted Project Timesheet " + item.name);
            this.refresh()
          });
        }
      }
    );
    
  }
 

  constructor(private timesheetService :TimesheetService,
    private timesheetProjectService: TimesheetProjectService,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    injector:Injector
    ) {
    super(injector)
    this.timesheetId = this.route.snapshot.queryParamMap.get('id');

   }
   ngOnInit(): void {
    this.refresh();
    
  }
   showDialog(command: String, Timesheet:any): void {
    let timesheetDetail = {} as ProjectTimesheetDto;
    // let timesheetDetailId={} as TimesheetDetailDto;
    // let uploadFile= {} as UploadFileDto;
    if (command == "edit") {
     timesheetDetail={
      projectId: timesheetDetail.projectId,
      timesheetId: timesheetDetail.timesheetId,
      note:timesheetDetail.note
      // file:timesheetDetail.file,
     }
      
    }
    const show=this.dialog.open(CreateEditTimesheetDetailComponent, {
      data: {
        item: timesheetDetail,
        command: command,
        // timesheetDetailId:timesheetDetailId.id,
        projectTimesheetDetailId: this.projectTimesheetDetailId,
     
      },
      width: "700px",
      disableClose: true,
    });
    show.afterClosed().subscribe(result => {
      // if (result === 'refresh') {
      //   this.ngOnInit();
      // }
      this.refresh();
    });
    
    
    

  }
  createTimeSheet() {
    this.showDialog('create', {})
  }
  editTimesheet(id:any) {
    //ProjectDto
    this.showDialog("edit", id);
  }
  
  importExcel(id:any) {
    const dialog = this.dialog.open(ImportFileTimesheetDetailComponent, { data:{id:id,width: '500px'}
    });
    dialog.afterClosed().subscribe(result => {
      this.refresh();
    });
  }
  




}
