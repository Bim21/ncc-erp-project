import { CreateEditPhaseComponent } from './create-edit-phase/create-edit-phase.component';
import { MatDialog } from '@angular/material/dialog';
import { result } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import { PhaseDto } from './../../../service/model/phase.dto';
import { PhaseService } from './../../../service/api/phase.service';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-phase',
  templateUrl: './phase.component.html',
  styleUrls: ['./phase.component.css']
})
export class PhaseComponent extends AppComponentBase implements OnInit {
  // protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
  //   this.pageSizeType=50;
  //   this.phaseService.getAll(request).pipe(catchError(this.phaseService)).subscribe((data)=>{
  //     this.phaseList= data.result.items;
  //     this.showPaging(data.result, pageNumber);
  //   })
  // }
  // protected delete(entity: PhaseComponent): void {
  //   throw new Error('Method not implemented.');
  // }

  public phaseList: PhaseDto[] = [];
  public searchText="";
  constructor(public injector: Injector,
    public phaseService: PhaseService,
    public dialog: MatDialog
  ) { super(injector) }

  ngOnInit(): void {
    this.getAllPhase();
  }
  public getAllPhase() {
    this.phaseService.getAll().subscribe((data) => {
      this.phaseList = data.result;
    })
  }
  changeStatus(phase) {
    if (phase.isCriteria) {
      this.phaseService.DeActive(phase.id).subscribe(rs => {
        abp.notify.success("DeActive phase: " + phase.name);

      })
    }
    else {
      this.phaseService.Active(phase.id).subscribe(rs => {
        abp.notify.success("Active phase: " + phase.name)

      })
    }
    this.getAllPhase();

  }
  showDialog(command: String, Phase:any):void{
    let phase={} as PhaseDto;
    if(command== "edit"){
      phase={
        name: Phase.name,
        year: Phase.year,
        parentId: 1,
        type:Phase.type,
        status: Phase.status,
        isCriteria: Phase.isCriteria,
        index:Phase.index,
        id: Phase.id
      }
    }
    const show = this.dialog.open(CreateEditPhaseComponent, {
      data: {
        item: phase,
        command: command,
      },
      width: "700px",
      disableClose: true,
    });
    show.afterClosed().subscribe(result => {
      if (result) {
        this.getAllPhase();
      }
    });

  }
  public create(){
    this.showDialog("create", {});
  }
  public edit(phase:PhaseDto){
    this.showDialog("edit",phase)
  }
  public done(phase){
    this.phaseService.Done(phase.id).subscribe((res)=>{
      abp.notify.success("Active phase: " + phase.name)
    })
  }
  
  

}
