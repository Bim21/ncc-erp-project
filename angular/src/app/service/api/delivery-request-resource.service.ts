import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError, Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';
@Injectable({
  providedIn: 'root'
})
export class DeliveryResourceRequestService extends BaseApiService {
  changeUrl() {
    return 'ResourceRequest'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  public getResourceRequestDetail(id: any): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/ResourceRequestDetail?resourceRequestId=' + id);
    }
  public searchAvailableUserForRequest(item:any):Observable<any>{
    return this.http.get<any>(this.rootUrl+'/SearchAvailableUserForRequest?startDate='+item)
  }
  public AddUserToRequest(item:any):Observable<any>{
    return this.http.post<any>(this.rootUrl + '/AddUserToRequest',item);
  }
  public delete(id: any): Observable<any> {
    return this.http.delete<any>(this.rootUrl + '/Delete', {
        params: new HttpParams().set('resourceRequestId', id)
    })
}
  
  
   
  
  

}