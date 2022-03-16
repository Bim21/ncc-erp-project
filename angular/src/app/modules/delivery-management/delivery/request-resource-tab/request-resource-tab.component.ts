import { AppComponentBase } from 'shared/app-component-base';
import { async } from '@angular/core/testing';
import { result } from 'lodash-es';
import { ResourcePlanDto } from './../../../../service/model/resource-plan.dto';
import { PERMISSIONS_CONSTANT } from './../../../../constant/permission.constant';
import { CreateUpdateResourceRequestComponent } from './create-update-resource-request/create-update-resource-request.component';
import { MatDialog } from '@angular/material/dialog';
import { DeliveryResourceRequestService } from './../../../../service/api/delivery-request-resource.service';
import { Router } from '@angular/router';

import { finalize, catchError } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { RequestResourceDto } from './../../../../service/model/delivery-management.dto';
import { Component, OnInit, Injector, ChangeDetectorRef } from '@angular/core';
import { InputFilterDto } from '@shared/filter/filter.component';
import { SkillDto } from '@app/service/model/list-project.dto';
import { SkillService } from '@app/service/api/skill.service';
import { FormPlanUserComponent } from './form-plan-user/form-plan-user.component';
import * as moment from 'moment';

@Component({
  selector: 'app-request-resource-tab',
  templateUrl: './request-resource-tab.component.html',
  styleUrls: ['./request-resource-tab.component.css']
})
export class RequestResourceTabComponent extends PagedListingComponentBase<RequestResourceDto> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    let requestBody:any = request
    requestBody.skillIds = this.skillIds
    requestBody.isAndCondition = this.isAndCondition
    let objFilter = [
      {name: 'status', isTrue: false, value: this.selectedStatus},
      {name: 'level', isTrue: false, value: this.selectedLevel},
    ];
    objFilter.forEach((item) => {
      if(!item.isTrue){
        requestBody.filterItems = this.AddFilterItem(requestBody, item.name, item.value)
      }
      if(item.value == -1){
        requestBody.filterItems = this.clearFilter(requestBody, item.name, "")
        item.isTrue = true
      }
    })
    this.resourceRequestService.getResourcePaging(requestBody, this.selectedOption).pipe(finalize(() => {
      finishedCallback();
    }), catchError(this.resourceRequestService.handleError)).subscribe(data => {
      this.listRequest = data.result.items;
      this.tempListRequest = data.result.items;
      this.showPaging(data.result, pageNumber);
      objFilter.forEach((item) => {
        if(!item.isTrue){
          request.filterItems = this.clearFilter(request, item.name, '')
        }
      })
      requestBody.skillIds = null
      this.isLoading = false;
    })
  }
  protected delete(item: RequestResourceDto): void {
    abp.message.confirm(
      "Delete request: " + item.name + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.resourceRequestService.delete(item.id).pipe(catchError(this.resourceRequestService.handleError)).subscribe(() => {
            abp.notify.success("Deleted request: " + item.name);
            this.refresh();
          });

        }
      }

    );


  }
  statusParam = Object.entries(this.APP_ENUM.ResourceRequestStatus).map(item => {
    return {
      displayName: item[0],
      value: item[1]
    }
  })
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', comparisions: [0, 6, 7, 8], displayName: "Name" },
    { propertyName: 'projectName', comparisions: [0, 6, 7, 8], displayName: "Project Name" },
    { propertyName: 'timeNeed', comparisions: [0, 1, 2, 3, 4], displayName: "Time Need", filterType: 1 },
    { propertyName: 'timeDone', comparisions: [0, 1, 2, 3, 4], displayName: "Time Done", filterType: 1 },
    // { propertyName: 'status', comparisions: [0], displayName: "status", filterType:3, dropdownData:this.statusParam },
  ];
  public selectedOption: string = "PROJECT"
  public selectedStatus: any = 0
  public listRequest: RequestResourceDto[] = [];
  public tempListRequest: RequestResourceDto[] = [];
  public statusList: string[] = Object.keys(this.APP_ENUM.ResourceRequestStatus)
  public selectedLevel: any = -1
  public levelList: string[] = Object.keys(this.APP_ENUM.UserLevel)
  public listSkills: SkillDto[] = [];
  public isAndCondition:boolean =false;
  public skillIds:number[]
  public theadTable: THeadTable[] = [
    {name: '#'},
    {name: 'Priority'},
    {name: 'Project'},
    {name: 'Skill'},
    {name: 'Level'},
    {name: 'Time request'},
    {name: 'Time need'},
    {name: 'Planned resource'},
    {name: 'PM Note'},
    {name: 'HPM Note'},
    {name: 'Status'},
    {name: 'Action'},
  ]
  DeliveryManagement_ResourceRequest = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest;
  DeliveryManagement_ResourceRequest_Create = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Create;
  DeliveryManagement_ResourceRequest_Delete = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Delete;
  DeliveryManagement_ResourceRequest_Update = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Update;
  DeliveryManagement_ResourceRequest_ViewDetailResourceRequest = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest;


  constructor(private injector: Injector,
    private resourceRequestService: DeliveryResourceRequestService,
    private skillService: SkillService,
    private ref: ChangeDetectorRef,
    private dialog: MatDialog) { super(injector) }

  ngOnInit(): void {
    this.getAllSkills()
    this.refresh();
  }
  ngAfterContentInit(): void {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    this.ref.detectChanges()
  }
  showDetail(item: any) {
    if (this.permission.isGranted(this.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest)) {
      this.router.navigate(['app/resourceRequestDetail'], {
        queryParams: {
          id: item.id,
          timeNeed: item.timeNeed
        }
      })
    }

  }

  public getValueByEnum(enumValue: number, enumObject) {
    for (const key in enumObject) {
      if (enumObject[key] == enumValue) {
        return key;
      }
    }
  }
  public onStatusChange() {
    this.refresh()
  }

  showDialog(command: string, request: any) {
    let resourceRequest = {} as RequestResourceDto;
    resourceRequest = {
      name: request.name,
      projectId: request.projectId,
      priority: request.id ? request.priority : 1,
      timeDone: request.timeDone,
      id: request.id ? request.id : null,
      quantity: request.id ? request.quantity : 1, 
      skillIds: request.id ? request.skillIds : null,
      timeNeed: request.id ? request.timeNeed : null,
      level: request.id ? request.level : null
    }
    const show = this.dialog.open(CreateUpdateResourceRequestComponent, {
      data: {
        command: command,
        item: resourceRequest
      },
      width: "700px",
      maxHeight: '90vh',
    })
    show.afterClosed().subscribe(result => {
      this.refresh()
    });
  }
  public createRequest() {
    this.showDialog("create", {});
  }
  public editRequest(item: any) {
    this.showDialog("edit", item);
  }
  async showModalPlanUser(item: any){
    let data = await this.getPlanResource(item);
    const show = this.dialog.open(FormPlanUserComponent, {
      data,
      width: "700px",
      maxHeight:"90vh"
    })
    show.afterClosed().subscribe(result => {
      this.refresh()
    });
  }
  getAllSkills(){
    this.skillService.getAll().subscribe((data) => {
      this.listSkills = data.result;
      this.skillsParam = data.result.map(item => {
        return {
          displayName: item.name,
          value: item.id
        }
      })
    })
  }

  styleObject(item: any){
    return {
      width: item.width,
      height: item.height
    }
  }

  cancelRequest(id){

  }

  async getPlanResource(item){
    let data = new ResourcePlanDto();
    data.projectUserId = item.projectId;
    data.resourceRequestId = item.id;
    if(!item.plannedProjectUserId) return data;
    let res = await this.resourceRequestService.getPlanResource(item.plannedProjectUserId, item.id)
    data = res.result
    return data
  }

  getInforUserPlan(user, date){
    if(user){
      return '<b>' + user + '</b>' + ' đã được plan cho request này từ ' + '<b>' + moment(date).format("DD/MM/YYYY") + '</b>';
    }
    return ''
  }
}

export class THeadTable{
  name: string;
  width?: string = 'auto';
  height?: string = 'auto';
  backgroud_color?: string;
}

