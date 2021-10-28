export interface ProjectDto {
    name: string;
    code: string;
    projectType: number;
    startTime: string;
    endTime: string;
    status: number;
    clientId: number;
    clientName?: string;
    isCharge: boolean;
    pmId: number;
    pmName?:string;
    id: number;
    currencyId: string;
}

export interface ClientDto {
    name: string;
    code: string;
    id: number;
}

export interface SkillDto {
    name: string;
    id: number;
}
export class projectForDM{
    projectName: string;
    pmName: string;
    listUsers: [];
    problemsOfTheWeek: []
}
