import { catchError, finalize } from 'rxjs/operators';
import { ChecklistService } from './../../../service/api/checklist.service';
import { Component, Injector, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { InputFilterDto } from '@shared/filter/filter.component';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { CreateChecklistItemComponent } from './create-checklist-item/create-checklist-item.component';
import { ChecklistDto } from '@app/service/model/checklist.dto';

@Component({
  selector: 'app-checklist',
  templateUrl: './checklist.component.html',
  styleUrls: ['./checklist.component.css']
})
export class ChecklistComponent extends PagedListingComponentBase<any> implements OnInit {
  checklistList:ChecklistDto[] =[]
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.isTLoading = true
    this.checklistService.getAllPaging(request).pipe(finalize(() => {
      finishedCallback();
    }), catchError( this.checklistService.handleError)).subscribe(data => {
      this.isTLoading = false
      this.checklistList = data.result.items;
      this.showPaging(data.result, pageNumber);
    },
    ()=>{
      this.isLoading = false
    })
  }
  protected delete(entity: any): void {
    throw new Error('Method not implemented.');
  }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'Name', comparisions: [0, 6, 7, 8], displayName: "Name" },
    { propertyName: 'Title', comparisions: [0, 6, 7, 8], displayName: "Title" },
    { propertyName: 'Mandatory', comparisions: [0, 6, 7, 8], displayName: "Mandatory" },
    { propertyName: 'projectType', comparisions: [0, 6, 7, 8], displayName: "Loại dự án" },
    { propertyName: 'personInCharge', comparisions: [0, 6, 7, 8], displayName: "Person in Charge" },
  ];

  constructor(injector: Injector,public dialog: MatDialog, private checklistService:ChecklistService) {
    super(injector);
  }

  ngOnInit(): void {
    this.refresh()
  }
  createChecklistTitle() {
    this.showDialogChecklistTitle();
  }
  showDialogChecklistTitle() {
    const dialogRef = this.dialog.open(CreateChecklistItemComponent, {
      width: '700px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}
