using BlogApp.Models;
using BlogApp.Models.InputModels;

namespace BlogApp.Repository.Interfaces;

public interface IUsersRepository
{
    void Add(UserModel userModel);
    User GetUserCredentials(string username);
    bool VerifyUserExist(string username);
}