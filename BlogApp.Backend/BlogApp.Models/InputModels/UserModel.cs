using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BlogApp.Models.InputModels;

public class UserModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    public string? ProfileImageContent { get; set; }

    [JsonIgnore]
    public string? ProfileImageName { get; set; }

    [Required]
    public int IdRole {  get; set; }
}