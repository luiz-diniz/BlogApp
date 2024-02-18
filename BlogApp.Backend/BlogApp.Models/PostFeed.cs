namespace BlogApp.Models;

public class PostFeed
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public string Username { get; set; }
    public int IdCategory { get; set; }
    public string Category { get; set; }
    public string Title { get; set; }
    public DateTime PublishDate { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
}