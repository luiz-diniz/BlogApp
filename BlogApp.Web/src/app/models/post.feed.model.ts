import { PostCategoryModel } from "./post.category.model";
import { UserModel } from "./user.model";

export class PostFeedModel{
    id?: number;
    title?: string;
    user?: UserModel;
    category?: PostCategoryModel;
    publishDate?: Date;
    likesCount?: number;
    commentsCount?: number;
}