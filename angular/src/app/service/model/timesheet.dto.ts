export class TimesheetDto{
    
     name :  string;
     month : Date;
     year : Date;
     totalProject?:number;
     totalTimesheet?:number;
     hasInvoice?:boolean;
     status : number;
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
}
export class UploadFileDto{
    TimesheetProjectId: number;
    File:any;
    
}