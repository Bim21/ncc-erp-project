import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { BaseApiService } from "./base-api.service";

import { Injectable } from "@angular/core";
import { IProjectHistoryUser, ResponseWrapper } from "../model/project.dto";

@Injectable({
  providedIn: "root",
})
export class ProjectUserService extends BaseApiService {
  changeUrl() {
    return "ProjectUser";
  }
  constructor(http: HttpClient) {
    super(http);
  }
  getAllProjectUser(id: number, viewHistory?: boolean): Observable<any> {
    return this.http.get<any>(
      this.rootUrl +
        `/GetAllByProject?projectId=${id}&viewHistory=${viewHistory}`
    );
  }
  getAllProjectUserInProject(id: number): Observable<any> {
    return this.http.get<any>(
      this.rootUrl + "/GetAllProjectUserInProject?projectId=" + id
    );
  }
  removeProjectUser(userId: number): Observable<any> {
    return this.http.delete<any>(
      this.rootUrl + `/Delete?projectUserId=${userId}`
    );
  }
  getProjectHistoryByUser(
    userId: number
  ): Observable<ResponseWrapper<IProjectHistoryUser[]>> {
    return this.http.get<ResponseWrapper<IProjectHistoryUser[]>>(
      this.rootUrl + `/GetProjectHistoryByUser?UserId=${userId}`
    );
  }
}
