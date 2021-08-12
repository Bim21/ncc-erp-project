import { PhaseDto } from './../../../../service/model/phase.dto';
import { AppComponentBase } from 'shared/app-component-base';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, OnInit, inject, Injector, Inject } from '@angular/core';

@Component({
  selector: 'app-create-edit-phase',
  templateUrl: './create-edit-phase.component.html',
  styleUrls: ['./create-edit-phase.component.css']
})
export class CreateEditPhaseComponent extends AppComponentBase implements OnInit {
  public phase={} as PhaseDto;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    public dialogRef: MatDialogRef<CreateEditPhaseComponent>,
     public injector:Injector) {super(injector) }

  ngOnInit(): void {
    this.phase= this.data.item;
    console.log(this.data.item);
    
  }

}
