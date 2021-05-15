import { IOrganization } from "./iorganization";

export interface IUser {
    id: number;
    email: string;
    password: string;
    name: string;
    cityId: number;
    token?: string;
    organization: IOrganization;
}