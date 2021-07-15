import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.css']
})
export class DeliveryComponent extends AppComponentBase implements OnInit {
  currentUrl: string = ""

  constructor(injector: Injector, private router: Router, private route: ActivatedRoute) {
    super(injector)
  }

  ngOnInit(): void {
    this.router.events.subscribe(res => this.currentUrl = this.router.url)

    this.router.navigate(['status-tab'], {
      relativeTo: this.route ,
      replaceUrl: true
    })
  }

  routingStatusTab() {
    this.router.navigate(['status-tab'], {
      relativeTo: this.route

    })
  }
  routingMileStoneTab() {
    this.router.navigate(['milestone-tab'], {
      relativeTo: this.route

    })
  }
  routingWeeklyReportTab(){
    this.router.navigate(['weekly-report-tab'],{
      relativeTo:this.route
    })
    
  }
  routingRequestResourceTab(){
    this.router.navigate(['request-resource-tab'],{
      relativeTo:this.route
    })
  }
  routingAvailableResourceTab(){
    this.router.navigate(['available-resource-tab'],{
      relativeTo:this.route
    })
  }
}
