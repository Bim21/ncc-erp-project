import { PERMISSIONS_CONSTANT } from './../../constant/permission.constant';
import { SkillService } from './../../service/api/skill.service';
import { UserSkillDto } from './../../service/model/skill.dto';
import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  UserServiceProxy,
  UserDto,
  RoleDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './edit-user-dialog.component.html'
})
export class EditUserDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  user = new UserDto();
  roles: RoleDto[] = [];
  checkedRolesMap: { [key: string]: boolean } = {};
  id: number;
  skillList:UserSkillDto[] = [];
  userLevelList = Object.keys(this.APP_ENUM.UserLevel);
  userBranchList = Object.keys(this.APP_ENUM.Branch);
  userTypeList = Object.keys(this.APP_ENUM.UserType);
  isviewOnlyMe:boolean =false
  @Output() onSave = new EventEmitter<any>();

  Pages_Users_UpdateMySkills = PERMISSIONS_CONSTANT.Pages_Users_UpdateMySkills
  Pages_Users_ViewOnlyMe = PERMISSIONS_CONSTANT.Pages_Users_ViewOnlyMe

  constructor(
    injector: Injector,
    private skillService:SkillService,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getAllSkill();
    this._userService.get(this.id).subscribe((result) => {
      this.user = result;
      console.log(this.user);
      this.user.userSkills = this.user.userSkills.map(item=>item.skillId)

      this._userService.getRoles().subscribe((result2) => {
        this.roles = result2.items;
        this.setInitialRolesStatus();
      });
    });

    if(this.permission.isGranted( this.Pages_Users_UpdateMySkills) && this.permission.isGranted( this.Pages_Users_ViewOnlyMe )){
      this.isviewOnlyMe =true
    }
  }
  getAllSkill(){
    this.skillService.getAll().subscribe(data =>{
      this.skillList =data.result
    })
  }

  setInitialRolesStatus(): void {
    _map(this.roles, (item) => {
      this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(
        item.normalizedName
      );
    });
  }

  isRoleChecked(normalizedName: string): boolean {
    return _includes(this.user.roleNames, normalizedName);
  }

  onRoleChange(role: RoleDto, $event) {
    this.checkedRolesMap[role.normalizedName] = $event.target.checked;
  }

  getCheckedRoles(): string[] {
    const roles: string[] = [];
    _forEach(this.checkedRolesMap, function (value, key) {
      if (value) {
        roles.push(key);
      }
    });
    return roles;
  }

  save(): void {
   this.user.userSkills =  this.user.userSkills.map(item=>{
      return  {
        userId: this.user.id,
        skillId: item
      };
    })
    this.saving = true;

    this.user.roleNames = this.getCheckedRoles();

    this._userService
      .update(this.user)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }
}
