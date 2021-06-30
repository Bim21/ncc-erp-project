import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-project-status-tab',
  templateUrl: './project-status-tab.component.html',
  styleUrls: ['./project-status-tab.component.css']
})
export class ProjectStatusTabComponent extends PagedListingComponentBase<any> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    throw new Error('Method not implemented.');
  }
  protected delete(entity: any): void {
    throw new Error('Method not implemented.');

    
  }

  constructor(injector:Injector) {
    super(injector)
   }

  ngOnInit(): void {
  }

}
