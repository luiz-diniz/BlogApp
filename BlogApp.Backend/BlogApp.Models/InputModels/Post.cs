using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.Models.InputModels;

public class Post
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public string? PostImageContent { get; set; }

    [JsonIgnore]
    public string? PostImageName { get; set; }

    [Required]
    public int IdUser { get; set; }

    [Required]
    public int IdCategory { get; set; }
}