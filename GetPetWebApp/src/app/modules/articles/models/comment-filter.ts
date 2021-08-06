import { BaseFilter } from "../../../shared/models/base-filter";

export class CommentFilter extends BaseFilter {

    constructor(
        public page: number,
        public perPage: number,
        public createdSince: Date,
        public articleId?: number,
        public userId?: number) {

        super(page, perPage, createdSince);
    }
}