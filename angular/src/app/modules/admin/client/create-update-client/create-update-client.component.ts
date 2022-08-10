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
  invoiceDateSettingList: [];
  paymentDueByList = [];
  temppaymentDueByList = [];
  searchPaymentDueBy : string;
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
    this.getAllInvoiceDate();
  }
  
  getPaymentDueBy() {
    this.clientService.getAllPaymentDueBy().pipe(catchError(this.clientService.handleError)).subscribe((res) => {
      this.paymentDueByList = res.result;
      this.temppaymentDueByList = res.result;
    })
  }
  getAllInvoiceDate() {
    this.clientService.getAllInvoiceDate().pipe(catchError(this.clientService.handleError)).subscribe((res) => {
      this.invoiceDateSettingList = res.result;
    })
  }
  searchPayment()
  {
    this.paymentDueByList = this.temppaymentDueByList.filter(item => item.text.trim().toLowerCase().includes(this.searchPaymentDueBy.trim().toLowerCase()));
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
