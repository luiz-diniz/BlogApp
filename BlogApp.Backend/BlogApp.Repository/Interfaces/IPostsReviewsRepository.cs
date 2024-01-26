using BlogApp.Models;
using System.Data;

namespace BlogApp.Repository.Interfaces;

public interface IPostsReviewsRepository
{
    void Add(Post post, IDbConnection connection, IDbTransaction transaction);
    void Update(PostReview postReview);
}