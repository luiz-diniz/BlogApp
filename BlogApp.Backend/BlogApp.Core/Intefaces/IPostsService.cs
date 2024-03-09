using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;

namespace BlogApp.Core.Intefaces;

public interface IPostsService
{
    void Add(Post postModel);
    PostInfo Get(int id);
    IEnumerable<PostFeed> GetFeedPosts();
}