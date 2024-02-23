namespace BlogApp.Models;

public class PostComment
{
    public int Id { get; set; }
    public User User { get; set; }
    public string Comment { get; set; }
    public DateTime CreationDate { get; set; }
}