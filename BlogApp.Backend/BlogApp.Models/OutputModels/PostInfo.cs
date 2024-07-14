namespace BlogApp.Models.OutputModels;

public class PostInfo : PostBase
{
    public string Content { get; set; }
    public string PostImageContent { get; set; }
    public string PostImageName { get; set; }
    public DateTime? PublishedDate { get; set; }
    public UserProfile User { get; set; }
    public PostCategory Category { get; set; }
    public IEnumerable<PostCommentContent> Comments { get; set; }
    public int LikesCount { get; set; }
}