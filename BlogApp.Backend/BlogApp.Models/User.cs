namespace BlogApp.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfileImageName { get; set; }
    public string ProfileImageContent { get; set; }
    public UserRole Role { get; set; }
}