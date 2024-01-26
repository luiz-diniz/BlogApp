using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data;
using BlogApp.Models.Enums;

namespace BlogApp.Repository.SqlRepository;

public class PostsReviewsRepository : IPostsReviewsRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public PostsReviewsRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Add(Post post, IDbConnection connection, IDbTransaction transaction)
    {
        var query = "INSERT INTO [PostsReviews] (IdPost, Status) VALUES (@P0, @P1);";

        using var cmd = new SqlCommand(query, connection as SqlConnection, transaction as SqlTransaction);

        var parameters = new object[]
        {
            post.Id,
            (int)StatusEnum.Pending
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public void Update(PostReview postReview)
    {
        var query = "UPDATE [PostsReviews] SET [IdUserReviewer] = @P0, [Status] = @P1, [Feedback] = @P2, [ReviewDate] = @P3 WHERE [IdPost] = @P4;";

        using var connection = _connectionFactory.CreateConnection() as SqlConnection;

        using var cmd = new SqlCommand(query, connection);

        var parameters = new object[]
        {
            postReview.UserReviewer.Id,
            (int)postReview.Status,
            postReview.Feedback,
            postReview.ReviewDate!,
            postReview.Post.Id
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }
}