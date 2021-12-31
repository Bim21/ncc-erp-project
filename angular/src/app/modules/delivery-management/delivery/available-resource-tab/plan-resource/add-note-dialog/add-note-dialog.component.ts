import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { UserService } from '@app/service/api/user.service';
import { IUser } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-add-note-dialog',
  templateUrl: './add-note-dialog.component.html',
  styleUrls: ['./add-note-dialog.component.css'],
})
export class AddNoteDialogComponent implements OnInit, OnDestroy {
  fullName: string;
  id: number;
  poolNote: string = '';
  user: IUser;

  saving = false;
  @Output() onSave = new EventEmitter<null>();

  subscription: Subscription[] = [];

  constructor(public bsModalRef: BsModalRef, public userService: UserService) {}

  ngOnInit(): void {
    if (this.id) {
      this.subscription.push(
        this.userService.getOne(this.id).subscribe((response) => {
          this.user = response.result;
          this.poolNote = this.user.poolNote;
        })
      );
    }
  }

  SaveAndClose() {
    this.user = { ...this.user, poolNote: this.poolNote };
    this.saving = true;
    this.subscription.push(
      this.userService
        .updatePoolNote(this.user)
        .pipe(
          finalize(() => {
            this.saving = false;
          })
        )
        .subscribe(() => {
          this.bsModalRef.hide();
          this.onSave.emit();
        })
    );
  }

  ngOnDestroy() {
    this.subscription.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
