export class BaseFilter {

    constructor(public page: number, public perPage: number, public createdSince: Date) {
    }
}