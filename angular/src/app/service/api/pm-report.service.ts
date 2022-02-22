import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseApiService } from './base-api.service';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PmReportService extends BaseApiService {
  changeUrl() {
    return 'PmReport';
  }
  public messageSource = new BehaviorSubject('OUTSOURCING');
  currentMessage = this.messageSource.asObservable();

  constructor(http: HttpClient) { super(http) }
  public closeReport(id: any): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/CloseReport?pmReportId=' + id);
  }
  public getPmReport(projectId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/GetAll?projectId=' + projectId);
  }
  public getStatisticsReport(pmReportId: number,reportDate:any): Observable<any> {
    return this.http.get<any>(this.rootUrl + `/StatisticsReport?pmReportId=${pmReportId}&startDate=${reportDate}` );
  }
  public updateReportNote(pmReportId: number,note:string): Observable<any> {
    // if (note) {
    //   note = JSON.stringify(note)
    // }
  let requestBody ={
    note:  note,
    id: pmReportId
  }
    return this.http.put<any>(this.rootUrl + `/UpdateNote`,requestBody);
  }
  changeMessage(message: string) {
    this.messageSource.next(message);
  }
  collectTimesheet(pmReportId:number, startTime:string, endTime:string):Observable<any>{
    return this.http.get(this.rootUrl + `/CollectTimesheet?pmReportId=${pmReportId}&startTime=${startTime}&endTime=${endTime}`)
  }


}
