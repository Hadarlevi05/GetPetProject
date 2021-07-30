import { ITrait } from "src/app/shared/models/itrait";
import { ITraitOption } from "src/app/shared/models/itraitoption";
import { IUser } from "src/app/shared/models/iuser";

export interface Pet {
  id?: number;

  name: string;

  description?: string;

  birthday: Date;

  gender: number;

  animalTypeId: number;

  animalType?: string;

  status?: string;

  userId: number;

  user?: IUser;

  images: string[];

  traits: {[key: string]: string; };

  traitsDTOs?: Map<ITrait, ITraitOption>;

  petSource: number;

  sourceLink?: string;

  UpdatedTimestamp?: Date;

  creationTimestamp: Date;
}