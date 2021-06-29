import { AppComponentBase } from '@shared/app-component-base';
import { BaseApiService } from '@app/service/api/base-api.service';
import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';
import { CreateEditTimesheetDetailComponent } from './create-edit-timesheet-detail/create-edit-timesheet-detail.component';
import { ActivatedRoute, Router } from '@angular/router';
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
export class TimesheetDetailComponent extends AppComponentBase implements OnInit {
 
  public TimesheetDetaiList:TimesheetDetailDto[] = [];
  public tempTimesheetDetaiList:TimesheetDetailDto[] = [];

  public requestId:any;
  public projectTimesheetDetailId:any;
  public searchText: string = "";
  public timesheetId:any;

  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: "Name", comparisions: [0, 6, 7, 8] },
  ];
  protected getAllTimesheetDetail(): void {
    this.timesheetService.GetTimesheetDetail(this.timesheetId).subscribe(data => {
      this.TimesheetDetaiList= data.result;
      this.tempTimesheetDetaiList= data.result;
      this.projectTimesheetDetailId=data.result.map(el=>{return el.projectId})
      console.log(this.projectTimesheetDetailId)
    })
  }
  protected delete (item: TimesheetDetailDto): void {
    abp.message.confirm(
      "Delete TimeSheet " + item.projectName + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.timesheetProjectService.delete(item.id).pipe(catchError(this.timesheetService.handleError)).subscribe(() => {
            abp.notify.success("Deleted Project Timesheet " + item.projectName);
            this.getAllTimesheetDetail();
          });
        }
      }
    );
    
  }
 

  constructor(private timesheetService :TimesheetService,
    public timesheetProjectService: TimesheetProjectService,
    private router: Router,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    injector:Injector
    ) {
    super(injector)
    

   }
   ngOnInit(): void {
    this.timesheetId = this.route.snapshot.queryParamMap.get('id');
    this.getAllTimesheetDetail();
    
  }
   showDialog(command: String, Timesheet:any): void {
    let timesheetDetail = {} as ProjectTimesheetDto;
    if (command == "edit") {
     timesheetDetail={
      projectId: Timesheet.projectId,
      timesheetId: Timesheet.timesheetId,
      note:Timesheet.note,
      id:Timesheet.id
     }
      
    }
    const show=this.dialog.open(CreateEditTimesheetDetailComponent, {
      data: {
        item: timesheetDetail,
        command: command,
        projectTimesheetDetailId: this.projectTimesheetDetailId,
     
      },
      width: "700px",
      disableClose: true,
    });
    show.afterClosed().subscribe(result => {
      console.log("aaaaaaaaaaaaaa",result)
      if(result){
        this.getAllTimesheetDetail();
      }
    });
    
    
    

  }
  createTimeSheet() {
    this.showDialog('create', {})
  }
  editTimesheet(timesheet: TimesheetDetailDto) {
    //ProjectDto
    this.showDialog("edit", timesheet);
  }
  
  showDialogUpdateFile(command:string){
    
  }
  importExcel(id:any) {
    const dialog = this.dialog.open(ImportFileTimesheetDetailComponent, { data:{id:id,width: '500px'}
    });
    dialog.afterClosed().subscribe(result => {
      this.getAllTimesheetDetail();
    });
  }
  DeleteFile(item:any){
    abp.message.confirm(
      "Delete File " + item.file + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.timesheetProjectService.UpdateFileTimeSheetProject(null,item.id).pipe(catchError(this.timesheetService.handleError)).subscribe(() => {
            abp.notify.success("Deleted File  " + item.file);
            this.getAllTimesheetDetail();
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
 
  




}
