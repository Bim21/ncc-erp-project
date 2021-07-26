import { Observable } from 'rxjs';
import { PagedRequestDto } from './../../../shared/paged-listing-component-base';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseApiService } from './base-api.service';
import { Injectable, Injector } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class  CriteriaService extends BaseApiService{
  changeUrl() {
    return "Criteria"
  }

  constructor(http:HttpClient) { 
    super(http)
  }
  public getAllCriteria(request: PagedRequestDto): Observable<any>{
    return this.http.post<any>(this.rootUrl+"/GetAll",request);
  }
  public deleteCriteria(criteriaId:any): Observable<any>{
    return this.http.delete<any>(this.rootUrl + '/Delete', {
      params: new HttpParams().set('criteriaId',criteriaId)
  })
  }
}
