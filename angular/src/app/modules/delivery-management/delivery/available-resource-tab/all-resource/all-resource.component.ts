import { MatDialog } from '@angular/material/dialog';
import { SkillService } from './../../../../../service/api/skill.service';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { availableResourceDto } from './../../../../../service/model/delivery-management.dto';
import { InputFilterDto } from './../../../../../../shared/filter/filter.component';
import { PlanResourceComponent } from './../plan-resource/plan-resource.component';
import { catchError, finalize } from 'rxjs/operators';
import { PagedRequestDto } from './../../../../../../shared/paged-listing-component-base';
import { SkillDto } from './../../../../../service/model/list-project.dto';
import { PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { PlanUserComponent } from './../plan-resource/plan-user/plan-user.component';
import { ProjectDetailComponent } from './../plan-resource/plan-user/project-detail/project-detail.component';
import { Component, OnInit, Injector, inject } from '@angular/core';

@Component({
  selector: 'app-all-resource',
  templateUrl: './all-resource.component.html',
  styleUrls: ['./all-resource.component.css']
})
export class AllResourceComponent extends PagedListingComponentBase<any> implements OnInit {

  public listSkills: SkillDto[] = [];
  public skill = '';
  public skillsParam = [];
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function, skill?): void {

    // this.pageSizeType = request.maxResultCount
    // this.pageSizeType = this.pageSize
    // request.maxResultCount = this.pageSize
    this.isLoading = true;
    // let check = false
    request.filterItems.forEach(item => {
      // if (item.filterType == 4) {
      //   request.filterItems = this.clearFilter(request, "skill", 0)
      //   check = true
      //   this.skill = item.value
      // }

    })
    // let check2 = false
    // request.filterItems.forEach(item => {
    //   if (item.propertyName == "used") {
    //     check2 = true
    //   }
    // })
    // if (check2 == false) {
    //   request.filterItems = this.AddFilterItem(request, "used", 0)
    // }

    this.availableRerourceService.getAvailableResource(request, this.skill).pipe(finalize(() => {
      finishedCallback();
    }), catchError(this.availableRerourceService.handleError)).subscribe(data => {
      this.availableResourceList = data.result.items
      // this.availableResourceList = data.result.items.filter((item => {
      //   if (item.userType !== 4) {
      //     return item;
      //   }
      // }));
      // if (check == true) {
      //   request.filterItems.push({ propertyName: 'skill', comparision: 0, value: this.skill, filterType: 4, dropdownData: this.skillsParam })
      //   this.skill = ''
      // }
      // if (check2 == false) {
      //   request.filterItems = this.clearFilter(request, "used", "")
      // }


      this.showPaging(data.result, pageNumber);
      this.isLoading = false;

      // if(this.count>0){

      // }
      // request.filterItems.forEach(item =>{
      //   if(item.filterType ==4 ){
      //     this.count++
      //     if(this.count>0){
      //       this.inputFilters= this.inputFilters.filter(filter => filter.filterType!=4)
      //     }
      //   }
      // })
      // console.log("aa",this.inputFilters)
    })
  }
  protected delete(entity: PlanResourceComponent): void {

  }
  userTypeParam = Object.entries(this.APP_ENUM.UserType).map((item) => {
    return {
      displayName: item[0],
      value: item[1]
    }

  })
  branchParam = Object.entries(this.APP_ENUM.UserBranch).map((item) => {
    return {
      displayName: item[0],
      value: item[1]
    }
  })

  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'fullName', comparisions: [0, 6, 7, 8], displayName: "User Name" },
    { propertyName: 'used', comparisions: [0, 1, 2, 3, 4], displayName: "Used" },
    { propertyName: 'userType', comparisions: [0], displayName: "User Type", filterType: 3, dropdownData: this.userTypeParam },
    { propertyName: 'branch', comparisions: [0], displayName: "Branch", filterType: 3, dropdownData: this.branchParam },
    { propertyName: 'projectName', comparisions: [0,6,7,8], displayName: "Project Name" },
    { propertyName: 'projectUserPlans', comparisions: [0,6,7,8], displayName: "Project User Plans" }
  ];

  public availableResourceList: availableResourceDto[] = [];

  constructor(public injector: Injector,
    private availableRerourceService: DeliveryResourceRequestService,
    private dialog: MatDialog,
    private skillService: SkillService,


  ) { super(injector) }

  ngOnInit(): void {
    this.pageSizeType = 100
    this.changePageSize();
    this.getAllSkills();
  }
  showDialogPlanUser(command: string, user: any) {
    let item = {
      userId: user.userId,
      fullName: user.fullName
    }

    const show = this.dialog.open(PlanUserComponent, {
      width: '700px',
      disableClose: true,
      data: {
        item: item,
        command: command
      },
    });
    show.afterClosed().subscribe(result => {
      if (result) {
        this.refresh()
      }
    });


  }


  planUser(user: any) {
    this.showDialogPlanUser("plan", user);
  }
  showUserDetail(userId: any) {

  }

  getAllSkills() {
    this.skillService.getAll().subscribe((data) => {
      this.listSkills = data.result;
      this.skillsParam = data.result.map(item => {
        return {
          displayName: item.name,
          value: item.id
        }
      })
      this.FILTER_CONFIG.push({ propertyName: 'skill', comparisions: [0], displayName: "Skill", filterType: 4, dropdownData: this.skillsParam },
      )
    })

  }

  skillsCommas(arr) {
    arr = arr.map((item) => {
      return item.name;
    })
    return arr.join(', ')
  }
  projectsCommas(arr) {
    arr = arr.map((item) => {
      return item.projectName;
    })
    return arr.join(', ')
  }

  showProjectDetail(projectId, projectName) {
    const show = this.dialog.open(ProjectDetailComponent, {
      data: {
        projectId: projectId,
        projectName: projectName,
      },
      width: '95vw',
      height: '90vh',
    })
  }
}
