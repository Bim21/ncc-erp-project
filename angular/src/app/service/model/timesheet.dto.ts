export class TimesheetDto {

    name: string;
    month: Date;
    year: Date;
    totalProject?: number;
    totalTimesheet?: number;
    totalWorkingDay: number;
    status: number;
    isActive: boolean;
    id?: number;
}
export class TimesheetDetailDto {
    projectId: number;
    projectName: string;
    pmId: number;
    pmName: string;
    clientId: number;
    clientName: string;
    file: string;
    note: string;
    id: number;
    timesheetId: number;
    projectBillInfomation: string;
    isComplete:boolean;
    
}
export class ProjectTimesheetDto {
    projectId: number;
    timesheetId: number;
    note: string;
    id?: number;
    createMode?: boolean;
    projectBillInfomation: string;
    timesheetFile:string;

}
export class UploadFileDto {
    TimesheetProjectId: number;
    File: any;

}
export class ClientInvoiceDto {
    clientId: number;
    clientName: string;
    totalProject: number
}
export class TimesheetProjectBill {
    userId: number
    userName: string
    projectId: number
    projectName: string
    billRole: string
    billRate: number
    startTime: string
    endTime: string
    note: string
    shadowNote: string
    isActive: boolean
    emailAddress: string
    fullName: string
    avatarPath: string
    userType: number
    branch: number
    workingTime: number
    id: number
    createMode?: boolean
    timesheetId: number
    currency: string
    userList:any[]
    searchText:string
    chargeType?: number
}
