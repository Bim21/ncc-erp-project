import { SetupReviewerService } from './../../../../service/api/setup-reviewer.service';
import { UserService } from './../../../../service/api/user.service';
import { ReviewUserDto } from './../../../../service/model/reviewUser.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, OnInit, Injector, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-edit-review-user',
  templateUrl: './create-edit-review-user.component.html',
  styleUrls: ['./create-edit-review-user.component.css']
})
export class CreateEditReviewUserComponent extends AppComponentBase implements OnInit {
  public review= {} as ReviewUserDto;
  public userList=[];
  public reviewerList=[];
  public reviewerTypeList: string[] = Object.keys(this.APP_ENUM.CheckPointUserType);
  
  constructor(@Inject(MAT_DIALOG_DATA ) public data: any,public injector:Injector,
  public userService: UserService,
  public reviewerService: SetupReviewerService,
  public dialogRef:MatDialogRef<CreateEditReviewUserComponent>) {
    super(injector);
  }

  ngOnInit(): void {
    this.review=this.data.item;
  }
  SaveAndClose(){

  }

  // getAllUsers(){
    
  //   this.reviewerService.getUserUnreview(this.phaseId).subscribe((data)=>{
  //     this.userList=data.result;
  //   })
  // }
  getAllReviewers(){
    this.userService.GetAllUserActive(true).subscribe((data)=>{
      this.reviewerList=data.result;
      
    })
    
  }
}
