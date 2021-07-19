import { AppComponentBase } from '@shared/app-component-base';
import { UserService } from './../../../app/service/api/user.service';
import { UserDto } from './../../service-proxies/service-proxies';
import { Component, OnInit, Input, Injector } from '@angular/core';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent extends AppComponentBase implements OnInit {
@Input() userData:UserDto
public user:UserDto
  constructor(injector:Injector) {
     super(injector)
   }
  ngOnInit(): void {
  }

  ngOnChanges(): void {
    this.user = this.userData
    console.log(this.user);
  }
  public getProjectTypefromEnum(projectType: number, enumObject: any) {
    for (const key in enumObject) {
      if (enumObject[key] == projectType) {
        return key;
      }
    }
  }

}
