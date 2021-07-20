import { AppComponentBase } from '@shared/app-component-base';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { CreateEditProjectChecklistComponent } from './create-edit-project-checklist/create-edit-project-checklist.component';
import { MatDialog } from '@angular/material/dialog';
import { ProjectChecklistService } from './../../../../../service/api/project-checklist.service';
import { projectChecklistDto } from './../../../../../service/model/checklist.dto';
import { Component, OnInit, Injector } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { result } from 'lodash-es';

@Component({
  selector: 'app-project-checklist',
  templateUrl: './project-checklist.component.html',
  styleUrls: ['./project-checklist.component.css']
})
export class ProjectChecklistComponent extends AppComponentBase implements OnInit {
  public listCheckList: projectChecklistDto[]= [];
  public projectId:any;
  public listChecklistItem=[];
  CheckList_ProjectChecklist_AddCheckListItemByProject=PERMISSIONS_CONSTANT.CheckList_ProjectChecklist_AddCheckListItemByProject;

  constructor(private projectChecklistService:ProjectChecklistService, 
    public route: ActivatedRoute,
    private dialog:MatDialog,
    injector:Injector) {super(injector) }
  ngOnInit(): void {
    this.projectId= this.route.snapshot.queryParamMap.get('id');
    this.getAllCheckList();
  }
  getAllCheckList(){
    this.projectChecklistService.GetCheckListItemByProject(this.projectId).subscribe(data=>{
      this.listCheckList=data.result;
      this.listChecklistItem=this.listCheckList.map(el=>el.id);
      console.log(this.listCheckList)
    })
  }
  showDialog(command:string,projectChecklist:any){
    const show= this.dialog.open(CreateEditProjectChecklistComponent,{
      data:{
        command:command,
        listChecklistItem: this.listChecklistItem
      },
      width:'700px'
    })
    show.afterClosed().subscribe(result=>{
      if(result){
        this.getAllCheckList();
      }
    })
    

  }
  createProjectChecklist(){
    this.showDialog("create",{});
  }
  

}
