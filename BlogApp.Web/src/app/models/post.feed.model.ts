import { PostCategoryModel } from "./post.category.model";
import { UserModel } from "./user.model";

export class PostFeedModel{
    Id?: number;
    Title?: string;
    User?: UserModel;
    Category?: PostCategoryModel;
    PublishDate?: Date;
    LikesCount?: number;
    CommentsCount?: number;
}