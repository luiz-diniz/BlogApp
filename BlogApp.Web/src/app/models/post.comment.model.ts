import { UserModel } from "./user.model";

export interface PostCommentModel{
    id: number;
    user: UserModel;
    comment: string;
    creationDate: Date;
}