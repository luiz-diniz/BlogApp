using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data;
using BlogApp.Models.Enums;

namespace BlogApp.Repository.SqlRepository;

public class PostReviewRepository : IPostReviewRepository
{
    public void Add(Post post, IDbConnection connection, IDbTransaction transaction)
    {
        var query = "INSERT INTO [PostReview] (IdPost, IdUserAuthor, Status) VALUES (@P0, @P1, @P2);";

        using var cmd = new SqlCommand(query, connection as SqlConnection, transaction as SqlTransaction);

        var parameters = new object[]
        {
            post.Id,
            post.UserAuthor.Id,
            (int)StatusEnum.Pending
        };

        ParametersBuilder.Build(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }
}