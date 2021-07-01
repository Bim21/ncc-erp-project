import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProjectResourceRequestService extends BaseApiService {

  changeUrl() {
    return 'ResourceRequest'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  getAllResourceRequest(id:number):Observable<any>{
    return this.http.get<any>(this.rootUrl + `/GetAllByProject?projectId=${id}`);
  }
}
