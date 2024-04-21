import { UserModel } from "./user.model";

export class PostCommentModel{
    id?: number;
    user?: UserModel;
    comment?: string;
    creationDate?: Date;
}