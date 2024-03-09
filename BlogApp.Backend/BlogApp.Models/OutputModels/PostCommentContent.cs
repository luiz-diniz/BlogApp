namespace BlogApp.Models.OutputModels;

public class PostCommentContent
{
    public int Id { get; set; }
    public UserProfile User { get; set; }
    public string Comment { get; set; }
    public DateTime CreationDate { get; set; }
}