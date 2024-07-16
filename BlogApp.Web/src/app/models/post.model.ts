import { SafeResourceUrl } from "@angular/platform-browser";
import { UserModel } from "./user.model";
import { PostCategoryModel } from "./post.category.model";
import { PostCommentModel } from "./post.comment.model";
import { PostBaseModel } from "./post.base.model";

export interface PostModel extends PostBaseModel{
    content: string;
    postImageContent?: string;
    postImageContentSafe?: SafeResourceUrl;
    publishedDate: string;
    likesCount: number;
    user: UserModel;
    category: PostCategoryModel;
    comments?: PostCommentModel[];
}