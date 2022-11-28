import { ApiResponse } from './../model/api-response.dto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class AppConfigurationService extends BaseApiService {

  changeUrl() {
    return 'Configuration';
  }
 
  constructor( http: HttpClient) { 
    super(http)
  }
  getConfiguration():Observable<any>{
    return this.http.get(this.rootUrl + '/Get')
  }
  GetGoogleClientAppId():Observable<any>{
    return this.http.get(this.rootUrl + '/GetGoogleClientAppId')
  }
  editConfiguration(item:any):Observable<any>{
    return this.http.post(this.rootUrl + '/Change', item)
  }

  updateProjectSettingConfig(item: any):Observable<any>{
    return this.http.post(this.rootUrl + '/ChangeProjectSetting', item)
  }

  /**
   * @param timeCountDown: seconds
   */
  setTimeCountDown(timeCountDown: number): Observable<ApiResponse<{timeCountDown: number}>> {
    return this.http.post<any>(this.rootUrl + '/SetTimeCountDown', {timeCountDown});
  }

  getTimeCountDown():Observable<ApiResponse<{timeCountDown: number}>>{
    return this.http.get<any>(this.rootUrl + '/GetTimeCountDown') 
  }

  checkConnectToHRM():Observable<any>{
    return this.http.get(this.rootUrl + '/CheckConnectToHRM');
  }

  checkConnectToTimesheet():Observable<any>{
    return this.http.get(this.rootUrl + '/CheckConnectToTimesheet');
  }

  checkConnectToFinfast():Observable<any>{
    return this.http.get(this.rootUrl + '/CheckConnectToFinfast');
  }

  checkConnectToTalent():Observable<any>{
    return this.http.get(this.rootUrl + '/CheckConnectToTalent');
  }
}
