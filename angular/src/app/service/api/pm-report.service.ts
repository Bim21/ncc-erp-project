import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseApiService } from './base-api.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PmReportService extends BaseApiService {
  changeUrl() {
    return 'PmReport';
  }

  constructor(http: HttpClient) { super(http) }
  public closeReport(id: any): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/CloseReport?pmReportId=' + id);
  }
  public getPmReport(projectId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/GetAll?projectId=' + projectId);
  }
  public getStatisticsReport(pmReportId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/StatisticsReport?pmReportId=' + pmReportId);
  }


}
