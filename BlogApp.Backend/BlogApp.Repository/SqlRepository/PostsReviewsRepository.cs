using BlogApp.Models.Enums;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;
using System.Data;

namespace BlogApp.Repository.SqlRepository;

public class PostsReviewsRepository : IPostsReviewsRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public PostsReviewsRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public void Add(Post postModel, IDbConnection connection, IDbTransaction transaction)
    {
        var query = "INSERT INTO [PostsReviews] (IdPost, Status) VALUES (@P0, @P1);";

        var parameters = new object[]
        {
            postModel.Id,
            (int)StatusEnum.Pending
        };

        _queryExecutor.ExecuteNonQuery(connection, transaction, query, parameters);
    }

    public void Update(PostReview postReviewModel)
    {
        var query = "UPDATE [PostsReviews] SET [IdUserReviewer] = @P0, [Status] = @P1, [Feedback] = @P2, [ReviewDate] = @P3 WHERE [IdPost] = @P4;";

        var parameters = new object[]
        {
            postReviewModel.IdUserReviewer,
            (int)postReviewModel.Status,
            postReviewModel.Feedback,
            DateTime.Now,
            postReviewModel.IdPost
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }

    public IEnumerable<PostReviewInfo> GetReviewPosts()
    {
        //TODO: Pagination
        var query = @"SELECT P.Id, P.Title, P.CreationDate, U.Username, PR.Status FROM
                        [Posts] AS P
                            INNER JOIN [Users] AS U ON P.IdUser = U.Id
                            INNER JOIN [PostsReviews] AS PR ON P.Id = PR.IdPost
                        ORDER BY CreationDate DESC";

        using var reader = _queryExecutor.ExecuteReader(query);

        var posts = new List<PostReviewInfo>();

        while (reader.Read())
        {
            posts.Add(new PostReviewInfo
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = Convert.ToString(reader["Title"]),
                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                Username = Convert.ToString(reader["Username"]),
                Status = ConvertToStatusEnum(Convert.ToInt32(reader["Status"]))
            });
        }

        return posts;
    }

    private StatusEnum ConvertToStatusEnum(int status)
    {
        switch (status)
        {
            case 0:
                return StatusEnum.Pending;
            case 1:
                return StatusEnum.Reviewing;
            case 2:
                return StatusEnum.Approved;
            case 3:
                return StatusEnum.Declined;
            default: return StatusEnum.Unknown;
        }
    }
}