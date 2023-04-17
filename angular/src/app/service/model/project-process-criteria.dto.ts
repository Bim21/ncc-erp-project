export class GetAllProjectProcessCriteriaDto {
    clientName: string;
    id: number;
    listProcessCriteriaIds: number[];
    pmName: string;
    processCriteriaId: number;
    projectCode: string;
    projectId: number;
    projectName: string;
    projectType: string;
}
export class GetAllPagingProjectProcessCriteriaDto{
    projectId: number;
    projectCode: string;
    projectName: string;
    projectType: string;
    pmName: string;
    clientName: string;
    selected: boolean;
}
export class CreateProjectProcessCriteriaDto{
    projectIds: number[];
}
