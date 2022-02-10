import { MatMenuTrigger } from '@angular/material/menu';
import { AppSessionService } from './../../../../shared/session/app-session.service';
import { PERMISSIONS_CONSTANT } from './../../../constant/permission.constant';
import { UserService } from './../../../service/api/user.service';
import { InputFilterDto } from './../../../../shared/filter/filter.component';
import { ListProjectService } from './../../../service/api/list-project.service';
import { Router } from '@angular/router';
import { catchError, finalize } from 'rxjs/operators';
import { CreateEditProductProjectComponent } from './create-edit-product-project/create-edit-product-project.component';
import { MatDialog } from '@angular/material/dialog';
import { ProductProjectDto, ProjectDto } from './../../../service/model/project.dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-product-projects',
  templateUrl: './product-projects.component.html',
  styleUrls: ['./product-projects.component.css']
})
export class ProductProjectsComponent extends PagedListingComponentBase<any> implements OnInit {
  PmManager_Project = PERMISSIONS_CONSTANT.PmManager_Project;
  PmManager_Project_Create = PERMISSIONS_CONSTANT.PmManager_Project_Create;
  PmManager_Project_Delete = PERMISSIONS_CONSTANT.PmManager_Project_Delete;
  PmManager_Project_Update = PERMISSIONS_CONSTANT.PmManager_Project_Update;
  PmManager_Project_ViewAll = PERMISSIONS_CONSTANT.PmManager_Project_ViewAll;
  PmManager_Project_ViewDetail = PERMISSIONS_CONSTANT.PmManager_Project_ViewDetail;
  PmManager_Project_ViewOnlyMe = PERMISSIONS_CONSTANT.PmManager_Project_ViewOnlyMe;
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', comparisions: [0, 6, 7, 8], displayName: "Tên dự án", },
    { propertyName: 'dateSendReport', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian gửi report", filterType: 1 },
    // { propertyName: 'status', comparisions: [0], displayName: "Trạng thái", filterType: 3, dropdownData: this.statusFilterList },
    { propertyName: 'isSent', comparisions: [0], displayName: "Đã gửi weekly", filterType: 2 },
    { propertyName: 'startTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian bắt đầu", filterType: 1 },
    { propertyName: 'endTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian kết thúc", filterType: 1 },


  ];

  public sortWeeklyReport: number = 0;
  public weeklyReport: number = -1;
  public pmId =  -1;
  public searchPM: string = "";
  statusFilterList = [{ displayName: "Not Closed", value: 3 },
  { displayName: "InProgress", value: 1 }, { displayName: "Potential", value: 0 },
  { displayName: "Closed", value: 2 },
  ]

  weeklyReportFilterList = [
    {
      displayName: "All",
      value: -1,
    },
    {
      displayName: "Penalized",
      value: 0,
    },
    {
      displayName: "Not Penalized",
      value: 1,
    },
  ]

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    let check = false
    let checkFilterPM = false;
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

    this.projectService.GetAllProductPaging(request).pipe(finalize(() => {
      finishedCallback()
    })).subscribe(data => {
      // this.listProductProjects = data.result.items;
      this.listProductProjects = data.result.items.filter((project: ProductProjectDto) => (
        this.weeklyReport === 0
        ? (!project.isSent) || (project.isSent && this.isReportLate(project.timeSendReport))
        : this.weeklyReport === 1
        ? project.isSent && !this.isReportLate(project.timeSendReport)
        : project
      ))
      if (check == false) {
        request.filterItems = this.clearFilter(request, "status", "");
      }
      if (!checkFilterPM) {
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
  public listProductProjects: ProductProjectDto[] = [];
  public projectStatus: any = 3;
  public pmList: any[] = [];
  @ViewChild(MatMenuTrigger)
  menu: MatMenuTrigger
  contextMenuPosition = { x: '0', y: '0' }
  constructor(public injector: Injector,
    public dialog: MatDialog,
    public router: Router,
    private userService: UserService,
    private projectService: ListProjectService,
    public sessionService: AppSessionService) {
    super(injector)
    this.pmId = this.sessionService.userId
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
        startTime: item.startTime,
        endTime: item.endTime,
        pmId: item.pmId,
        id: item.id,
        status: item.status
      }
    }
    const dialogRef = this.dialog.open(CreateEditProductProjectComponent, {
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
  showActions(e) {
    e.preventDefault();
    this.contextMenuPosition.x = e.clientX + 'px';
    this.contextMenuPosition.y = e.clientY + 'px';
    this.menu.openMenu();
  }
  showDetail(id: any) {
    if (this.permission.isGranted(this.PmManager_Project_ViewDetail)) {
      this.router.navigate(['app/product-project-detail/product-project-general'], {
        queryParams: {
          id: id
        }
      })
    }

  }

  isReportLate(time: string | null) {
    if(!time) return false;
    const timeSendReport = moment(new Date(time))
    const penaltyTime = moment().day(2).hour(15).minute(0).second(0);
    return timeSendReport.isAfter(penaltyTime)
  }

  handleSortWeeklyReportClick () {
    this.sortWeeklyReport = (this.sortWeeklyReport + 1) % 3;
    if(!this.sortWeeklyReport) {
      this.refresh();
      return;
    }

    this.listProductProjects.sort((project1: ProductProjectDto, project2: ProductProjectDto) => {
      if(project1.timeSendReport && !project2.timeSendReport) {
        return -1;
      }

      if(!project1.timeSendReport && project2.timeSendReport) {
        return 1;
      }

      let time1: number = 0, time2: number = 0;
      if(project1.timeSendReport && project2.timeSendReport) {
        time1 = new Date(project1.timeSendReport).getTime();
        time2 = new Date(project2.timeSendReport).getTime();
      }

      return this.sortWeeklyReport === 1
      ? time1 - time2
      : this.sortWeeklyReport === 2
      ? time2 - time1
      : 0;
    })
  }

}
