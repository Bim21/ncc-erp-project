import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class SkillService extends BaseApiService{

  changeUrl() {
    return 'Skill'
  }
  constructor(http: HttpClient) {
    super(http);
  }
}
