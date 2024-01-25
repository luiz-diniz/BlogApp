using BlogApp.Api.Models;
using BlogApp.Models;

namespace BlogApp.Api.Extensions.Converters;

public static class PostCommentConverter
{
    public static PostComment ConvertToPostComment(this PostCommentModel postComment)
    {
        if(postComment is null)
            throw new ArgumentNullException(nameof(postComment), "PostCommentModel is null.");

        return new PostComment
        {
            IdPost = postComment.IdPost,
            User = new User
            {
                Id = postComment.IdUser
            },
            Comment = postComment.Comment
        };
    }
}