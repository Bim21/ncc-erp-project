import { Component, Inject, Injector, OnInit, Input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { catchError } from 'rxjs/operators';
import { ProjectCriteriaDto, CriteriaDto, CriteriaCategoryDto } from './../../../../../../../service/model/criteria-category.dto';
import { CriteriaService } from '@app/service/api/criteria.service';
import { AppComponentBase } from '@shared/app-component-base';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-guide-line-dialog',
  templateUrl: './guide-line-dialog.component.html',
  styleUrls: ['./guide-line-dialog.component.css']
})
export class GuideLineDialogComponent extends AppComponentBase implements OnInit {

  isEditMode = false;
  public criteria = {} as ProjectCriteriaDto;
  public trustedHtml: SafeHtml;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public injector: Injector,
    public dialogRef: MatDialogRef<GuideLineDialogComponent>,
    private criteriaService: CriteriaService,
    private sanitizer: DomSanitizer
  ) {
    super(injector);
    this.trustedHtml = this.sanitizer.bypassSecurityTrustHtml(this.data.guideline);
  }

  @Input() item: any;

  ngOnInit(): void {
    this.criteria = this.item;
  }

  SaveAndClose() {
    const criteriaToUpdate = { ...this.criteria }; // create a copy of the criteria object to avoid mutating the original data
    criteriaToUpdate.guideline = this.data.guideline;
    criteriaToUpdate.id = this.data.id;
    criteriaToUpdate.name = this.data.criteriaName;
    criteriaToUpdate.isActive = this.data.isActive;

    this.criteriaService.update(criteriaToUpdate)
      .pipe(catchError(this.criteriaService.handleError))
      .subscribe((res) => {
        if (res.success) {
          abp.notify.success("Update Successfully!");
          this.dialogRef.close(criteriaToUpdate);
        }
      }, () => { this.isLoading = false });
  }


}
