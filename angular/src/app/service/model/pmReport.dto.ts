export class pmReportDto{
    name:string;
    isActive: boolean;
    year: string;
    type: number;
    numberOfProject: number;
    reportId?: number;
    isClose?:boolean;
    status:any;
    note:string;
    pmReportName:string;
    pmReportProjectId:number;
    
}
export class pmReportProjectDto{
    pmReportId: number;
    pmReportName: string;
    projectId: number;
    projectName: string;
    status: string;
    projectHealth:string;
    pmId: number;
    pmName: string ;
    note: string;
    id?: number;
    createMode?:boolean;
    setBackground?:boolean;
    pmEmailAddress:string;
    totalNormalWorkingTime:number;
    totalOverTime:number;
}