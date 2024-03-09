using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.InputModels;

public class PostComment
{
    [Required]
    public int IdPost { get; set; }

    [Required]
    public int IdUser { get; set; }

    [Required]
    public string Comment { get; set; }
}