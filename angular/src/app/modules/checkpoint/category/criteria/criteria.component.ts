import { CreateEditCriteriaComponent } from './create-edit-criteria/create-edit-criteria.component';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { result } from 'lodash-es';
import { catchError } from 'rxjs/operators';
import { CriteriaDto } from './../../../../service/model/criteria-category.dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';
import { CriteriaService } from '@app/service/api/criteria.service';

@Component({
  selector: 'app-criteria',
  templateUrl: './criteria.component.html',
  styleUrls: ['./criteria.component.css']
})
export class CriteriaComponent extends PagedListingComponentBase<CriteriaComponent> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.criteriaService.getAllPaging(request).pipe(catchError(this.criteriaService.handleError)).subscribe((data)=>{
      this.criteriaList=data.result.items;
      this.showPaging(data.result, pageNumber);
    },()=>{})
  }
  protected delete(item): void {
    abp.message.confirm(
      "Delete criteria category"+ item.name +"?",
      "",
      (result:boolean)=>{
        if(result){
          this.criteriaService.deleteCriteria(item.id).pipe(catchError(this.criteriaService.handleError)).subscribe((res)=>{
            abp.notify.success("Delete "+ item.name+ " successfully!");
            this.refresh();
          })
        }

      }
      
    )
  }

  public criteriaList: CriteriaDto[]=[];
  public criteriaCategoryId;
  constructor(public injector:Injector,
    private criteriaService: CriteriaService,
    private route:ActivatedRoute,
    private dialog:MatDialog) {super(injector) }

  ngOnInit(): void {
    this.criteriaCategoryId = this.route.snapshot.queryParamMap.get("id");
    this.refresh();
  }
  public showDialog(command: string, Criteria){
    let item={
      name:Criteria.name,
      weight:Criteria.weight,
      note:Criteria.note,
      criteriaCategoryId:Criteria.criteriaCategoryId,
      id:Criteria.id

    }
    const show= this.dialog.open(CreateEditCriteriaComponent,{
      data:{
        item:item,
        command:command,
        
      },
      width: "700px"
    })
    show.afterClosed().subscribe((res)=>{
      if(res){
        this.refresh();
      }
    })
  }
  
  public create(){
    this.showDialog("create",{})
  }
  public edit(item){
    this.showDialog("edit", item);
  }


}
