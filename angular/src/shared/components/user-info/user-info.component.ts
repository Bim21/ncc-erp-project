import { UserService } from './../../../app/service/api/user.service';
import { UserDto } from './../../service-proxies/service-proxies';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {
@Input() userData:UserDto
public user:UserDto
  constructor() {
   
   }
  ngOnInit(): void {
  }

  ngOnChanges(): void {
    this.user = this.userData
  }


}
