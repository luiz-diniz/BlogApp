namespace BlogApp.Models;

public class PostReview
{
    public int Id { get; set; }
    public User UserAuthor { get; set; }
    public User UserReviewer { get; set; }
    public string Status { get; set; }
    public string Feedback { get; set; }
    public DateTime? ReviewDate { get; set; }
}