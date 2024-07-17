import { PostCommentModel } from "./post.comment.model";
import { PostInfoBaseModel } from "./post.info.base.model";

export interface PostModel extends PostInfoBaseModel{
    publishedDate: string;
    likesCount: number;
    comments?: PostCommentModel[];
}