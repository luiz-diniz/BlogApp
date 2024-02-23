import { UserModel } from "./user.model";

export class PostCommentModel{
    Id?: number;
    User?: UserModel;
    Comment?: string;
    CreationDate?: Date;
}