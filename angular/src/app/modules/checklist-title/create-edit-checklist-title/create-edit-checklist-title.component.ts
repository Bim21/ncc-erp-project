import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-edit-checklist-title',
  templateUrl: './create-edit-checklist-title.component.html',
  styleUrls: ['./create-edit-checklist-title.component.css']
})
export class CreateEditChecklistTitleComponent implements OnInit {
  isEdit: boolean;
  isDisable: boolean = false;
  constructor() { }
  checklistTitle = {} as checklistTitleDto;
  ngOnInit(): void {
  }
  // saveAndClose() {
  //   this.isDisable = true
  //   if (this.data.command == "create") {
  //     this._bankService.create(this.bank).subscribe(res => {
  //       this.notify.success(this.l('Create bank successfully'));
  //       this.dialogRef.close()
  //     }, () => this.isDisable = false);
  //   }
  //   else {
  //     this._bankService.update(this.bank).subscribe(res => {
  //       this.notify.success(this.l('Update bank successfully'));
  //       this.dialogRef.close()
  //     }, () => {
  //       this.isDisable = false
  //     });

  //   }
  // }
}
export class checklistTitleDto {
  name: string;
  code: string;
}