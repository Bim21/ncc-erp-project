export class PosistionTalentDto {
    id: number;
    name: string;
    code: string;
    colorCode: string;
}

export class BranchTalentDto {
    id: number;
    name: string;
    color: string;
}

export class SendRecuitmentDto {
    resourceRequestId: number;
    positionId: number;
    branchId: number;
}