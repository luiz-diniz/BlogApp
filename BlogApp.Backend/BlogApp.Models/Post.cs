namespace BlogApp.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string PostImageContent { get; set; }
    public string PostImageName { get; set; }
    public DateTime? PublishedDate { get; set; }
    public User User { get; set; }
    public PostCategory Category { get; set; }
    public IEnumerable<PostComment> Comments { get;set; }
    public int LikesCount { get; set; }
}