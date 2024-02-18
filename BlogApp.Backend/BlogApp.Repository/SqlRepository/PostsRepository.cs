using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class PostsRepository : IPostsRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public PostsRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public int Add(PostModel post, IDbConnection connection, IDbTransaction transaction)
    {
        var query = @"INSERT INTO [Posts] (IdUserAuthor, IdCategory, Title, Content, PostImageName) OUTPUT INSERTED.Id
            VALUES (@P0, @P1, @P2, @P3, @P4);";

        var parameters = new object[]
        {
            post.IdUserAuthor,
            post.IdCategory,
            post.Title,
            post.Content,
            post.PostImageName
        };
       
        return Convert.ToInt32(_queryExecutor.ExecuteScalar(connection, transaction, query, parameters));
    }

    public Post Get(int id)
    {
        var query = @"SELECT U.Id AS IdUser, U.Username, C.Id AS IdCategory, C.Name AS CategoryName, P.Id, P.Title, P.Content, P.PostImageName, P.CreationDate 
                        FROM [Posts] AS P
                        INNER JOIN [Users] AS U ON P.IdUserAuthor = U.Id
                        INNER JOIN [PostsCategories] AS C ON P.IdCategory = C.Id
                        INNER JOIN [PostsReviews] AS PR ON P.Id = PR.IdPost
                        WHERE P.Id = @P0 AND PR.Status = 2;";
  
        var parameters = new object[]
        {
            id
        };        

        using var reader = _queryExecutor.ExecuteReader(query, parameters);

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

    public IEnumerable<PostFeed> GetFeedPosts()
    {
        var query = @"SELECT U.Id AS IdUser, U.Username, C.Id AS IdCategory, C.Name AS CategoryName, P.Id, P.Title, PR.ReviewDate AS PublishDate, 
											(SELECT COUNT(*) FROM [PostsLikes] WHERE IdPost = P.Id) AS LikesCount,
											(SELECT COUNT(*) FROM [PostsComments] WHERE IdPost = P.Id) AS CommentsCount
                        FROM [Posts] AS P
                        INNER JOIN [Users] AS U ON P.IdUserAuthor = U.Id
                        INNER JOIN [PostsCategories] AS C ON P.IdCategory = C.Id
						INNER JOIN [PostsReviews] AS PR ON P.Id = PR.IdPost
                     WHERE PR.Status = 2";


        using var reader = _queryExecutor.ExecuteReader(query);

        var posts = new List<PostFeed>();

        while (reader.Read())
        {
            posts.Add(new PostFeed
            {
                IdUser = Convert.ToInt32(reader["IdUser"]),
                Username = Convert.ToString(reader["Username"]),
                IdCategory = Convert.ToInt32(reader["IdCategory"]),
                Category = Convert.ToString(reader["CategoryName"]),
                Id = Convert.ToInt32(reader["Id"]),
                Title = Convert.ToString(reader["Title"]),
                PublishDate = Convert.ToDateTime(reader["PublishDate"]),
                LikesCount = Convert.ToInt32(reader["LikesCount"]),
                CommentsCount = Convert.ToInt32(reader["CommentsCount"])
            });
        }

        if (posts.Count != 0)
            return posts;

        return null!;
    }

    public void Publish(int idPost, IDbConnection connection, IDbTransaction transaction)
    {
        var query = "UPDATE [Posts] SET PublishedDate = @P0 WHERE Id = @P1";

        var parameters = new object[]
        {
            idPost
        };

        _queryExecutor.ExecuteNonQuery(connection, transaction, query, parameters);
    }
}