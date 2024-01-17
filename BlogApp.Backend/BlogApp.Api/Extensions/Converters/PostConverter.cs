using BlogApp.Api.Models;
using BlogApp.Models;

namespace BlogApp.Api.Extensions.Converters;

public static class PostConverter
{
    public static Post ConvertModelToPost(this PostModel post)
    {
        if (post == null) 
            throw new ArgumentNullException(nameof(post), "PostModel null.");

        return new Post
        {
            Title = post.Title,
            Content = post.Content,
            PostImageContent = post.PostImageContent,
            UserAuthor = new User
            {
                Id = post.IdUserAuthor
            },
            Category = post.Category
        };
    }
}