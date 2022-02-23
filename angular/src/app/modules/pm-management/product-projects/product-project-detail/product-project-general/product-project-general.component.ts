import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { catchError } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { ListProjectService } from './../../../../../service/api/list-project.service';
import { UserService } from './../../../../../service/api/user.service';
import { ProjectDto } from './../../../../../service/model/list-project.dto';
import { UserDto } from './../../../../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, OnInit, Injector } from '@angular/core';
import * as moment from 'moment';
import { ProductProjectDto } from '@app/service/model/project.dto';

@Component({
  selector: 'app-product-project-general',
  templateUrl: './product-project-general.component.html',
  styleUrls: ['./product-project-general.component.css']
})
export class ProductProjectGeneralComponent extends AppComponentBase implements OnInit {
  PmManager_Project_Update = PERMISSIONS_CONSTANT.PmManager_Project_Update;
  public searchPM: string = "";
  public readMode: boolean = true;
  public projectStatusList: string[] = Object.keys(this.APP_ENUM.ProjectStatus);
  public pmList: UserDto[] = [];
  public project = {} as ProductProjectDto;
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
    this.projectService.GetDetailProductProject(this.projectId).pipe(catchError(this.projectService.handleError)).subscribe(data => {
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
    this.project.projectType=3;
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
