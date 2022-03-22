import { FormSetDoneComponent } from './form-set-done/form-set-done.component';
import { SortableComponent } from './../../../../../shared/components/sortable/sortable.component';
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
import { Component, OnInit, Injector, ChangeDetectorRef, ViewChild, ViewChildren, QueryList } from '@angular/core';
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
    if(this.sortable.sort){
      requestBody.sort = this.sortable.sort;
      requestBody.sortDirection = this.sortable.sortDirection
    }
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
      requestBody.sort = null
      requestBody.sortDirection = null
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
  public listStatuses: any[] = []
  public listLevels: any[] = []
  public listSkills: SkillDto[] = [];
  public listPriorities: any[] = []
  public selectedLevel: any = -1
  public isAndCondition:boolean =false;
  public skillIds:number[]
  public theadTable: THeadTable[] = [
    {name: '#'},
    {name: 'Priority', sortName: 'priority', defaultSort: 'ASC'},
    {name: 'Project', sortName: 'projectName', defaultSort: ''},
    {name: 'Skill'},
    {name: 'Level', sortName: 'level', defaultSort: ''},
    {name: 'Time request', sortName: 'requestStartTime', defaultSort: ''},
    {name: 'Time need', sortName: 'timeNeed', defaultSort: ''},
    {name: 'Planned resource'},
    {name: 'PM Note'},
    {name: 'HR/DM Note'},
    {name: 'Status'},
    {name: 'Action'},
  ]
  public isShowModal: string = 'none'
  public modal_title: string
  public strNote: string
  public typePM: string
  public resourceRequestId: number
  public sortable = {
    sort: 'priority',
    sortDirection: 0, 
    typeSort: 'ASC'
  }

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
    this.getLevels()
    this.getPriorities()
    this.getStatuses()
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
    let resourceRequest = {
      id: request.id ? request.id : null,
    }
    const show = this.dialog.open(CreateUpdateResourceRequestComponent, {
      data: {
        command: command,
        item: resourceRequest, 
        skills: this.listSkills,
        levels: this.listLevels
      },
      width: "700px",
      maxHeight: '90vh',
    })
    show.afterClosed().subscribe(result => {
      if(command == 'create' && result)
        this.refresh()
      else if(command == 'edit'){
        let index = this.listRequest.findIndex(x => x.id == result.id)
        console.log(result)
        if(index >= 0){
          this.listRequest[index] = result
        }
      }
    });
  }
  public createRequest() {
    this.showDialog("create", {});
  }
  public editRequest(item: any) {
    this.showDialog("edit", item);
  }

  public modalSetDoneRequest(data){
    const showModal = this.dialog.open(FormSetDoneComponent, {
      data,
      width: "700px",
      maxHeight: "90vh"
    })
    showModal.afterClosed().subscribe((rs) => {

    })
  }

  public openModal(name, typePM, content, id){
    this.typePM = typePM
    this.modal_title = name
    this.strNote = content
    this.resourceRequestId = id
    this.isShowModal = 'block'
  }

  public closeModal(){
    this.isShowModal = 'none'
  }

  public updateNote(){
    let request = {
      resourceRequestId: this.resourceRequestId,
      pmNote: '',
      hpmNote: ''
    }
    if(this.typePM == 'PM'){
      this.updatePMNote(request,this.strNote)
    }
    else{
      this.updateHPMNote(request,this.strNote)
    }
    this.closeModal()
  }

  updatePMNote(request, note){
    request.pmNote = note
    this.resourceRequestService.updateNotePM(request).subscribe(res => {
      if(res.success){
        abp.notify.success('Update Note Successfully!')
        let index = this.listRequest.findIndex(x => x.id == request.resourceRequestId);
        if(index >= 0){
          this.listRequest[index].pmNote = note;
        }
      }
      else{
        abp.notify.error(res.result)
      }
    })
  }

  updateHPMNote(request, note){
    request.hpmNote = note
    this.resourceRequestService.updateNoteHPM(request).subscribe(res => {
      if(res.success){
        abp.notify.success('Update Note Successfully!')
        let index = this.listRequest.findIndex(x => x.id == request.resourceRequestId);
        if(index >= 0){
          this.listRequest[index].dmNote = note;
        }
      }
      else{
        abp.notify.error(res.result)
      }
    })
  }

  async showModalPlanUser(item: any){
    let data = await this.getPlanResource(item);
    const show = this.dialog.open(FormPlanUserComponent, {
      data,
      width: "700px",
      maxHeight:"90vh"
    })
    show.afterClosed().subscribe(result => {
      let resourceRequestId;
        resourceRequestId = result.data.resourceRequestId
      let index = this.listRequest.findIndex(x => x.id == resourceRequestId)
      if(index >= 0){
        if(result.type == 'delete'){
          this.listRequest[index].plannedEmployee = null
          this.listRequest[index].plannedDate = null
        }
        else{
          this.listRequest[index].plannedEmployee = result.data.userName
          this.listRequest[index].plannedDate = result.data.timeJoin
        }
      }
    });
  }

  /*get skills, statuses, levels, priorities*/
  getAllSkills(){
    this.resourceRequestService.getSkills().subscribe((data) => {
      this.listSkills = data.result;
    })
  }
  getLevels(){
    this.resourceRequestService.getLevels().subscribe(res => {
      this.listLevels = res.result
    })
  }
  getPriorities(){
    this.resourceRequestService.getPriorities().subscribe(res => {
      this.listPriorities = res.result
    })
  }
  getStatuses(){
    this.resourceRequestService.getStatuses().subscribe(res => {
      this.listStatuses = res.result
    })
  }
  /*end get skills, statuses, levels, priorities */

  sortTable(event: any){
    this.sortable = event
    this.refresh()
  }

  styleObject(item: any){
    return {
      width: item.width,
      height: item.height
    }
  }

  cancelRequest(id){
    abp.message.confirm(
      'Are you sure cancel request?',
      '',
      (result) => {
        if(result){
          this.resourceRequestService.cancelResourceRequest(id).subscribe(res => {
            if(res.success){
              abp.notify.success('Cancel Request Success!')
              this.refresh()
            }
            else{
              abp.notify.error(res.result)
            }
          })
        }
      }
    )
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

  showActionViewRecruitment(status, isRecruitment){
    if(
      isRecruitment &&
      (status == 'INPROGRESS' || status == 'CANCELLED' || status == 'DONE')
    )
    {
      return true
    }
    return false
  }

  viewRecruitment(url){
    window.open(url, '_blank')
  }

  setDoneRequest(id: number){

  }
}

export class THeadTable{
  name: string;
  width?: string = 'auto';
  height?: string = 'auto';
  backgroud_color?: string;
  sortName?: string;;
  defaultSort?: string;
}

