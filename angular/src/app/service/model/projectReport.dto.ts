export class projectReportDto{
    userId: number;
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
}