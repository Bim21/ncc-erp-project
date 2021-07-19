import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class PMReportProjectService extends BaseApiService {


  changeUrl() {
    return 'PMReportProject'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  public getChangesDuringWeek(projectId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/ResourceChangesDuringTheWeek?projectId=' + projectId);
  }
  public getChangesInFuture(projectId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/ResourceChangesInTheFuture?projectId=' + projectId);
  }
  public GetAllPmReportProjectForDropDown(): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/GetAllPmReportProjectForDropDown');
  }
  public GetAllByPmReport(pmReportId: number,item:any):Observable<any>{
    return this.http.post<any>(this.rootUrl +'/GetAllByPmReport?pmReportId='+pmReportId,item);
  }



}
