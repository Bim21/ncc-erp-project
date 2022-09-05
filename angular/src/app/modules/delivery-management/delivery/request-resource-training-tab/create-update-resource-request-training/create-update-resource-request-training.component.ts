import { SkillService } from './../../../../../service/api/skill.service';
import { catchError } from 'rxjs/operators';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { result } from 'lodash-es';
import { ProjectDto } from './../../../../../service/model/list-project.dto';
import { ListProjectService } from './../../../../../service/api/list-project.service';
import { RequestResourceTrainingDto, ResourceRequestDetailDto } from './../../../../../service/model/delivery-management.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { APP_ENUMS } from './../../../../../../shared/AppEnums';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit, Inject, Injector, ChangeDetectorRef } from '@angular/core';
import * as moment from 'moment';
import * as _ from 'lodash'
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';

@Component({
  selector: 'app-create-update-resource-request-training',
  templateUrl: './create-update-resource-request-training.component.html',
  styleUrls: ['./create-update-resource-request-training.component.css']
})
export class CreateUpdateResourceRequestTrainingComponent extends AppComponentBase implements OnInit {

  public isLoading: boolean = false;
  public listProject: ProjectDto[] = [];
  public priorityList: string[] = Object.keys(this.APP_ENUM.Priority)
  public userLevelList: string[] = Object.keys(this.APP_ENUM.UserLevel)
  public resourceRequestTrainingDto = {} as RequestResourceTrainingDto;
  public title
  public searchProject: string = ""
  public isAddingSkill: boolean = false
  listPosition: any[] = []
  listSkill: any[] = []
  listSkillDetail: any[] = []
  public selectedSkill: any[] = []
  public typeControl: string //include: request, requestProject
  constructor(
    injector: Injector,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private listProjectService: ListProjectService,
    private resourceRequestService: DeliveryResourceRequestService,
    private skillService: SkillService,
    public dialogRef: MatDialogRef<CreateUpdateResourceRequestTrainingComponent>,
    public ref: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getAllProject();
    this.typeControl = this.data.typeControl
    if (this.data.command == 'create') {
      this.resourceRequestTrainingDto = new RequestResourceTrainingDto()
      //if create from resource request project then command = 'create' & projectId != 0
      //assign projectId in dto = projectId
      if (this.data.item.projectId > 0) {
        this.resourceRequestTrainingDto.projectId = this.data.item.projectId
      }
    }
    else {
      this.getResourceRequestById(this.data.item.id)
    }
    this.listPosition = this.data.positions
    this.listSkill = this.data.skills
    this.userLevelList = this.data.levels
  }

  ngAfterViewChecked(): void {
    //Called after every check of the component's view. Applies to components only.
    //Add 'implements AfterViewChecked' to the class.
    this.ref.detectChanges()
  }

  getResourceRequestById(id: number) {
    this.resourceRequestService.getResourceRequestById(id).subscribe(res => {
      this.resourceRequestTrainingDto = res.result
      this.title = res.result.name
    })
  }

  addSkill() {
    this.listSkillDetail.push({ pending: true })
    this.isAddingSkill = true
  }

  SaveAndClose() {
    this.isLoading = true;
    // this.resourceRequestTrainingDto.timeNeed = moment(this.resourceRequestTrainingDto.timeNeed).format("YYYY/MM/DD");
    let request = {
      name: '',
      projectId: this.resourceRequestTrainingDto.projectId,
      timeNeed: this.formatDateYMD(this.resourceRequestTrainingDto.timeNeed),
      position: this.resourceRequestTrainingDto.position,
      level: this.resourceRequestTrainingDto.level,
      priority: this.resourceRequestTrainingDto.priority,
      id: this.resourceRequestTrainingDto.id,
      skillIds: this.resourceRequestTrainingDto.skillIds
    }

    if (this.data.command == "create") {
      request.id = 0
      let createRequest = { ...request, quantity: 1 };
      this.resourceRequestService.create(createRequest).pipe(catchError(this.resourceRequestService.handleError)).subscribe((res) => {
        abp.notify.success("Create Successfully!");
        this.dialogRef.close(this.resourceRequestTrainingDto);
      }, () => this.isLoading = false)
    } else {
      this.resourceRequestService.update(request).pipe(catchError(this.resourceRequestService.handleError)).subscribe((res) => {
        let updateRequest = { ...request, }
        abp.notify.success("Update Successfully!");
        this.dialogRef.close(res.result);
      }, () => this.isLoading = false)
    }

  }

  getAllProject() {
    this.listProjectService.getMyTrainingProjects().subscribe(data => {
      this.listProject = data.result;
    })
  }

  getSkillDetail() {
    if (this.data.command == "edit") {
      this.resourceRequestService.GetSkillDetail(this.data.item.id).pipe(catchError(this.resourceRequestService.handleError)).subscribe(data => {
        this.listSkillDetail = data.result

        let b = this.listSkillDetail.map(item => item.skillId)
        this.resourceRequestTrainingDto.skillIds = b
        console.log(this.resourceRequestTrainingDto.skillIds)
      })
    }
  }

  pushSkill(skill) {
    this.listSkillDetail.push(skill)
  }

  removeSkill(skill) {
    if (skill.id) {
      this.resourceRequestService.deleteSkill(skill.id).pipe(catchError(this.skillService.handleError)).subscribe(rs => {
        abp.notify.success("delete Successful")
        this.getSkillDetail()
      })
    }
    else {
      this.listSkillDetail.splice(this.listSkillDetail.indexOf(skill), 1)
    }
    this.isAddingSkill = false
  }

  saveSkill(skill) {
    if (skill.skillId && skill.quantity > 0) {
      skill.resourceRequestId = this.data.item.id
      this.resourceRequestService.createSkill(skill).pipe(catchError(this.resourceRequestService.handleError)).subscribe(rs => {
        abp.notify.success("added new Skill")
        this.getSkillDetail()
      })
    }
    else {
      abp.notify.error("Skill or Quantity not valid")
    }
    this.isAddingSkill = false

  }

  focusOut() {
    this.searchProject = '';
  }

}
