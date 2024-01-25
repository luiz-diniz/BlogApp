using BlogApp.Api.Models;
using BlogApp.Models;

namespace BlogApp.Api.Extensions.Converters;

public static class PostReviewConverter
{
    public static PostReview ConvertModelToPostReview(this PostReviewModel post)
    {
        if(post is null)
            throw new ArgumentNullException(nameof(post), "PostReviewModel is null.");

        return new PostReview
        {
            Post = new Post
            {
                Id = post.IdPost,
            },
            UserReviewer = new User
            {
                Id = post.IdUserReviewer
            },
            Status = post.Status,
            Feedback = post.Feedback,
            ReviewDate = DateTime.Now
        };
    }
}