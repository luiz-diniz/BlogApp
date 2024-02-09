using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace BlogApp.Repository.SqlRepository;

public class SavedPostsRepository : ISavedPostsRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public SavedPostsRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Save(int idPost, int idUser)
    {
        var query = "INSERT INTO [SavedPosts] (IdPost, IdUser) VALUES (@P0, @P1);";

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

    public void Delete(int idSavedPost)
    {
        var query = "DELETE FROM [SavedPosts] WHERE Id = @P0";

        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            idSavedPost
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }
}