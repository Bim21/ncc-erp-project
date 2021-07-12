import { ProjectUserService } from './../../../../../../service/api/project-user.service';
import { catchError } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DeliveryResourceRequestService } from './../../../../../../service/api/delivery-request-resource.service';
import { Component, Inject, OnInit } from '@angular/core';
import { userToRequestDto } from '@app/service/model/delivery-management.dto';
import * as moment from 'moment';

@Component({
  selector: 'app-add-user-to-request',
  templateUrl: './add-user-to-request.component.html',
  styleUrls: ['./add-user-to-request.component.css']
})
export class AddUserToRequestComponent implements OnInit {

  public userToRequest={} as userToRequestDto;
  public editToRequest={} as userToRequestDto;

  public isDisable:boolean=false;
  constructor(@Inject (MAT_DIALOG_DATA) public data: any
    ,private resourceRequestService:DeliveryResourceRequestService,
    private route:ActivatedRoute,
    public dialogRef: MatDialogRef<AddUserToRequestComponent>,
    private projectUserService:ProjectUserService) { }

  ngOnInit(): void {
    this.userToRequest=this.data.item;
    this.userToRequest.userId=this.data.userId;
    this.userToRequest.resourceRequestId=Number(this.route.snapshot.queryParamMap.get('id'));
    
  }
  SaveAndClose(){
    this.userToRequest.startTime=moment(this.userToRequest.startTime).format("YYYY/MM/DD");
    if(this.data.command=="create"){
      this.resourceRequestService.AddUserToRequest(this.userToRequest).pipe(catchError(this.resourceRequestService.handleError)).subscribe((res) => {
        abp.notify.success("Created timesheet detail successfully");
        this.dialogRef.close(this.userToRequest)
      }, () => this.isDisable = false);
    }else{
      this.projectUserService.update(this.userToRequest).pipe(catchError(this.resourceRequestService.handleError)).subscribe((res) => {
        abp.notify.success("Created timesheet detail successfully");
        this.dialogRef.close(this.userToRequest)
      }, () => this.isDisable = false);
    }
  }
  

}
