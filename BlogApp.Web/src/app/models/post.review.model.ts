import { PostBaseModel } from "./post.base.model";

export interface PostReviewModel extends PostBaseModel{
    username: string;
    status: number;
    creationDate: Date;
}