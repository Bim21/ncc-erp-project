import { catchError } from 'rxjs/operators';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { result } from 'lodash-es';
import { ProjectDto } from './../../../../../service/model/list-project.dto';
import { ListProjectService } from './../../../../../service/api/list-project.service';
import { RequestResourceDto } from './../../../../../service/model/delivery-management.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { APP_ENUMS } from './../../../../../../shared/AppEnums';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit, Inject, Injector } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-create-update-resource-request',
  templateUrl: './create-update-resource-request.component.html',
  styleUrls: ['./create-update-resource-request.component.css']
})
export class CreateUpdateResourceRequestComponent extends AppComponentBase implements OnInit {
  public isLoading:boolean=false;
  public listProject:ProjectDto[]=[];
  public statusList:string[]=Object.keys(this.APP_ENUM.ResourceRequestStatus);
  public resourceRequestDto={} as RequestResourceDto;
  public title 
  constructor(injector: Injector,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private listProjectService:ListProjectService,
    private resourceRequestService:DeliveryResourceRequestService,
    public  dialogRef: MatDialogRef<CreateUpdateResourceRequestComponent>) { super(injector);
    }

  ngOnInit(): void {
    this.getAllProject();
    this.resourceRequestDto=this.data.item;
    this.title = this.resourceRequestDto.name
  }
  SaveAndClose(){
    this.isLoading =true;
    this.resourceRequestDto.timeNeed=moment(this.resourceRequestDto.timeNeed).format("YYYY/MM/DD");
    if(this.resourceRequestDto.timeDone){
      this.resourceRequestDto.timeDone=moment(this.resourceRequestDto.timeDone).format("YYYY/MM/DD");
    }
    if(this.data.command=="create"){
      this.resourceRequestService.create(this.resourceRequestDto).pipe(catchError(this.resourceRequestService.handleError)).subscribe((res)=>{
        abp.notify.success("Create Successfully!");
        this.dialogRef.close(this.resourceRequestDto);
      },()=>this.isLoading=false)
    }else{
      this.resourceRequestService.update(this.resourceRequestDto).pipe(catchError(this.resourceRequestService.handleError)).subscribe((res)=>{
        abp.notify.success("Create Successfully!");
        this.dialogRef.close(this.resourceRequestDto);
      },()=>this.isLoading=false)
    }

  }
  getAllProject(){
    this.listProjectService.getAll().subscribe(data=>{
      this.listProject=data.result;
    })
  }

}
