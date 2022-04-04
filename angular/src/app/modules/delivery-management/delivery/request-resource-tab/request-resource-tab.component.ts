import { result } from 'lodash-es';
import { FormSetDoneComponent } from './form-set-done/form-set-done.component';
import { SortableComponent, SortableModel } from './../../../../../shared/components/sortable/sortable.component';
import { AppComponentBase } from 'shared/app-component-base';
import { ResourcePlanDto } from './../../../../service/model/resource-plan.dto';
import { PERMISSIONS_CONSTANT } from './../../../../constant/permission.constant';
import { CreateUpdateResourceRequestComponent } from './create-update-resource-request/create-update-resource-request.component';
import { MatDialog } from '@angular/material/dialog';
import { DeliveryResourceRequestService } from './../../../../service/api/delivery-request-resource.service';
import { finalize, catchError } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { RequestResourceDto } from './../../../../service/model/delivery-management.dto';
import { Component, OnInit, Injector, ChangeDetectorRef, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { InputFilterDto } from '@shared/filter/filter.component';
import { SkillDto } from '@app/service/model/list-project.dto';
import { FormPlanUserComponent } from './form-plan-user/form-plan-user.component';
import * as moment from 'moment';
import { IDNameDto } from '@app/service/model/id-name.dto';

@Component({
  selector: 'app-request-resource-tab',
  templateUrl: './request-resource-tab.component.html',
  styleUrls: ['./request-resource-tab.component.css']
})
export class RequestResourceTabComponent extends PagedListingComponentBase<RequestResourceDto> implements OnInit {
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', comparisions: [0, 6, 7, 8], displayName: "Name" },
    { propertyName: 'projectName', comparisions: [0, 6, 7, 8], displayName: "Project Name" },
    { propertyName: 'timeNeed', comparisions: [0, 1, 2, 3, 4], displayName: "Time Need", filterType: 1 },
    { propertyName: 'timeDone', comparisions: [0, 1, 2, 3, 4], displayName: "Time Done", filterType: 1 },
  ];
  public selectedOption: string = "PROJECT"
  public selectedStatus: any = 0
  public listRequest: RequestResourceDto[] = [];
  public tempListRequest: RequestResourceDto[] = [];
  public listStatuses: any[] = []
  public listLevels: any[] = []
  public listSkills: SkillDto[] = [];
  public listProjectUserRoles: IDNameDto[] = []
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
    {name: 'Time request', sortName: 'creationTime', defaultSort: ''},
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
  public sortable = new SortableModel('priority',0,'ASC')

  DeliveryManagement_ResourceRequest = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest;
  DeliveryManagement_ResourceRequest_Create = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Create;
  DeliveryManagement_ResourceRequest_Delete = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Delete;
  DeliveryManagement_ResourceRequest_Update = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Update;
  DeliveryManagement_ResourceRequest_ViewDetailResourceRequest = PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest;

  @ViewChildren('sortThead') private elementRefSortable: QueryList<any>;
  constructor(
    private injector: Injector,
    private resourceRequestService: DeliveryResourceRequestService,
    private ref: ChangeDetectorRef,
    private dialog: MatDialog
  )
  { 
    super(injector) 
  }

  ngOnInit(): void {
    this.getAllSkills()
    this.getLevels()
    this.getPriorities()
    this.getStatuses()
    this.getProjectUserRoles()
    this.refresh();
  }

  ngAfterContentInit(): void {
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
  showDialog(command: string, request: any) {
    let resourceRequest = {
      id: request.id ? request.id : null,
      projectId: 0
    }
    const show = this.dialog.open(CreateUpdateResourceRequestComponent, {
      data: {
        command: command,
        item: resourceRequest, 
        skills: this.listSkills,
        levels: this.listLevels,
        typeControl: 'request'
      },
      width: "700px",
      maxHeight: '90vh',
    })
    show.afterClosed().subscribe(rs => {
      if(!rs) return
      if(command == 'create')
        this.refresh()
      else if(command == 'edit'){
        let index = this.listRequest.findIndex(x => x.id == rs.id)
        if(index >= 0){
          this.listRequest[index] = rs
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
  public setDoneRequest(item){
    let data = {
      ...item.planUserInfo, 
      requestName: item.name, 
      resourceRequestId: item.id
    }
    const showModal = this.dialog.open(FormSetDoneComponent, {
      data,
      width: "700px",
      maxHeight: "90vh"
    })
    showModal.afterClosed().subscribe((rs) => {
      if(rs)
        this.refresh()
    })
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

  async showModalPlanUser(item: any){
    const data = await this.getPlanResource(item);
    const show = this.dialog.open(FormPlanUserComponent, {
      data: {...data, projectUserRoles: this.listProjectUserRoles},
      width: "700px",
      maxHeight:"90vh"
    })
    show.afterClosed().subscribe(rs => {
      if(!rs) return
      if(rs.type == 'delete'){
        this.refresh()
      }
      else{
        let index = this.listRequest.findIndex(x => x.id == rs.data.resourceRequestId)
        if(index >= 0)
          this.listRequest[index].planUserInfo = rs.data.result
      }
      
    });
  }
  async getPlanResource(item){
    let data = new ResourcePlanDto(item.id, 0);
    if(!item.planUserInfo) 
      return data;
    let res = await this.resourceRequestService.getPlanResource(item.planUserInfo.projectUserId, item.id)
    return res.result
  }

  sendRecruitment(){
    abp.message.info('Chức năng này sẽ được cập nhật trong bản release sắp tới', 'Thông báo')
  }

  // #region update note for pm, dmPm
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
      note: this.strNote,
    }
    this.resourceRequestService.updateNote(request,this.typePM).subscribe(rs => {
      if(rs.success){
        abp.notify.success('Update Note Successfully!')
        let index = this.listRequest.findIndex(x => x.id == request.resourceRequestId);
        if(index >= 0){
          if(this.typePM == 'PM')
            this.listRequest[index].pmNote = request.note;
          else
            this.listRequest[index].dmNote = request.note;
        }
        this.closeModal()
      }
      else{
        abp.notify.error(rs.result)
      }
    })
  }
  // #endregion

  // #region paging, search, sortable, filter
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
      this.listRequest = this.tempListRequest = data.result.items;
      this.showPaging(data.result, pageNumber);
    },
    (error)=> {
      abp.notify.error(error)
    })
    let rsFilter = this.resetDataSearch(requestBody, request, objFilter)
    request = rsFilter.request
    requestBody = rsFilter.requestBody
  }

  resetDataSearch(requestBody: any, request: any, objFilter: any){
    objFilter.forEach((item) => {
      if(!item.isTrue){
        request.filterItems = this.clearFilter(request, item.name, '')
      }
    })
    requestBody.skillIds = null
    requestBody.sort = null
    requestBody.sortDirection = null
    this.isLoading = false;

    return {
      request,
      requestBody,
      objFilter
    }
  }

  clearAllFilter(){
    this.filterItems = []
    this.searchText = ''
    this.skillIds = []
    this.selectedLevel = -1
    this.selectedStatus = -1
    this.changeSortableByName('priority', 'ASC')
    this.sortable = new SortableModel('priority',0,'ASC')
    this.refresh()
  }

  onChangeStatus(){
    let status = this.listStatuses.find(x => x.id == this.selectedStatus)
    if(status && status.name == 'DONE')
    {
      this.sortable = new SortableModel('timeDone', 1, 'DESC')
      this.changeSortableByName('','')
    }
    this.refresh()
  }

  sortTable(event: any){
    this.sortable = event
    this.changeSortableByName(this.sortable.sort, this.sortable.typeSort)
    this.refresh()
  }

  changeSortableByName(sort: string, sortType: string){
    this.elementRefSortable.forEach((item) => {
      if(item.childValue.sort != sort){
        item.childValue.typeSort = ''
      }
      else{
        item.childValue.typeSort = sortType
      }
    })
    this.ref.detectChanges()
  }
  // #endregion

  //#region get skills, statuses, levels, priorities
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
  getProjectUserRoles(){
    this.resourceRequestService.getProjectUserRoles().subscribe((rs: any) => {
      this.listProjectUserRoles = rs.result
    })
  }
  // #endregion

  showActionViewRecruitment(status, isRecruitment){
    if(isRecruitment && (status == 'INPROGRESS' || status == 'CANCELLED' || status == 'DONE'))
      return true
    return false
  }
  styleThead(item: any){
    return {
      width: item.width,
      height: item.height
    }
  }
  public getValueByEnum(enumValue: number, enumObject) {
    for (const key in enumObject) {
      if (enumObject[key] == enumValue) {
        return key;
      }
    }
  }
  viewRecruitment(url){
    window.open(url, '_blank')
  }
  protected delete(item: RequestResourceDto): void {
    abp.message.confirm(
      "Delete this request?",
      "",
      (result: boolean) => {
        if (result) {
          this.resourceRequestService.delete(item.id).pipe(catchError(this.resourceRequestService.handleError)).subscribe(() => {
            abp.notify.success(" Delete request successfully");
            this.refresh();
          });

        }
      }

    );
  }
  isShowButtonMenuAction(item){
    if((item.statusName != 'DONE' && !item.isRecruitmentSend) || item.statusName != 'CANCELLED')
      return true;
    return false;
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

