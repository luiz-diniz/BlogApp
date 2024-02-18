using BlogApp.Models;
using BlogApp.Models.InputModels;
using System.Data;

namespace BlogApp.Repository.Interfaces;

public interface IPostsRepository
{
    int Add(PostModel post, IDbConnection connection, IDbTransaction transaction);
    Post Get(int id);
    IEnumerable<PostFeed> GetFeedPosts();
    void Publish(int idPost, IDbConnection connection, IDbTransaction transaction);
}