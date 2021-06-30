import { MatDialogRef } from '@angular/material/dialog';
import { ChecklistDto } from './../../../../service/model/checklist.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { ChecklistService } from './../../../../service/api/checklist.service';
import { Component, OnInit, Injector } from '@angular/core';
import { ChecklistCategoryService } from '@app/service/api/checklist-category.service';
import { ChecklistTitleComponent } from '../../checklist-title/checklist-title.component';

@Component({
  selector: 'app-create-checklist-item',
  templateUrl: './create-checklist-item.component.html',
  styleUrls: ['./create-checklist-item.component.css']
})
export class CreateChecklistItemComponent extends AppComponentBase implements OnInit {
  public checklist = {} as ChecklistDto;
  public checklistTitleList:ChecklistTitleComponent[]=[]
  public isshowProject:boolean =false
  public projectTypeList:string[] = Object.keys(this.APP_ENUM.ProjectType) 
  constructor(injector:Injector ,private checkListService:ChecklistService,
    private checklisttitleService:ChecklistCategoryService,
    public dialogRef:MatDialogRef<CreateChecklistItemComponent>) { 
    super(injector)
  }

  ngOnInit(): void {
    this.getChecklistTitle()
  }
  public saveAndClose(): void {
    this.isLoading = true;
    this.checkListService.create(this.checklist).subscribe(res => {
      abp.notify.success("create and linked transaction ");
    }, () => this.isLoading = false);
  }
  private getChecklistTitle(): void{
    this.checklisttitleService.getAll().subscribe(data=>{
      this.checklistTitleList = data.result
    })
  }


}
