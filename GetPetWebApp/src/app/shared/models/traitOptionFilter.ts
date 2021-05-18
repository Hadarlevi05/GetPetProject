import { BaseFilter } from './base-filter';

export class TraitOptionFilter extends BaseFilter {

    traitId: number = 0;

    constructor(public page: number, public perPage: number, public createdSince: Date, public id: number = 0) {

        super(page, perPage,createdSince);
        this.traitId = id;
    }
}