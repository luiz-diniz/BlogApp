namespace BlogApp.Models;

public class PostComments
{
    public int Id { get; set; }
    public Post Post { get; set; }
    public User User { get; set; }
    public string Comment { get; set; }
    public DateTime CommentDate { get; set; }
}