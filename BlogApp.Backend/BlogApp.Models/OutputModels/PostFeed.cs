namespace BlogApp.Models.OutputModels;

public class PostFeed : PostBase
{
    public PostCategory Category { get; set; }
    public UserProfile User { get; set; }
    public DateTime PublishDate { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
}