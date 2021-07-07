import { projectUserDto } from './../../../../../service/model/project.dto';
import { AuditResultService } from './../../../../../service/api/auditresult.service';
import { catchError } from 'rxjs/operators';
import { AuditResultPeopleService } from './../../../../../service/api/audit-result-people.service';

import { ProjectUserService } from './../../../../../service/api/project-user.service';
import { projectChecklistDto } from './../../../../../service/model/checklist.dto';
import { ProjectChecklistService } from './../../../../../service/api/project-checklist.service';
import { AppComponentBase } from '@shared/app-component-base';
import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Injector } from '@angular/core';
import { SaodoProjectUserDto } from '@app/service/model/saodo.dto';

@Component({
  selector: 'app-sao-do-project-detail',
  templateUrl: './sao-do-project-detail.component.html',
  styleUrls: ['./sao-do-project-detail.component.css']
})
export class SaoDoProjectDetailComponent extends AppComponentBase implements OnInit {
  SaoDo_AuditResultPeople = PERMISSIONS_CONSTANT.SaoDo_AuditResultPeople;
  SaoDo_AuditResultPeople_Create = PERMISSIONS_CONSTANT.SaoDo_AuditResultPeople_Create;
  SaoDo_AuditResultPeople_Update = PERMISSIONS_CONSTANT.SaoDo_AuditResultPeople_Update;
  SaoDo_AuditResultPeople_Delete = PERMISSIONS_CONSTANT.SaoDo_AuditResultPeople_Delete;
  public projectName='';
  public projectId:any;
  public auditSessionId='';
  public projectUserList=[];
  public isEditing:boolean=false;
  public isEditingNote:boolean=false;
  public projectUser={} as SaodoProjectUserDto;
  public note='';

  public listCheckList:projectChecklistDto[]=[];
  constructor(private route: ActivatedRoute , injector:Injector,
    private projectChecklistService:ProjectChecklistService,
    private projectUserService:ProjectUserService,
    private auditProjectResultService: AuditResultPeopleService,
    private auditResultService: AuditResultService) {super(injector) }
  ngOnInit(): void {
    this.projectName=this.route.snapshot.queryParamMap.get('projectName');
    this.projectId=Number(this.route.snapshot.queryParamMap.get('projectId'));
    this.auditSessionId=this.route.snapshot.queryParamMap.get('saodoId');
    this.projectUser.auditResultId=this.route.snapshot.queryParamMap.get('id');
    this.getAllCheckList();
    this.getAllProjectUser();
    this.getNote();

  }
  getAllCheckList(){
    this.projectChecklistService.GetCheckListItemByProject(this.projectId,this.auditSessionId).subscribe(data=>{
      this.listCheckList=data.result;
      console.log(this.listCheckList)
    })
  }
  getAllProjectUser(){
    this.projectUserService.getAllProjectUser(this.projectId,true).subscribe(data=>{
      this.projectUserList=data.result;

    })
  }
  submit(id:any){
    let requestBody = {
      checkListItemId: id,
      userId: this.projectUser.userId,
      curatorId: this.projectUser.curatorId,
      auditResultId:this.projectUser.auditResultId
    } 
    this.auditProjectResultService.create(requestBody).pipe(catchError(this.auditProjectResultService.handleError)).subscribe((res) => {
      this.projectUser={};
      abp.notify.success("Created successfully");
      this.getAllCheckList();
    });
  }
  editPeople(){
    this.isEditing=true;
  }

  save(id,form){
    let requestBody = {
      checkListItemId: id,
      userId: form.userId,
      curatorId: form.curatorId,
      id: form.id,
      auditResultId:this.projectUser.auditResultId
    } 
    delete form.createMode
    this.auditProjectResultService.update(requestBody).pipe(catchError(this.auditProjectResultService.handleError)).subscribe((res) => {
      abp.notify.success("Created successfully");
      this.getAllCheckList();
      this.isEditing=false;
    });
  }

  deletePeople(form:any){
    abp.message.confirm(
      "Delete Audit ",
      "",
      (result: boolean) => {
        if (result) {
          this.auditProjectResultService.delete(form.id).pipe(catchError(this.auditProjectResultService.handleError)).subscribe(() => {
            abp.notify.success("Deleted Audit ");
            this.getAllCheckList();
          
          });
        }
      }
    );

  }
  getNote(){
    this.auditResultService.getById(this.projectUser.auditResultId).subscribe(data=>{
      this.note=data.result;
    })
  }
  saveNote(){
    this.auditResultService.updateNote(this.projectUser.auditResultId,this.note).subscribe(data=>{
      this.note=data.result;
    })
  }



}
