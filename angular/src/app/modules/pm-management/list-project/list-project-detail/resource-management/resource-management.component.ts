import { Component, Injector, OnInit } from '@angular/core';
import { ClientDto } from '@app/service/model/list-project.dto';
import { InputFilterDto } from '@shared/filter/filter.component';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-resource-management',
  templateUrl: './resource-management.component.html',
  styleUrls: ['./resource-management.component.css']
})
export class ResourceManagementComponent extends PagedListingComponentBase<ClientDto> implements OnInit{
  public clientList:ClientDto[] = [];
  constructor(injector:Injector) { super(injector)}
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: "Name", comparisions: [0, 6, 7, 8] },
  ];
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    // throw new Error('Method not implemented.');
   
  }
  protected delete (item: ClientDto): void {
    // throw new Error('Method not implemented.');
    
  
    
  }


  ngOnInit(): void {
  }

}
