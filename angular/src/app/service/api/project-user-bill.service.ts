import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProjectUserBillService extends BaseApiService {
  changeUrl() {
    return 'ProjectUserBill'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  getAllUserBill(id:number):Observable<any>{
    return this.http.get<any>(this.rootUrl + `/GetAllByProject?projectId=${id}`);
  }
}
