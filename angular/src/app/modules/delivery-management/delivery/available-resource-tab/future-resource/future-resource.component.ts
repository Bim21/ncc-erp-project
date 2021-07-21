import { ProjectUserService } from '@app/service/api/project-user.service';
import { EditFutureResourceComponent } from './edit-future-resource/edit-future-resource.component';
import { PlanUserComponent } from './../plan-resource/plan-user/plan-user.component';
import { MatDialog } from '@angular/material/dialog';
import { InputFilterDto } from './../../../../../../shared/filter/filter.component';
import { result } from 'lodash-es';
import { futureResourceDto } from './../../../../../service/model/delivery-management.dto';
import { catchError, finalize } from 'rxjs/operators';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-future-resource',
  templateUrl: './future-resource.component.html',
  styleUrls: ['./future-resource.component.css']
})
export class FutureResourceComponent extends PagedListingComponentBase<FutureResourceComponent> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.availableRerourceService.availableResourceFuture(request).pipe(finalize(()=>{
      finishedCallback();
    }),catchError(this.availableRerourceService.handleError)).subscribe((data)=>{
      this.futureResourceList=data.result.items;
      this.showPaging(data.result,pageNumber);
    })
  }
  protected delete(item: FutureResourceComponent): void {
    abp.message.confirm(
      "Delete TimeSheet " + item.userName + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.projectUserService.removeProjectUser(item.id).pipe(catchError(this.projectUserService.handleError)).subscribe(() => {
            abp.notify.success("Deleted TimeSheet " + item.userName);
            this.refresh();
          });
        }
      }
    );
  }
  
  public futureResourceList:futureResourceDto[]=[];
  constructor(public injector:Injector,
    private availableRerourceService: DeliveryResourceRequestService,
    private dialog:MatDialog,
    private projectUserService: ProjectUserService) {super(injector)}

    public readonly FILTER_CONFIG: InputFilterDto[] = [
      { propertyName: 'userName', comparisions: [0, 6, 7, 8], displayName: "User Name",isDate:true },
      { propertyName: 'use', comparisions: [0, 1, 2, 3], displayName: "Used" },
      
    ];

  ngOnInit(): void {
    this.refresh();
  }
  showDialog(command:string,User:futureResourceDto){
    let item={
      userId:User.userId,
      allocatePercentage:User.use,
      startTime:User.startDate,
      projectId:User.projectid,
      id:User.id
    }
    const show=this.dialog.open(EditFutureResourceComponent, {
      width: '700px',
      disableClose: true,
      data: {
        futureResource:item,
        command:command
      },
    });
    show.afterClosed().subscribe(result => {
      if(result){
        this.refresh()
      }
    });
    
  }

  updateFutureResource(user){
    this.showDialog("update",user);
  }


  


}
