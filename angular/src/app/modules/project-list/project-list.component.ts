import { InputFilterDto } from './../../../shared/filter/filter.component';
import { ProjectDto } from './../../service/model/projectDTO';
import { finalize } from 'rxjs/operators';
import { ProjectService } from './../../service/api/project.service';
import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent extends PagedListingComponentBase<ProjectDto> implements OnInit {
  public projectList:ProjectDto[]=[]
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.projectService.getAllPaging(request).pipe(finalize(() => {
      finishedCallback();
    })).subscribe(data => {
      this.projectList = data.result.items;
      this.showPaging(data.result, pageNumber);
    })
  }
  protected delete(entity: any): void {
    throw new Error('Method not implemented.');
  }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: 'Name', comparisions: [0, 6, 7, 8] },
   
  ];

  constructor(injector: Injector, private projectService:ProjectService) {
    super(injector)
   }

  ngOnInit(): void {
    this.refresh()
  }

}
