using BlogApp.Models;

namespace BlogApp.Core.Intefaces;

public interface IPostReviewService
{
    public void Update(PostReview postReview);
}