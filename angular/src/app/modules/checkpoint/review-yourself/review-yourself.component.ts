import { AppSessionService } from './../../../../shared/session/app-session.service';
import { PhaseService } from '@app/service/api/phase.service';
import { getAllPhaseDto } from './../../../service/model/phase.dto';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-review-yourself',
  templateUrl: './review-yourself.component.html',
  styleUrls: ['./review-yourself.component.css']
})
export class ReviewYourselfComponent implements OnInit {

  constructor(public phaseService: PhaseService,public sessionService:AppSessionService) { }
  public year = new Date().getFullYear();
  public listYear: number[] = [];
  private currentYear = new Date().getFullYear();

  ngOnInit(): void {
    for (let i = this.currentYear - 4; i < this.currentYear + 2; i++) {
      this.listYear.push(i)
    }
    this.getAllPhase();
    console.log("id",this.sessionService.userId)
  }
  phaseList: getAllPhaseDto[] = []
  getAllPhase() {
    this.phaseService.getAllPhase(this.year).subscribe((data) => {
      this.phaseList = data.result;
      console.log(this.phaseList)
    })
  }
  getShowHistory(){
    
  }
  
}
