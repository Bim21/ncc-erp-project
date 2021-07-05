import { InputFilterDto } from './../../../../shared/filter/filter.component';
import { PERMISSIONS_CONSTANT } from './../../../constant/permission.constant';
import { ListProjectService } from './../../../service/api/list-project.service';
import { ProjectDto } from './../../../service/model/list-project.dto';
import { Component, Injector, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PagedListingComponentBase, PagedRequestDto, PagedResultResultDto } from '@shared/paged-listing-component-base';
import { finalize, catchError } from 'rxjs/operators';
import { CreateEditListProjectComponent } from './create-edit-list-project/create-edit-list-project.component';
@Component({
  selector: 'app-list-project',
  templateUrl: './list-project.component.html',
  styleUrls: ['./list-project.component.css']
})
export class ListProjectComponent extends PagedListingComponentBase<any> implements OnInit {
  PmManager_Project = PERMISSIONS_CONSTANT.PmManager_Project;
  PmManager_Project_Create = PERMISSIONS_CONSTANT.PmManager_Project_Create;
  PmManager_Project_Delete = PERMISSIONS_CONSTANT.PmManager_Project_Delete;
  PmManager_Project_Update = PERMISSIONS_CONSTANT.PmManager_Project_Update;
  PmManager_Project_ViewAll = PERMISSIONS_CONSTANT.PmManager_Project_ViewAll;
  PmManager_Project_ViewDetail = PERMISSIONS_CONSTANT.PmManager_Project_ViewDetail;
  PmManager_Project_ViewOnlyMe = PERMISSIONS_CONSTANT.PmManager_Project_ViewOnlyMe;


  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', comparisions: [0, 6, 7, 8], displayName: "Name" },
    { propertyName: 'clientName', comparisions: [0, 6, 7, 8], displayName: "Client name" },
    { propertyName: 'pmName', comparisions: [0, 6, 7, 8], displayName: "PM name" },
    { propertyName: 'startTime', comparisions: [0, 1, 2], displayName: "Start time", isDate: true },
    { propertyName: 'endTime', comparisions: [0, 1, 2], displayName: "End time", isDate: true },





  ];



  projectTypeList: string[] = Object.keys(this.APP_ENUM.ProjectType)

  setValueProjectType(projectType, enumObject) {
    for (const key in enumObject) {
      if (enumObject[key] == projectType) {
        return key;
      }
    }
  }


  listProjects: ProjectDto[] = [];
  protected delete(entity: any): void {
    abp.message.confirm(
      "Delete project: " + entity.name + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.listProjectService.delete(entity.id).pipe(catchError(this.listProjectService.handleError)).subscribe(() => {
            abp.notify.success("Deleted project: " + entity.name);
            this.refresh()
          });
        }
      }
    );
  }

  constructor(injector: Injector, public dialog: MatDialog,
    public listProjectService: ListProjectService) {
    super(injector);
  }

  ngOnInit(): void {
    this.refresh();
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

  createProject() {
    this.showDialogListProject('create');
  }
  editProject(project: ProjectDto) {
    this.showDialogListProject('edit', project);
  }
  showDialogListProject(command: string, item?: ProjectDto): void {
    let project = {} as ProjectDto
    if (command == 'edit') {
      project = {
        name: item.name,
        code: item.code,
        projectType: item.projectType,
        startTime: item.startTime,
        endTime: item.endTime,
        status: item.status,
        clientId: item.clientId,
        isCharge: item.isCharge,
        pmId: item.pmId,
        id: item.id
      }
    }
    const dialogRef = this.dialog.open(CreateEditListProjectComponent, {
      data: {
        command: command,
        dialogData: project
      },
      width: '800px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.refresh();
      }
    });
  }
  showDetail(id: any) {
    if (this.permission.isGranted(this.PmManager_Project_ViewDetail)) {
      this.router.navigate(['app/list-project-detail'], {
        queryParams: {
          id: id,
        }
      })
    }

  }
}
