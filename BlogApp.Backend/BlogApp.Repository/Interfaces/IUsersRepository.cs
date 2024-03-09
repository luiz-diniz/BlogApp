using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;

namespace BlogApp.Repository.Interfaces;

public interface IUsersRepository
{
    void Add(User userModel);
    UserCredentialsModel GetUserCredentials(string username);
    bool VerifyUserExist(string username);
    UserProfile GetProfileInfo(int id);
}