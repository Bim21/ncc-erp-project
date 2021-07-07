import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-weekly-report-tab',
  templateUrl: './weekly-report-tab.component.html',
  styleUrls: ['./weekly-report-tab.component.css']
})
export class WeeklyReportTabComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit(): void {
  }
  showDetail(){
    this.router.navigate(['app/weeklyReportTabDetail'], {
      queryParams: {
        
      }
    })
  }

}
