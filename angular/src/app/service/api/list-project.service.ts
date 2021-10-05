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


}
