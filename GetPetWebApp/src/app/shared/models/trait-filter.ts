import { BaseFilter } from "./base-filter";

export class TraitFilter extends BaseFilter {

    animalTypeId: number = 0;
    
    constructor(public page: number, public perPage: number, public createdSince: Date, public id: number) {

        super(page, perPage, createdSince);
        this.animalTypeId = id;
    }
}