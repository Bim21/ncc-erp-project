import { ProjectdetailDto } from './../../../../../service/model/projectDetail.dto';
import { catchError } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ListProjectService } from '@app/service/api/list-project.service';

@Component({
  selector: 'app-project-description',
  templateUrl: './project-description.component.html',
  styleUrls: ['./project-description.component.css']
})
export class ProjectDescriptionComponent implements OnInit {
  projectId
  projectDetail={} as ProjectdetailDto
  constructor(private projectService:ListProjectService, private router:Router, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.projectId = this.route.snapshot.queryParamMap.get("id")
  }
  getProjectDetail(){
    this.projectService.getProjectDetail(this.projectId).pipe(catchError(this.projectService.handleError)).subscribe(data=>{
      this.projectDetail =data.result
    })
  }
}
