import { ProjectDetailComponent } from './plan-user/project-detail/project-detail.component';
import { SkillService } from './../../../../../service/api/skill.service';
import { InputFilterDto } from './../../../../../../shared/filter/filter.component';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { availableResourceDto } from './../../../../../service/model/delivery-management.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { PlanUserComponent } from './plan-user/plan-user.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { result } from 'lodash-es';
import { catchError, finalize, filter } from 'rxjs/operators';
// import { DeliveryResourceRequestService } from './../../../../service/api/delivery-request-resource.service';
// import { availableResourceDto } from './../../../../service/model/delivery-management.dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { Component, OnInit, Injector } from '@angular/core';
import { SkillDto } from '@app/service/model/list-project.dto';
import { ProjectResourceRequestService } from '@app/service/api/project-resource-request.service';

@Component({
  selector: 'app-plan-resource',
  templateUrl: './plan-resource.component.html',
  styleUrls: ['./plan-resource.component.css']
})
export class PlanResourceComponent extends PagedListingComponentBase<PlanResourceComponent> implements OnInit {

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function,skill?): void {
    this.isLoading =true;
    this.availableRerourceService.getAvailableResource(request,this.skill).pipe(finalize(()=>{
      finishedCallback();
    }),catchError(this.availableRerourceService.handleError)).subscribe(data=>{
      this.availableResourceList=data.result.items.filter((item=>{
        if(item.userType !==4){
          return item;
        }
      }));
      this.showPaging(data.result,pageNumber);
      this.isLoading =false;
    })
  }
  protected delete(entity: PlanResourceComponent): void {
    
  }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'fullName', comparisions: [0, 6, 7, 8], displayName: "User Name" },
    { propertyName: 'used', comparisions:  [0, 1, 2, 3, 4], displayName: "Used" },

    
  ];
  public availableResourceList:availableResourceDto[]=[];
  public listSkills:SkillDto[]=[];
  public skill='';
  constructor(public injector:Injector,
    private availableRerourceService: DeliveryResourceRequestService,
    private dialog:MatDialog,
    private skillService:SkillService,
    
  
    ) {super(injector) }

  ngOnInit(): void {
    this.refresh();
    this.getAllSkills();
  }
  showDialogPlanUser(command:string,user:any){
    let item={
      userId:user.userId,
      fullName:user.fullName
    }
    
    const show=this.dialog.open(PlanUserComponent, {
      width: '700px',
      disableClose: true,
      data: {
        item:item,
        command:command
      },
    });
    show.afterClosed().subscribe(result => {
      if(result){
        this.refresh()
      }
    });

    
  }
  planUser(user:any){
    this.showDialogPlanUser("plan",user);
  }
  showUserDetail(userId:any){
    
  }
  getAllSkills(){
    this.skillService.getAll().subscribe((data)=>{
      this.listSkills=data.result;
    })
    
  }

  skillsCommas(arr){
    arr=arr.map((item)=>{
      return item.name;
    })
    return arr.join(',')
  }
  projectsCommas(arr){
    arr=arr.map((item)=>{
      return item.projectName;
    })
    return arr.join(',')
  }
 
  showProjectDetail(projectId,projectName){
    const show= this.dialog.open(ProjectDetailComponent,{
      data:{
        projectId:projectId,
        projectName:projectName,
        width:'700px',
      
      }
    })
  }
  
  
  
  





}
