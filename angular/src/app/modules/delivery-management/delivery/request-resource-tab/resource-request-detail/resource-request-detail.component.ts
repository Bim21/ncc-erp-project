import { AppComponentBase } from '@shared/app-component-base';
import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { userToRequestDto } from '@app/service/model/delivery-management.dto';
import { ProjectUserService } from './../../../../../service/api/project-user.service';
import { catchError } from 'rxjs/operators';
import { AddUserToRequestComponent } from './add-user-to-request/add-user-to-request.component';
import { MatDialog } from '@angular/material/dialog';
import { extend, result } from 'lodash-es';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { ResourceRequestDetailDto, userAvailableDto } from './../../../../../service/model/delivery-management.dto';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Injector } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-resource-request-detail',
  templateUrl: './resource-request-detail.component.html',
  styleUrls: ['./resource-request-detail.component.css']
})
export class ResourceRequestDetailComponent extends AppComponentBase implements OnInit {

  public resourceRequestId:any;
  public resourceRequestList:ResourceRequestDetailDto[]=[];
  public userAvailableList:userAvailableDto[]=[];
  public userAvailable={} as userAvailableDto;
  DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest=PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest;
  DeliveryManagement_ResourceRequest_AddUserToRequest=PERMISSIONS_CONSTANT.DeliveryManagement_ResourceRequest_AddUserToRequest;
 

  constructor(private route:ActivatedRoute,
    private resourceRequestService:DeliveryResourceRequestService,
    private dialog:MatDialog,
    private projectUserService:ProjectUserService,
  injector:Injector) {super(injector) }

  ngOnInit(): void {
    this.resourceRequestId= this.route.snapshot.queryParamMap.get('id');
    this.getAllResourceRequestDetail();
    this.userAvailable.startDate=new Date();
    this.search();
    
    
  }
  public getAllResourceRequestDetail(){
    this.resourceRequestService.getResourceRequestDetail(this.resourceRequestId).subscribe(data=>{
      this.resourceRequestList=data.result;
    })
  }
  public search(){
    this.resourceRequestService.searchAvailableUserForRequest(moment(this.userAvailable.startDate).format("MM/DD/YYYY")).
    pipe(catchError(this.resourceRequestService.handleError)).subscribe((data) => {
      this.userAvailableList=data.result;
    },
    ()=>{  this.userAvailableList=[] }
    );

    
  }
  showDialog(command:string,id,userRequest:any){
    let user={} as userToRequestDto;
    if(command=="edit"){
      user={
        allocatePercentage:userRequest.allocatePercentage,
        startTime: userRequest.startTime,
        id: userRequest.id,
        projectId:userRequest.projectId
      }
    }
    const show=this.dialog.open(AddUserToRequestComponent, {
      data: {
        userId:id,
        item:user,
        command:command

      },
      width: "700px",
      disableClose: true,
    });
    show.afterClosed().subscribe(result=>{
      if(result){
        this.getAllResourceRequestDetail();
      }
    })
    

  }
  
  public addUser(id:any){
    this.showDialog("create",id,{});
    this.search();
  }
  public delete(item){
    abp.message.confirm(
      "Delete User " + item.userName + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.projectUserService.removeProjectUser(item.id).pipe(catchError(this.projectUserService.handleError)).subscribe(() => {
            abp.notify.success("Deleted User " + item.userName);
            this.getAllResourceRequestDetail();
          });
        }
      }
    );

  }

  public edit(item){
    this.showDialog("edit",item.userId,item);

  }
}
