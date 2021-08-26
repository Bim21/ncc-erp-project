import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseApiService } from './base-api.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CheckpointUserDetailService extends BaseApiService{
  changeUrl() {
    return 'CheckpointUserDetail';
  }

  constructor(http:HttpClient) {
    super(http);
  }
  showHistory(id:any):Observable<any>{
    return this.http.get<any>(this.rootUrl+'ShowHistory?userId=',id)
  }
  
}
