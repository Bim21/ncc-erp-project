import { AppComponentBase } from '@shared/app-component-base';
import { ChecklistService } from './../../../../../../service/api/checklist.service';
import { catchError, filter } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { ProjectChecklistService } from './../../../../../../service/api/project-checklist.service';
import { ChecklistTitleDto, projectChecklistDto } from './../../../../../../service/model/checklist.dto';
import { ChecklistCategoryService } from './../../../../../../service/api/checklist-category.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit, Inject, Injector } from '@angular/core';
import { MatCheckboxChange } from '@angular/material/checkbox';

@Component({
  selector: 'app-create-edit-project-checklist',
  templateUrl: './create-edit-project-checklist.component.html',
  styleUrls: ['./create-edit-project-checklist.component.css']
})
export class CreateEditProjectChecklistComponent extends AppComponentBase implements OnInit {

  public listItems: ChecklistTitleDto[] = [];
  public listProjectType: string[] = Object.keys(this.APP_ENUM.ProjectType);

  public projectChecklist = {} as projectChecklistDto;
  public projectId: any;
  public checklistItemId: any;
  public listChecklistItemId: any;
  public checkAll: boolean = false;



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
    this.getChecklistCategory();
  }
  public getChecklistCategory() {
    this.checklistItemService.getAll().subscribe(data => {
      this.listItems = data.result.filter(item => !this.listChecklistItemId.includes(item.id))
    })
  }
  SaveAndClose() {
    this.isLoading = true;
    let item = {
      projectId: this.projectId,
      checkListItemId: this.checklistItemId,
      isActive: false,
    }
    this.arrCheckLists = this.arrCheckLists.map(element => {
      return element.id
    });
    this.projectChecklistService.addCheckListItemByProject(this.projectId, this.arrCheckLists).pipe(catchError(this.projectChecklistService.handleError)).subscribe((res) => {
      abp.notify.success("created outcomeRequest ");
      this.dialogRef.close(this.arrCheckLists);
      this.isLoading = false;
    }, () => this.isLoading = false);



  }


  public getByEnum(enumValue: number, enumObject: any) {
    for (const key in enumObject) {
      if (enumObject[key] == enumValue) {
        return key;
      }
    }
  }
  public checkAllItems: number;
  public arrCheckLists: any = [];


  checkAllTask(event: MatCheckboxChange) {
   
    

      this.listItems.forEach(el => {
        el.createMode = event.source.checked;
        this.arrCheckLists.push(el);
  
      })
      if(event.source.checked==false){
       
        
              this.arrCheckLists=[]
            console.log("hihiiiiiiiiiiiiiiii")
  
      }
  
    // else{
    //   this.listItems.forEach(item=>{
    //     this.arrCheckLists.splice(this.arrCheckLists.indexOf(item),1);
    //   })
    // }
    this.checkAllItems = event.source.checked ? 1 : 0;
    console.log(event.checked)
  }
  updateCheckAllStatus(item, event) {
    if (this.listItems.every(el =>  el.createMode == false )) {
      this.checkAllItems = 0;
      alert("hihi")
      
    }
    else if (this.listItems.every(el =>  el.createMode == true)) {
      this.checkAllItems = 1;
      this.arrCheckLists.push(item)
      alert("huhu")

    }
    else {
      alert("hfdfdfuhu")

      this.checkAllItems = 2;
      if (event.checked) {
        this.arrCheckLists.push(item)
      }
      else if(!event.checked){
        let index = this.arrCheckLists.indexOf(item)
        this.arrCheckLists.splice(index,1)
      }
    }
  }

}
