import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PmReportIssueService } from '@app/service/api/pm-report-issue.service';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-edit-meeting-note-dialog',
  templateUrl: './edit-meeting-note-dialog.component.html',
  styleUrls: ['./edit-meeting-note-dialog.component.css']
})
export class EditMeetingNoteDialogComponent extends AppComponentBase implements OnInit {
  projectIssue = {} as any
  reportId:any
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, injector: Injector,
    public dialogRef: MatDialogRef<EditMeetingNoteDialogComponent>, private reportIssueService: PmReportIssueService) {
    super(injector)
  }

  ngOnInit(): void {
    this.projectIssue = this.data
  }
  saveAndClose() {
   
    this.reportIssueService.EditMeetingNote(this.projectIssue).subscribe(rs =>{
      abp.notify.success("Edited Meeting solution")
      this.dialogRef.close(true)
    })
  }
}
