using BlogApp.Models;

namespace BlogApp.Core.Intefaces;

public interface IPostsService
{
    void Add(Post post);
    Post Get(int id);
    IEnumerable<Post> GetAll();
}