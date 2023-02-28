import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateEditCriteriaComponent } from '@app/modules/checkpoint/category/criteria/create-edit-criteria/create-edit-criteria.component';

@Component({
  selector: 'app-guide-line-dialog',
  templateUrl: './guide-line-dialog.component.html',
  styleUrls: ['./guide-line-dialog.component.css']
})
export class GuideLineDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  public injector: Injector,
  public dialogRef: MatDialogRef<CreateEditCriteriaComponent>,) { }

  ngOnInit(): void {
    console.log(this.data);

  }

}
