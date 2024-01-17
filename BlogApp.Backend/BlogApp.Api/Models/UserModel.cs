using BlogApp.Models.Enums;
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

    public string ProfileImageContent { get; set; }

    [Required]
    public RoleEnum Role {  get; set; }
}