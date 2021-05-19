import { ITraitOption } from "./iTraitOption";

export interface ITrait {
    
    id: number;
    
    name: string;

    animalTypeId: number;

    traitOptions: ITraitOption[];

}