import { BaseFilter } from '../../../shared/models/base-filter';

export class PetFilter extends BaseFilter {

    constructor(public page: number, public perPage: number, public createdSince: Date) {

        super(page, perPage, createdSince);
    }
}