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
  deleteUserBill(id: number): Observable<any> {
    return this.http.delete<any>(this.rootUrl + `/Delete?projectUserBillId=${id}`)
  }
  getRate(id: number):Observable<any>{
    return this.http.get<any>(this.rootUrl + `/GetRate?projectId=${id}`);
  }
  getLastInvoiceNumber(id:number):Observable<any>{
    return this.http.get<any>(this.rootUrl + `/GetLastInvoiceNumber?projectId=${id}`);
  }
  updateLastInvoiceNumber(item: any): Observable<any> {
    return this.http.put<any>(this.rootUrl + '/UpdateLastInvoiceNumber', item);
  }
  getDiscount(id:number):Observable<any>{
    return this.http.get<any>(this.rootUrl + `/GetDiscount?projectId=${id}`);
  }
  updateDiscount(item: any): Observable<any> {
    return this.http.put<any>(this.rootUrl + '/UpdateDiscount', item);
  }
  updateNote(item: any): Observable<any> {
    return this.http.put<any>(this.rootUrl + '/UpdateNote', item);
  }
}
