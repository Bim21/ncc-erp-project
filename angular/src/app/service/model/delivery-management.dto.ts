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
  userName :  string ;
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
