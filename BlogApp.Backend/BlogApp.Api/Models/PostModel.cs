using System.ComponentModel.DataAnnotations;

namespace BlogApp.Api.Models;

public class PostModel
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public string PostImageContent { get; set; }

    [Required]
    public int IdUserAuthor { get; set; }

    [Required]
    public int IdCategory { get; set; }
}