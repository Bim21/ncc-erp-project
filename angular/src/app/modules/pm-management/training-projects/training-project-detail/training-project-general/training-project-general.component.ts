import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { catchError } from 'rxjs/operators';
import { ListProjectService } from '@app/service/api/list-project.service';
import { UserService } from './../../../../../service/api/user.service';
import { ActivatedRoute } from '@angular/router';
import { ProjectDto } from './../../../../../service/model/list-project.dto';
import { UserDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from 'shared/app-component-base';
import { Component, OnInit, Injector } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-training-project-general',
  templateUrl: './training-project-general.component.html',
  styleUrls: ['./training-project-general.component.css']
})
export class TrainingProjectGeneralComponent extends AppComponentBase implements OnInit {
  PmManager_Project_Update = PERMISSIONS_CONSTANT.PmManager_Project_Update;
  public readMode: boolean = true;
  public projectStatusList: string[] = Object.keys(this.APP_ENUM.ProjectStatus);
  public pmList: UserDto[] = [];
  public project = {} as ProjectDto;
  public projectId: number;
  constructor(public injector: Injector,
    private userService: UserService,
     private projectService: ListProjectService, private route: ActivatedRoute) {super(injector) }

  ngOnInit(): void {
    this.projectId = Number(this.route.snapshot.queryParamMap.get("id"));
    this.getProjectDetail();
    this.getPm();
  }
  public getByEnum(enumValue: number, enumObject: any) {
    for (const key in enumObject) {
      if (enumObject[key] == enumValue) {
        return key;
      }
    }
  }
  public getProjectDetail(): void {
    this.projectService.GetDetailTrainingProject(this.projectId).pipe(catchError(this.projectService.handleError)).subscribe(data => {
      this.project = data.result
 
    })
  }
  public editRequest(): void {
    this.readMode = false
  }
  public getPm(): void {
    this.userService.GetAllUserActive(true).pipe(catchError(this.userService.handleError)).subscribe(data => {
      this.pmList = data.result;
    })
  }
  public saveAndClose(): void {
    this.isLoading=true;
    this.project.startTime = moment(this.project.startTime).format("YYYY-MM-DD");
    if (this.project.endTime) {
      this.project.endTime = moment(this.project.endTime).format("YYYY-MM-DD");
    }
      this.isLoading = true;
      // this.project.status = 0;
      this.projectService.update(this.project).pipe(catchError(this.projectService.handleError)).subscribe((res) => {
        abp.notify.success("updated: " + this.project.name);
        this.readMode = true;
        this.isLoading=false;
        this.getProjectDetail();
      }, () => this.isLoading = false);
  }
  

}
