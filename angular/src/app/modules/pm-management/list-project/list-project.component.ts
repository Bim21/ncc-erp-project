import { UserDto } from './../../../../shared/service-proxies/service-proxies';
import { UserService } from './../../../service/api/user.service';
import { InputFilterDto, DropDownDataDto } from './../../../../shared/filter/filter.component';
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
  statusFilterList = [{ displayName: "Not Closed", value: 3 },
  { displayName: "InProgress", value: 1 }, { displayName: "Potential", value: 0 },
  { displayName: "Closed", value: 2 },

  ]
  projectTypeParam = Object.entries(this.APP_ENUM.ProjectType).map(item => {
    return {
      displayName: item[0],
      value: item[1]
    }
  })
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', comparisions: [0, 6, 7, 8], displayName: "Tên project", },
    { propertyName: 'clientName', comparisions: [0, 6, 7, 8], displayName: "Tên khách hàng", },
    { propertyName: 'pmName', comparisions: [0, 6, 7, 8], displayName: "Tên PM", },
    { propertyName: 'status', comparisions: [0], displayName: "Trạng thái", filterType: 3, dropdownData: this.statusFilterList },
    { propertyName: 'isCharge', comparisions: [0], displayName: "Charge khách hàng", filterType: 2 },
    { propertyName: 'isSent', comparisions: [0], displayName: "Đã gửi weekly", filterType: 2 },
    { propertyName: 'startTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian bắt đầu", filterType: 1 },
    { propertyName: 'endTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian kết thúc", filterType: 1 },
    { propertyName: 'timeSendReport', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian gửi report", filterType: 1 },
    { propertyName: 'TimeAndMaterials', comparisions: [0], displayName: "Loại project", filterType: 3, dropdownData: this.projectTypeParam },

  ];
  private userList: UserDto[] = [];
  public projectStatus: any = 3;
  projectTypeList: string[] = Object.keys(this.APP_ENUM.ProjectType);
  projectWeeklys: string[] = Object.keys(this.APP_ENUM.WeeklySent);

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

  constructor(injector: Injector, public dialog: MatDialog, private userService: UserService,
    public listProjectService: ListProjectService) {
    super(injector);
  }

  ngOnInit(): void {
    this.getAllUser();
    this.refresh();
  }


  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    if (this.projectStatus !== "" || this.projectStatus == 0) {
      request.filterItems = this.AddFilterItem(request, "status", this.projectStatus)
    }
    if (this.projectStatus === "") {
      request.filterItems = this.clearFilter(request, "status", this.projectStatus)
    }
    this.listProjectService
      .getAllPaging(request)
      .pipe(finalize(() => {
        finishedCallback();
      }))
      .subscribe((result: PagedResultResultDto) => {
        this.listProjects = result.result.items;
        this.showPaging(result.result, pageNumber);
        request.filterItems = this.clearFilter(request, "status", this.projectStatus)
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
      width: '700px',
      maxHeight: '100vh',
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
      this.router.navigate(['app/list-project-detail/list-project-general'], {
        queryParams: {
          id: id,
        }
      })
    }

  }
  getAllUser() {
    this.userService.GetAllUserActive(false).subscribe(data => {
      this.userList = data.result
    })
  }
  public filterUser(userId: number) {
    return this.userList.filter(item => item.id == userId)[0];
  }
  public getByEnum(enumValue: number, enumObject: any) {
    for (const key in enumObject) {
      if (enumObject[key] == enumValue) {
        return key;
      }
    }
  }
  onStatuschange() {

  }
  clearStatus() {
    this.projectStatus = "";
    this.refresh();
  }
}

