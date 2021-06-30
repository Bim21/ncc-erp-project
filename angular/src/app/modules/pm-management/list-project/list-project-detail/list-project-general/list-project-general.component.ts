import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-list-project-general',
  templateUrl: './list-project-general.component.html',
  styleUrls: ['./list-project-general.component.css']
})
export class ListProjectGeneralComponent implements OnInit {
  public readMode: boolean = true;
  public requestId: any;

  constructor() { }

  ngOnInit(): void {

  }
  editRequest() {
    this.readMode = false
  }
  

}
