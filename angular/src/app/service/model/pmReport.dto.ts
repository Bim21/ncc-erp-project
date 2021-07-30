export class pmReportDto {
    name: string;
    isActive: boolean;
    year: number;
    type: number;
    numberOfProject: number;
    reportId?: number;
    isClose?: boolean;
    status: any;
    note: string;
    pmReportName: string;
    pmReportProjectId: number;

}
export class pmReportProjectDto {
    pmReportId: number;
    pmReportName: string;
    projectId: number;
    projectName: string;
    status: string;
    projectHealth: string;
    pmId: number;
    pmName: string;
    note: string;
    id?: number;
    createMode?: boolean;
    setBackground?: boolean;
    pmEmailAddress: string;
    totalNormalWorkingTime: number;
    totalOverTime: number;
}
export class PmReportInfoDto {
    note: string;
    issues: ReportIssueDto[];
    resourceInTheWeek: reportInfoInWeekDto[];
    resourceInTheFuture: ReportFutureDto[];
}
export class reportInfoInWeekDto {
    userId: number;
    fullName: string;
    userType: string;
    branch: string;
    allocatePercentage: number
}
export class ReportFutureDto {
    userId: number;
    fullName: string;
    userType: string;
    branch: string;
    allocatePercentage: number
}
export class ReportIssueDto {
    pmReportProjectId: number;
    projectName: string;
    description: string;
    impact: string;
    critical: string;
    source: string;
    solution: string;
    meetingSolution: string;
    projectHealth: number;
    status: string;
    createdAt: string;
    id: number;
}