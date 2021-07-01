import { SaodoDto } from './../../../service/model/saodo.dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-sao-do',
  templateUrl: './sao-do.component.html',
  styleUrls: ['./sao-do.component.css']
})
export class SaoDoComponent extends PagedListingComponentBase<SaodoDto> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    throw new Error('Method not implemented.');
  }
  protected delete(entity: SaodoDto): void {
    throw new Error('Method not implemented.');
  }

  constructor(injector:Injector) {
    super(injector)
   }

  ngOnInit(): void {
  }

}
