export class ChecklistTitleDto{
  id?:number;
  name: string;
}
export class ChecklistDto{
    name: string;
    code: string;
    categoryId: number;
    title: string;
    description: string;
    mandatorys: [
      
    ];
    auditTarget: string;
    personInCharge: string;
    note: string;
    id: number;
}