import { PostCommentModel } from "./post.comment.model";
import { PostInfoBaseModel } from "./post.info.base.model";

export interface PostModel extends PostInfoBaseModel{
    likesCount: number;
    comments?: PostCommentModel[];
}