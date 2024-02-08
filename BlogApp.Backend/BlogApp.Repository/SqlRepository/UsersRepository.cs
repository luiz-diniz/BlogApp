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
            user.Username.ToLower(),
            user.Email,
            user.Password,
            user.ProfileImageName
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public User GetUserCredentials(string username)
    {
        var query = @"SELECT Id, IdRole, Password FROM [Users] WHERE Username = @P0";

        using var connection = _connectionFactory.CreateConnection() as SqlConnection;

        using var cmd = new SqlCommand(query, connection);

        var parameters = new object[]
        {
            username
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new User()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Role = new UserRole
                {
                    Id = Convert.ToInt32(reader["IdRole"])
                },
                Username = username.ToLower(),
                Password = Convert.ToString(reader["Password"]),
            };
        }

        return null!;
    }

    public bool VerifyUserExist(string username)
    {
        var query = @"SELECT COUNT(*) AS Value FROM [Users] WHERE Username = @P0";

        using var connection = _connectionFactory.CreateConnection() as SqlConnection;

        using var cmd = new SqlCommand(query, connection);

        var parameters = new object[]
        {
            username.ToLower()
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        var reader = cmd.ExecuteReader();

        if (reader.Read())
            return Convert.ToInt32(reader["Value"]) > 0;      

        return false;
    }
}