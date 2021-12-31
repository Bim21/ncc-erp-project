import { IUser } from '@app/service/model/user.inteface';
import { PagedRequestDto } from './../../../shared/paged-listing-component-base';
import { Observable } from 'rxjs';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { AppConsts } from '@shared/AppConsts';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseApiService {
  changeUrl() {
    return 'User';
  }
  constructor(http: HttpClient) {
    super(http);
  }
  public GetAllUserActive(onlyStaff: boolean, isFake?: any): Observable<any> {
    if (isFake) {
      return this.http.get<any>(
        this.rootUrl +
          `/GetAllUserActive?onlyStaff=${onlyStaff}&isFake=${isFake}`
      );
    } else {
      return this.http.get<any>(
        this.rootUrl + `/GetAllUserActive?onlyStaff=${onlyStaff}`
      );
    }
  }
  public uploadImageFile(file, id): Observable<any> {
    const formData = new FormData();
    if (navigator.msSaveBlob) {
      formData.append('file', file, 'image.jpg');
    } else {
      formData.append('file', file);
    }
    formData.append('userId', id);
    // const uploadReq = new HttpRequest(
    //     'POST', AppConsts.remoteServiceBaseUrl + '/api/services/app/User/UpdateAvatar', formData,
    //     {
    //         reportProgress: true
    //     }
    // );
    // return this.http.request(uploadReq);
    return this.http.post<any>(this.rootUrl + '/UpdateAvatar', formData);
  }

  public upLoadOwnAvatar(file): Observable<any> {
    const formData = new FormData();
    if (navigator.msSaveBlob) {
      formData.append('file', file, 'image.jpg');
    } else {
      formData.append('file', file);
    }
    const uploadReq = new HttpRequest(
      'POST',
      AppConsts.remoteServiceBaseUrl +
        '/api/services/app/User/UpdateYourOwnAvatar',
      formData,
      {
        reportProgress: true,
      }
    );
    return this.http.request(uploadReq);
  }
  uploadFile(file: File): Observable<any> {
    const formData = new FormData();
    const url = '/api/services/app/User/ImportUsersFromFile';
    formData.append('file', file);
    const uploadReq = new HttpRequest(
      'POST',
      AppConsts.remoteServiceBaseUrl + url,
      formData,
      {
        reportProgress: true,
      }
    );
    return this.http.request(uploadReq);
  }
  autoUpdateUserFromHRM(): Observable<any> {
    return this.http.post<any>(this.rootUrl + '/AutoUpdateUserFromHRM', {});
  }
  getUserPaging(request: PagedRequestDto, skillId: any): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + `/GetAllPaging?skillId=${skillId}`,
      request
    );
  }

  updatePoolNote(user: IUser) {
    return this.http.put<IUser>(this.rootUrl + '/Update', user);
  }
}
