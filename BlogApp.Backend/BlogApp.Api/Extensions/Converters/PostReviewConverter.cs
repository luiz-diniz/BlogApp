using BlogApp.Api.Models;
using BlogApp.Models;

namespace BlogApp.Api.Extensions.Converters;

public static class PostReviewConverter
{
    public static PostReview ConvertModelToPostReview(this PostReviewModel post)
    {
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