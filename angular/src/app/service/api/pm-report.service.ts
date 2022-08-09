import { pmReportProjectHealthDto } from './../model/pmReport.dto';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseApiService } from './base-api.service';
import { Injectable, Input } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { APP_ENUMS } from '@shared/AppEnums';

@Injectable({
  providedIn: 'root'
})
export class PmReportService extends BaseApiService {
  changeUrl() {
    return 'PmReport';
  }

  
  data = {} as pmReportProjectHealthDto;
  public projectHealth = new BehaviorSubject(this.data);
  currentProjectHealth = this.projectHealth.asObservable();

  public projectType = new BehaviorSubject('OUTSOURCING');
  currentProjectType = this.projectType.asObservable();

  public projectStatus = new BehaviorSubject('ALL');
  currentProjectStatus = this.projectStatus.asObservable();

  public sortHealth = new BehaviorSubject('No_Order');
  currentSortData = this.sortHealth.asObservable();

  constructor(http: HttpClient) { super(http) }
  public closeReport(id: any): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/CloseReport?pmReportId=' + id);
  }
  public getPmReport(projectId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/GetAll?projectId=' + projectId);
  }
  public getStatisticsReport(pmReportId: number, reportDate: any): Observable<any> {
    return this.http.get<any>(this.rootUrl + `/StatisticsReport?pmReportId=${pmReportId}&startDate=${reportDate}`);
  }
  public updateReportNote(pmReportId: number, note: string): Observable<any> {
    // if (note) {
    //   note = JSON.stringify(note)
    // }
    let requestBody = {
      note: note,
      id: pmReportId
    }
    return this.http.put<any>(this.rootUrl + `/UpdateNote`, requestBody);
  }
  changeProjectHealth(data) {
    this.projectHealth.next(data);
  }
  collectTimesheet(pmReportId: number, startTime: string, endTime: string): Observable<any> {
    return this.http.get(this.rootUrl + `/CollectTimesheet?pmReportId=${pmReportId}&startTime=${startTime}&endTime=${endTime}`)
  }
  changeProjectType(message: string) {
    this.projectType.next(message);
  }

  changeProjectSelectionHealth(message: string){
    this.projectStatus.next(message);
  }

  changeSelectSortHealth(message: string){
    this.sortHealth.next(message)
  }
}
