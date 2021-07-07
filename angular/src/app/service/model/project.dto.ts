export class ProjectDto {
    name: string;
    code: string;
    projectType: 0;
    startTime: string;
    endTime: string;
    status: number;
    clientId: number;
    isCharge: boolean;
    pmId: number;
    id?: number;
}
export class projectUserDto {
    userId: number
    userName: string;
    projectId: number;
    projectName: string;
    projectRole: string;
    allocatePercentage: number;
    startTime: string;
    status: string;
    isExpense: boolean;
    resourceRequestId: number;
    resourceRequestName: string;
    pmReportId: number;
    pmReportName: string;
    isFutureActive: boolean;
    id: number;
    createMode?:boolean;
    viewMode?:boolean;
}
export class projectResourceRequestDto {
    name: string;
    projectId: number;
    timeNeed: string;
    status: string;
    timeDone: string;
    note: string;
    id: number;
    createMode?:boolean;
}
export class projectUserBillDto {
    userId: number;
    userName: string;
    projectId: number;
    projectName: string;
    billRole: string;
    billRate: number;
    startTime: string;
    endTime: string;
    currency: number;
    isActive: boolean;
    id: number;
    createMode?:boolean;
}