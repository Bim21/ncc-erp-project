import { SkillService } from './../../../../../service/api/skill.service';
import { catchError } from 'rxjs/operators';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { result } from 'lodash-es';
import { ProjectDto, SkillDto } from './../../../../../service/model/list-project.dto';
import { ListProjectService } from './../../../../../service/api/list-project.service';
import { TrainingRequestDto, ResourceRequestDetailDto } from './../../../../../service/model/delivery-management.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { APP_ENUMS } from './../../../../../../shared/AppEnums';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit, Inject, Injector, ChangeDetectorRef, ViewChild } from '@angular/core';
import * as moment from 'moment';
import * as _ from 'lodash'
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';

@Component({
  selector: 'app-create-update-training-request',
  templateUrl: './create-update-training-request.component.html',
  styleUrls: ['./create-update-training-request.component.css']
})
export class CreateUpdateTrainingRequestComponent extends AppComponentBase implements OnInit {

  public isLoading: boolean = false;
  public listProject: ProjectDto[] = [];
  public priorityList: string[] = Object.keys(this.APP_ENUM.Priority)
  public userLevelList: string[] = Object.keys(this.APP_ENUM.UserLevel)
  public trainingRequestDto = {} as TrainingRequestDto;
  public title: string = ""
  public searchProject: string = ""
  public isAddingSkill: boolean = false
  public listSkill: SkillDto[] = []
  public listSkillDetail: any[] = []
  public filteredSkillList: SkillDto[] = []
  public typeControl: string    //include: request, requestProject

  constructor(
    injector: Injector,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private listProjectService: ListProjectService,
    private resourceRequestService: DeliveryResourceRequestService,
    private skillService: SkillService,
    public dialogRef: MatDialogRef<CreateUpdateTrainingRequestComponent>,
    public ref: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getAllProject();
    this.typeControl = this.data.typeControl
    if (this.data.command == 'create') {
      this.trainingRequestDto = new TrainingRequestDto()
      //if create from resource request project then command = 'create' & projectId != 0
      //assign projectId in dto = projectId
      if (this.data.item.projectId > 0) {
        this.trainingRequestDto.projectId = this.data.item.projectId
      }
    }
    else {
      this.getResourceRequestById(this.data.item.id)
    }
    this.listSkill = this.data.skills
    this.filteredSkillList = this.data.skills
    this.userLevelList = this.data.levels
    this.trainingRequestDto.level = this.APP_ENUM.UserLevel.Intern_3
  }

  ngAfterViewChecked(): void {
    //Called after every check of the component's view. Applies to components only.
    //Add 'implements AfterViewChecked' to the class.
    this.ref.detectChanges()
  }

  getResourceRequestById(id: number) {
    this.resourceRequestService.getResourceRequestById(id).subscribe(res => {
      this.trainingRequestDto = res.result
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
      projectId: this.trainingRequestDto.projectId,
      timeNeed: this.formatDateYMD(this.trainingRequestDto.timeNeed),
      level: this.trainingRequestDto.level,
      priority: this.trainingRequestDto.priority,
      id: this.trainingRequestDto.id,
      skillIds: this.trainingRequestDto.skillIds,
      quantity: this.trainingRequestDto.quantity
    }

    if (this.data.command == "create") {
      request.id = 0
      let createRequest = { ...request, quantity: 1 };
      this.resourceRequestService.create(createRequest).pipe(catchError(this.resourceRequestService.handleError)).subscribe((res) => {
        abp.notify.success("Create Successfully!");
        this.dialogRef.close(this.trainingRequestDto);
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
        this.trainingRequestDto.skillIds = b
        console.log(this.trainingRequestDto.skillIds)
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

  filterSkills(event) {
    this.filteredSkillList = this.listSkill.filter(
      (skill) => this.l(skill.name.toLowerCase()).includes(
        this.l(event.toLowerCase())
      )
    );
  }

}
