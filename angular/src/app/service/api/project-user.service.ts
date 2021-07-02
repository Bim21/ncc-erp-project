import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseApiService } from './base-api.service';

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProjectUserService extends BaseApiService {
  changeUrl() {
    return 'ProjectUser'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  getAllProjectUser(id:number):Observable<any>{
    return this.http.get<any>(this.rootUrl + `/GetAllByProject?projectId=${id}`);
  }
}
