import { TimesheetProjectService } from '@app/service/api/timesheet-project.service';
import { PagedResultDto } from './../../../../shared/paged-listing-component-base';
import { AppSessionService } from './../../../../shared/session/app-session.service';
import { result } from 'lodash-es';
import { UserDto } from './../../../../shared/service-proxies/service-proxies';
import { UserService } from './../../../service/api/user.service';
import { InputFilterDto, DropDownDataDto } from './../../../../shared/filter/filter.component';
import { PERMISSIONS_CONSTANT } from './../../../constant/permission.constant';
import { ListProjectService } from './../../../service/api/list-project.service';
import { ProjectDto } from './../../../service/model/list-project.dto';
import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PagedListingComponentBase, PagedRequestDto, PagedResultResultDto } from '@shared/paged-listing-component-base';
import { finalize, catchError } from 'rxjs/operators';
import { CreateEditListProjectComponent } from './create-edit-list-project/create-edit-list-project.component';
import { MatMenuTrigger } from '@angular/material/menu';
import { ProductProjectDto } from '@app/service/model/project.dto';
import * as moment from 'moment';

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
  PmManager_Project_Close = PERMISSIONS_CONSTANT.PmManager_Project_Close;
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

  public sortWeeklyReport: number = 0;
  private userList: UserDto[] = [];
  public projectStatus: any = 3;
  projectTypeList: string[] = Object.keys(this.APP_ENUM.ProjectType);
  projectWeeklys: string[] = Object.keys(this.APP_ENUM.WeeklySent);
  public pmList: any[] = [];
  public tempPMList: any[] = [];
  public pmId = -1;
  public searchPM: string = "";
  @ViewChild(MatMenuTrigger)
  menu: MatMenuTrigger;
  contextMenuPosition = { x: '0px', y: '0px' };
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', comparisions: [0, 6, 7, 8], displayName: "Tên dự án", },
    { propertyName: 'projectType', comparisions: [0], displayName: "Loại dự án", filterType: 3, dropdownData: this.projectTypeParam },
    { propertyName: 'clientName', comparisions: [0, 6, 7, 8], displayName: "Tên khách hàng", },
    // { propertyName: 'status', comparisions: [0], displayName: "Trạng thái", filterType: 3, dropdownData: this.statusFilterList },
    { propertyName: 'isCharge', comparisions: [0], displayName: "Charge khách hàng", filterType: 2 },
    { propertyName: 'isSent', comparisions: [0], displayName: "Đã gửi weekly", filterType: 2 },
    { propertyName: 'dateSendReport', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian gửi report", filterType: 1 },
    { propertyName: 'startTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian bắt đầu", filterType: 1 },
    { propertyName: 'endTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian kết thúc", filterType: 1 },
  ];

  setValueProjectType(projectType, enumObject) {
    for (const key in enumObject) {
      if (enumObject[key] == projectType) {
        return key;
      }
    }
  }
  listProjects: ProductProjectDto[] = [];
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
    public listProjectService: ListProjectService ,
    private timesheetProjectService: TimesheetProjectService,
    public sessionService:AppSessionService) {
    super(injector);
    this.pmId = Number(this.sessionService.userId);
  }

  ngOnInit(): void {
    this.getAllUser();
    this.refresh();
    this.getAllPM();
  }

  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    let check = false;
    let checkFilterPM = false;


    if(this.permission.isGranted( this.PmManager_Project_ViewOnlyMe) && !this.permission.isGranted(this.PmManager_Project_ViewAll)){
      this.pmId = Number(this.sessionService.userId);
    }

    if(this.sortWeeklyReport) {
      request.sort = 'timeSendReport';
      request.sortDirection = this.sortWeeklyReport - 1;
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
    if(!checkFilterPM){
      request.filterItems = this.AddFilterItem(request, "pmId", this.pmId)
    }

    if (this.projectStatus == -1) {
      request.filterItems = this.clearFilter(request, "status", "")
      check = true
    }
    if (this.pmId == -1) {
      request.filterItems = this.clearFilter(request, "pmId", "")
      checkFilterPM = true
    }

    this.listProjectService
      .getAllPaging(request)
      .pipe(finalize(() => {
        finishedCallback();
      }))

      .subscribe((result: PagedResultResultDto) => {
        this.listProjects = result.result.items;
        this.showPaging(result.result, pageNumber)
        // request.filterItems = this.clearFilter(request, "status", this.projectStatus)
        if (check == false) {
          request.filterItems = this.clearFilter(request, "status", "");
        }
        if(!checkFilterPM){
          request.filterItems = this.clearFilter(request, "pmId", "");
        }
      });
  }

  public getAllPM(): void {
    this.timesheetProjectService.getAllPM().pipe(catchError(this.userService.handleError))
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
        projectType: item.projectType,
        startTime: item.startTime,
        endTime: item.endTime,
        status: item.status,
        clientId: item.clientId,
        isCharge: item.isCharge,
        chargeType : item.chargeType,
        pmId: item.pmId,
        id: item.id,
        currencyId: item.currencyId,
        requireTimesheetFile: item.requireTimesheetFile
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
  // showDetail(project: any) {
  //   if (this.permission.isGranted(this.PmManager_Project_ViewDetail)) {
  //     this.router.navigate(['app/list-project-detail/list-project-general'], {
  //       queryParams: {
  //         id: project.id,
  //         type: project.projectType
  //       }
  //     })
  //   }

  // } 

  showActions(e , item){
    e.preventDefault();
    this.contextMenuPosition.x = e.clientX + 'px';
    this.contextMenuPosition.y = e.clientY + 'px';
    this.menu.openMenu();
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

  changeTextProjectType(projectType: string) {
    return projectType === 'TAM' ? 'T&M' : projectType
  }

  clearStatus() {
    this.projectStatus = "";
    this.refresh();
  }
  isReportLate(time: string | null) {
    if(!time) return false;
    const timeSendReport = moment(new Date(time))
    const penaltyTime = moment().day(2).hour(15).minute(0).second(0); // 15:00:00 thứ 3
    return timeSendReport.isAfter(penaltyTime)
  }

  handleSortWeeklyReportClick () {
    this.sortWeeklyReport = (this.sortWeeklyReport + 1) % 3;
    this.refresh();
  }

  handleSelectionPunishChange = () => {
    this.pageSize = 100;
    this.refresh();
  }

  presentPUs(arr) {
    if(arr.length == 0){
      return "";
    }
    arr = arr.map((item) => {
      return `- User ${item.fullName}`;
    })
    return `<div style='text-align:left'> And release following resources: <br/> ${arr.join('<br/>')} </div>`
  }

  protected closeProject(project: any): void {
    let item = {
      id: project.id    
    }

    this.listProjectService.getAllWorkingUserFromProject(project.id).pipe(catchError(this.listProjectService.handleError)).subscribe((res) => {
      let message = this.presentPUs(res.result);
      abp.message.confirm(
        `${message}`,
        `Are your sure close project: ${project.name}?`,
        (result: boolean) => {
          if (result) {
            this.listProjectService.closeProject(item).pipe(catchError(this.listProjectService.handleError)).subscribe((res) => {
              abp.notify.success("Update status project: " + project.name);
              if(res.result == "update-only-project-tool"){
                abp.notify.success("Update status project: "+ project.name);
              }
              else if(res.result == null || res.result == ""){
                abp.message.success(`<p>Update status project name <b>${project.name}</b> in <b>PROJECT TOOL</b> successful!</p> 
                <p style='color:#28a745'>Update status project name <b>${project.name}</b> in <b>TIMESHEET TOOL</b> successful!</p>`, 
               'Update status project result',true);
              }
              else{
                abp.message.error(`<p>Update status project <b>${project.name}</b> in <b>PROJECT TOOL</b> successful!</p> 
                <p style='color:#dc3545'>${res.result}</p>`, 
                'Update status project result',true);
              }
              this.refresh()
            });
          }
        },
        true
      );
    });
  }
}
