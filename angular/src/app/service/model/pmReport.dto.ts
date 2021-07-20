export class pmReportDto{
    name:string;
    isActive: boolean;
    year: string;
    type: number;
    numberOfProject: number;
    id?: number;
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
}