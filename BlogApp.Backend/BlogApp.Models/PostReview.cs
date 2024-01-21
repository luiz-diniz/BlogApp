using BlogApp.Models.Enums;

namespace BlogApp.Models;

public class PostReview
{
    public int Id { get; set; }
    public Post Post { get; set; }
    public User UserReviewer { get; set; }
    public StatusEnum Status { get; set; }
    public string Feedback { get; set; }
    public DateTime? ReviewDate { get; set; }
}