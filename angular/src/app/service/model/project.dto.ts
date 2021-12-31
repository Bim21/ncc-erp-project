export class ProjectDto {
  name: string;
  code: string;
  projectType?: 0;
  startTime: string;
  endTime: string;
  status?: number;
  clientId?: number;
  isCharge?: boolean;
  pmId: number;
  id?: number;
}
export interface ResponseWrapper<T> {
  result: T;
  error?: unknown;
  success?: boolean;
  targetUrl?: unknown;
}
export interface IProjectHistoryUser {
  userId: number;
  fullName: string;
  projectId: number;
  projectName: string;
  projectRole: string;
  allocatePercentage: number;
  startTime: string;
  status: string;
  isExpense: true;
  resourceRequestId: number;
  resourceRequestName: string;
  pmReportId: number;
  pmReportName: string;
  isFutureActive: true;
  emailAddress: string;
  userName: string;
  avatarPath: string;
  userType: number;
  branch: number;
  note: string;
  id: number;
}

export class projectUserDto {
  userId: number;
  userName: string;
  fullName: string;
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
  createMode?: boolean;
  viewMode?: boolean;
}
export class projectResourceRequestDto {
  name: string;
  projectId: number;
  timeNeed: string;
  status: string;
  timeDone: string;
  note: string;
  id: number;
  createMode?: boolean;
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
  createMode?: boolean;
  note: string;
  shadowNote: string;
  workingTime: string;
  id: number;
  timesheetId: number;
  userList?: any[];
}
export class MilestoneDto {
  projectId: number;
  name: string;
  description: string;
  flag: string;
  status: string;
  uatTimeStart: string;
  uatTimeEnd: string;
  note: string;
  id?: number;
  createMode?: boolean;
}
export class ProjectInfoDto {
  projectName: string;
  clientName: string;
  pmName: string;
  totalBill: number;
  totalResource: number;
}
export class TrainingProjectDto {
  name: string;
  code: string;
  projectType?: number;
  startTime: string;
  endTime: string;
  status?: number;
  clientId?: number;
  clientName?: string;
  currencyId?: number;
  currencyName?: string;
  isCharge?: true;
  pmId: number;
  pmName?: string;
  pmFullName?: string;
  pmEmailAddress?: string;
  pmUserName?: string;
  pmAvatarPath?: string;
  pmUserType?: number;
  pmBranch?: number;
  isSent?: number;
  timeSendReport?: string;
  dateSendReport?: string;
  evaluation?: string;
  id: number;
}
export class ProductProjectDto {
  name: string;
  code: string;
  startTime: string;
  endTime: string;
  status: number;
  pmId: number;
  pmName: string;
  pmFullName: string;
  pmEmailAddress: string;
  pmUserName: string;
  pmAvatarPath: string;
  pmUserType: number;
  projectType: number;
  pmBranch: number;
  isSent: number;
  timeSendReport: string;
  dateSendReport: string;
  id: number;
}
