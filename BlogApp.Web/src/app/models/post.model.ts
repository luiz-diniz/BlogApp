import { SafeResourceUrl } from "@angular/platform-browser";
import { UserModel } from "./user.model";
import { PostCategoryModel } from "./post.category.model";
import { PostCommentModel } from "./post.comment.model";

export class PostModel{
    id: number;
    title: string;
    content: string;
    postImageContent?: string;
    postImageContentSafe?: SafeResourceUrl;
    publishedDate: string;
    likesCount: number;
    user: UserModel;
    category: PostCategoryModel;
    comments?: PostCommentModel[];
}