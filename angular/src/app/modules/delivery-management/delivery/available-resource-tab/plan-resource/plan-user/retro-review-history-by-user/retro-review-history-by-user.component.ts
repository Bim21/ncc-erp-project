import { ReviewInternRetroHisotyDto } from './../../../../../../../service/model/delivery-management.dto';
import { RetroReviewInternHistoriesDto } from './../../../../../../../service/model/resource-plan.dto';
import { ResourceManagerService } from '@app/service/api/resource-manager.service';
import { Inject, Input } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { ProjectUserService } from './../../../../../../../service/api/project-user.service';
import { PlanUserComponent } from './../plan-user.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { OnDestroy, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-retro-review-history-by-user',
  templateUrl: './retro-review-history-by-user.component.html',
  styleUrls: ['./retro-review-history-by-user.component.css']
})
export class RetroReviewHistoryByUserComponent extends AppComponentBase
  implements OnInit, OnDestroy {
  emailAddress: string;
  reviewInternRetroHisotyDto: ReviewInternRetroHisotyDto[] = [];
  subscription: Subscription[] = [];
  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: {
      item: {
        emailAddress: string;
      };
    },
    public injector: Injector,
    public dialogRef: MatDialogRef<PlanUserComponent>,
    private projectUserService: ResourceManagerService
  ) {
    super(injector);
    console.log(data)
    this.emailAddress = data.item.emailAddress
  }
  ngOnInit(): void {
    this.GetTimesheetOfRetroReviewInternHistories();
  }
  public GetTimesheetOfRetroReviewInternHistories() {
    this.isLoading = true
    this.projectUserService
      .GetTimesheetOfRetroReviewInternHistories({emails:[this.emailAddress], maxCountHistory: 12})
      .pipe(catchError(this.projectUserService.handleError))
      .subscribe((data) => {
        this.reviewInternRetroHisotyDto = data.result;
        this.isLoading = false
      });
  }
  ngOnDestroy() {
    this.subscription.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}