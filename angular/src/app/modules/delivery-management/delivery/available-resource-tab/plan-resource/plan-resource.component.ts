import { InputFilterDto } from './../../../../../../shared/filter/filter.component';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { availableResourceDto } from './../../../../../service/model/delivery-management.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { PlanUserComponent } from './plan-user/plan-user.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { result } from 'lodash-es';
import { catchError, finalize } from 'rxjs/operators';
// import { DeliveryResourceRequestService } from './../../../../service/api/delivery-request-resource.service';
// import { availableResourceDto } from './../../../../service/model/delivery-management.dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-plan-resource',
  templateUrl: './plan-resource.component.html',
  styleUrls: ['./plan-resource.component.css']
})
export class PlanResourceComponent extends PagedListingComponentBase<PlanResourceComponent> implements OnInit {

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.isLoading =true;
    this.availableRerourceService.getAvailableResource(request).pipe(finalize(()=>{
      finishedCallback();
    }),catchError(this.availableRerourceService.handleError)).subscribe(data=>{
      this.availableResourceList=data.result.items;
      this.showPaging(data.result,pageNumber);
      this.isLoading =false;

    })
  }
  protected delete(entity: PlanResourceComponent): void {
    
  }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'fullName', comparisions: [0, 6, 7, 8], displayName: "User Name" },
    { propertyName: 'used', comparisions:  [0, 1, 2, 3, 4], displayName: "Used" },
    // { propertyName: 'projects', comparisions:  [0, 6, 7, 8], displayName: "projectName" },

    
  ];
  public availableResourceList:availableResourceDto[]=[];
  constructor(public injector:Injector,
    private availableRerourceService: DeliveryResourceRequestService,
    private dialog:MatDialog,
    ) {super(injector) }

  ngOnInit(): void {
    this.refresh();
  }
  showDialogPlanUser(command:string,user:any){
    let item={
      userId:user.userId,
      fullName:user.fullName
    }
    
    const show=this.dialog.open(PlanUserComponent, {
      width: '700px',
      disableClose: true,
      data: {
        item:item,
        command:command
      },
    });
    show.afterClosed().subscribe(result => {
      if(result){
        this.refresh()
      }
    });

    
  }
  planUser(user:any){
    this.showDialogPlanUser("plan",user);
  }
  showUserDetail(userId:any){
    
  }
  





}
