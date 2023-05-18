
import { AppComponentBase } from "@shared/app-component-base";
import { Component, Inject, Injector, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ProcessCriteria } from "@app/service/model/process-criteria-audit.dto";
import { AuditCriteriaProcessService } from "@app/service/api/audit-criteria-process.service";
import { catchError } from "rxjs/operators";
import { DomSanitizer } from "@angular/platform-browser";
import { FormControl } from "@angular/forms";
import { PERMISSIONS_CONSTANT } from "@app/constant/permission.constant";

@Component({
  selector: "app-create-edit-criteria-audit",
  templateUrl: "./create-edit-criteria-audit.component.html",
  styleUrls: ["./create-edit-criteria-audit.component.css"],
})
export class CreateEditCriteriaAuditComponent
  extends AppComponentBase
  implements OnInit
{
  tinyMCE1 = new FormControl('');
  tinyMCE2 = new FormControl('');
  criteriaAudit = {} as ProcessCriteria ;
  listCriteriaAudit = []
  listCriteriaAuditFilter = []
  searchCriteria:string = "";
  parentCurrent:string='';
  nameParent:string=''
  isCreateCriteria:boolean = true;
  isCheckedCreateAnother:boolean=false;
  codeParent:string='';
  codeChild:number;
  code=[];
  maxCode: number
  oldCode = '';

  public Audits_Criteria_ChangeApplicable = PERMISSIONS_CONSTANT.Audits_Criteria_ChangeApplicable

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<CreateEditCriteriaAuditComponent>,
    private processCriteriaService: AuditCriteriaProcessService,
    private sanitizer: DomSanitizer,
    injector: Injector
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.processCriteriaService.getForDropDown().pipe(catchError(this.processCriteriaService.handleError)).subscribe(data => {
      this.listCriteriaAudit = data.result
      this.listCriteriaAuditFilter = data.result.filter(x => x.isActive == true);
      this.maxCode = this.listCriteriaAudit.filter(res => res.level == 1).map(res => { return Number(res.code) }).sort(function (a, b) { return a - b }).pop()

      if (this.isCreateCriteria && this.parentCurrent) {
        this.codeChild = data.result.find(res => res.id == this.data.item.id).maxValueOfListCode + 1
      }
      if (this.isCreateCriteria && !this.parentCurrent) {
        if (this.data.childrens.length > 0) {
          this.codeChild = this.maxCode + 1
        }
        else {
          this.codeChild = 1
        }
      }
    })

    if (this.data.command == "edit") {
      this.oldCode = this.data.item.code
      this.isCreateCriteria = false;
      if (this.data.item.parentId) {
        this.processCriteriaService.getCriteriaById(this.data.item.parentId).pipe(catchError(this.processCriteriaService.handleError)).subscribe(res => {
          this.parentCurrent = res.result.code
          this.nameParent = res.result.code + ' ' + res.result.name
        })
      }
      else {
        this.parentCurrent = null;
      }
      this.criteriaAudit = this.data.item;
      this.tinyMCE1.setValue(this.data.item.guidLine)
      this.tinyMCE2.setValue(this.data.item.qaExample)
      this.code = this.data.item.code.split('.')
      this.codeChild = Number(this.code.splice(-1, 1))
      this.codeParent = this.code.join('.')
    } else {
      this.isCreateCriteria = true;
      this.parentCurrent = this.data.item.code;
      this.codeParent = this.data.item.code || '';
      if (!this.parentCurrent) {
        this.codeChild = this.data.childrens.length + 1
      }
      this.criteriaAudit = { ...this.criteriaAudit, isApplicable: false, parentId: this.data.item.id, name: this.data.name }
    }
  }

  changeParent(e) {
    this.parentCurrent= e.value
    this.codeParent= e.value
    this.criteriaAudit.parentId=this.listCriteriaAudit.find(res=>res.code==e.value).id
    this.codeChild=this.listCriteriaAudit.find(res=>res.code==e.value).maxValueOfListCode + 1
  }

  setCheckedCreateAnother(){
  this.isCheckedCreateAnother=!this.isCheckedCreateAnother
  }

  removeParent(){
    this.parentCurrent=null;
    this.criteriaAudit.parentId=null;
    this.codeParent = '';
    this.searchCriteria = '';
    if(this.parentCurrent){
      this.codeChild = undefined;
    }
    else{
      this.codeChild = this.maxCode + 1
    }
  }

  SaveAndClose() {
    console.log(this.oldCode);

    const criteriaCreate = {
      code: this.codeParent ? (this.codeParent + '.' + this.codeChild) : this.codeChild,
      name: this.criteriaAudit.name,
      isApplicable: this.criteriaAudit.isApplicable,
      guidLine: this.tinyMCE1.value,
      qaExample: this.tinyMCE2.value,
      id: this.criteriaAudit.id,
      parentId: this.criteriaAudit.parentId
    }
    const criteriaUpdate = {
      code: this.codeParent ? (this.codeParent + '.' + this.codeChild) : this.codeChild,
      isApplicable: this.criteriaAudit.isApplicable,
      guidLine: this.tinyMCE1.value,
      qaExample: this.tinyMCE2.value,
      id: this.criteriaAudit.id,
      name: this.criteriaAudit.name
    }



    if (this.data.command == "create") {
      this.processCriteriaService.create(criteriaCreate).pipe(catchError(this.processCriteriaService.handleError)).subscribe((res) => {
        abp.notify.success("Create Successfully!");
        if (!this.isCheckedCreateAnother) {
          this.dialogRef.close(this.criteriaAudit)
        }
        else {
          this.processCriteriaService.getForDropDown().pipe(catchError(this.processCriteriaService.handleError)).subscribe(data => {
            this.listCriteriaAudit = data.result
            this.maxCode = this.listCriteriaAudit.filter(res => res.level == 1).map(res => { return Number(res.code) }).sort(function (a, b) { return a - b }).pop()
            if (this.parentCurrent) {
              this.codeChild = data.result.find(res => res.code == this.parentCurrent).maxValueOfListCode + 1
            }
            else {
              this.codeChild = this.maxCode + 1
            }
          })
          this.criteriaAudit.name = '';
          this.tinyMCE1.setValue('')
          this.tinyMCE2.setValue('')
        }
      }, () => { this.isLoading = false })
    } else {
      if (criteriaUpdate.code != this.oldCode) {
        abp.message.confirm("Code is change! Do you want to update Code for all descendants ", "", (result: boolean) => {
          if (result) {
            this.processCriteriaService.update(criteriaUpdate).pipe(catchError(this.processCriteriaService.handleError)).subscribe((res) => {
              abp.notify.success("Update Successfully!");
              this.dialogRef.close(this.criteriaAudit);
            }, () => { this.isLoading = false })
          }
          else {
            criteriaUpdate.code = this.oldCode;
            this.processCriteriaService.update(criteriaUpdate).pipe(catchError(this.processCriteriaService.handleError)).subscribe((res) => {
              abp.notify.success("Update Successfully!");
              this.dialogRef.close(this.criteriaAudit);
            }, () => { this.isLoading = false })
          }
        })
      }
      else {
        this.processCriteriaService.update(criteriaUpdate).pipe(catchError(this.processCriteriaService.handleError)).subscribe((res) => {
          abp.notify.success("Update Successfully!");
          this.dialogRef.close(this.criteriaAudit);
        }, () => { this.isLoading = false })
      }
    }
  }
  checkCode(e) {
    if (e!=null && e < 1) {
      this.codeChild=1
    }
  }
}

