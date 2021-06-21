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
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'Name', comparisions: [0, 6, 7, 8], displayName: "Name" },
  ];
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    throw new Error('Method not implemented.');
  }
  protected delete(entity: any): void {
    throw new Error('Method not implemented.');
  }

  constructor(injector: Injector,public dialog: MatDialog) {
    super(injector);
  }
  createChecklistTitle() {
    this.showDialogChecklistTitle();
  }
  showDialogChecklistTitle() {
    const dialogRef = this.dialog.open(CreateEditChecklistTitleComponent, {
      width: '500px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  ngOnInit(): void {
  }

}
