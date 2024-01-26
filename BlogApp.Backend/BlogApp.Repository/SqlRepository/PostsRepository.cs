using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class PostsRepository : IPostsRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public PostsRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public int Add(Post post, IDbConnection connection, IDbTransaction transaction)
    {
        var query = @"INSERT INTO [Posts] (IdUserAuthor, IdCategory, Title, Content, PostImageName) OUTPUT INSERTED.Id
            VALUES (@P0, @P1, @P2, @P3, @P4);";

        using var cmd = new SqlCommand(query, connection as SqlConnection, transaction as SqlTransaction);

        var parameters = new object[]
        {
            post.UserAuthor.Id,
            post.Category.Id,
            post.Title,
            post.Content,
            post.PostImageName
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public Post Get(int id)
    {
        var query = @"SELECT U.Id AS IdUser, U.Username, C.Id AS IdCategory, C.Name AS CategoryName, P.Id, P.Title, P.Content, P.PostImageName, P.CreationDate 
                        FROM [Posts] AS P
                        INNER JOIN [Users] AS U ON P.IdUserAuthor = U.Id
                        INNER JOIN [PostsCategories] AS C ON P.IdCategory = C.Id
                        INNER JOIN [PostsReviews] AS PR ON P.Id = PR.IdPost
                        WHERE P.Id = @P0 AND PR.Status = 3;";

        var connection = _connectionFactory.CreateConnection() as SqlConnection;

        using var cmd = new SqlCommand(query, connection);

        var parameters = new object[]
        {
            id
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Post
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = Convert.ToString(reader["Title"]),
                Content = Convert.ToString(reader["Content"]),
                PostImageName = Convert.ToString(reader["PostImageName"]),
                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                UserAuthor = new User
                {
                    Id = Convert.ToInt32(reader["IdUser"]),
                    Username = Convert.ToString(reader["Username"])
                },
                Category = new PostCategory
                {
                    Id = Convert.ToInt32(reader["IdCategory"]),
                    Name = Convert.ToString(reader["CategoryName"])
                }
            };
        }

        return null!;
    }

    public IEnumerable<Post> GetAll()
    {
        var query = @"SELECT U.Id AS IdUser, U.Username, C.Id AS IdCategory, C.Name AS CategoryName, P.Id, P.Title, P.Content, P.PostImageName, P.CreationDate 
                        FROM [Posts] AS P
                        INNER JOIN [Users] AS U ON P.IdUserAuthor = U.Id
                        INNER JOIN [PostsCategories] AS C ON P.IdCategory = C.Id;";

        var connection = _connectionFactory.CreateConnection() as SqlConnection;

        using var cmd = new SqlCommand(query, connection);

        cmd.CommandType = CommandType.Text;

        using var reader = cmd.ExecuteReader();

        var posts = new List<Post>();

        while (reader.Read())
        {
            posts.Add(new Post
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = Convert.ToString(reader["Title"]),
                Content = Convert.ToString(reader["Content"]),
                PostImageName = Convert.ToString(reader["PostImageName"]),
                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                UserAuthor = new User
                {
                    Id = Convert.ToInt32(reader["IdUser"]),
                    Username = Convert.ToString(reader["Username"])
                },
                Category = new PostCategory
                {
                    Id = Convert.ToInt32(reader["IdCategory"]),
                    Name = Convert.ToString(reader["CategoryName"])
                }
            });
        }

        if (posts.Count != 0)
            return posts;

        return null!;
    }
}