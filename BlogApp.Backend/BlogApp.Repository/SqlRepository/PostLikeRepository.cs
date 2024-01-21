using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class PostLikeRepository : IPostLikeRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public PostLikeRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void AddLike(int idPost, int idUser)
    {
        var query = "INSERT INTO [PostLikes] (IdPost, IdUser) VALUES (@P0, @P1);";

        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            idPost,
            idUser
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public void RemoveLike(int idPost, int idUser)
    {
        var query = "DELETE FROM [PostLikes] WHERE IdPost = @P0 AND IdUser = @P1;";

        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            idPost,
            idUser
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public bool VerifyPostLiked(int idPost, int idUser)
    {
        var query = "SELECT COUNT(*) AS COUNT FROM [PostLikes] WHERE IdPost = @P0 AND IdUser = @P1;";

        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            idPost,
            idUser
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        var reader = cmd.ExecuteReader();
        
        if(reader.Read())
          return Convert.ToInt32(reader["COUNT"]) == 1;

        return false;
    }
}