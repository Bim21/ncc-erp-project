import { UserDto } from './../../../../../../shared/service-proxies/service-proxies';
import { UserService } from './../../../../../service/api/user.service';
import { ActivatedRoute } from '@angular/router';
import { projectUserDto, projectResourceRequestDto } from './../../../../../service/model/project.dto';
import { ProjectResourceRequestService } from './../../../../../service/api/project-resource-request.service';
import { ProjectUserBillService } from './../../../../../service/api/project-user-bill.service';
import { ProjectUserService } from './../../../../../service/api/project-user.service';
import { AppComponentBase } from 'shared/app-component-base';
import { Component, Injector, OnInit } from '@angular/core';
import { ClientDto } from '@app/service/model/list-project.dto';
import { InputFilterDto } from '@shared/filter/filter.component';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-resource-management',
  templateUrl: './resource-management.component.html',
  styleUrls: ['./resource-management.component.css']
})
export class ResourceManagementComponent extends AppComponentBase implements OnInit {
  private projectId:number;
  public userBillCurrentPage = 1;
  public resourceRequestCurrentPage = 1;
  public userListCurrentPage = 1;
  public projectUserList: projectUserDto[] = [];
  public projectUserBill: projectUserDto[] = [];
  public projectResourceRequestList: projectResourceRequestDto[] = [];
  public userStatusList = Object.keys(this.APP_ENUM.ProjectUserStatus);
  public userList:UserDto[] =[];

  constructor(injector: Injector, private projectUserService: ProjectUserService, private projectUserBillService: ProjectUserBillService, private userService:UserService,
    private projectRequestService: ProjectResourceRequestService, private route: ActivatedRoute) { super(injector) }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: "Name", comparisions: [0, 6, 7, 8] },
  ];
  ngOnInit(): void {
    this.projectId = Number(this.route.snapshot.queryParamMap.get("id"));
    this.getProjectUser();
    this.getResourceRequestList();
    this.getUserBill();
    this.getAllUser();
  }
  private getProjectUser() {
    this.projectUserService.getAllProjectUser(this.projectId).pipe(catchError(this.projectUserService.handleError)).subscribe(data=>{
      this.projectUserList =data.result;
    })
  }
  private getResourceRequestList(): void {
    this.projectRequestService.getAllResourceRequest(this.projectId).pipe(catchError(this.projectRequestService.handleError)).subscribe(data => {
      this.projectResourceRequestList = data.result
    })
  }
  private getUserBill(): void {
    this.projectUserBillService.getAllUserBill(this.projectId).pipe(catchError(this.projectUserBillService.handleError)).subscribe(data => {
      this.projectUserBill = data.result
    })
  }
  private getAllUser(){
    this.userService.getAll().pipe(catchError(this.userService.handleError)).subscribe(data=>{
      this.userList =data.result.items
    })
  }
  
  public addProjectUser(){
    let newUser = {} as projectUserDto
    newUser.createMode =true;
    this.projectUserList.push(newUser)
  }

  private getValueByEnum(enumValue:number, enumObject) {
    for (const key in enumObject) {
      if (enumObject[key] == enumValue) {
        return key;
      }
    }
  }

}
