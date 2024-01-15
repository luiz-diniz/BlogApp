using BlogApp.Api.Models;
using BlogApp.Models;

namespace BlogApp.Api.Extensions;

public static class UserConverter
{
    public static User ConvertModelToUser(this UserModel user)
    {
        return new User
        {
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            ProfilePictureName = user.ProfilePictureName,
            Role = new UserRole
            {
                Id = Convert.ToInt32(user.Role)
            }
        };
    }
}