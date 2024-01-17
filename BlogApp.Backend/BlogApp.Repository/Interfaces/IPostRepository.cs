using BlogApp.Models;

namespace BlogApp.Repository.Interfaces;

public interface IPostRepository
{
    void Add(Post post);
}