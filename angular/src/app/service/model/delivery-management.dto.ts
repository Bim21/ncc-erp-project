export class RequestResourceDto{
  name :  string  ;
  projectId : number ;
  projectName? :  string  ;
  timeNeed :  string  ;
  status : number;
  statusName? :  string ;
  timeDone :  string;
  pmNote?: string ;
  dmNote?: string ;
  plannedNumberOfPersonnel? : number;
  id?: number;
  userSkills?:any;
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
  status:number;
}
export class availableResourceDto{
  userId: number;
  userName: string;
  projects: [

  ];
  used:number;
  listSkills:any[];
}
export class planUserDto{
  projectId: number;
  userId: number;
  percentUsage: number;
  projectRole: number;
  startTime: string;
  isExpense?: true;
  fullName:string;
}
export class editFutureResourceDto{
  fullName:string;
  userId: number;
  projectId: number;
  allocatePercentage: number;
  startTime: string;
  id?: number
  status:number;
}
export class futureResourceDto{
  fullName:string;
  userId: number;
  userName: string;
  projectid: number;
  projectName: string;
  startDate: string;
  use: number;
  status:number;
  id?: number
}
