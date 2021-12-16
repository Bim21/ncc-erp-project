import { catchError } from 'rxjs/operators';
import { ClientService } from './../../../../service/api/client.service';
import { ClientDto } from '@app/service/model/list-project.dto';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, OnInit, Injector, Inject } from '@angular/core';

@Component({
  selector: 'app-create-update-client',
  templateUrl: './create-update-client.component.html',
  styleUrls: ['./create-update-client.component.css']
})
export class CreateUpdateClientComponent extends AppComponentBase implements OnInit {

  public client = {} as ClientDto;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    public injector: Injector,
    public clientService: ClientService,
    public dialogRef: MatDialogRef<CreateUpdateClientComponent>,) { super(injector) }

  ngOnInit(): void {
    this.client = this.data.item;
    console.log(this.client)
  }
  SaveAndClose() {
    if (this.data.command == "create") {
      this.clientService.create(this.client).pipe(catchError(this.clientService.handleError)).subscribe((res) => {
        abp.notify.success("Create Client Successfully!");
        this.dialogRef.close(this.client);
      }, () => { this.isLoading = false })
    }
    else {
      this.clientService.update(this.client).pipe(catchError(this.clientService.handleError)).subscribe((res) => {
        abp.notify.success("Create Client Successfully!");
        this.dialogRef.close(this.client);
      }, () => { this.isLoading = false })
    }

  }

}
