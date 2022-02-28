import { PagedRequestDto } from './../../../shared/paged-listing-component-base';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError, Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';
@Injectable({
  providedIn: 'root',
})
export class DeliveryResourceRequestService extends BaseApiService {
  changeUrl() {
    return 'ResourceRequest';
  }
  constructor(http: HttpClient) {
    super(http);
  }
  public getResourceRequestDetail(id: any): Observable<any> {
    return this.http.get<any>(
      this.rootUrl + '/ResourceRequestDetail?resourceRequestId=' + id
    );
  }
  public searchAvailableUserForRequest(
    item: any,
    request: PagedRequestDto
  ): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + '/SearchAvailableUserForRequest?startDate=' + item,
      request
    );
  }
  public AddUserToRequest(item: any): Observable<any> {
    return this.http.post<any>(this.rootUrl + '/AddUserToRequest', item);
  }
  public delete(id: any): Observable<any> {
    return this.http.delete<any>(this.rootUrl + '/Delete', {
      params: new HttpParams().set('resourceRequestId', id),
    });
  }
  public getAvailableResource(
    request: PagedRequestDto,
    skillId?: any
  ): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + '/AvailableResource?skillId=' + skillId,
      request
    );
  }
  public planUser(item: any): Observable<any> {
    return this.http.post<any>(this.rootUrl + '/PlanUser', item);
  }
  public availableResourceFuture(request: PagedRequestDto): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + '/AvailableResourceFuture',
      request
    );
  }
  public createSkill(skill): Observable<any> {
    return this.http.post<any>(this.rootUrl + '/CreateSkill', skill);
  }
  public deleteSkill(resourceRequestSkillId: any): Observable<any> {
    return this.http.delete<any>(
      this.rootUrl +
      `/DeleteSkill?resourceRequestSkillId=${resourceRequestSkillId}`
    );
  }

  public GetSkillDetail(id: any): Observable<any> {
    return this.http.get<any>(
      this.rootUrl + '/GetSkillDetail?resourceRequestId=' + id
    );
  }
  public getResourcePaging(
    request: PagedRequestDto,
    option: string
  ): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + `/GetAllPaging?order=${option}`,
      request
    );
  }


  public GetVendorResource(
    request: PagedRequestDto, skillId?: any
  ): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + '/GetVendorResource?skillId=' + skillId,
      request
    );
  }

  public GetAllPoolResource(
    request: PagedRequestDto, skillId?: any
  ): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + '/GetAllPoolResource?skillId=' + skillId,
      request
    );
  }
  public GetAllResource(
    request: PagedRequestDto, skillId?: any
  ): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + '/GetAllResource?skillId=' + skillId,
      request
    );
  }

  public CancelResourcePlan(
    id: number
  ): Observable<any> {
    return this.http.delete<any>(
      this.rootUrl + '/CancelResourcePlan?projectUserId=' + id,

    );
  }

  public updatePoolNote(
    poolNote: any
  ): Observable<any> {
    return this.http.put<any>(
      this.rootUrl + '/UpdateUserPoolNote', poolNote

    );
  }

}
