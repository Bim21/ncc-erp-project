import { catchError } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { ListProjectService } from './../../../../../service/api/list-project.service';
import { ProjectdetailDto } from './../../../../../service/model/projectDetail.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-product-project-description',
  templateUrl: './product-project-description.component.html',
  styleUrls: ['./product-project-description.component.css']
})
export class ProductProjectDescriptionComponent extends AppComponentBase {
  public projectId
  projectDetail = {} as ProjectdetailDto
  constructor(public injector: Injector, private projectService: ListProjectService, private router: Router, private route: ActivatedRoute) {
    super(injector)
  }
  readMode: boolean = true;
  ngOnInit(): void {
    this.projectId = this.route.snapshot.queryParamMap.get("id")
    this.getProjectDetail()
  }
  getProjectDetail() {
    this.projectService.getProjectDetail(this.projectId).pipe(catchError(this.projectService.handleError)).subscribe(data => {
      this.projectDetail = data.result
    })
  }
  updateInfo() {
    this.isLoading = true
    this.projectService.UpdateProjectDetail(this.projectDetail).pipe(catchError(this.projectService.handleError)).subscribe(rs => {
      abp.notify.success("Update successful")
      this.isLoading = false
      this.readMode = true
    },
      () => this.isLoading = false)
  }
  editRequest() {
    this.readMode = false
  }
}
