export class ResourcePlanDto{
    constructor(){
        this.projectUserId = 0;
        this.startTime = new Date();
        this.userId = 0;
        this.resourceRequestId = 0
    }
    public projectRole?: number
    public projectUserId?: number
    public resourceRequestId?: number
    public startTime?: any
    public userId?: number
}