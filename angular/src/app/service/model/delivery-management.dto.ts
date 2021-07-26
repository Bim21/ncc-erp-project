export class RequestResourceDto{
  name :  string  ;
  projectId : number ;
  projectName? :  string  ;
  timeNeed :  string  ;
  status : number;
  statusName? :  string ;
  timeDone :  string;
  note ?:  string ;
  plannedNumberOfPersonnel? : number;
  id?: number
}
export class ResourceRequestDetailDto{
  
  userId : number;
  FullName :  string ;
  projectId : number;
  projectName :  string ;
  projectRole :  string ;
  allocatePercentage : number;
  startTime :string ;
  status :  string ;
  isExpense : true;
  resourceRequestId : number;
  resourceRequestName :  string ;
  pmReportId : number;
  pmReportName :  string ;
  isFutureActive : true;
  id?: number
  
}
export class userAvailableDto{
  userId : number;
  userName :  string ;
  undisposed : number;
  startDate?:any;
}
export class userToRequestDto{
  id?:number;
  userId?:number;
  allocatePercentage: number;
  startTime: any;
  resourceRequestId?:number;
  projectId?:number;
}
export class availableResourceDto{
  userId: number;
  userName: string;
  projects: [

  ];
  used:number
}
export class planUserDto{
  projectId: number;
  userId: number;
  percentUsage: number;
  projectRole: number;
  startTime: string;
  isExpense?: true
}
export class editFutureResourceDto{
  userId: number;
  projectId: number;
  allocatePercentage: number;
  startTime: string;
  id?: number
}
export class futureResourceDto{
  userId: number;
  userName: string;
  projectid: number;
  projectName: string;
  startDate: string;
  use: number;
  status:number;
  id?: number
}
