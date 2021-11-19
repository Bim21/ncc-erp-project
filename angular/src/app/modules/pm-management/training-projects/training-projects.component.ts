import { InputFilterDto } from './../../../../shared/filter/filter.component';
import { TrainingProjectDto } from './../../../service/model/project.dto';
import { finalize, catchError } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { ListProjectService } from '@app/service/api/list-project.service';
import { Router } from '@angular/router';
import { CreateEditTrainingProjectComponent } from './create-edit-training-project/create-edit-training-project.component';
import { MatDialog } from '@angular/material/dialog';
import { Component, OnInit, Injector } from '@angular/core';


@Component({
  selector: 'app-training-projects',
  templateUrl: './training-projects.component.html',
  styleUrls: ['./training-projects.component.css']
})
export class TrainingProjectsComponent extends PagedListingComponentBase<TrainingProjectsComponent> implements OnInit {
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', comparisions: [0, 6, 7, 8], displayName: "Tên dự án", },
    { propertyName: 'pmName', comparisions: [0, 6, 7, 8], displayName: "Tên PM", },
    // { propertyName: 'status', comparisions: [0], displayName: "Trạng thái", filterType: 3, dropdownData: this.statusFilterList },
    { propertyName: 'isSent', comparisions: [0], displayName: "Đã gửi weekly", filterType: 2 },
    { propertyName: 'startTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian bắt đầu", filterType: 1 },
    { propertyName: 'endTime', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian kết thúc", filterType: 1 },
    { propertyName: 'dateSendReport', comparisions: [0, 1, 2, 3, 4], displayName: "Thời gian gửi report", filterType: 1 },

  ];
  statusFilterList = [{ displayName: "Not Closed", value: 3 },
  { displayName: "InProgress", value: 1 }, { displayName: "Potential", value: 0 },
  { displayName: "Closed", value: 2 },

  ]
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    let check=false
    request.filterItems.forEach(item =>{
      if(item.propertyName == "status"){
        check =true
        item.value = this.projectStatus
      }
    })
    if(check == false){
      request.filterItems = this.AddFilterItem(request, "status", this.projectStatus)
    }
    if(this.projectStatus === -1){
      request.filterItems = this.clearFilter(request, "status", "")
      check =true
      
    }
    this.projectService.GetAllTrainingPaging(request).pipe(finalize(() => {
      finishedCallback()
    })).subscribe(data => {
      this.listTrainingProjects = data.result.items;
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
  public projectStatus: any=3;
  constructor(public dialog: MatDialog,
    public injector: Injector,
    public router: Router,
    private projectService: ListProjectService) {
    super(injector)
  }

  ngOnInit(): void {
    this.refresh();
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
    this.router.navigate(['/app/training-project-detail/training-project-general'], {
      queryParams: {
        id: id
      }
    })

  }
  create() {
    this.showDialog('create',);
  }
  edit(project: TrainingProjectDto) {
    this.showDialog('edit', project)
  }

}
