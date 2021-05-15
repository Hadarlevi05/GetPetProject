import { BaseFilter } from "./baseFilter";


export class AnimalTraitFilter extends BaseFilter {

    animalTypeId: number = 0;

    animalTypeName: string = '';

    constructor(public page: number, public perPage: number, public createdSince: Date, public id: number) {

        super(page, perPage, createdSince);
        this.animalTypeId = id;
    }
}