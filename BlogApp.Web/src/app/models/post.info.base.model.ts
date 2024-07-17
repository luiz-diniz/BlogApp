import { SafeResourceUrl } from "@angular/platform-browser";
import { PostBaseModel } from "./post.base.model";
import { UserModel } from "./user.model";
import { PostCategoryModel } from "./post.category.model";

export interface PostInfoBaseModel extends PostBaseModel{
    content: string;
    postImageContent?: string;
    postImageContentSafe?: SafeResourceUrl;
    user: UserModel;
    category: PostCategoryModel;
}