using BlogApp.Models;
using System.Data;

namespace BlogApp.Repository.Interfaces;

public interface IPostRepository
{
    int Add(Post post, IDbConnection connection, IDbTransaction transaction);
    Post Get(int id);
    IEnumerable<Post> GetAll();
}