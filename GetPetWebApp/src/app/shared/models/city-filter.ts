import { BaseFilter } from "./base-filter";

export class CityFilter extends BaseFilter {

    constructor(public page: number, public perPage: number, public createdSince: Date) {
        super(page, perPage, createdSince);
    }
}