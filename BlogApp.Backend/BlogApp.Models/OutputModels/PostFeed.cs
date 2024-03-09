namespace BlogApp.Models.OutputModels;

public class PostFeed
{
    public int Id { get; set; }
    public string Title { get; set; }
    public UserProfile User { get; set; }
    public PostCategory Category { get; set; }
    public DateTime PublishDate { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
}