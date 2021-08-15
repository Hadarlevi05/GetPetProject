import { IUser } from "src/app/shared/models/iuser";
import { IPetHistoryStatus } from "./pet-status-history";

export interface IPet {
  id?: number;

  name: string;

  description?: string;

  birthday: string;

  gender?: string;

  animalTypeId: number;

  animalType?: string;

  status?: string;

  userId: number;

  user?: IUser;

  sourceLink?: string;

  images: string[];

  petHistoryStatuses: IPetHistoryStatus[];

  traits: Map<string, string>;

  booleanTraits: Map<string, string>;

  creationTimestamp: Date;
}