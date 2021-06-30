import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedRequestDto } from '@shared/paged-listing-component-base';
import { Observable } from 'rxjs';
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
  public delete(id: any): Observable<any> {
    return this.http.delete<any>(this.rootUrl + '/Delete', {
        params: new HttpParams().set('timesheetId', id)
    })
}
public GetTimesheetDetail(id: any): Observable<any> {
  return this.http.get<any>(this.rootUrl + '/GetTimesheetDetail?timesheetId=' + id);
}

}
