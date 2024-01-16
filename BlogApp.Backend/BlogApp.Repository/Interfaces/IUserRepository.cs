using BlogApp.Models;

namespace BlogApp.Repository.Interfaces;

public interface IUserRepository
{
    void Add(User user);
    User Get(int id);
}