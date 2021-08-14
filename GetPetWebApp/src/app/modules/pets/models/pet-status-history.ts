import { PetStatus } from "src/app/shared/enums/pet-status";

export interface IPetHistoryStatus {
    id?: number;
    petId: number;
    status: PetStatus;
    remarks: string;
}