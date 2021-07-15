import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class ClientService extends BaseApiService{

  changeUrl() {
    return 'Client'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  
  public deleteClient(id: any): Observable<any> {
    return this.http.delete<any>(this.rootUrl + '/Delete', {
        params: new HttpParams().set('clientId', id)
    })
}
}
