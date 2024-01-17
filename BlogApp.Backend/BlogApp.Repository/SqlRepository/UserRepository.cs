using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class UserRepository : IUserRepository
{
    private readonly IConnectionFactory _connection;

    public UserRepository(IConnectionFactory connection)
    {
        _connection = connection;
    }

    public void Add(User user)
    {
        var query = "INSERT INTO [User] (IdRole, Username, Email, Password, ProfileImageName) " +
            "VALUES (@P0, @P1, @P2, @P3, @P4)";

        using var connection = _connection.Create() as SqlConnection;

        using var cmd = new SqlCommand(query, connection);

        var parameters = new object[]
        {
            (int)user.Role,
            user.Username,
            user.Email,
            user.Password,
            user.ProfileImageName
        };

        ParametersBuilder.Build(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public User Get(int id)
    {
        throw new NotImplementedException();
    }
}