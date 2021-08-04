import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { Component, Injector } from '@angular/core';
import { catchError, finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  UserServiceProxy,
  UserDto,
  UserDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateUserDialogComponent } from './create-user/create-user-dialog.component';
import { EditUserDialogComponent } from './edit-user/edit-user-dialog.component';
import { ResetPasswordDialogComponent } from './reset-password/reset-password.component';
import { UploadAvatarComponent } from './upload-avatar/upload-avatar.component';
import { AppConsts } from '@shared/AppConsts';
import { UserService } from '@app/service/api/user.service';
import { MatDialog } from '@angular/material/dialog';
import { InputFilterDto } from '@shared/filter/filter.component';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './users.component.html',
  animations: [appModuleAnimation()]
})
export class UsersComponent extends PagedListingComponentBase<UserDto> {
  public readonly FILTER_CONFIG: InputFilterDto[] = [
    { propertyName: 'fullName', displayName: "Name", comparisions: [0, 6, 7, 8] },
    { propertyName: 'emailAddress', displayName: "emailAddress", comparisions: [0, 6, 7, 8] },
    { propertyName: 'userCode', displayName: "User Code", comparisions: [0, 6, 7, 8] },
    { propertyName: 'lastLoginTime', displayName: "Last Login Time", comparisions: [0, 1 ,3], isDate:true },
    { propertyName: 'creationTime', displayName: "Creation Time", comparisions: [0, 1 ,3], isDate:true },



  ];
  users: UserDto[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;
  Pages_Users_Create=PERMISSIONS_CONSTANT.Pages_Users_Create;
  Pages_Users_Delete=PERMISSIONS_CONSTANT.Pages_Users_Delete;
  Pages_Users_ImportUserFromFile=PERMISSIONS_CONSTANT.Pages_Users_ImportUserFromFile;
  Pages_Users_Update=PERMISSIONS_CONSTANT.Pages_Users_Update;
  Pages_Users_UpdateAvatar=PERMISSIONS_CONSTANT.Pages_Users_UpdateAvatar;
  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _modalService: BsModalService,
    private userInfoService:UserService,
    private dialog:MatDialog
  ) {
    super(injector);
  }

  createUser(): void {
    this.showCreateOrEditUserDialog();
  }

  editUser(user: UserDto): void {
    this.showCreateOrEditUserDialog(user.id);
  }

  public resetPassword(user: UserDto): void {
    this.showResetPasswordUserDialog(user.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedUsersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.isActive = this.isActive;

    this.userInfoService
      .getAllPaging(
        request
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: any) => {
        this.users = result.result.items;
        this.showPaging(result.result, pageNumber);
      });
  }

  protected delete(user: UserDto): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', user.fullName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._userService.delete(user.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showResetPasswordUserDialog(id?: number): void {
    this._modalService.show(ResetPasswordDialogComponent, {
      class: 'modal-lg',
      initialState: {
        id: id,
      },
    });
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(
        CreateUserDialogComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditUserDialog = this._modalService.show(
        EditUserDialogComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
  public getByEnum(enumValue: number, enumObject: any) {
    for (const key in enumObject) {
      if (enumObject[key] == enumValue) {
        return key;
      }
    }
  }
  upLoadAvatar(user): void {
    let diaLogRef = this.dialog.open(UploadAvatarComponent, {
      width: '600px',
      data: user.id
    });
    diaLogRef.afterClosed().subscribe(res => {
      if (res) {
        this.userInfoService.uploadImageFile(res, user.id).pipe(catchError(this.userInfoService.handleError)).subscribe(data => {
          if (data) {
            this.notify.success('Upload Avatar Successfully!');
            this.refresh();
            if (this.appSession.user.id == user.id) {
              this.appSession.user.avatarPath = data.body.result;
            }
            user.avatarPath = AppConsts.remoteServiceBaseUrl + data.body.result;
            this.users.forEach(u => {
              if (u.managerId == user.id) {
                u.managerAvatarPath = user.avatarPath;
              }
            });
           
          } else { this.notify.error('Upload Avatar Failed!'); }
        });
      }
    });
  }
}
