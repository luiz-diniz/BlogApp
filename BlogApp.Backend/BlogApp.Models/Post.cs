using BlogApp.Models.Enums;

namespace BlogApp.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string PostImageContent { get; set; }
    public string PostImageName { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? PublishedDate { get; set; }
    public int ViewCount { get; set; }
    public User UserAuthor { get; set; }
    public PostCategoryEnum Category { get; set; }
}