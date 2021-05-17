import { DateFilterFn } from "@angular/material/datepicker";
import { IUser } from "src/app/shared/models/iuser";
import { IComment } from "./icomment";

export interface IArticle {
    id: number;
    title: string;
    content: string;
    user: IUser;
    imageUrl: string;
    comments: IComment[];
    creationTimestamp: Date;
    commentCount: number;
}