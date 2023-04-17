import { CreateEditCriteriaAuditComponent } from "./create-edit-criteria-audit/create-edit-criteria-audit.component";
import { Component, Inject, Injector, OnInit } from "@angular/core";
import { catchError, finalize } from "rxjs/operators";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { NestedTreeControl } from "@angular/cdk/tree";
import { MatTreeNestedDataSource } from "@angular/material/tree";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ProcessCriteria } from "@app/service/model/process-criteria-audit.dto";
import { AuditCriteriaProcessService } from "@app/service/api/audit-criteria-process.service";
import { ViewGuilineComponent } from "./view-guiline/view-guiline.component";
import { forkJoin } from 'rxjs';
import { escape } from "lodash";
import { PERMISSIONS_CONSTANT } from "@app/constant/permission.constant";

@Component({
  selector: "app-criteria-management",
  templateUrl: "./criteria-management.component.html",
  styleUrls: ["./criteria-management.component.css"],
})
export class CriteriaManagementComponent
  extends PagedListingComponentBase<ProcessCriteria>
  implements OnInit {

  search:string = ''

  public Audits_Criteria = PERMISSIONS_CONSTANT.Audits_Criteria
  public Audits_Criteria_Create = PERMISSIONS_CONSTANT.Audits_Criteria_Create
  public Audits_Criteria_Edit = PERMISSIONS_CONSTANT.Audits_Criteria_Edit
  public Audits_Criteria_Delete = PERMISSIONS_CONSTANT.Audits_Criteria_Delete
  public Audits_Criteria_Active = PERMISSIONS_CONSTANT.Audits_Criteria_Active
  public Audits_Criteria_DeActive = PERMISSIONS_CONSTANT.Audits_Criteria_DeActive
  public Audits_Criteria_ChangeApplicable = PERMISSIONS_CONSTANT.Audits_Criteria_ChangeApplicable

  treeControl = new NestedTreeControl<PeriodicElement>((node) => node.children);
  dataSource = new MatTreeNestedDataSource<PeriodicElement>();
  hasChild = (_: number, node: PeriodicElement) =>
    !!node.children && node.children.length > 0;


  showDialog(command: string, node: any) {
    let criteria = {} as ProcessCriteria;
    if (node) {
      criteria = {
        name: node.item.name,
        code: node.item.code,
        isApplicable: node.item.isApplicable,
        isActive: node.item.isActive,
        guidLine: node.item.guidLine,
        qaExample: node.item.qaExample,
        parentId: node.item.parentId,
        level: node.item.level,
        isLeaf: node.item.isLeaf,
        id: node.item.id,
      }
    }

    const show = this.dialog.open(CreateEditCriteriaAuditComponent, {
      panelClass: 'my-dialog',
      data: {
        item: criteria,
        command: command,
        childrens: (node ? node.children : this.dataSource.data)
      },
      width: "60%",
      height: "70%",
      disableClose: true,
    });
    show.afterClosed().subscribe((result) => {
      this.refresh();
    });
  }


  constructor(injector: Injector, private dialog: MatDialog, private processCriteriaService: AuditCriteriaProcessService) {
    super(injector);
  }

  ngOnInit(): void {
    this.refresh()
  }

  searchCriteria() {
    this.processCriteriaService.search(this.search).pipe(catchError(this.processCriteriaService.handleError)).subscribe(data =>
      this.dataSource.data = data.result.childrens)
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.processCriteriaService.getAll().pipe(finalize(() => {
      finishedCallback();
    })).subscribe((data) => {

      this.dataSource.data = data.result.childrens
      this.showPaging(data.result, pageNumber);
    }, () => { })
  }

  protected delete(node: ProcessCriteria): void {
    if (!node.isLeaf) {
      forkJoin([this.processCriteriaService.ValidToDeleteListCriteria(node.id), this.processCriteriaService.GetAllProcessTailoringContain(node.id)])
        .pipe(catchError(this.processCriteriaService.handleError))
        .subscribe(([rsValid, rsTailoring]) => {
          if (rsValid.success) {
            if (rsTailoring.result.length > 0) {
              let message = `<span><strong> Delete <span style="color:#2991BF">${node.code}: ${node.name}</span> will REMOVE these criteria below from tailoring</strong></span><br>`;
              rsTailoring.result.forEach(element => {
                message += `<span class="text-left"> ${element.code}: ${element.name}</span><br>`;
              });
              abp.message.confirm(`<div style="display: flex; flex-direction: column; align-items: stretch; overflow-y: auto; max-height: 500px;">${message}</div>`, "",
                (result: boolean) => {
                  if (result) {
                    forkJoin([this.processCriteriaService.delete(node.id), this.processCriteriaService.RemoveCriteriaFromTailoring(node.id)])
                      .pipe(catchError(this.processCriteriaService.handleError)).subscribe(() => {
                        abp.notify.success("Delete " + node.name);
                        this.refresh()
                      })
                  }
                }, true
              );
            }
            else {
              abp.message.confirm(
                "Delete " + node.name + "?",
                "",
                (result: boolean) => {
                  if (result) {
                    this.processCriteriaService.delete(node.id).pipe(catchError(this.processCriteriaService.handleError)).subscribe(() => {
                      abp.notify.success("Deleted " + node.name);
                      this.refresh()
                    });
                  }
                }
              );
            }
          }
        });
    }
    if (node.isLeaf) {
      forkJoin([this.processCriteriaService.ValidToDeleteLeafCriteria(node.id), this.processCriteriaService.ValidTailoringContain(node.id)])
        .pipe(catchError(this.processCriteriaService.handleError))
        .subscribe(([rsValid, rsTailoring]) => {
          if (rsValid.success) {
            if (rsTailoring.result) {
              let message = `<span><strong> Delete <span style="color:#2991BF">${node.code}: ${node.name}</span> will REMOVE these criteria below from tailoring</strong></span><br>`;
              abp.message.confirm(`<div style="display: flex; flex-direction: column; align-items: stretch; overflow-y: auto; max-height: 500px;">${message}</div>`, "",
                (result: boolean) => {
                  if (result) {
                    this.processCriteriaService.RemoveCriteriaFromTailoring(node.id)
                      .pipe(catchError(this.processCriteriaService.handleError)).subscribe(() => {
                        abp.notify.success("Delete " + node.name);
                        this.refresh()
                      })
                  }
                }, true
              );
            }
            else {
              abp.message.confirm(
                "Delete " + node.name + "?",
                "",
                (result: boolean) => {
                  if (result) {
                    this.processCriteriaService.delete(node.id).pipe(catchError(this.processCriteriaService.handleError)).subscribe(() => {
                      abp.notify.success("Deleted " + node.name);
                      this.refresh()
                    });
                  }
                }
              );
            }
          }
        })
    }
  }

  Inactive(node: ProcessCriteria) {
    this.processCriteriaService.GetAllProcessTailoringContain(node.id).subscribe(rs => {
      if (rs.result.length > 0) {
        let message = `<span><strong> Deactive <span style="color:#2991BF">${node.code}: ${node.name}</span> will REMOVE these criteria below from tailoring</strong></span><br>`;
        rs.result.forEach(element => {
          message += `<span class="text-left"> ${element.code}: ${element.name}</span><br>`;
        });
        abp.message.confirm(`<div style="display: flex; flex-direction: column; align-items: stretch; overflow-y: auto; max-height: 500px;">
        ${message}
        </div>`, "",
          (result: boolean) => {
            if (result) {
              forkJoin([this.processCriteriaService.DeActive(node.id), this.processCriteriaService.RemoveCriteriaFromTailoring(node.id)])
                .pipe(catchError(this.processCriteriaService.handleError)).subscribe(() => {
                  abp.notify.success("Deactive " + node.name);
                  this.refresh()
                })

            }
          }, true
        );
      }
    })
    abp.message.confirm(
      "Deactive " + node.name + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.processCriteriaService.DeActive(node.id).pipe(catchError(this.processCriteriaService.handleError)).subscribe(() => {
            abp.notify.success("Deactive " + node.name);
            this.refresh()
          });
        }
      }
    );
  }

  Active(node: ProcessCriteria) {
    abp.message.confirm(
      "Active " + node.name + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.processCriteriaService.Active(node.id).pipe(catchError(this.processCriteriaService.handleError)).subscribe(() => {
            abp.notify.success("Active " + node.name);
            this.refresh()
          });
        }
      }
    );
  }

  editCriteria(node: PeriodicElement) {
    this.showDialog("edit", node);
  }
  createCriteria(node?: PeriodicElement) {
    this.showDialog("create", node);
  }

  viewGuiline(node): void {
    const dialogRef = this.dialog.open(ViewGuilineComponent, {
       panelClass: 'my-dialog',
      width: '45%',
      height: '50%',
      data: node,
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }
  setIsApplicable(id) {
    this.processCriteriaService.ChangeApplicable(id).pipe(catchError(this.processCriteriaService.handleError)).subscribe(() => {
      abp.notify.success("Change applicable successfully!");
    });
  }
}
export interface PeriodicElement {
  item: ProcessCriteria
  children?: PeriodicElement[];
}

