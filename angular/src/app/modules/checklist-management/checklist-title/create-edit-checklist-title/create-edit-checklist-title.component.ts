import { ChecklistTitleDto } from './../../../../service/model/checklist.dto';
import { MatDialogRef } from '@angular/material/dialog';
import { AppComponentBase } from '@shared/app-component-base';
import { ChecklistCategoryService } from './../../../../service/api/checklist-category.service';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-create-edit-checklist-title',
  templateUrl: './create-edit-checklist-title.component.html',
  styleUrls: ['./create-edit-checklist-title.component.css']
})
export class CreateEditChecklistTitleComponent extends AppComponentBase implements OnInit {
  isEdit: boolean;
  constructor(private checkListTitleService: ChecklistCategoryService, injector: Injector,
  public dialogRef: MatDialogRef<CreateEditChecklistTitleComponent>) {
    super(injector)
  }
  checklistTitle = {} as ChecklistTitleDto;
  ngOnInit(): void {
  }
  public saveAndClose(): void {
    this.isLoading = true;
    this.checkListTitleService.create(this.checklistTitle).subscribe(res => {
      abp.notify.success("create and linked transaction ");
    }, () => this.isLoading = false);
  }
}
