import { ITraitOption } from "./itraitoption";

export interface ITrait {

    name: string;

    animalTypeId: number;

    traitOptions: ITraitOption[];

    isBoolean: boolean;

}