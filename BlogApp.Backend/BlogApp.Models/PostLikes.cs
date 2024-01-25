namespace BlogApp.Models;

public class PostLikes
{
    public int Id { get; set; }
    public User User { get; set; }
    public DateTime LikeDate { get; set; }
}