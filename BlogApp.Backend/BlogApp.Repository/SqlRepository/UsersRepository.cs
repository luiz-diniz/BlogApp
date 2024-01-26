using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class UsersRepository : IUsersRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public UsersRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Add(User user)
    {
        var query = @"INSERT INTO [Users] (IdRole, Username, Email, Password, ProfileImageName)
            VALUES (@P0, @P1, @P2, @P3, @P4);";

        using var connection = _connectionFactory.CreateConnection() as SqlConnection;

        using var cmd = new SqlCommand(query, connection);

        var parameters = new object[]
        {
            user.Role.Id,
            user.Username,
            user.Email,
            user.Password,
            user.ProfileImageName
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public User Get(int id)
    {
        throw new NotImplementedException();
    }
}