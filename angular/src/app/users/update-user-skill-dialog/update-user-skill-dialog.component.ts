import { catchError } from 'rxjs/operators';
import { result } from 'lodash-es';
import { SkillDto } from './../../service/model/list-project.dto';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { SkillService } from '@app/service/api/skill.service';
import { UserService } from '@app/service/api/user.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-update-user-skill-dialog',
  templateUrl: './update-user-skill-dialog.component.html',
  styleUrls: ['./update-user-skill-dialog.component.css']
})
export class UpdateUserSkillDialogComponent implements OnInit {
  userSkillList: any[] = []
  skillList:SkillDto[] = []
  tempSkillList:SkillDto[] = []
  subscription: Subscription[] = [];
  selectedSkills: any[] = []

  public searchSkill:string =""
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private userService:UserService,
   public dialogRef: MatDialogRef<UpdateUserSkillDialogComponent>,
   private skillService:SkillService) { }

  ngOnInit(): void {
    this.userSkillList = this.data.userSkills.map(skill => skill.skillId)
    this.getAllSkill()
    this.selectedSkills = [...this.data.userSkills.map(skill => {return {
      id: skill.skillId,
      name: skill.skillName
    }})]
  }

  getAllSkill(){
    this.subscription.push(
      this.skillService.getAll().subscribe(data=>{
        this.skillList = data.result
        this.tempSkillList = this.skillList
      })
    )
   
  }
  saveAndClose(){
    let requestBody = {
      userId: this.data.id,
      userSkills: this.userSkillList
    }
    this.subscription.push(
      this.userService.updateUserSkills(requestBody).pipe(catchError(this.userService.handleError)).subscribe(rs=>{
        abp.notify.success(`Update skill for user ${this.data.fullName}`)
        this.dialogRef.close(true)
      })
    )
  
  }

  filterSkill(){
    this.skillList = [...new Set( this.tempSkillList.filter(skill => skill.name.toLowerCase()
    .includes(this.searchSkill.toLowerCase())).concat(...this.selectedSkills))]
  }

  ngOnDestroy(): void {
    this.subscription.forEach(sub => sub.unsubscribe())
    this.selectedSkills = []
  }

  onSkillSelect(skill,e){
    console.log(e)
    if(e._selected){
      this.selectedSkills.push(skill)
    }
    else{
      this.selectedSkills.splice(this.selectedSkills.findIndex(skill),1)
    }
  }

}
