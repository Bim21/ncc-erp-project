import { CreateUpdateClientComponent } from './create-update-client/create-update-client.component';
import { MatDialog } from '@angular/material/dialog';
import { result } from 'lodash-es';
import { finalize, catchError } from 'rxjs/operators';
import { ClientDto } from '@app/service/model/list-project.dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { ClientService } from './../../../service/api/client.service';
import { Component, OnInit, inject, Injector } from '@angular/core';
import { isNgTemplate } from '@angular/compiler';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent extends PagedListingComponentBase<ClientComponent> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.clientService.getAllPaging(request).pipe(finalize(()=>{
      finishedCallback();
    }),catchError(this.clientService.handleError)).subscribe(data=>{
      this.clientList=data.result.items;
      this.showPaging(data.result, pageNumber)
    })
  }

  protected delete(client: ClientComponent): void {
    abp.message.confirm(
      "Delete Client" + client.name+ "?",
      "",
      (result:boolean)=>{
        if(result){
          this.clientService.deleteClient(client.id).pipe(catchError(this.clientService.handleError)).subscribe((res)=>{
            abp.notify.success("Delele Client"+client.name);
            this.refresh()
          })
        }
      }
    )
  }

  public clientList:ClientDto[]=[];
  constructor(private clientService:ClientService,
    injector:Injector,
    private dialog:MatDialog) {super(injector) }

  ngOnInit(): void {
    this.refresh();
  }
  public showDialog(command:string , Client: any){
    let client={
      name:Client.name,
      code:Client.code,
      id:Client.id
    }
    const show= this.dialog.open(CreateUpdateClientComponent,{
      data:{
        item:client,
        command:command
      },
      width:"700px"
    })
    show.afterClosed().subscribe((res)=>{
      if(res){
        this.refresh();
      }
    })

  }
  public createClient(){
    this.showDialog("create",{});
  }
  public editClient(client){
    this.showDialog("update",client);

  }

}
