import { SafeResourceUrl } from "@angular/platform-browser";
import { UserModel } from "./user.model";
import { PostCategoryModel } from "./post.category.model";
import { PostCommentModel } from "./post.comment.model";

export class PostModel{
    Id?: number;
    Title?: string;
    Content?: string;
    PostImageContent?: string;
    PostImageContentSafe?: SafeResourceUrl;
    PublishedDate?: string;
    LikesCount?: number;
    User?: UserModel;
    Category?: PostCategoryModel;
    Comments?: PostCommentModel[];
}