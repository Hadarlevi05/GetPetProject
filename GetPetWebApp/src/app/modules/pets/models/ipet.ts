import { IUser } from "src/app/shared/models/iuser";

export interface IPet {
  id: number;

  name: string;

  description: string;

  birthday: string;

  gender: string;

  animalType: string;

  status: string;

  user: IUser;

  images: string[];

  traits: Map<string, string>;
}