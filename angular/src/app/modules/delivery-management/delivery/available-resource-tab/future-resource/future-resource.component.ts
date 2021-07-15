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
  protected delete(entity: FutureResourceComponent): void {
    throw new Error('Method not implemented.');
  }
  
  public futureResourceList:futureResourceDto[]=[];
  constructor(public injector:Injector,
    private availableRerourceService: DeliveryResourceRequestService) {super(injector)}

    public readonly FILTER_CONFIG: InputFilterDto[] = [
      { propertyName: 'userName', comparisions: [0, 6, 7, 8], displayName: "User Name",isDate:true },
      { propertyName: 'use', comparisions: [0, 1, 2, 3], displayName: "Used" },
      
    ];

  ngOnInit(): void {
    this.refresh();
  }

}
