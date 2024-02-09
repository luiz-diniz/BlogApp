using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class UsersRepository : IUsersRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public UsersRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public void Add(UserModel userModel)
    {
        var query = @"INSERT INTO [Users] (IdRole, Username, Email, Password, ProfileImageName)
            VALUES (@P0, @P1, @P2, @P3, @P4);";

        var parameters = new object[]
        {
            userModel.IdRole,
            userModel.Username.ToLower(),
            userModel.Email,
            userModel.Password,
            userModel.ProfileImageName
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }

    public User GetUserCredentials(string username)
    {
        var query = @"SELECT Id, IdRole, Password FROM [Users] WHERE Username = @P0";

        var parameters = new object[]
        {
            username
        };

        using var reader = _queryExecutor.ExecuteReader(query, parameters);

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

        var parameters = new object[]
        {
            username.ToLower()
        };

        using var reader = _queryExecutor.ExecuteReader(query, parameters);

        if (reader.Read())
            return Convert.ToInt32(reader["Value"]) > 0;      

        return false;
    }
}