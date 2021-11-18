import { PagedRequestDto } from './../../../shared/paged-listing-component-base';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError, Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';
@Injectable({
  providedIn: 'root'
})
export class ListProjectService extends BaseApiService {
  changeUrl() {
    return 'Project'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  public getProjectById(id: any): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/Get?projectId=' + id);
}
  public delete(id: any): Observable<any> {
    return this.http.delete<any>(this.rootUrl + '/Delete', {
        params: new HttpParams().set('projectID', id)
    })
}
public getProjectDetail(id: any): Observable<any> {
  return this.http.get<any>(this.rootUrl + '/GetProjectDetail?projectId=' + id);
}
public UpdateProjectDetail(requestBody): Observable<any> {
  return this.http.put<any>(this.rootUrl + '/UpdateProjectDetail',requestBody);
}
public GetAllTrainingPaging(request: PagedRequestDto): Observable<any> {
  return this.http.post<any>(this.rootUrl + '/GetAllTraningPaging', request);
}
public GetAllProductPaging(request: PagedRequestDto): Observable<any> {
  return this.http.post<any>(this.rootUrl + '/GetAllProductProjectPaging', request);
}
public GetDetailProductProject(id: any): Observable<any> {
  return this.http.get<any>(this.rootUrl + '/GetDetailProductProject?projectId=' + id);
}
}
