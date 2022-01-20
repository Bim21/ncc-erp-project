import { AppComponentBase } from '@shared/app-component-base';
import { extend } from 'lodash-es';
import { PERMISSIONS_CONSTANT } from '@app/constant/permission.constant';
import { MatDialog } from '@angular/material/dialog';
import { ProjectFileDto } from './../../../../../service/model/projectFile.dto';
import { ActivatedRoute } from '@angular/router';
import { ProjectFileService } from './../../../../../service/api/project-file.service';
import { Component, OnInit, Injector } from '@angular/core';
import { UploadFileDto } from '@app/service/model/timesheet.dto';
import * as FileSaver from 'file-saver';

@Component({
  selector: 'app-project-file',
  templateUrl: './project-file.component.html',
  styleUrls: ['./project-file.component.css']
})
export class ProjectFileComponent extends AppComponentBase implements OnInit {
  private projectId: number
  public fileList: any[] = []
  public isLoading: boolean = false

  PmManager_ProjectFile = PERMISSIONS_CONSTANT.PmManager_ProjectFile
  PmManager_ProjectFile_DeleteFile = PERMISSIONS_CONSTANT.PmManager_ProjectFile_DeleteFile
  PmManager_ProjectFile_UploadNewFile = PERMISSIONS_CONSTANT.PmManager_ProjectFile_UploadNewFile
  PmManager_ProjectFile_ViewAllFiles = PERMISSIONS_CONSTANT.PmManager_ProjectFile_ViewAllFiles


  constructor(private projectFileService: ProjectFileService, private route: ActivatedRoute, injector: Injector) {
    super(injector)
    this.projectId = Number(route.snapshot.queryParamMap.get("id"))
  }

  ngOnInit(): void {
    this.getAllFile()
  }
  private getAllFile() {

    this.projectFileService.getAllFile(this.projectId).subscribe(data => {
      this.fileList = data.result
    })
  }


  public selectFile(event) {
    let fileData = this.fileList.map(item => {
      let file = new Blob([this.converFile(atob(item.bytes))], {
        type: ""
      });
      let arrayOfBlob = new Array<Blob>();
      arrayOfBlob.push(file);
      item = new File(arrayOfBlob, item.fileName)
      return item;
    })
    fileData.push(...event.target.files);
    if (!this.fileList) {
      abp.message.error("Choose a file!")
      return
    }
    this.isLoading = true

    this.projectFileService.UploadFiles(fileData, this.projectId)
      .subscribe((res) => {
        abp.notify.success("Upload file successful")
        this.getAllFile()
        this.isLoading = false
      },
        () => { this.isLoading = false });
  }
  private converFile(s) {
    var buf = new ArrayBuffer(s.length);
    var view = new Uint8Array(buf);
    for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
    return buf;
  }
  private downloadFile(projectFile: ProjectFileDto) {
    const file = new Blob([this.converFile(atob(projectFile.bytes))], {
      type: ""
    });
    FileSaver.saveAs(file, projectFile.fileName);
  }
  removeFile(file: ProjectFileDto) {
    abp.message.confirm(
      "Remove file " + file.fileName + "?",
      "",
      (result: boolean) => {
        if (result) {
          this.projectFileService.removeFile(file.fileName, this.projectId).subscribe(rs => {
            abp.notify.success(`remove file ${file.fileName}`)
            this.getAllFile()
          })
        }
      }
    )
  }

}