using System.ComponentModel.DataAnnotations;

namespace BlogApp.Api.Models;

public class PostCommentModel
{
    [Required]
    public int IdPost { get; set; }

    [Required]
    public int IdUser { get; set; }

    [Required]
    public string Comment { get; set; }
}