export class TimesheetDto{
    
     name :  string;
     month : Date;
     year : Date;
     totalProject?:number;
     totalTimesheet?:number;
     status : number;
     isActive:boolean;
     id?:number;
}
export class TimesheetDetailDto{
    projectId: number;
    projectName: string;
    pmId : number;
    pmName : string;
    clientId : number;
    clientName :string ;
    file :  string;
    note :  string;
    id :number;
}
export class ProjectTimesheetDto{
    projectId: number;
    timesheetId: number;
    note:string;
    id?:number;
    createMode?:boolean;
}
export class UploadFileDto{
    TimesheetProjectId: number;
    File:any;
    
}