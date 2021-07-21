import { AppConsts } from './../../../shared/AppConsts';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class TimesheetProjectService extends BaseApiService{
  changeUrl() {
    return 'TimesheetProject';
  }
  constructor(http:HttpClient) { 
    super(http)
  }
  public create(item: any): Observable<any> {
    return this.http.post<any>(this.rootUrl + '/Create', item);
  }
  public update(item: any): Observable<any> {
    return this.http.put<any>(this.rootUrl + '/Update', item);
  }
  public delete(id: any): Observable<any> {
    return this.http.delete<any>(this.rootUrl + '/Delete', {
        params: new HttpParams().set('timesheetProjectId', id)
    })
  }
  
  public UpdateFileTimeSheetProject(file, id): Observable<any> {
    const formData = new FormData();
    if (navigator.msSaveBlob) {
        formData.append('File', file);
    } else {
        formData.append('File', file);
    }
    formData.append('TimesheetProjectId', id);
    const uploadReq = new HttpRequest(
        'POST', AppConsts.remoteServiceBaseUrl + '/api/services/app/TimesheetProject/UpdateFileTimeSheetProject', formData,
        {
            reportProgress: true
        }
    );
    return this.http.request(uploadReq);
  }
  public GetTimesheetDetail(id: any): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/GetAllProjectTimesheetByTimesheet?timesheetId=' + id);
  }
  public getAllByProject(projectId:number){
    return this.http.get<any>(this.rootUrl + '/GetAllByProject?projectId=' + projectId);

  }
  public DownloadFileTimesheetProject(projectId:number){
    return this.http.get<any>(this.rootUrl + '/DownloadFileTimesheetProject?timesheetProjectId=' + projectId);

  }
  public GetTimesheetFile(id: any): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/DownloadFileTimesheetProject?timesheetProjectId=' + id);
  }
}
