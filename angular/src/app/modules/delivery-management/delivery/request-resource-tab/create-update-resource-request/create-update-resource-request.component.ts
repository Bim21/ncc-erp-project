import { SkillService } from './../../../../../service/api/skill.service';
import { catchError } from 'rxjs/operators';
import { DeliveryResourceRequestService } from './../../../../../service/api/delivery-request-resource.service';
import { result } from 'lodash-es';
import { ProjectDto } from './../../../../../service/model/list-project.dto';
import { ListProjectService } from './../../../../../service/api/list-project.service';
import { RequestResourceDto } from './../../../../../service/model/delivery-management.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { APP_ENUMS } from './../../../../../../shared/AppEnums';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit, Inject, Injector } from '@angular/core';
import * as moment from 'moment';
import * as _ from 'lodash'

@Component({
  selector: 'app-create-update-resource-request',
  templateUrl: './create-update-resource-request.component.html',
  styleUrls: ['./create-update-resource-request.component.css']
})
export class CreateUpdateResourceRequestComponent extends AppComponentBase implements OnInit {
  public isLoading: boolean = false;
  public listProject: ProjectDto[] = [];
  public statusList: string[] = Object.keys(this.APP_ENUM.ResourceRequestStatus);
  public resourceRequestDto = {} as RequestResourceDto;
  public title
  public searchProject: string = ""
  public isAddingSkill: boolean = false

  listSkill: any[] = []
  listSkillDetail: any[] = []
  public selectedSkill: any[] = []
  constructor(injector: Injector,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private listProjectService: ListProjectService,
    private resourceRequestService: DeliveryResourceRequestService, private skillService: SkillService,
    public dialogRef: MatDialogRef<CreateUpdateResourceRequestComponent>) {
    super(injector);
  }

  ngOnInit(): void {
    this.getAllProject();
    this.resourceRequestDto = this.data.item;
    this.title = this.resourceRequestDto.name;
    this.getallskill();
    
  }
  addSkill() {
    this.listSkillDetail.push({ pending: true })
    this.isAddingSkill = true
  }
  SaveAndClose() {
    this.isLoading = true;
    this.resourceRequestDto.timeNeed = moment(this.resourceRequestDto.timeNeed).format("YYYY/MM/DD");
    if (this.resourceRequestDto.timeDone) {
      this.resourceRequestDto.timeDone = moment(this.resourceRequestDto.timeDone).format("YYYY/MM/DD");
    }
    if (this.data.command == "create") {
      this.resourceRequestService.create(this.resourceRequestDto).pipe(catchError(this.resourceRequestService.handleError)).subscribe((res) => {
        abp.notify.success("Create Successfully!");
        this.dialogRef.close(this.resourceRequestDto);
      }, () => this.isLoading = false)
    } else {
      this.resourceRequestService.update(this.resourceRequestDto).pipe(catchError(this.resourceRequestService.handleError)).subscribe((res) => {
        abp.notify.success("Create Successfully!");
        this.dialogRef.close(this.resourceRequestDto);
      }, () => this.isLoading = false)
    }

  }
  getAllProject() {
    this.listProjectService.getAll().subscribe(data => {
      this.listProject = data.result;
    })
  }
  getallskill() {
    this.skillService.getAll().pipe(catchError(this.skillService.handleError)).subscribe(data => {
      this.listSkill = data.result
      this.getSkillDetail();
    })
  }
  getSkillDetail() {
    if (this.data.command == "edit") {
      this.resourceRequestService.GetSkillDetail(this.data.item.id).pipe(catchError(this.resourceRequestService.handleError)).subscribe(data => {
        this.listSkillDetail = data.result
      // console.log(this.listSkill)

      //   let b = this.listSkillDetail.map(item => {
      //     return {
      //       name: item.skillName,
      //       id: item.skillId
      //     }
      //   })
      //   console.log(b);
      //   this.listSkill=  this.listSkill.filter(item=> b.indexOf(item) ==-1)


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
      abp.notify.warn("skill and quantity is require")
    }
    this.isAddingSkill = false

  }
}
