using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.InputModels;

public class Login
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}