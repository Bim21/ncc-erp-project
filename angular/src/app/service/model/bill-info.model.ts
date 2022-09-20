export class ParentInvoice {
    projectId: number;
    parentId?: number;
    isMainInvoice: boolean;
    subInvoices: SubInvoice[];
}
export class SubInvoice {
    projectId: number;
    projectName: string;
}