import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-checklist-item',
  templateUrl: './create-checklist-item.component.html',
  styleUrls: ['./create-checklist-item.component.css']
})
export class CreateChecklistItemComponent implements OnInit {
  checklist: string = '';
  checklistItem = {}
  checked: boolean;
  constructor() { }

  ngOnInit(): void {
  }

}
