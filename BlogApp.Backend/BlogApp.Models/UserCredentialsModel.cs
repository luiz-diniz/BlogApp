using BlogApp.Models.OutputModels;

namespace BlogApp.Models
{
    public class UserCredentialsModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
