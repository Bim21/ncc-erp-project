import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class PMReportProjectService extends BaseApiService {


  changeUrl() {
    return 'PMReportProject'
  }
  constructor(http: HttpClient) {
    super(http);
  }
  public getChangesDuringWeek(projectId: number, pmReportId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + `/GetchangedResourceByPmReport?projectId=${projectId}&pmReportId=${pmReportId}`);
  }
  public getChangesInFuture(projectId: number, pmReportId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + `/GetPlannedResourceByPmReport?projectId=${projectId}&pmReportId=${pmReportId}`);
  }
  public GetAllPmReportProjectForDropDown(): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/GetAllPmReportProjectForDropDown');
  }
  public GetAllByPmReport(pmReportId: number,projectType: any): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/GetAllByPmReport?pmReportId=' + pmReportId+'&projectType='+ projectType);
  }
  public sendReport(projectId: number, pmReportId: number): Observable<any> {
    return this.http.post<any>(this.rootUrl + `/SendReport?projectId=${projectId}&pmReportId=${pmReportId}`, {});
  }
  public problemsOfTheWeekForReport(projectId: number, pmReportId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + `/ProblemsOfTheWeekForReport?projectId=${projectId}&pmReportId=${pmReportId}`);
  }
  public updateHealth(pmReportProjectId: number, projectHealth: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + '/UpdateHealth?pmReportProjectId=' + pmReportProjectId + '&projectHealth=' + projectHealth)
  }
  public reverseDelete(pmReportProjectId: number, { }): Observable<any> {
    return this.http.post<any>(this.rootUrl + `/ReverseSeen?pmReportProjectId=${pmReportProjectId}`, {});
  }
  public GetAllByProject(projectId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + `/GetAllByProject?projectId=${projectId}`);
  }
  public updateNote(note: string, pmReportProjectId: number): Observable<any> {
    // if (note) {
    //   note = JSON.stringify(note)
    // }
    let requestBody ={
      note:  note,
      id: pmReportProjectId
    }
    return this.http.put<any>(this.rootUrl + `/UpdateNote`, requestBody);
  }
  public GetInfoProject(pmReportProjectId: number): Observable<any> {
    return this.http.get<any>(this.rootUrl + `/GetInfoProject?pmReportProjectId=${pmReportProjectId}`);

  }
  public GetCurrentResourceOfProject(projectId:number){
    return this.http.get<any>(this.rootUrl + `/GetCurrentResourceOfProject?projectId=${projectId}`);

  }

  public GetTimesheetWorking(pmReportProjectId: number,startTime:any,endTime:any): Observable<any> {
    return this.http.post<any>(this.rootUrl + `/GetWorkingTimeFromTimesheet?pmReportProjectId=${pmReportProjectId}&startTime=${startTime}&endTime=${endTime}`, {});
  }

  
  public CancelResourcePlan(projectUserId: number) {
    return this.http.delete(this.rootUrl + `/CancelResourcePlan?projectUserId=${projectUserId}`)
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
}
