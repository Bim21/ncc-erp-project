import { TimesheetDto } from './../../service/model/timesheet.dto';
import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { InputFilterDto } from '@shared/filter/filter.component';

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
  }
  protected delete(entity: any): void {
    // throw new Error('Method not implemented.');
  }

  constructor(injector:Injector) {
    super(injector)
   }

  ngOnInit(): void {
  }

}
