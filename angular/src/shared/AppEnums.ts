import { TenantAvailabilityState } from '@shared/service-proxies/service-proxies';


export class AppTenantAvailabilityState {
    static Available: number = TenantAvailabilityState._1;
    static InActive: number = TenantAvailabilityState._2;
    static NotFound: number = TenantAvailabilityState._3;
}
export const APP_ENUMS = {
    ProjectType: {
        ODC: 0,
        'T&M': 1,
        FIXPRICE: 2,
        PRODUCT: 3
    },
    ProjectStatus: {
        POTENTIAL: 0,
        'IN PROGRESS': 1,
        MAINTAIN: 2,
        CLOSED: 3
    }

    
}
