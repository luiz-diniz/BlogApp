using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;

namespace BlogApp.Core.Intefaces;

public interface IUsersService
{
    void Add(User userModel);
    UserProfile GetUserProfile(string username);
}