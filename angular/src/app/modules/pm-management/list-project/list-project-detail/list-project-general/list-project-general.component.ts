import { ProjectDto } from './../../../../../service/model/list-project.dto';
import { catchError } from 'rxjs/operators';
import { ListProjectService } from './../../../../../service/api/list-project.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-list-project-general',
  templateUrl: './list-project-general.component.html',
  styleUrls: ['./list-project-general.component.css']
})
export class ListProjectGeneralComponent implements OnInit {
  public readMode: boolean = true;
  private projectId:number
  constructor(private projectService:ListProjectService, private route:ActivatedRoute) { }
  public project ={} as ProjectDto
  ngOnInit(): void {
    this.projectId = Number(this.route.snapshot.queryParamMap.get("id"));
    this.getProjectDetail()
    
  }
  getProjectDetail(){
    this.projectService.getProjectById(this.projectId).pipe(catchError(this.projectService.handleError)).subscribe(data=>{
        this.project = data.result
    })
  }
  editRequest() {
    this.readMode = false
  }
  

}
