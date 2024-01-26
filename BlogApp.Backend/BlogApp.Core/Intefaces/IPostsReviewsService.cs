using BlogApp.Models;

namespace BlogApp.Core.Intefaces;

public interface IPostsReviewsService
{
    public void Update(PostReview postReview);
}