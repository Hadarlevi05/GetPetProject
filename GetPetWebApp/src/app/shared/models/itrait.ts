import { ITraitOption } from "./itrait-option";

export interface ITrait {

    id: number;

    name: string;

    animalTypeId: number;

    traitOptions: ITraitOption[];

    isBoolean: boolean;

}
