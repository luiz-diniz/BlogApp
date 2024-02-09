using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class PostsLikesRepository : IPostsLikesRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public PostsLikesRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void AddLike(PostLikeModel postLikeModel)
    {
        var query = "INSERT INTO [PostsLikes] (IdPost, IdUser) VALUES (@P0, @P1);";

        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            postLikeModel.IdPost,
            postLikeModel.IdUser
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public void RemoveLike(PostLikeModel postLikeModel)
    {
        var query = "DELETE FROM [PostsLikes] WHERE IdPost = @P0 AND IdUser = @P1;";

        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            postLikeModel.IdPost,
            postLikeModel.IdUser
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public bool VerifyPostLiked(PostLikeModel postLikeModel)
    {
        var query = "SELECT COUNT(*) AS COUNT FROM [PostsLikes] WHERE IdPost = @P0 AND IdUser = @P1;";

        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            postLikeModel.IdPost,
            postLikeModel.IdUser
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        var reader = cmd.ExecuteReader();
        
        if(reader.Read())
          return Convert.ToInt32(reader["COUNT"]) == 1;

        return false;
    }
}