import { ActivatedRoute } from '@angular/router';
import { TimesheetDto } from './../../service/model/timesheet.dto';
import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { InputFilterDto } from '@shared/filter/filter.component';
import {TimesheetService} from '@app/service/api/timesheet.service'
import { catchError, finalize } from 'rxjs/operators';
import { CreateEditTimesheetComponent } from './create-edit-timesheet/create-edit-timesheet.component';
import { MatDialog } from '@angular/material/dialog';
@Component({
  selector: 'app-timesheet',
  templateUrl: './timesheet.component.html',
  styleUrls: ['./timesheet.component.css']
})
export class TimesheetComponent extends PagedListingComponentBase<TimesheetDto> implements OnInit {
  public timesheetList:TimesheetDto[] = [];

  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: "Name", comparisions: [0, 6, 7, 8] },
  ];
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    // throw new Error('Method not implemented.');
    request.sort = this.transDate;
    request.sortDirection = this.sortDrirect;
    this.timesheetService.getAllPaging(request).pipe(finalize(() => {
      finishedCallback();
    })).subscribe(data => {

      this.timesheetList = data.result.items;
      this.showPaging(data.result, pageNumber);

    })
  }
  protected delete (item: TimesheetDto): void {
    // throw new Error('Method not implemented.');
    abp.message.confirm(
      "Delete TimeSheet " + item.name + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.timesheetService.delete(item.id).pipe(catchError(this.timesheetService.handleError)).subscribe(() => {
            abp.notify.success("Deleted TimeSheet " + item.name);
            this.refresh()
          });
        }
      }
    );
    
  }
 

  constructor(private timesheetService :TimesheetService,
    private dialog: MatDialog,
    injector:Injector,
    private route: ActivatedRoute
    ) {
    super(injector)
   }
   ngOnInit(): void {
    this.refresh()
    this.requestId = this.route.snapshot.queryParamMap.get("id")
    
  }
   showDialog(command: String, Timesheet:any): void {
    let timesheet = {} as TimesheetDto
    if (command == "edit") {
      timesheet = {
        name :  Timesheet.name,
        month : Timesheet.month,
        year : Timesheet.year,
        status : Timesheet.status,
        id: Timesheet.id
      }
    }

    this.dialog.open(CreateEditTimesheetComponent, {
      data: {
        item: timesheet,
        command: command,
      },
      width: "700px",
      disableClose: true,
    });

  }
  createTimeSheet() {
    this.showDialog('create', {})
  }
  editTimesheet(timesheet: TimesheetDto) {
    this.showDialog("edit", timesheet);
  }

  
  showDetail(id:any){
      this.router.navigate(['app/timesheetDetail'], {
        queryParams: {
          id: id,
        }
      })
    }
  
  


}
