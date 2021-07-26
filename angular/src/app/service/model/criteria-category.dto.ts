export class CriteriaCategoryDto{
    name: string;
    id?:number;
}
export class CriteriaDto{
    name: string;
    weight: number;
    criteriaCatagoryName: string;
    note: string;
    id?: number
    criteriaCategoryId:number;
}