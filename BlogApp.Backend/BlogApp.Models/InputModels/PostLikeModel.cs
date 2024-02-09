using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.InputModels;

public class PostLikeModel
{
    [Required]
    public int IdPost { get; set; }

    [Required]
    public int IdUser { get; set; }
}