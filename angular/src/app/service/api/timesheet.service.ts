import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';


@Injectable({
  providedIn: 'root'
})
export class TimesheetService extends BaseApiService{
  changeUrl() {
    return 'Timesheet';
  }

  constructor(http:HttpClient) { 
    super(http)
  }
}
