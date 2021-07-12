import { AppComponentBase } from 'shared/app-component-base';
import { ChecklistService } from './../../../../../../service/api/checklist.service';
import { catchError, filter } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { ProjectChecklistService } from './../../../../../../service/api/project-checklist.service';
import { ChecklistTitleDto, projectChecklistDto } from './../../../../../../service/model/checklist.dto';
import { ChecklistCategoryService } from './../../../../../../service/api/checklist-category.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit, Inject, Injector } from '@angular/core';

@Component({
  selector: 'app-create-edit-project-checklist',
  templateUrl: './create-edit-project-checklist.component.html',
  styleUrls: ['./create-edit-project-checklist.component.css']
})
export class CreateEditProjectChecklistComponent extends AppComponentBase implements OnInit {

  public listItems: ChecklistTitleDto[] = [];

  public projectChecklist = {} as projectChecklistDto;
  public projectId: any;
  public checklistItemId: any;
  public listChecklistItemId: any;



  constructor(@Inject(MAT_DIALOG_DATA) public data: any, injector: Injector,
    private checklistCategoryService: ChecklistCategoryService,
    private projectChecklistService: ProjectChecklistService,
    private route: ActivatedRoute,
    public dialogRef: MatDialogRef<CreateEditProjectChecklistComponent>,
    private checklistItemService: ChecklistService) {
    super(injector)
  }

  ngOnInit(): void {
    this.projectId = this.route.snapshot.queryParamMap.get('id');
    this.listChecklistItemId = this.data.listChecklistItem;
    console.log(this.listChecklistItemId);
    this.getChecklistCategory();
  }
  public getChecklistCategory() {
    this.checklistItemService.getAll().subscribe(data => {
      this.listItems = data.result.filter(item => !this.listChecklistItemId.includes(item.id))
      console.log(this.listItems);
    })
  }
  SaveAndClose() {
    this.isLoading = true;
    let item = {
      projectId: this.projectId,
      checkListItemId: this.checklistItemId,
      isActive: false,
    }
    this.projectChecklistService.create(item).pipe(catchError(this.projectChecklistService.handleError)).subscribe((res) => {
      abp.notify.success("created outcomeRequest ");
      this.dialogRef.close(item);
      this.isLoading = false;
    }, () => this.isLoading = false);


  }

}
