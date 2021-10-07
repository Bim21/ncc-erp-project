import { ProjectdetailDto } from './../../../../../service/model/projectDetail.dto';
import { catchError } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, Injector } from '@angular/core';
import { ListProjectService } from '@app/service/api/list-project.service';
import { AppComponentBase } from '@shared/app-component-base';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';

@Component({
  selector: 'app-project-description',
  templateUrl: './project-description.component.html',
  styleUrls: ['./project-description.component.css']
})
export class ProjectDescriptionComponent extends AppComponentBase{
  projectId
  projectDetail={} as ProjectdetailDto
  PmManager_Project_UpdateProjectDetail = PERMISSIONS_CONSTANT.PmManager_Project_UpdateProjectDetail
  constructor(private projectService:ListProjectService, private router:Router, private route:ActivatedRoute, injector:Injector) { 
    super(injector)
  }
  readMode:boolean = true;
  ngOnInit(): void {
    this.projectId = this.route.snapshot.queryParamMap.get("id")
    this.getProjectDetail()
  }
  getProjectDetail(){
    this.projectService.getProjectDetail(this.projectId).pipe(catchError(this.projectService.handleError)).subscribe(data=>{
      this.projectDetail =data.result
    })
  }
  updateInfo(){
    this.isLoading =true
    this.projectService.UpdateProjectDetail(this.projectDetail).pipe(catchError(this.projectService.handleError)).subscribe(rs=>{
      abp.notify.success("Update successful")
    this.isLoading =false
    this.readMode = true
    },
    ()=> this.isLoading =false)
  }
  editRequest(){
    this.readMode =false
  }
}
