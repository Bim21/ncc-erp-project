export class ResourceRequestDto {
    id: number;
    name: string;
    creationTime: Date;
    timeNeed: Date;
    timeDone: Date;
    level: number;
    priority: number;
    pmNote: string;
    dmNote: string;
    isRecruitmentSend: boolean;
    recruitmentUrl: string;
    skills: ResourceRequestSkillDto[];
    planUserInfo: UserPlanDto;
    projectId: number;
    projectName: string;
    projectType: number;
    projectStatus: number;
    projectTypeName: string;
    projectStatusName: string;
    komuInfo: string;
    skillIds: number[];
    skillName: string;
    statusName: string;
    priorityName: string;
    levelName: string;
}

export class ResourceRequestTrainingDto {
    id: number;
    name: string;
    creationTime: Date;
    timeNeed: Date;
    timeDone: Date;
    level: number;
    priority: number;
    pmNote: string;
    dmNote: string;
    isRecruitmentSend: boolean;
    recruitmentUrl: string;
    skills: ResourceRequestSkillDto[];
    planUserInfo: UserPlanDto;
    projectId: number;
    projectName: string;
    projectType: number;
    projectStatus: number;
    projectTypeName: string;
    projectStatusName: string;
    komuInfo: string;
    skillIds: number[];
    skillName: string;
    statusName: string;
    priorityName: string;
    levelName: string;
    position: ResourceRequestPositionDto[];
}
export class UserPlanDto {
    projectUserId: number;
    plannedDate: Date;
    role: number;
    komuInfo: string;
}

export class ResourceRequestSkillDto {
    id: number;
    name: string;
}

export class ResourceRequestPositionDto {
    id: number;
    name: string;
}
