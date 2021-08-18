import { ActivatedRoute } from '@angular/router';

import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, Inject, OnInit, Injector, inject } from '@angular/core';

@Component({
  selector: 'app-result-reviewer',
  templateUrl: './result-reviewer.component.html',
  styleUrls: ['./result-reviewer.component.css']
})
export class ResultReviewerComponent extends PagedListingComponentBase<ResultReviewerComponent> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    throw new Error('Method not implemented.');
  }
  protected delete(entity: ResultReviewerComponent): void {
    throw new Error('Method not implemented.');
  }
   
  public phaseId="";
  public phaseName="";
  public phaseType="";
  public reviewerId="";
  constructor(
    public injector :Injector,
    public route:ActivatedRoute) {
    super(injector);
  }

  ngOnInit(): void {
    this.phaseId=this.route.snapshot.queryParamMap.get("phaseId");
    this.phaseName=this.route.snapshot.queryParamMap.get("phaseName");
    this.phaseType=this.route.snapshot.queryParamMap.get("type");
    this.reviewerId=this.route.snapshot.queryParamMap.get("id");
    console.log(this.phaseType)
  }


}
