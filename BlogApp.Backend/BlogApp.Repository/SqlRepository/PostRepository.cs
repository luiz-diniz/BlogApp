using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class PostRepository : IPostRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public PostRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Add(Post post)
    {
        var query = @"INSERT INTO [Post] (IdUserAuthor, IdCategory, Title, Content, PostImageName)
            VALUES (@P0, @P1, @P2, @P3, @P4);";

        using var connection = _connectionFactory.Create() as SqlConnection;

        using var cmd = new SqlCommand(query, connection);

        var parameters = new object[]
        {
            post.UserAuthor.Id,
            (int)post.Category,
            post.Title,
            post.Content,
            post.PostImageName
        };

        ParametersBuilder.Build(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }
}