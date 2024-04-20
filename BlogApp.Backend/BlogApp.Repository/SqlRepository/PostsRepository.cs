using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;
using System.Data;

namespace BlogApp.Repository.SqlRepository;

public class PostsRepository : IPostsRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public PostsRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public int Add(Post post, IDbConnection connection, IDbTransaction transaction)
    {
        var query = @"INSERT INTO [Posts] (IdUser, IdCategory, Title, Content, PostImageName) OUTPUT INSERTED.Id
            VALUES (@P0, @P1, @P2, @P3, @P4);";

        var parameters = new object[]
        {
            post.IdUser,
            post.IdCategory,
            post.Title,
            post.Content,
            post.PostImageName
        };
       
        return Convert.ToInt32(_queryExecutor.ExecuteScalar(connection, transaction, query, parameters));
    }

    public PostInfo Get(int id)
    {
        var query = @"SELECT U.Id AS IdUser, U.Username, U.ProfileImageName, C.Id AS IdCategory, C.Name AS CategoryName, P.Id, P.Title, P.Content, P.PostImageName, PR.ReviewDate AS PublishedDate,
						    (SELECT COUNT(*) FROM [PostsLikes] WHERE IdPost = P.Id) AS LikesCount
                        FROM [Posts] AS P
                            INNER JOIN [Users] AS U ON P.IdUser = U.Id
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
            return new PostInfo
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = Convert.ToString(reader["Title"]),
                Content = Convert.ToString(reader["Content"]),
                PostImageName = Convert.ToString(reader["PostImageName"]),
                PublishedDate = Convert.ToDateTime(reader["PublishedDate"]),
                LikesCount = Convert.ToInt32(reader["LikesCount"]),
                User = new UserProfile
                {
                    Id = Convert.ToInt32(reader["IdUser"]),
                    Username = Convert.ToString(reader["Username"]),
                    ProfileImageName = Convert.ToString(reader["ProfileImageName"])
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
        var query = @"SELECT P.Id, P.Title, C.Id AS IdCategory, C.Name AS CategoryName, PR.ReviewDate AS PublishDate, 
                                            U.Id AS IdUser, U.Username, U.ProfileImageName,
											(SELECT COUNT(*) FROM [PostsLikes] WHERE IdPost = P.Id) AS LikesCount,
											(SELECT COUNT(*) FROM [PostsComments] WHERE IdPost = P.Id) AS CommentsCount
                        FROM [Posts] AS P
                            INNER JOIN [Users] AS U ON P.IdUser = U.Id
                            INNER JOIN [PostsCategories] AS C ON P.IdCategory = C.Id
						    INNER JOIN [PostsReviews] AS PR ON P.Id = PR.IdPost
                        WHERE PR.Status = 2
                            ORDER BY PublishDate DESC";


        using var reader = _queryExecutor.ExecuteReader(query);

        var posts = new List<PostFeed>();

        while (reader.Read())
        {
            posts.Add(new PostFeed
            {           
                Id = Convert.ToInt32(reader["Id"]),
                Title = Convert.ToString(reader["Title"]),
                User = new UserProfile
                {
                    Id = Convert.ToInt32(reader["IdUser"]),
                    Username = Convert.ToString(reader["Username"]),
                    ProfileImageName = Convert.ToString(reader["ProfileImageName"])
                },
                Category = new PostCategory
                {
                    Id = Convert.ToInt32(reader["IdCategory"]),
                    Name = Convert.ToString(reader["CategoryName"])
                },             
                PublishDate = Convert.ToDateTime(reader["PublishDate"]),
                LikesCount = Convert.ToInt32(reader["LikesCount"]),
                CommentsCount = Convert.ToInt32(reader["CommentsCount"])
            });
        }

        if (posts.Count != 0)
            return posts;

        return null!;
    }

    public IEnumerable<PostFeed> GetProfileFeedPosts()
    {
        var query = @"SELECT P.Id, P.Title, C.Id AS IdCategory, C.Name AS CategoryName, PR.ReviewDate AS PublishDate, 
											(SELECT COUNT(*) FROM [PostsLikes] WHERE IdPost = P.Id) AS LikesCount,
											(SELECT COUNT(*) FROM [PostsComments] WHERE IdPost = P.Id) AS CommentsCount
                        FROM [Posts] AS P
                            INNER JOIN [PostsCategories] AS C ON P.IdCategory = C.Id
						    INNER JOIN [PostsReviews] AS PR ON P.Id = PR.IdPost
                        WHERE PR.Status = 2 AND P.IdUser = 1
                            ORDER BY PublishDate DESC"; using var reader = _queryExecutor.ExecuteReader(query);

        var posts = new List<PostFeed>();

        while (reader.Read())
        {
            posts.Add(new PostFeed
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = Convert.ToString(reader["Title"]),              
                Category = new PostCategory
                {
                    Id = Convert.ToInt32(reader["IdCategory"]),
                    Name = Convert.ToString(reader["CategoryName"])
                },
                PublishDate = Convert.ToDateTime(reader["PublishDate"]),
                LikesCount = Convert.ToInt32(reader["LikesCount"]),
                CommentsCount = Convert.ToInt32(reader["CommentsCount"])
            });
        }

        if (posts.Count != 0)
            return posts;

        return null!;
    }
}