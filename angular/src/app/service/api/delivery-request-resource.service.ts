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
    return this.http.post<any>(this.rootUrl + '/PlanEmployeeJoinOrOutProject', item);
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
      this.rootUrl + `/GetAllPaging`,
      request
    );
  }


  public GetVendorResource(
    request: PagedRequestDto
  ): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + '/GetVendorResource',
      request
    );
  }

  public GetAllPoolResource(
    request: any, skillId?: any
  ): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + '/GetAllPoolResource',
      request
    );
  }
  public GetAllResource(
    request: PagedRequestDto
  ): Observable<any> {
    return this.http.post<any>(
      this.rootUrl + '/GetAllResource',
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

  public async getPlanResource(projecUserId,id){
    return await this.http.get<any>(this.rootUrl + '/GetResourceRequestPlan?projectUserId='+ projecUserId +'&resourceRequestId=' + id).toPromise()
  }

  public getPlanResourceUser(): Observable<any>{
    return this.http.post<any>(this.rootUrl + '/GetResourceRequestPlanUser', '')
  }

  public createPlanUser(data: any): Observable<any>{
    return this.http.post<any>(this.rootUrl + '/CreateResourceRequestPlan', data)
  }

  public updatePlanUser(data: any): Observable<any>{
    return this.http.post<any>(this.rootUrl + '/UpdateResourceRequestPlan', data)
  }

  public deletePlanUser(id: number): Observable<any>{
    return this.http.delete<any>(this.rootUrl + '/DeleteResourceRequestPlan?Id=' + id)
  }

  public cancelResourceRequest(id: number): Observable<any>{
    return this.http.post<any>(this.rootUrl + '/Cancel?requestId=' + id, {});
  }

  public getSkills(): Observable<any>{
    return this.http.get<any>(this.baseUrl + '/api/services/app/Skill/GetAll');
  }

  public getLevels(): Observable<any>{
    return this.http.get<any>(this.rootUrl + '/GetRequestLevels');
  }

  public getPriorities(): Observable<any>{
    return this.http.get<any>(this.rootUrl + '/GetPriorities');
  }

  public getStatuses(): Observable<any>{
    return this.http.get<any>(this.rootUrl + '/GetStatuses');
  }

  public getResourceRequestById(id: number): Observable<any>{
    return this.http.get<any>(this.rootUrl + '/GetById?requestId=' +id)
  }
  public updateNotePM(data: any): Observable<any>{
    return this.http.post<any>(this.rootUrl + '/UpdatePMNote', data)
  }
  public updateNoteHPM(data: any): Observable<any>{
    return this.http.post<any>(this.rootUrl + '/UpdateHPMNote', data)
  }
  public setDoneRequest(data: any): Observable<any>{
    return this.http.post<any>(this.rootUrl + '/SetDone', data)
  }
  public getAllResourceRequestByProject(projectId: number, status: number): Observable<any>{
    return this.http.get<any>(this.rootUrl + '/GetAllByProject?projectId=' + projectId + '&status=' + status)
  }

  public ConfirmOutProject(input: any) {
    return this.http.post(this.rootUrl + `/ConfirmOutProject`, input)

  }
  public ConfirmJoinProject(projectUserId: number, startTime: any) {
    return this.http.get(this.rootUrl + `/ConfirmJoinProject?projectUserId=${projectUserId}&startTime=${startTime}`)
  }


  public EditProjectUserPlan( input:any) {
    return this.http.post(this.rootUrl + `/EditProjectUserPlan`,input)
  }

  public PlanNewResourceToProject( input:any) {
    return this.http.post(this.rootUrl + `/PlanEmployeeJoinProject`,input)
  }

  public AddUserToTempProject( input:any) {
    return this.http.post(this.rootUrl + `/AddUserFromPoolToTempProject`,input)
  }

  public deleteMyRequest(id): Observable<any>{
    return this.http.delete<any>(this.rootUrl + '/DeleteMyRequest?resourceRequestId='+id)
  }

}
