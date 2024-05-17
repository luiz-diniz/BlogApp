using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;

namespace BlogApp.Repository.Interfaces;

public interface IUsersRepository
{
    void Add(User userModel);
    UserCredentials GetUserCredentials(string username);
    bool VerifyUserExist(string username);
    bool VerifyEmailExist(string email);
    UserProfile GetProfileInfo(string username);
}