import { HttpClient } from '@angular/common/http';
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
}
