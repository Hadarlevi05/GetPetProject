import { IUser } from "src/app/shared/models/iuser";

export interface IComment {
    text: string;
    user: IUser;
}