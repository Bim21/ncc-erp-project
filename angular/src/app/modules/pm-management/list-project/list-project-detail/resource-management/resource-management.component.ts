import { ActivatedRoute } from '@angular/router';
import { projectUserDto, projectResourceRequestDto } from './../../../../../service/model/project.dto';
import { ProjectResourceRequestService } from './../../../../../service/api/project-resource-request.service';
import { ProjectUserBillService } from './../../../../../service/api/project-user-bill.service';
import { ProjectUserService } from './../../../../../service/api/project-user.service';
import { AppComponentBase } from 'shared/app-component-base';
import { Component, Injector, OnInit } from '@angular/core';
import { ClientDto } from '@app/service/model/list-project.dto';
import { InputFilterDto } from '@shared/filter/filter.component';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-resource-management',
  templateUrl: './resource-management.component.html',
  styleUrls: ['./resource-management.component.css']
})
export class ResourceManagementComponent extends AppComponentBase implements OnInit {
  private projectId:number;
  public userBillCurrentPage = 1;
  public resourceRequestCurrentPage = 1;
  public userListCurrentPage = 1;
  public projectUserList: projectUserDto[] = [];
  public projectUserBill: projectUserDto[] = [];
  public projectResourceRequestList: projectResourceRequestDto[] = [];

  constructor(injector: Injector, private projectUserService: ProjectUserService, private projectUserBillService: ProjectUserBillService,
    private projectRequestService: ProjectResourceRequestService, private route: ActivatedRoute) { super(injector) }
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'name', displayName: "Name", comparisions: [0, 6, 7, 8] },
  ];
  ngOnInit(): void {
    this.projectId = Number(this.route.snapshot.queryParamMap.get("id"));
    // this.getProjectUser();
    // this.getResourceRequestList();
    this.getUserBill();
  }
  private getProjectUser() {
    // this.projectUserService.getAll().subscribe(dat)
  }
  private getResourceRequestList(): void {
    this.projectRequestService.getAllResourceRequest(this.projectId).pipe(catchError(this.projectRequestService.handleError)).subscribe(data => {
      this.projectResourceRequestList = data.result
    })
  }
  private getUserBill(): void {
    this.projectUserBillService.getAllUserBill(this.projectId).pipe(catchError(this.projectUserBillService.handleError)).subscribe(data => {
      this.projectUserBill = data.result
    })
  }

}
