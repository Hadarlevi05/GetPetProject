import { BaseFilter } from './../../../shared/models/baseFilter';

export class PetFilter extends BaseFilter {

    constructor(public page: number, public perPage: number, public createdSince: Date) {

        super(page, perPage,createdSince);
    }
}