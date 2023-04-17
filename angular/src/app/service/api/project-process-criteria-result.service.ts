import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';
import { InputToGetProjectProcessCriteriaResultDto } from "app/service/model/project-process-criteria-result.dto"

@Injectable({
    providedIn: 'root'
})
export class ProjectProcessCriteriaResultAppService extends BaseApiService {

    changeUrl() {
        return 'ProjectProcessCriteriaResult';
    }

    constructor(http: HttpClient) {
        super(http);
    }

    public search(projectProcessResultId: number, projectId: number, searchText: string, status?: string): Observable<any> {
        let params = new HttpParams();
        params = params.append('projectProcessResultId', projectProcessResultId.toString());
        params = params.append('projectId', projectId.toString());
        params = params.append('SearchText', searchText);
        if (status) {
            params = params.append('Status', status);
        }
        return this.http.get<any>(`${this.rootUrl}/GetTreeListProjectProcessCriteriaResults`, { params });
    }
    public getTreeListPPCR(projectProcessResultId: number, projectId: number, input?: InputToGetProjectProcessCriteriaResultDto): Observable<any> {
        const url = `${this.rootUrl}/GetTreeListProjectProcessCriteriaResults?projectProcessResultId=${projectProcessResultId}&projectId=${projectId}`;
        return this.http.get(url);
    }

    public updateProjectProcessCriteriaResult(item: any): Observable<any> {
        return this.http.put<any>(this.rootUrl + '/UpdateProjectProcessCriteriaResult', item);
    }

    public getProjectProcessCriteriaById(id: number): Observable<any> {
        return this.http.get<any>(this.rootUrl + '/GetProjectProcessCriteriaResultById?Id=' + id);
    }
}
