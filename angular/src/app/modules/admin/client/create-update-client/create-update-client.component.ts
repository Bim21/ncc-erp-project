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
  title:string =""
  public client = {} as ClientDto;
  public clientInvoiceDateSettingList: string[] = Object.keys(this.APP_ENUM.ClientInvoiceDateSetting);
  paymentDueByList = [];
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    public injector: Injector,
    public clientService: ClientService,
    public dialogRef: MatDialogRef<CreateUpdateClientComponent>,) { super(injector) }

  ngOnInit(): void {
    if(this.data.command == "update"){
      this.client = this.data.item;
      this.title = this.data.item.name ? this.data.item.name : ''
    }
    this.getPaymentDueBy();
  }


  getPaymentDueBy() {
    this.paymentDueByList = [
      {value: 0, label: "Last day this month"},
      {value: 15, label: "15th"},
      {value: 1, label: "1st"},
      {value: 2, label: "2nd"},
      {value: 3, label: "3rd"},
      {value: 4, label: "4th"},
      {value: 5, label: "5th"},
      {value: 6, label: "6th"},
      {value: 7, label: "7th"},
      {value: 8, label: "8th"},
      {value: 9, label: "9th"},
      {value: 10, label: "10th"},
      {value: 11, label: "11th"},
      {value: 12, label: "12th"},
      {value: 13, label: "13th"},
      {value: 14, label: "14th"},
      {value: 16, label: "16th"},
      {value: 17, label: "17th"},
      {value: 18, label: "18th"},
      {value: 19, label: "19th"},
      {value: 20, label: "20th"},
      {value: 21, label: "21st"},
      {value: 22, label: "22nd"},
      {value: 23, label: "23rd"},
      {value: 24, label: "24th"},
      {value: 25, label: "25th"},
      {value: 26, label: "26th"},
      {value: 27, label: "27th"},
      {value: 28, label: "28th"},
      {value: 29, label: "29th"},
      {value: 30, label: "30th"}
    ]
  }

  SaveAndClose() {
    if (this.data.command == "create") {
      this.clientService.create(this.client).pipe(catchError(this.clientService.handleError)).subscribe((res) => {
        abp.notify.success("Create Client Successfully!");
        this.dialogRef.close(this.client);
        if(res.result == null || res.result == ""){
          abp.message.success(`<p>Create client name <b>${this.client.name}</b> in <b>PROJECT TOOL</b> successful!</p> 
          <p style='color:#28a745'>Create client name <b>${this.client.name}</b> in <b>TIMESHEET TOOL</b> successful!</p>`, 
         'Create client result',true);
        }
        else{
          abp.message.error(`<p>Create client name <b>${this.client.name}</b> in <b>PROJECT TOOL</b> successful!</p> 
          <p style='color:#dc3545'>${res.result}</p>`, 
          'Create client result',true);
        }
      }, () => { this.isLoading = false })
    }
    else {
      this.clientService.update(this.client).pipe(catchError(this.clientService.handleError)).subscribe((res) => {
        abp.notify.success("Update Client Successfully!");
        this.dialogRef.close(this.client);
      }, () => { this.isLoading = false })
    }

  }

}
