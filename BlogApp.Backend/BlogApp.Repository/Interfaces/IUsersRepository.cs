using BlogApp.Models;

namespace BlogApp.Repository.Interfaces;

public interface IUsersRepository
{
    void Add(User user);
    User Get(int id);
}