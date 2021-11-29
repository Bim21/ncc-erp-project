import { PERMISSIONS_CONSTANT } from './../../../../../constant/permission.constant';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ProjectChecklistService } from './../../../../../service/api/project-checklist.service';
import { projectChecklistDto } from './../../../../../service/model/checklist.dto';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, OnInit, Injector } from '@angular/core';
import { CreateEditProductProjectChecklistComponent } from './create-edit-product-project-checklist/create-edit-product-project-checklist.component';

@Component({
  selector: 'app-product-project-checklist',
  templateUrl: './product-project-checklist.component.html',
  styleUrls: ['./product-project-checklist.component.css']
})
export class ProductProjectChecklistComponent extends AppComponentBase implements OnInit {
  CheckList_ProjectChecklist_AddCheckListItemByProject=PERMISSIONS_CONSTANT.CheckList_ProjectChecklist_AddCheckListItemByProject;
  public listCheckList: projectChecklistDto[] = [];
  public projectId: any;

  public listChecklistItem = [];
  constructor(private projectChecklistService: ProjectChecklistService,
    public route: ActivatedRoute,
    private dialog: MatDialog,
    injector: Injector) { super(injector) }
  ngOnInit(): void {
    this.projectId = this.route.snapshot.queryParamMap.get('id');
    this.getAllCheckList();
  }
  getAllCheckList() {
    this.projectChecklistService.GetCheckListItemByProject(this.projectId).subscribe(data => {
      this.listCheckList = data.result;
      this.listChecklistItem = this.listCheckList.map(el => el.id);
    })
  }
  showDialog(command: string, projectChecklist: any) {
    const show = this.dialog.open(CreateEditProductProjectChecklistComponent, {
      data: {
        command: command,
        listChecklistItem: this.listChecklistItem
      },
      width: '700px'
    })
    show.afterClosed().subscribe(result => {
      if (result) {
        this.getAllCheckList();
      }
    })


  }
  createProjectChecklist() {
    this.showDialog("create", {});
  }


}