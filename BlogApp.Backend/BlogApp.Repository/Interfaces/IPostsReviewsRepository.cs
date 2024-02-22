using BlogApp.Models;
using BlogApp.Models.InputModels;
using System.Data;

namespace BlogApp.Repository.Interfaces;

public interface IPostsReviewsRepository
{
    void Add(PostModel postModel, IDbConnection connection, IDbTransaction transaction);
    void Update(PostReviewModel postReviewModel);
}