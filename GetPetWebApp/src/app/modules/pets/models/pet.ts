import { ITrait } from "src/app/shared/models/itrait";
import { ITraitOption } from "src/app/shared/models/itrait-option";
import { IUser } from "src/app/shared/models/iuser";

export interface Pet {
  id?: number;

  name: string;

  description?: string;

  birthday: Date;

  gender: number;

  animalTypeId: number;

  animalType?: string;

  status: number;

  userId: number;

  user?: IUser;

  images: string[];

  //formFiles: FormData;

  traits: { [key: string]: string; };

  source: number;

  sourceLink?: string;

  UpdatedTimestamp?: Date;

  creationTimestamp: Date;
}