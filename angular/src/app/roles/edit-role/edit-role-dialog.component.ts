import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  Inject,
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  RoleServiceProxy,
  GetRoleForEditOutput,
  RoleDto,
  PermissionDto,
  RoleEditDto,
  FlatPermissionDto,
  Permission,
  RolePermissionDto
} from '@shared/service-proxies/service-proxies';
import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import * as _ from 'lodash';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  templateUrl: 'edit-role-dialog.component.html',
  styleUrls: ['edit-role-dialog.component.css']
})
export class EditRoleDialogComponent extends AppComponentBase
  implements OnInit {
  selection = new SelectionModel<string>(true, []);
  treeControl: NestedTreeControl<any>;
  dataSource: MatTreeNestedDataSource<any>;
  isStatic: boolean;
  path = '';
  saving = false;
  roleId: number;
  role = new RoleDto();
  id: number;
  roles = [];
  listRoles = [];
  selectedRole: any;
  keyword: string = '';
  //permissions: PermissionDto[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;
  permissions: RolePermissionDto = new RolePermissionDto();
  grantedPermissionNames: string[];

  @Output() onSave = new EventEmitter<any>();
  constructor(
    //@Inject(MAT_DIALOG_DATA) public data: any,
    private activatedRoute: ActivatedRoute,
   
    injector: Injector,
    private _roleService: RoleServiceProxy,
    public bsModalRef: BsModalRef,


  ) {
    super(injector);
    this.dataSource = new MatTreeNestedDataSource<Permission>();
    this.treeControl = new NestedTreeControl<Permission>(node => node.childrens);
  }
  hasChild = (_: number, node: Permission) => !!node.childrens && node.childrens.length > 0;
  
  ngOnInit() {
    this._roleService
      .getRoleForEdit(this.id)
      .subscribe((result: any) => {
        this.role = result.result.role;
        // this.getAllByRoles();
        // this.getAllRoles();
        this.isStatic = result.result.role.isStatic;
        this.grantedPermissionNames = result.result.grantedPermissionNames;
        this.dataSource.data = result.result.permissions;
        this.treeControl.dataNodes = result.result.permissions;
        this.treeControl.dataNodes.forEach(node =>
          this.initSelectionList(node)
        );
      });

  }

  isPermissionChecked(permissionName: string): boolean {
    // just return default permission checked status
    // it's better to use a setting
    return this.defaultPermissionCheckedStatus;
  }

  onPermissionChange(permission: PermissionDto, $event) {
    this.checkedPermissionsMap[permission.name] = $event.target.checked;
  }

  getCheckedPermissions(): string[] {
    const permissions: string[] = [];
    _.forEach(this.checkedPermissionsMap, function (value, key) {
      if (value) {
        permissions.push(key);
      }
    });
    return permissions;
  }

  initSelectionList(node: Permission) {
    const selectedList = this.grantedPermissionNames as any;
    if (selectedList.includes(node.name)) {
      this.selected(node);
    }

    if (!node.childrens || node.childrens.length === 0) {
      return;
    } else {
      node.childrens.forEach(child => this.initSelectionList(child));
    }
  }
  isSelected(node: Permission) {
    return this.selection.isSelected(node.name);
  }

  deselected(node: Permission) {
    this.selection.deselect(node.name);
  }
  selected(node: Permission) {
    this.selection.select(node.name);
  }

  descendantsAllSelected(node: Permission): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected = descendants.every(child => this.isSelected(child));
    descAllSelected ? this.selected(node) : this.deselected(node);
    return descAllSelected;
  }

  descendantsPartiallySelected(node: Permission): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const result = descendants.some(child => this.isSelected(child));
    return result && !this.descendantsAllSelected(node);
  }

  todoLeafItemSelectionToggle(node: Permission) {
    this.isSelected(node) ? this.deselected(node) : this.selected(node);
    this.descendantsPartiallySelected(node);
    this.onSaveData(node);
  }

  todoItemSelectionToggle(node: Permission) {
    this.isSelected(node) ? this.deselected(node) : this.selected(node);
    const descendants = this.treeControl.getDescendants(node);
    descendants.forEach(child => {
      this.isSelected(node) ? this.selected(child) : this.deselected(child);
    });
    this.onSaveData(node);
  }

  // Trước khi gọi API
  // Cho những node có trạng thái indeterminate vào list.
  selectOrDeselectAllIndeterminateParents(doSelect: boolean) {
    this.treeControl.dataNodes.forEach(
      parent => { this.selectOrDeselectTheIndeterminateParent(parent, doSelect); }
    );
  }

  selectOrDeselectTheIndeterminateParent(node: Permission, doSelect: boolean) {
    if (!node.childrens || node.childrens.length < 1) { return; }
    const descendants = this.treeControl.getDescendants(node);
    const descSomeSelected = descendants.some(child => this.isSelected(child));

    if (descSomeSelected) {
      if (doSelect) {
        this.selected(node);
      } else if (!this.descendantsAllSelected(node)) {
        this.deselected(node);
      }
    }

    descendants.forEach(
      child => this.selectOrDeselectTheIndeterminateParent(child, doSelect)
    );
  }

  onBack() {
    history.back();
  }

  onSaveClick() {
    this._roleService
      .update(this.role)
      .subscribe(() => {
        this.notify.success(this.l('Saved successfully'));
        this.onSave.emit()

      });
  }

  onSaveData(node: Permission) {
    node.isLoading = true;
    let descendants = this.treeControl.getDescendants(node);
    if (descendants) {
      descendants.forEach(child => child.isLoading = true);
    }
    this.selectOrDeselectAllIndeterminateParents(true);
    this.permissions.permissions = this.selection.selected;
    this.permissions.id = this.role.id;
    this._roleService.changeRolePermission(this.permissions).subscribe(() => {
      node.isLoading = false
      if (descendants) {
        descendants.forEach(child => child.isLoading = false);
      }
    });
  }


  // getAllRoles() {
  //   this.roleService.getAll().subscribe((data) => {
  //     this.roles = data.result;
  //     this.listRoles.forEach(item => {
  //       var role = this.roles.find(value => value.id == item.id);
  //       var index = this.roles.indexOf(role);
  //       this.roles.splice(index, 1);
  //     })
  //   })
  // }

  // getAllByRoles() {
  //   this.roleService.getAllByRoles(this.role.id).subscribe((data) => {
  //     this.listRoles = data.result;
  //   })
  // }

  // addToRoleList(value) {
  //   const outcomingentype = {
  //     roleId: this.role.id,
  //     outcomingEntryTypeId: value.id
  //   }
  //   this.roleService.addToRoleList(outcomingentype).subscribe(() => {
  //     this.getAllByRoles();
  //     this.roles = this.roles.filter(item => item.id !== value.id)
  //   })
  // }

  // close(value) {
  //   this.roleService.close(this.role.id, value.id).subscribe(() => {
  //     this.getAllByRoles();
  //     this.roles.push(value);
  //   });
  // }
}
