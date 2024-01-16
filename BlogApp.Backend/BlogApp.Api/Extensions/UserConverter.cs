using BlogApp.Api.Models;
using BlogApp.Models;

namespace BlogApp.Api.Extensions;

public static class UserConverter
{
    public static User ConvertModelToUser(this UserModel user)
    {
        if(user is null)
            throw new ArgumentNullException(nameof(user), "UserModel null.");

        return new User
        {
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            ProfilePictureContent = user.ProfilePictureContent,
            Role = new UserRole
            {
                Id = Convert.ToInt32(user.Role)
            }
        };
    }
}