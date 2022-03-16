import { ResourcePlanDto } from './../../../../../service/model/resource-plan.dto';
import { result } from 'lodash-es';
import { ChangeDetectorRef, Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DeliveryResourceRequestService } from '@app/service/api/delivery-request-resource.service';
import { RequestResourceDto } from '@app/service/model/delivery-management.dto';
import { AppComponentBase } from '@shared/app-component-base';
import * as moment from 'moment';

@Component({
  selector: 'app-form-plan-user',
  templateUrl: './form-plan-user.component.html',
  styleUrls: ['./form-plan-user.component.css']
})
export class FormPlanUserComponent extends AppComponentBase implements OnInit {
  public search: string = '';
  public listUsers: any[] = [];
  public resourcePlan = {} as ResourcePlanDto
  public typePlan: string = 'create';
  constructor(
    injector: Injector,
    @Inject(MAT_DIALOG_DATA) public input: any,
    public dialogRef: MatDialogRef<FormPlanUserComponent>,
    private resourceRequestService: DeliveryResourceRequestService,
    private ref: ChangeDetectorRef,
  ) 
  {
    super(injector);
  }

  ngOnInit(): void {
    this.resourcePlan = this.input;
    if(this.resourcePlan.userId){
      this.typePlan = 'update'
    }
    this.getAllUser()
  }

  ngAfterViewChecked(): void {
    //Called after every check of the component's view. Applies to components only.
    //Add 'implements AfterViewChecked' to the class.
    this.ref.detectChanges()
  }


  getAllUser(){
    let unassigned = {
      userId: -1,
      surname: '',
      name: 'Unassigned',
      fullname: 'Unassigned',
      email: ''
    }
    this.resourceRequestService.getPlanResourceUser().subscribe(res => {
      this.listUsers = res.result
      if(this.typePlan == 'update'){
        this.listUsers.unshift(unassigned)
      }
    })
  }

  SaveAndClose(){
    this.resourcePlan.joinDate = moment(this.resourcePlan.joinDate).format('YYYY/MM/DD')
    if(this.typePlan == 'create'){
      this.resourceRequestService.createPlanUser(this.resourcePlan).subscribe(res => {
        if(res.success){
          abp.notify.success("Plan Success")
          this.dialogRef.close(this.resourcePlan)
        }
        else{
          abp.notify.error(res.result)
        }
      })
    }
    else{
      if(this.resourcePlan.userId == -1){
        this.resourceRequestService.deletePlanUser(this.resourcePlan.projectUserId).subscribe(res => {
          if(res.success){
            abp.notify.success("Plan Success")
            this.dialogRef.close()
          }
          else{
            abp.notify.error(res.result)
          }
        })
      }
      else{
        this.resourceRequestService.updatePlanUser(this.resourcePlan).subscribe(res => {
          if(res.success){
            abp.notify.success("Update Success")
            this.dialogRef.close()
          }
          else{
            abp.notify.error(res.result)
          }
        })
      }
    }
  }
}
