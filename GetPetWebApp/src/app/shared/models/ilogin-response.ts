import { IUser } from "./iuser";

export interface ILoginResponse {
    token: string;
    user: IUser
}