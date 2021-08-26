import { PhaseService } from './../../../../service/api/phase.service';
import { APP_ENUMS } from './../../../../../shared/AppEnums';
import { AppComponentBase } from '@shared/app-component-base';
import { result } from 'lodash-es';
import { catchError } from 'rxjs/operators';
import { CheckpointResultDto } from './../../../../service/model/result-review.dto';
import { CheckpointUserResultService } from './../../../../service/api/checkpoint-user-result.service';
import { ActivatedRoute, Router } from '@angular/router';

import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, Inject, OnInit, Injector, inject } from '@angular/core';
import { PhaseDto } from '@app/service/model/phase.dto';

@Component({
  selector: 'app-result-reviewer',
  templateUrl: './result-reviewer.component.html',
  styleUrls: ['./result-reviewer.component.css']
})
export class ResultReviewerComponent extends AppComponentBase implements OnInit {
  
  
   
  public phaseId="";
  public phaseName="";
  public phaseType="";
  public reviewerId="";
  public listYear=[];
  public year=-1;
  public currentYear=new Date().getFullYear();
  public phaseList=[];
  
  public listCheckpointResult:CheckpointResultDto[]=[];
  public listStatusCheckpointResult: string[]=Object.keys(APP_ENUMS.CheckpointUserResult);
  public reviewerTypeList: string[] = Object.keys(this.APP_ENUM.CheckPointUserType);
  public reiviewerStatus: string[] = Object.keys(this.APP_ENUM.CheckPointUserStatus); 
  constructor(
    public injector :Injector,
    public route:ActivatedRoute,
    public checkpointUserResultService: CheckpointUserResultService,
    public phaseService: PhaseService,
    public router:Router){
    super(injector);
    
  }

  ngOnInit(): void {
    this.phaseId=this.route.snapshot.queryParamMap.get("phaseId");
    this.phaseName=this.route.snapshot.queryParamMap.get("phaseName");
    this.phaseType=this.route.snapshot.queryParamMap.get("type");
    this.reviewerId=this.route.snapshot.queryParamMap.get("id");
    console.log(this.phaseType);
    this.getAll();
    for(let i=this.currentYear-2; i< this.currentYear+4;i++){
      this.listYear.push(i);
    }
    
  }
  public getAll(){
    this.checkpointUserResultService.getAllUserResult(this.phaseId).subscribe((data)=>{
      this.listCheckpointResult=data.result;
    })
    this.setParamToUrl();
  }
  create(){

  }
  done(item){
    this.checkpointUserResultService.Done(item.id).subscribe((res)=>{
      abp.notify.success("Done!");
    })
  }
  public getByEnum(enumValue, enumObject){
    for(const key in enumObject){
      if(enumObject[key]==enumValue){
        return key;
      }
    }
  }
  filterYear(year){

    this.phaseService.getAllPhase(this.year).subscribe((data)=>{
      this.phaseList=data.result;
    })
  }
  phaseSelected="";
  filterPhase(id){
    this.phaseSelected=id;
    this.checkpointUserResultService.getAllUserResult(id).subscribe((data)=>{
      this.listCheckpointResult=data.result;
    })
    this.setParamToUrl();
  }
  
  setParamToUrl(){
    this.router.navigate([],{
      queryParams:{
        phaseId:this.phaseSelected,
      },
      queryParamsHandling:"merge"
    })
  }
 

}
