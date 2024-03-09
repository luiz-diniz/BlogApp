using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using System.Data;

namespace BlogApp.Repository.Interfaces;

public interface IPostsRepository
{
    int Add(Post post, IDbConnection connection, IDbTransaction transaction);
    PostInfo Get(int id);
    IEnumerable<PostFeed> GetFeedPosts();
    IEnumerable<PostFeed> GetProfileFeedPosts();
}