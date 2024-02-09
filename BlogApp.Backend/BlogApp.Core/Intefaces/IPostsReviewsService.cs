using BlogApp.Models;
using BlogApp.Models.InputModels;

namespace BlogApp.Core.Intefaces;

public interface IPostsReviewsService
{
    public void Update(PostReviewModel postReviewModel);
}