import { result } from 'lodash-es';
import { Component, OnInit, Injector, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ProjectResourceRequestService } from '@app/service/api/project-resource-request.service';
import { projectForDM } from '@app/service/model/list-project.dto';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {

  public projectId='';
  public projectDetail={} as projectForDM;
  constructor(@Inject( MAT_DIALOG_DATA) public data:any,
    private resourceRequestService: ProjectResourceRequestService,
    public dialogRef: MatDialogRef<ProjectDetailComponent>,
  ) { }

  ngOnInit(): void {
    this.projectId=this.data.projectId;
    console.log(this.data)
    this.getProjectDetail();
  }
  public getProjectDetail(){
    this.resourceRequestService.getProjectForDM(this.projectId).subscribe((data)=>{
      this.projectDetail=data.result;
    })
  }


}
