import { CategoryModel } from "./category.model";
import { UserModel } from "./user.model";

export class PostFeedModel{
    Id?: number;
    Title?: string;
    User?: UserModel;
    Category?: CategoryModel;
    PublishDate?: Date;
    LikesCount?: number;
    CommentsCount?: number;
}