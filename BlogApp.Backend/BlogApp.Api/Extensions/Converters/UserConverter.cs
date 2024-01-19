using BlogApp.Api.Models;
using BlogApp.Models;
using Microsoft.Extensions.Hosting;

namespace BlogApp.Api.Extensions.Converters;

public static class UserConverter
{
    public static User ConvertModelToUser(this UserModel user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user), "UserModel null.");

        return new User
        {
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            ProfileImageContent = user.ProfileImageContent,
            Role = new UserRole
            {
                Id = user.IdRole
            }
        };
    }
}