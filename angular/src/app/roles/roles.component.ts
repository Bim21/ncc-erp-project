import { Route, Router, ActivatedRoute } from '@angular/router';
import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
  RoleServiceProxy,
  RoleDto,
  RoleDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateRoleDialogComponent } from './create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './edit-role/edit-role-dialog.component';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';

class PagedRolesRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  templateUrl: './roles.component.html',
  animations: [appModuleAnimation()]
})
export class RolesComponent extends PagedListingComponentBase<RoleDto> {
  roles: RoleDto[] = [];
  keyword = '';
  Admin_Roles_Create = PERMISSIONS_CONSTANT.Admin_Roles_Create;
  Admin_Roles_Edit = PERMISSIONS_CONSTANT.Admin_Roles_Edit;
  Admin_Roles_Delete = PERMISSIONS_CONSTANT.Admin_Roles_Delete;

  constructor(
    injector: Injector,
    private _rolesService: RoleServiceProxy,
    private _modalService: BsModalService,
    private _router: Router,
    private _route: ActivatedRoute
  ) {
    super(injector);
  }

  list(
    request: PagedRolesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._rolesService
      .getAll(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: RoleDtoPagedResultDto) => {
        this.roles = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(role: RoleDto): void {
    abp.message.confirm(
      this.l('RoleDeleteWarningMessage', role.displayName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._rolesService
            .delete(role.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  createRole(): void {
    this.showCreateOrEditRoleDialog();
  }

  showCreateOrEditRoleDialog(): void {
    let createOrEditRoleDialog: BsModalRef;
    createOrEditRoleDialog = this._modalService.show(
      CreateRoleDialogComponent,
      {
        class: 'modal-lg',
      },
    );
    createOrEditRoleDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  editPage(roleId, name) {
    this._router.navigate(['/app/edit-role'], { queryParams: { id: roleId, name: name } })
  }
}
