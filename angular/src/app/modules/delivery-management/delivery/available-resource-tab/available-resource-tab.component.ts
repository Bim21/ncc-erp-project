import { Router, ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { PlanUserComponent } from './plan-resource/plan-user/plan-user.component';
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
export class AvailableResourceTabComponent extends AppComponentBase implements OnInit {
  currentUrl: string = ""

  constructor(injector: Injector, private router: Router, private route: ActivatedRoute) {
    super(injector)
  }

  ngOnInit(): void {
    this.router.events.subscribe(res => this.currentUrl = this.router.url)

    // this.router.navigate(['plan-resource'],{
    //   relativeTo:this.route,
    //   replaceUrl:true
    // })
  }

  routingPlanResourceTab(){
    this.router.navigate(['plan-resource'],{
      relativeTo:this.route,
    })

  }
  routingFutureResourceTab(){
    this.router.navigate(['future-resource'],{
      relativeTo:this.route
    })
  }
  
  

}
