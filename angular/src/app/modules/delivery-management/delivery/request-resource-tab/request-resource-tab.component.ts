import { PERMISSIONS_CONSTANT } from './../../../../constant/permission.constant';
import { CreateUpdateResourceRequestComponent } from './create-update-resource-request/create-update-resource-request.component';
import { MatDialog } from '@angular/material/dialog';
import { DeliveryResourceRequestService } from './../../../../service/api/delivery-request-resource.service';
import { Router } from '@angular/router';

import { finalize, catchError } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { RequestResourceDto } from './../../../../service/model/delivery-management.dto';
import { Component, OnInit, Injector } from '@angular/core';
import { InputFilterDto } from '@shared/filter/filter.component';

@Component({
  selector: 'app-request-resource-tab',
  templateUrl: './request-resource-tab.component.html',
  styleUrls: ['./request-resource-tab.component.css']
})
export class RequestResourceTabComponent extends PagedListingComponentBase<RequestResourceDto> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.isLoading =true;
    this.resourceRequestService.getAllPaging(request).pipe(finalize(() => {
      finishedCallback();
    }), catchError(this.resourceRequestService.handleError)).subscribe(data => {
      this.listRequest = data.result.items;
      this.tempListRequest= data.result.items;
      this.showPaging(data.result, pageNumber);
      this.isLoading=false;
    })
  }
  
  protected delete(item: RequestResourceDto): void {
    
    abp.message.confirm(
      "Delete request: " +item.name+"?",
      "",
      (result:boolean)=>{
        if(result){
          this.resourceRequestService.delete(item.id).pipe(catchError(this.resourceRequestService.handleError)).subscribe(()=>{
            abp.notify.success("Deleted request: "+item.name);
            this.refresh();
          });
            
          }
        }
      
    );

    
  }
  statusParam = Object.entries(this.APP_ENUM.ResourceRequestStatus).map(item=>{
    return { 
      displayName: item[0],
      value: item[1]
    }
  })
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', comparisions: [0, 6, 7, 8], displayName: "Name" },
    { propertyName: 'projectName', comparisions: [0, 6, 7, 8], displayName: "Project Name" },
    { propertyName: 'timeNeed', comparisions: [0, 1, 2, 3, 4], displayName: "Time Need", filterType:1 },
    { propertyName: 'timeDone', comparisions: [0, 1, 2, 3, 4], displayName: "Time Done", filterType:1 },
    { propertyName: 'status', comparisions: [0], displayName: "status", filterType:3, dropdownData:this.statusParam },
  ];
  public listRequest:RequestResourceDto[]=[];
  public tempListRequest:RequestResourceDto[]=[];
  public statusList: string[] = Object.keys(this.APP_ENUM.ResourceRequestStatus);
  DeliveryManagement_ResourceRequest=PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest;
  DeliveryManagement_ResourceRequest_Create=PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Create;
  DeliveryManagement_ResourceRequest_Delete=PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Delete;
  DeliveryManagement_ResourceRequest_Update=PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_Update;
  DeliveryManagement_ResourceRequest_ViewDetailResourceRequest=PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest;
  
  
  constructor(private injector:Injector,
  private resourceRequestService:DeliveryResourceRequestService,
  private dialog: MatDialog) {super(injector) }

  ngOnInit(): void {
    this.refresh();
   
   
  }
  showDetail(item:any){
    if(this.permission.isGranted(this.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest)){
      this.router.navigate(['app/resourceRequestDetail'], {
        queryParams: {
          id: item.id,
          timeNeed:item.timeNeed
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
  
  showDialog(command:string, request:any){
    let resourceRequest={} as RequestResourceDto;
    if(command=="edit"){
      resourceRequest={
        name:request.name,
        projectId:request.projectId,
        timeNeed:request.timeNeed,
        status:request.status,
        timeDone:request.timeDone,
        id:request.id,
        pmNote:request.pmNote,
        dmNote:request.dmNote
      }
    }
    const show=this.dialog.open(CreateUpdateResourceRequestComponent,{
      data:{
        command:command,
        item:resourceRequest
      },
      width:"700px",
      maxHeight: '90vh',
    })
    show.afterClosed().subscribe(result => {
      if(result){
        this.refresh()
      }
    });
  }
  public createRequest(){
    this.showDialog("create",{});
  }
  public editRequest(item:any){
    this.showDialog("edit",item);
  }
  
}
