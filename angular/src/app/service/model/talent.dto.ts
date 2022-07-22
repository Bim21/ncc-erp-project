export class PosistionTalentDto {
    id: number;
    name: string;
    colorCode: string;
}

export class BranchTalentDto {
    id: number;
    name: string;
    color: string;
}

export class SendRecuitmentDto {
    resourceRequestId: number;
    subPositionId: number;
    branchId: number;
}