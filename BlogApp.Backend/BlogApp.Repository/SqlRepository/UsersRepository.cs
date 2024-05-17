using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;

namespace BlogApp.Repository.SqlRepository;

public class UsersRepository : IUsersRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public UsersRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public void Add(User userModel)
    {
        var query = @"INSERT INTO [Users] (IdRole, Username, Email, Password, Description, ProfileImageName)
            VALUES (@P0, @P1, @P2, @P3, @P4, @P5);";

        var parameters = new object[]
        {
            userModel.IdRole,
            userModel.Username.ToLower(),
            userModel.Email.ToLower(),
            userModel.Password,
            string.Empty,
            userModel.ProfileImageName!
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }

    public UserProfile GetProfileInfo(string username)
    {
        var query = @"SELECT U.Id AS IdUser, U.Username, U.ProfileImageName, U.Description
                        FROM Users U
                        WHERE U.Username = @P0";

        var parameters = new object[]
        {
            username
        };

        using var reader = _queryExecutor.ExecuteReader(query, parameters);

        if (reader.Read())
        {
            return new UserProfile
            {
                Id = Convert.ToInt32(reader["IdUser"]),
                Username = Convert.ToString(reader["Username"]),
                ProfileImageName = Convert.ToString(reader["ProfileImageName"]),
                Description = Convert.ToString(reader["Description"])
            };
        }

        return null!;
    }

    public UserCredentials GetUserCredentials(string username)
    {
        var query = "SELECT Id, IdRole, Password FROM [Users] WHERE Username = @P0";

        var parameters = new object[]
        {
            username
        };

        using var reader = _queryExecutor.ExecuteReader(query, parameters);

        if (reader.Read())
        {
            return new UserCredentials
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
        var query = "SELECT COUNT(*) AS Value FROM [Users] WHERE Username = @P0";

        var parameters = new object[]
        {
            username.ToLower()
        };

        using var reader = _queryExecutor.ExecuteReader(query, parameters);

        if (reader.Read())
            return Convert.ToInt32(reader["Value"]) > 0;      

        return false;
    }

    public bool VerifyEmailExist(string email)
    {
        var query = "SELECT COUNT(*) AS Value FROM [Users] WHERE Email = @P0";

        var parameters = new object[]
        {
            email.ToLower()
        };

        using var reader = _queryExecutor.ExecuteReader(query, parameters);

        if (reader.Read())
            return Convert.ToInt32(reader["Value"]) > 0;

        return false;
    }
}