import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import {
  UserDto,
  UserServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { BsModalRef } from "ngx-bootstrap/modal";

@Component({
  selector: "app-add-note-dialog",
  templateUrl: "./add-note-dialog.component.html",
  styleUrls: ["./add-note-dialog.component.css"],
})
export class AddNoteDialogComponent implements OnInit {
  fullName: string;
  id: number;
  note: string = "";
  user = new UserDto();

  saving = false;
  @Output() onSave = new EventEmitter<any>();
  constructor(
    public bsModalRef: BsModalRef,
    public _userService: UserServiceProxy
  ) {}

  ngOnInit(): void {
    this._userService.get(this.id).subscribe((result) => {
      this.user = result;
      console.log("test", this.user);
    });
  }

  SaveAndClose() {
    console.log("save and close", this.note);
    this.bsModalRef.hide();
  }
}
