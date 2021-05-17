import { ITraitOption } from "./iTraitOption";

export interface ITrait {
    
    name: string;

    animalTypeId: number;

    traitOptions: ITraitOption[];

}