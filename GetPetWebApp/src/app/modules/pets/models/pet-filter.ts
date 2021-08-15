import { PetStatus } from 'src/app/shared/enums/pet-status';
import { BaseFilter } from '../../../shared/models/base-filter';

export class PetFilter extends BaseFilter {

    constructor(
        public page: number,
        public perPage: number,
        public createdSince: Date,
        public animalTypes: number[],
        public traitValues?: {},
        public booleanTraits?: number[],
        public petStatus?: PetStatus) {

        super(page, perPage, createdSince);
    }
}