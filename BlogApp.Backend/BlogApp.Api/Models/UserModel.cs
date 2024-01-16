using BlogApp.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Api.Models;

public class UserModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    public string ProfilePictureContent { get; set; }

    [Required]
    public RoleEnum Role {  get; set; }
}