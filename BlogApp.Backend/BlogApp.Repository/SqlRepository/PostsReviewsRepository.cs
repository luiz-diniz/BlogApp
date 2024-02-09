using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data;
using BlogApp.Models.Enums;
using BlogApp.Models.InputModels;

namespace BlogApp.Repository.SqlRepository;

public class PostsReviewsRepository : IPostsReviewsRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public PostsReviewsRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Add(PostModel postModel, IDbConnection connection, IDbTransaction transaction)
    {
        var query = "INSERT INTO [PostsReviews] (IdPost, Status) VALUES (@P0, @P1);";

        using var cmd = new SqlCommand(query, connection as SqlConnection, transaction as SqlTransaction);

        var parameters = new object[]
        {
            postModel.Id,
            (int)StatusEnum.Pending
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public void Update(PostReviewModel postReviewModel)
    {
        var query = "UPDATE [PostsReviews] SET [IdUserReviewer] = @P0, [Status] = @P1, [Feedback] = @P2, [ReviewDate] = @P3 WHERE [IdPost] = @P4;";

        using var connection = _connectionFactory.CreateConnection() as SqlConnection;

        using var cmd = new SqlCommand(query, connection);

        var parameters = new object[]
        {
            postReviewModel.IdUserReviewer,
            (int)postReviewModel.Status,
            postReviewModel.Feedback,
            DateTime.Now,
            postReviewModel.IdPost
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public void Publish(PostReviewModel postReviewModel, IDbConnection connection, IDbTransaction transaction)
    {
        var query = "UPDATE [PostsReviews] SET [IdUserReviewer] = @P0, [Status] = @P1, [Feedback] = @P2, [ReviewDate] = @P3 WHERE [IdPost] = @P4;";

        using var cmd = new SqlCommand(query, connection as SqlConnection, transaction as SqlTransaction);

        var parameters = new object[]
        {
            postReviewModel.IdUserReviewer,
            (int)postReviewModel.Status,
            postReviewModel.Feedback,
            DateTime.Now,
            postReviewModel.IdPost
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }
}