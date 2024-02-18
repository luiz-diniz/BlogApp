using BlogApp.Models;
using BlogApp.Models.InputModels;

namespace BlogApp.Core.Intefaces;

public interface IPostsService
{
    void Add(PostModel postModel);
    Post Get(int id);
    IEnumerable<PostFeed> GetFeedPosts();
}