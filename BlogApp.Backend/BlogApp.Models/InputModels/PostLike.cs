using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.InputModels;

public class PostLike
{
    [Required]
    public int IdPost { get; set; }

    [Required]
    public int IdUser { get; set; }
}