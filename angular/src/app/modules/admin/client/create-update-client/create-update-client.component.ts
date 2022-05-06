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
  public getTextClientInvoiceDateSetting(enumValue) {
    if(enumValue==this.APP_ENUM.ClientInvoiceDateSetting.LastDateThisMonth){
      return "Last date this month";
    }
    return "First date next month";
  }
  getPaymentDueBy() {
    this.paymentDueByList = [
      {value: 0, label: "Last date next month"},
      {value: 15, label: "15th next month"},
      {value: 1, label: "1st next month"},
      {value: 2, label: "2nd next month"},
      {value: 3, label: "3rd next month"},
      {value: 4, label: "4th next month"},
      {value: 5, label: "5th next month"},
      {value: 6, label: "6th next month"},
      {value: 7, label: "7th next month"},
      {value: 8, label: "8th next month"},
      {value: 9, label: "9th next month"},
      {value: 10, label: "10th next month"},
      {value: 11, label: "11th next month"},
      {value: 12, label: "12th next month"},
      {value: 13, label: "13th next month"},
      {value: 14, label: "14th next month"},
      {value: 16, label: "16th next month"},
      {value: 17, label: "17th next month"},
      {value: 18, label: "18th next month"},
      {value: 19, label: "19th next month"},
      {value: 20, label: "20th next month"},
      {value: 21, label: "21st next month"},
      {value: 22, label: "22nd next month"},
      {value: 23, label: "23rd next month"},
      {value: 24, label: "24th next month"},
      {value: 25, label: "25th next month"},
      {value: 26, label: "26th next month"},
      {value: 27, label: "27th next month"},
      {value: 28, label: "28th next month"},
      {value: 29, label: "29th next month"},
      {value: 30, label: "30th next month"}
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
