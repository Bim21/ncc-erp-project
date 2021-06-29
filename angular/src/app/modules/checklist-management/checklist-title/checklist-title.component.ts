import { ChecklistTitleDto } from './../../../service/model/checklist.dto';
import { ChecklistCategoryService } from './../../../service/api/checklist-category.service';
import { finalize, catchError } from 'rxjs/operators';
import { Component, Injector, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { InputFilterDto } from '@shared/filter/filter.component';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { CreateEditChecklistTitleComponent } from './create-edit-checklist-title/create-edit-checklist-title.component';

@Component({
  selector: 'app-checklist-title',
  templateUrl: './checklist-title.component.html',
  styleUrls: ['./checklist-title.component.css']
})
export class ChecklistTitleComponent extends PagedListingComponentBase<any> implements OnInit {
  public checkListTitleList: ChecklistTitleDto[] = []
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'Name', comparisions: [0, 6, 7, 8], displayName: "Name" },
  ];
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.isLoading = true
    this.checklistTileService.getAllPaging(request).pipe(finalize(() => {
      finishedCallback();
    }), catchError( this.checklistTileService.handleError)).subscribe(data => {
      this.isLoading = false
      this.checkListTitleList = data.result.items;
      this.showPaging(data.result, pageNumber);
    },
    ()=>{
      this.isLoading = false
    })
  }
  protected delete(entity: any): void {
    // throw new Error('Method not implemented.');
  }

  constructor(injector: Injector, public dialog: MatDialog, private checklistTileService: ChecklistCategoryService) {
    super(injector);
  }
  public createChecklistTitle(): void {
    this.showDialogChecklistTitle();
  }
  public showDialogChecklistTitle(): void {
    const dialogRef = this.dialog.open(CreateEditChecklistTitleComponent, {
      width: '500px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this.refresh()
      }
    });
  }

  ngOnInit(): void {
    this.refresh()
  }

}
