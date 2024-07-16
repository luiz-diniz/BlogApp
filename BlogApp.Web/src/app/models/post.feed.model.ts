import { PostBaseModel } from "./post.base.model";
import { PostCategoryModel } from "./post.category.model";
import { UserModel } from "./user.model";

export interface PostFeedModel extends PostBaseModel{
    user: UserModel;
    category: PostCategoryModel;
    publishDate: Date;
    likesCount: number;
    commentsCount: number;
}