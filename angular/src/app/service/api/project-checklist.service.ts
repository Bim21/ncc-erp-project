import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class ProjectChecklistService extends BaseApiService{

  changeUrl() {
    return 'ProjectChecklist'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  public GetCheckListItemByProject(projectId:any,auditSessionId?:any): Observable<any> {
    if(auditSessionId){
      return this.http.get<any>(this.rootUrl + '/GetCheckListItemByProject?projectId='+projectId+'&auditSessionId='+auditSessionId);
    }
    else{
      return this.http.get<any>(this.rootUrl + '/GetCheckListItemByProject?projectId='+projectId);
    }
   
  }
}