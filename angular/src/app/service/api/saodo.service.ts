import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class SaodoService extends BaseApiService {

  changeUrl() {
    return 'AuditSession'
  }
  constructor(http: HttpClient) {
    super(http);
  }

}
