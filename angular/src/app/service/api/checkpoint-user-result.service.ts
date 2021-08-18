import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseApiService } from './base-api.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CheckpointUserResultService extends BaseApiService{
  changeUrl() {
    return 'CheckpointUserResult'
  }

  constructor(http:HttpClient) {
    super(http);
  }
  public getAllUserResult(id:any):Observable<any>{
    return this.http.get<any>(this.rootUrl+ 'CheckpointUserResult?phaseId='+id);
  }
}
