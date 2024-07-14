using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using System.Data;

namespace BlogApp.Repository.Interfaces;

public interface IPostsReviewsRepository
{
    void Add(Post postModel, IDbConnection connection, IDbTransaction transaction);
    void Update(PostReview postReviewModel);
    IEnumerable<PostReviewInfo> GetReviewPosts();
}