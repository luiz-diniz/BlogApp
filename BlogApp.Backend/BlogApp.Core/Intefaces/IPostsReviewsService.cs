using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;

namespace BlogApp.Core.Intefaces;

public interface IPostsReviewsService
{
    public void Update(PostReview postReviewModel);
    IEnumerable<PostReviewInfo> GetPostsReviews();
}