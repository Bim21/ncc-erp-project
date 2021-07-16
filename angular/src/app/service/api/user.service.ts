import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseApiService{

  changeUrl() {
    return 'User'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  public GetAllUserActive(onlyStaff:boolean):Observable<any>{
    return this.http.get<any>(this.rootUrl + '/GetAllUserActive?onlyStaff='+onlyStaff);

  }
}
