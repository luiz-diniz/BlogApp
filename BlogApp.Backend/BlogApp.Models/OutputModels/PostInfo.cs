namespace BlogApp.Models.OutputModels;

public class PostInfo : PostInfoBase
{    
    public DateTime? PublishedDate { get; set; }
    public IEnumerable<PostCommentContent>? Comments { get; set; }
    public int? LikesCount { get; set; }
}