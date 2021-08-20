import { MatDialog } from '@angular/material/dialog';
import { CreateEditReviewUserComponent } from './create-edit-review-user/create-edit-review-user.component';
import { catchError } from 'rxjs/operators';
import { ReviewUserDto } from './../../../service/model/reviewUser.dto';
import { SetupReviewerService } from './../../../service/api/setup-reviewer.service';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';
import { ResultReviewerComponent } from '../set-up-reviewer/result-reviewer/result-reviewer.component';

@Component({
  selector: 'app-review-user',
  templateUrl: './review-user.component.html',
  styleUrls: ['./review-user.component.css']
})
export class ReviewUserComponent extends PagedListingComponentBase<ResultReviewerComponent> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.pageSizeType=50;
    this.checkpointUserService.getAllReviewForSelf(request).pipe(catchError(this.checkpointUserService.handleError)).subscribe((data)=>{
      this.reviewUserForSelf= data.result.items;
      this.showPaging(data.result, pageNumber);
    })
    this.checkpointUserService.getAllReviewBySelf(request).pipe(catchError(this.checkpointUserService.handleError)).subscribe((data)=>{
      this.reviewUserBySelf= data.result.items;
      this.showPaging(data.result, pageNumber);
    })
  }
  protected delete(item): void {
    abp.message.confirm(
      "Delete Review "+ "?",
      "",
      (result: boolean) => {
        if (result) {
          // this.checkpointUserService.delete(item.reviewerId).subscribe(() => {
          //   abp.notify.success("Deleted Reviewed");
          //   this.refresh();
          // });
          this.checkpointUserService.delete(item.userId).subscribe(() => {
            abp.notify.success("Deleted Reviewed");
            this.refresh();
          });
        }
      }
    );
  }
  public reviewUserForSelf: ReviewUserDto[]=[];
  public reviewUserBySelf:ReviewUserDto[]=[];

  constructor(public injector:Injector,
    public checkpointUserService: SetupReviewerService,
    public dialog:MatDialog
    ) {super(injector) }

  ngOnInit(): void {
  
    this.refresh();
  }
  showDialog(command:string, Review:any){
    let review={} as ReviewUserDto;
    if(command== "edit"){
      review={
        reviewerId:Review.reviewerId,
        reviewerName:Review.reviewerName,
        userId:Review.userId,
        userName:Review.userName,
        status:Review.status,
        type:Review.type,
        id:Review.id,
        note:Review.note,
        score:Review.score,

      }
    }
    const show = this.dialog.open(CreateEditReviewUserComponent, {
      data: {
        item: review,
        command: command,
      },
      width: "700px",
      disableClose: true,
    });
    show.afterClosed().subscribe(result => {
      if (result) {
        this.refresh();
      }
    });
  }
  create(){
    this.showDialog("create", {})
  }
  edit(review:ReviewUserDto){
    this.showDialog("edit",review)
  }

}
