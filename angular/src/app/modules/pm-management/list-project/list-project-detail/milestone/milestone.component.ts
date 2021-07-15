import { ActivatedRoute } from '@angular/router';
import { finalize, catchError } from 'rxjs/operators';
import { ProjectMilestoneService } from './../../../../../service/api/project-milestone.service';
import { MilestoneDto } from './../../../../../service/model/project.dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';
import { format } from 'path';
import * as moment from 'moment';

@Component({
  selector: 'app-mistone',
  templateUrl: './milestone.component.html',
  styleUrls: ['./milestone.component.css']
})
export class MilestoneComponent extends PagedListingComponentBase<MilestoneDto> implements OnInit {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    request.filterItems.push({
      "propertyName": "projectId",
      "value":this.projectId ,
      "comparision": 0
    })
    this.milestoneService.getAllPaging(request).pipe(finalize(() => {
      finishedCallback();
    }), catchError(this.milestoneService.handleError)).subscribe(data => {
      this.milestoneList = data.result.items
      this.showPaging(data.result, pageNumber);
      console.log(this.milestoneList)
    })
  }
  protected delete(item: MilestoneDto): void {
    abp.message.confirm(
      "Delete Milestone " + item.name + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.milestoneService.delete(item.id).pipe(catchError(this.milestoneService.handleError)).subscribe(() => {
            abp.notify.success("Deleted Milestone " + item.name);
            this.refresh()
          });
        }
      }
    );
    
    
  }
  public isAllowed = true;
  public milestoneList: MilestoneDto[] = [];
  public flagList: string[] = Object.keys(this.APP_ENUM.MilestoneFlag);
  public statusList: string[] = Object.keys(this.APP_ENUM.ProjectMilestoneStatus);
  public command='';
  public isEditing:boolean=false;
  public projectId:any;
  public newMilestone={} as MilestoneDto;


  constructor(injector: Injector,
    private milestoneService: ProjectMilestoneService, private route:ActivatedRoute) { super(injector) }

  ngOnInit(): void {
    this.projectId=this.route.snapshot.queryParamMap.get('id');
    this.refresh();
  }
  addMore() {
    let newMilestone={} as MilestoneDto;
    newMilestone.createMode=true;
    this.milestoneList.push(newMilestone);
    this.isAllowed = false;
    this.isEditing=true;
    this.command = "create";
    
  }
  edit(item:MilestoneDto){
    item.status=this.APP_ENUM.ProjectMilestoneStatus[item.status];
    item.flag=this.APP_ENUM.MilestoneFlag[item.flag];
    this.isAllowed = false;
    this.command = "edit";
    this.isEditing=true;
    console.log("heelo",item)
  }
  public saveMilestoneRequest( item:MilestoneDto): void {
    delete item["createMode"]

    
    if (this.command=="create") {
      item.projectId=this.projectId;
      this. milestoneService.create(item).pipe(catchError(this. milestoneService.handleError)).subscribe(res => {
      this.isEditing=false;
      this.isAllowed=true;
      abp.notify.success("Create Milestone Successful!");
      this.refresh();
      },
      () => {() =>this.isEditing=false;})
      console.log(this.isEditing)
    }
    else {
      this. milestoneService.update(item).pipe(catchError(this. milestoneService.handleError)).subscribe(res => {    
      this.isEditing=false;
      this.isAllowed=true;
      abp.notify.success("Update Milestone Successfully!");
      
      this.refresh();
    
    },
    () => { () => this.isEditing=false;})
    }
    
    }
  

}
