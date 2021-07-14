import { PlanUserComponent } from './plan-user/plan-user.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { result } from 'lodash-es';
import { catchError, finalize } from 'rxjs/operators';
import { DeliveryResourceRequestService } from './../../../../service/api/delivery-request-resource.service';
import { availableResourceDto } from './../../../../service/model/delivery-management.dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-available-resource-tab',
  templateUrl: './available-resource-tab.component.html',
  styleUrls: ['./available-resource-tab.component.css']
})
export class AvailableResourceTabComponent extends PagedListingComponentBase<AvailableResourceTabComponent> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.availableRerourceService.getAvailableResource(request).pipe(finalize(()=>{
      finishedCallback();
    }),catchError(this.availableRerourceService.handleError)).subscribe(data=>{
      this.availableResourceList=data.result.items;
      this.showPaging(data.result,pageNumber);
    })
  }
  protected delete(entity: AvailableResourceTabComponent): void {
    
  }
  public availableResourceList:availableResourceDto[]=[];
  constructor(public injector:Injector,
    private availableRerourceService: DeliveryResourceRequestService,
    private dialog:MatDialog,
    ) {super(injector) }

  ngOnInit(): void {
    this.refresh();
  }
  showDialogPlanUser(user:any){
    let item={
      userId:user.userId
    }
    
    const show=this.dialog.open(PlanUserComponent, {
      width: '700px',
      disableClose: true,
      data: {
        item:item
      },
    });
    show.afterClosed().subscribe(result => {
      if(result){
        this.refresh()
      }
    });

    
  }
  planUser(user:any){
    this.showDialogPlanUser(user);
  }
  

}
