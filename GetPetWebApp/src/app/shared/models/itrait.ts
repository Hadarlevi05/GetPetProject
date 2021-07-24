import { ITraitOption } from "./itraitoption";

export interface ITrait {

    id: number;

    name: string;

    animalTypeId: number;

    traitOptions: ITraitOption[];

    isBoolean: boolean;

}