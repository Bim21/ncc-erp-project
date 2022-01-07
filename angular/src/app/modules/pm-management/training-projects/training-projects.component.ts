import { MatMenuTrigger } from '@angular/material/menu';
import { PERMISSIONS_CONSTANT } from './../../../constant/permission.constant';
import { AppSessionService } from './../../../../shared/session/app-session.service';
import { UserService } from './../../../service/api/user.service';
import { InputFilterDto } from './../../../../shared/filter/filter.component';
import { TrainingProjectDto } from './../../../service/model/project.dto';
import { finalize, catchError } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { ListProjectService } from '@app/service/api/list-project.service';
import { Router } from '@angular/router';
import { CreateEditTrainingProjectComponent } from './create-edit-training-project/create-edit-training-project.component';
import { MatDialog } from '@angular/material/dialog';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';


@Component({
  selector: 'app-training-projects',
  templateUrl: './training-projects.component.html',
  styleUrls: ['./training-projects.component.css']
})
export class TrainingProjectsComponent extends PagedListingComponentBase<TrainingProjectsComponent> implements OnInit {
  PmManager_Project = PERMISSIONS_CONSTANT.PmManager_Project;
  PmManager_Project_Create = PERMISSIONS_CONSTANT.PmManager_Project_Create;
  PmManager_Project_Delete = PERMISSIONS_CONSTANT.PmManager_Project_Delete;
  PmManager_Project_Update = PERMISSIONS_CONSTANT.PmManager_Project_Update;
  PmManager_Project_ViewAll = PERMISSIONS_CONSTANT.PmManager_Project_ViewAll;
  PmManager_Project_ViewDetail = PERMISSIONS_CONSTANT.PmManager_Project_ViewDetail;
  PmManager_Project_ViewOnlyMe = PERMISSIONS_CONSTANT.PmManager_Project_ViewOnlyMe;
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', comparisions: [0, 6, 7, 8], displayName: "Tên dự án", },
    // { propertyName: 'status', comparisions: [0], displayName: "Trạng thái", filterType: 3, dropdownData: this.statusFilterList },
    { propertyName: 'isSent', comparisions: [0], displayName: "Đã gửi weekly", filterType: 2 },
    { propertyName: 'dateSendReport', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian gửi report", filterType: 1 },
    { propertyName: 'startTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian bắt đầu", filterType: 1 },
    { propertyName: 'endTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian kết thúc", filterType: 1 },
    

  ];
  statusFilterList = [{ displayName: "Not Closed", value: 3 },
  { displayName: "InProgress", value: 1 }, { displayName: "Potential", value: 0 },
  { displayName: "Closed", value: 2 },

  ]
  public pmId =  -1;
  public searchPM: string = "";
  @ViewChild(MatMenuTrigger)
  menu: MatMenuTrigger
  contextMenuPosition = {x: '0', y: '0'}
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    let check = false
    let checkFilterPM = false;
    if (this.permission.isGranted(this.PmManager_Project_ViewOnlyMe) && !this.permission.isGranted(this.PmManager_Project_ViewAll)) {
      this.pmId = this.sessionService.userId;
    }
    request.filterItems.forEach(item => {
      if (item.propertyName == "status") {
        check = true
        item.value = this.projectStatus
      }
      if (item.propertyName == "pmId") {
        checkFilterPM = true;
        item.value = this.pmId;
      }
    })
    if (check == false) {
      request.filterItems = this.AddFilterItem(request, "status", this.projectStatus)
    }
    if (!checkFilterPM) {
      request.filterItems = this.AddFilterItem(request, "pmId", this.pmId)
    }
    if (this.projectStatus === -1) {
      request.filterItems = this.clearFilter(request, "status", "")
      check = true

    }


    if (this.pmId == -1) {
      request.filterItems = this.clearFilter(request, "pmId", "")
      checkFilterPM = true

    }
    this.projectService.GetAllTrainingPaging(request).pipe(finalize(() => {
      finishedCallback()
    })).subscribe(data => {
      this.listTrainingProjects = data.result.items;
      if (check == false) {
        request.filterItems = this.clearFilter(request, "status", "");
      }
      if(!checkFilterPM){  
        request.filterItems = this.clearFilter(request, "pmId", "");
      }
      this.showPaging(data.result, pageNumber);
    })
  }
  protected delete(project: any): void {
    abp.message.confirm(
      "Delete project: " + project.name + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.projectService.delete(project.id).pipe(catchError(this.projectService.handleError)).subscribe(() => {
            abp.notify.success("Deleted project: " + project.name);
            this.refresh()
          });
        }
      }
    );
  }
  public listTrainingProjects: TrainingProjectDto[] = [];
  public searchText: string = "";
  public projectStatus: any = 3;
  public pmList: any[] = [];

  constructor(public dialog: MatDialog,
    public sessionService: AppSessionService,
    public injector: Injector,
    public router: Router,
    private projectService: ListProjectService,
    private userService: UserService) {
    super(injector)
  }

  ngOnInit(): void {
    this.refresh();
    this.getAllPM();
  }
  public getAllPM(): void {
    this.userService.GetAllUserActive(true).pipe(catchError(this.userService.handleError))
      .subscribe(data => {
        this.pmList = data.result;
      })
  }
  showDialog(command: string, item?: TrainingProjectDto): void {
    let project = {} as TrainingProjectDto
    if (command == 'edit') {
      project = {
        name: item.name,
        code: item.code,
        startTime: item.startTime,
        endTime: item.endTime,
        pmId: item.pmId,
        id: item.id,
        status: item.status,
      }
    }
    const dialogRef = this.dialog.open(CreateEditTrainingProjectComponent, {
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
  showDetail(id) {
    if (this.permission.isGranted(this.PmManager_Project_ViewDetail)){
      this.router.navigate(['/app/training-project-detail/training-project-general'], {
        queryParams: {
          id: id
        }
      })
    }
  }
  showActions(e){
    e.preventDefault();
    this.contextMenuPosition.x = e.clientX + 'px';
    this.contextMenuPosition.y = e.clientY + 'px';
    this.menu.openMenu();

  }
  create() {
    this.showDialog('create',);
  }
  edit(project: TrainingProjectDto) {
    this.showDialog('edit', project)
  }

}
