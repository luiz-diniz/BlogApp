using System.ComponentModel.DataAnnotations;

namespace BlogApp.Api.Models;

public class SavedPostModel
{
    [Required]
    public int IdPost { get; set; }

    [Required]
    public int IdUser { get; set; }
}