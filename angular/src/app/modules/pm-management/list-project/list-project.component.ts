import { ListProjectService } from './../../../service/api/list-project.service';
import { ProjectDto } from './../../../service/model/list-project.dto';
import { Component, Injector, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PagedListingComponentBase, PagedRequestDto, PagedResultResultDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';
import { CreateEditListProjectComponent } from './create-edit-list-project/create-edit-list-project.component';
@Component({
  selector: 'app-list-project',
  templateUrl: './list-project.component.html',
  styleUrls: ['./list-project.component.css']
})
export class ListProjectComponent extends PagedListingComponentBase<any> implements OnInit {

  projectTypeList:string[] = Object.keys(this.APP_ENUM.ProjectType) 

  setValueProjectType(projectType, enumObject) {
    for (const key in enumObject) {
      if(enumObject[key] ==projectType){
        return key;
      }
    }
  }


  listProjects: ProjectDto[] = [];
  protected delete(entity: any): void {
    throw new Error('Method not implemented.');
  }
  
  constructor(injector: Injector,public dialog: MatDialog,
    public listProjectService: ListProjectService) {
      super(injector);
    }
    
    ngOnInit(): void {
      this.refresh();
    console.log('abc',this.dayProjectTypeList);
    
    
  }

  
  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    this.listProjectService
      .getAllPaging(request)
      .pipe(finalize(() => {
        finishedCallback();
      }))
      .subscribe((result: PagedResultResultDto) => {
        this.listProjects = result.result.items;        
        this.showPaging(result.result, pageNumber);
      });
  }

  createListProject() {
    this.showDialogListProject('create', {});
  }
  showDialogListProject(command: string,item: any): void {
    let request = {}
    if(command == 'edit') {
      request = {
        name: item.name,
        code: item.code,
        projectType: item.projectType,
        startTime: item.startTime,
        endTime: item.endTime,
        status: item.status,
        clientId: item.clientId,
        clientName: item.clientName,
        isCharge: item.isCharge,
        pmId: item.pmId,
        id: item.id
      }
    }
    const dialogRef = this.dialog.open(CreateEditListProjectComponent, {
      data: {
        command: command,
        item: request
      },
      width: '800px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      this.refresh();
    });
  }
}
